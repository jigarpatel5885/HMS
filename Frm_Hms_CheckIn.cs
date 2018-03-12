using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMS.Custom_Classes;
using HMS.Custom_Classes.Service_Classes;
namespace HMS
{
    public partial class Frm_Hms_CheckIn : Form
    {
        #region Initialization & Declaration
        ClsRestService _clsRestService = new ClsRestService();
        ClsGlobalConstants _clsGlobalConstants = new ClsGlobalConstants();
        ClsGeneralLibrary _clsGeneralLibrary = new ClsGeneralLibrary();
        ClsGuest _clsGuest;
        ClsReservationDetails _clsReservationDetails;
        ClsReservation _clsReservation;
        ClsRooms _clsRooms = new ClsRooms();
        ClsCorporateClients _clsCorporateClient = new ClsCorporateClients();
        CommonServices _commonServices = new CommonServices();
        string _message = string.Empty;
        #endregion


        public Frm_Hms_CheckIn()
        {
            InitializeComponent();
        }

        private void Frm_Hms_CheckIn_Load(object sender, EventArgs e)
        {
            clearData();
            rbtRegClientY.Checked = true;
            dtpTime.Text = DateTime.Now.ToString("hh:mm:ss");
            fillCountries();
            fillIdProofs();
            fillRooms();
           
            //fillPurposeOfVisit();
           
        }

        private void Frm_Hms_CheckIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        #region Utilities

        private void fillRooms()
        {
            var dt = new DataTable();

            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);
                _clsGeneralLibrary.fillComboBox(dt, cmbRoomNo, _clsRooms.roomNumber, _clsRooms.id);
            }
            else
            {
                var ds = new DataSet();
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "AVIALABLE_ROOMS");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbRoomNo, "Key", "Key");
                   
                }
                else
                {
                    MessageBox.Show(_commonServices._message);
                }
            }
        }

        private string setRoomIdByRoomNo(TextBox t1)
        {
             var dt = new DataTable();
             string [] rooms;
             var roomId = "";
             dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);            
             rooms = t1.Text.Split(',');

             DataRow[] dr = dt.Select("roomNumber in (" + string.Join(",", rooms) + ")");

             string[] roomIdArray = dr.Select(ss => ss["id"].ToString()).ToArray();

             for (int i = 0; i < roomIdArray.Length; i++)
             {
                 if (i == 0)
                 {
                     roomId = roomId + roomIdArray[i].ToString();
                 }
                 else
                 {
                     roomId = roomId + "," + roomIdArray[i].ToString();
                 }
             }

                 return roomId.ToString();
                
        }

        private void fillCountries()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.countries);
                _clsGeneralLibrary.fillComboBox(dt, cmbCountries, "name", "id");
            }
            else
            {
                ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "COUNTRIES");

                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbCountries, "Code_Descr", "Key");
            }



            cmbCountries.SelectedIndex = cmbCountries.FindStringExact("India");

        }
        private void fillState(string countryId)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.states + countryId);
                _clsGeneralLibrary.fillComboBox(dt, cmbState, "name", "id");
            }
            else
            {
                ds = _commonServices.getStatesByCountryId(cmbCountries.SelectedValue.ToString());
                _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbState, "Code_Descr", "Key");
            }
        }
        private void fillCities(string stateId)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.cities + stateId);
                _clsGeneralLibrary.fillComboBox(dt, cmbCities, "name", "id");
            }
            else
            {
                ds = _commonServices.getCitiesByStateId(cmbState.SelectedValue.ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbCities, "Code_Descr", "Key");
                }
            }

        }


        private void fillIdProofs()
        {
            var dt = new DataTable();

             var ds = new DataSet();
             if (!_clsGeneralLibrary.tempPatch)
             {
                 dt = _clsRestService.GetHttpRequestDataTableNoFormat(_clsGlobalConstants.idProofs);
                 _clsGeneralLibrary.fillComboBox(dt, cmbIdProof, _clsGlobalConstants.globalName, _clsGlobalConstants.globalID);
             }
             else
             {
                 ds = _commonServices.getComboDetails(_clsGlobalConstants.glvUserId, "IDPROOFS");

                 _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbIdProof, "Code_Descr", "Key");
             }
        }

        private void fillCorporateClient()
        {
            var dt = new DataTable();
            if (!_clsGeneralLibrary.tempPatch)
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.corporateClients);
                _clsGeneralLibrary.fillComboBox(dt, cmbGuest, _clsGlobalConstants.globalName, _clsGlobalConstants.globalID);
            }
            else
            {
                var ds = new DataSet();
                ds = _commonServices.getCorporateClient();
                if (ds.Tables.Count > 0)
                {
                    _clsGeneralLibrary.fillComboBox(ds.Tables[0], cmbGuest, "Name", "Corporate_Id");
                }      
            }
        }

        private void setTotalGuest()
        {
            string male = string.Empty, female = string.Empty, minor = string.Empty, extraGuest = string.Empty;

            if (txtMaleGuest.Text.Equals(string.Empty))
            {
                male = "0";
            }
            else
            {
                male = txtMaleGuest.Text;
            }
            if (txtFemaleGuest.Text.Equals(string.Empty))
            {
                female = "0";
            }
            else
            {
                female = txtFemaleGuest.Text;    
            }
            if (txtMinor.Text.Equals(string.Empty))
            {
                minor = "0";
            }
            else
            {
                minor = txtMinor.Text;
            }
            if (txtExtraGuest.Text.Equals(string.Empty))
            {
                extraGuest = "0";
            }
            else
            {
                extraGuest = txtExtraGuest.Text;
            }

            txtTotalGuest.Text = (Convert.ToInt32(male) + Convert.ToInt32(female) + Convert.ToInt32(minor) + Convert.ToInt32(extraGuest)).ToString();

        }

        //private void fillPurposeOfVisit()
        //{
        //    var dt = new DataTable();
        //    dt = _clsRestService.GetHttpRequestDataTableNoFormat(_clsGlobalConstants.visitPurpose);
        //    _clsGeneralLibrary.fillComboBox(dt, cmbVisitPurpose, _clsGlobalConstants.globalName, _clsGlobalConstants.globalID);
        //}

        private string getRoomPriceByRoomNo(string roomNr)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var roomCharges = "0";

            if (true)
            {
                ds = _commonServices.getRoomPriceById(roomNr);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    roomCharges = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    roomCharges = "0";
                }
            }
            else
            {
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.rooms);


            var result = dt.AsEnumerable().Where(myRow => myRow.Field<string>(_clsRooms.roomNumber) == roomNr);
            
            DataView view = result.AsDataView();


            roomCharges = view[0][_clsRooms.pricePerNight].ToString();

                if (roomCharges.ToString().Equals(string.Empty))
                {
                    roomCharges = "0";
                }
            }
            


            return roomCharges;
            
        }

        private void setCorporateClientDetails(string clientId)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var gstnNo = "0";
            if (!_clsGeneralLibrary.tempPatch)
            {
                
                dt = _clsRestService.GetHttpRequestDataTable(_clsGlobalConstants.corporateClients);


                var result = dt.AsEnumerable().Where(myRow => myRow.Field<Int64>(_clsGlobalConstants.globalID) == Convert.ToInt64(clientId));

                DataView view = result.AsDataView();


                gstnNo = view[0][_clsCorporateClient.GstnNo].ToString();

                if (gstnNo.ToString().Equals(string.Empty))
                {
                    gstnNo = "0";
                }

            }
            else
            {
                ds = _commonServices.getCorporateDetailByClienId(cmbGuest.SelectedValue.ToString());
                try
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            txtGstn.Text = row[_clsCorporateClient.GstnNo].ToString();
                            txtAddress1.Text = row[_clsCorporateClient.address1].ToString();
                            txtAddress2.Text = row[_clsCorporateClient.address2].ToString();
                            txtAddress3.Text = row[_clsCorporateClient.address3].ToString();
                            txtEmailId.Text = row[_clsCorporateClient.email].ToString();
                            txtMobileNo.Text = row[_clsCorporateClient.mobileNo].ToString();
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
               
            }
            
        }

        private void clearData()
        {
           
            txtStayDays.Text = "0";
            rbtRegClientY.Checked = true;
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtLandLineNo.Text = "";
            txtEmailId.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtArea.Text = "";
            txtPostalCode.Text = "";
            txtReferance.Text = "";
            txtVisitPurpose.Text = "";
            chkMultipleRooms.Checked = false;
            txtMultipleRooms.Text = "";
            txtIdProofNo.Text = "0";
            txtMinor.Text = "0";
            txtMaleGuest.Text = "0";
            txtFemaleGuest.Text = "0";
            txtAdvanceReciept.Text = "0";
            txtExtraGuest.Text = "0";
            txtTotalGuest.Text = "0";
          //  txtRoomCharges.Text = "0";
            txtBookingAdvance.Text = "0";
            rbtRegClientY.Checked = true;
            panel1.Enabled = false;
        }

        #endregion

        private void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fillState(cmbCountries.SelectedValue.ToString());
            }
            catch (Exception)
            {
                                
            }
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fillCities(cmbState.SelectedValue.ToString());
            }
            catch (Exception)
            {

            }
        }
        private void txtLandLineNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
                   
               _clsGeneralLibrary.isNumericValue(sender, e);
             
        }

       
        private void txtPricePerNight_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }   

        private void txtTaxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }

        private void txtTaxExcluded_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }

        private void txtTotalPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValueAllowDecimals(sender, e);
        }

        private void txtMaleGuest_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);

        }

        private void txtFemaleGuest_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
            
        }

        private void txtExtraGuest_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtMinor_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void rbtRegClientY_CheckedChanged(object sender, EventArgs e)
        {
            cmbGuest.Visible = true;
            fillCorporateClient();
        }

        private void rbtRegClientN_CheckedChanged(object sender, EventArgs e)
        {
            cmbGuest.Visible = false;
            cmbGuest.DataSource = null;
            txtGstn.Text = "0";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtMobileNo.Text = "";

        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
           
            this.Dispose();
        }

        private void chkMultipleRooms_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultipleRooms.Checked == true)
            {
                Frm_AvailableRooms _form = new Frm_AvailableRooms(this);
                _form.ShowDialog();
            }
            else
            {
                txtMultipleRooms.Text = string.Empty;
            }
        }

        private void txtStayDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void txtMaleGuest_TextChanged(object sender, EventArgs e)
        {
            setTotalGuest();
            
        }

        private void txtFemaleGuest_TextChanged(object sender, EventArgs e)
        {
            setTotalGuest();
        }

        private void txtMinor_TextChanged(object sender, EventArgs e)
        {
            setTotalGuest();
        }

        private void txtExtraGuest_TextChanged(object sender, EventArgs e)
        {
            setTotalGuest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = string.Empty, middleName = string.Empty, lastName = string.Empty,
           mobileNo = string.Empty, landLineNo = string.Empty, emailId = string.Empty, address1 = string.Empty,
           address2 = string.Empty, address3 = string.Empty, area = string.Empty, country = string.Empty, city = string.Empty,
           postalCode = string.Empty, typeOfIdProof = string.Empty, idProofNumber = string.Empty,userId =string.Empty,state=string.Empty;
            string checkInTime = string.Empty,expectedCheckOut=string.Empty,multiRooms = string.Empty;
            string checkInDate = string.Empty, checkOutDate = string.Empty, roomId = string.Empty, totalMale = string.Empty,
                totalFemale = string.Empty, totalMinor = string.Empty, totalExtraGuest = string.Empty,roomRent=string.Empty,
                advanceAmt=string.Empty,totGuest,guestType=string.Empty,corporateClientId = string.Empty;
            string referance = string.Empty,advanceReciept = string.Empty,paymentMode=string.Empty;
            string visitPurpose = string.Empty, referanceName = string.Empty, gstnNo = string.Empty,stayDays = string.Empty;
           
             userId= _clsGlobalConstants.glvUserId;
            try
            {
                if (true)
                {
                    firstName = txtFirstName.Text;
                    lastName = txtLastName.Text;
                    middleName = txtMiddleName.Text;
                    mobileNo = txtMobileNo.Text;
                    landLineNo = txtLandLineNo.Text;
                    emailId = txtEmailId.Text;
                    address1 = txtAddress1.Text;
                    address2 = txtAddress2.Text;
                    address3 = txtAddress3.Text;
                    area = txtArea.Text;
                    country = cmbCountries.Text;
                    city = cmbCities.Text;
                    postalCode = txtPostalCode.Text;
                    typeOfIdProof = cmbIdProof.Text;
                    idProofNumber = txtIdProofNo.Text;
                    state=cmbState.Text;
                   // corporateClientId = cmbGuest.SelectedValue.ToString();
                    roomRent = txtRoomCharges.Text;
                    if (rbtCard.Checked==true)
                    {
                        paymentMode= rbtCard.Text;
                    }
                    else if (rbtCash.Checked==true)
                    {
                        paymentMode = rbtCash.Text;
                    }

                    if (rbtRegClientY.Checked == true)
                    {

                        guestType = _clsGlobalConstants.guestCorparateTypeId;
                        corporateClientId = cmbGuest.SelectedValue.ToString();
                    }
                    else
                    {
                        guestType = _clsGlobalConstants.guestIndividualTypeId;
                        corporateClientId = "";
                    }

                    totalMale = txtMaleGuest.Text;
                    totalFemale = txtFemaleGuest.Text;
                    totalMinor = txtMinor.Text;
                    totalExtraGuest = txtExtraGuest.Text;
                    totGuest = txtTotalGuest.Text;
                    advanceAmt = txtBookingAdvance.Text;
                    advanceReciept = txtAdvanceReciept.Text;
                    checkInTime = dtpTime.Text;
                    checkInDate = dtpCheckin.Text;
                    expectedCheckOut=dtpExpectedCheckOut.Text;
                    stayDays = txtStayDays.Text.Trim();
                    if (chkMultipleRooms.Checked == true)
                    {
                        multiRooms = cmbRoomNo.Text.Trim() + ","+ txtMultipleRooms.Text;
                    }
                   
                    roomId = cmbRoomNo.SelectedValue.ToString();
                    
                    visitPurpose = txtVisitPurpose.Text;
                    referanceName = txtReferance.Text;
                    gstnNo = txtGstn.Text;


                   _message = _commonServices.setChekinData(userId,
                       firstName,
                       middleName,
                       lastName,
                       Convert.ToDecimal(string.IsNullOrEmpty(mobileNo) ? "0" : mobileNo),
                       Convert.ToDecimal(string.IsNullOrEmpty(landLineNo) ? "0" : landLineNo),
                       emailId,
                       address1,
                       address2,
                       address3,
                       area,
                       country,
                       state,
                       city,
                       Convert.ToDecimal(string.IsNullOrEmpty(postalCode) ? "0" : postalCode),
                       referance,
                       visitPurpose,
                       typeOfIdProof,
                       idProofNumber,
                       Convert.ToInt32(string.IsNullOrEmpty(totalMinor) ? "0" : totalMinor),
                       Convert.ToInt32(string.IsNullOrEmpty(totalFemale) ? "0" : totalFemale),
                       Convert.ToInt32(string.IsNullOrEmpty(totalMale) ? "0" : totalMale),
                       Convert.ToInt32(string.IsNullOrEmpty(advanceReciept) ? "0" : advanceReciept),
                       Convert.ToInt32(string.IsNullOrEmpty(totalExtraGuest) ? "0" : totalExtraGuest),
                       Convert.ToInt32(string.IsNullOrEmpty(totGuest) ? "0" : totGuest),
                       Convert.ToInt32(string.IsNullOrEmpty(stayDays) ? "0" : stayDays),                       
                       paymentMode,
                       Convert.ToInt32(string.IsNullOrEmpty(advanceAmt) ? "0" : advanceAmt),                                              
                       Convert.ToInt32(roomId),
                       checkInDate,
                       checkInTime,
                       multiRooms,
                       expectedCheckOut,
                       gstnNo,
                       Convert.ToInt32(string.IsNullOrEmpty(corporateClientId) ? "0" : corporateClientId),
                        Convert.ToDecimal(string.IsNullOrEmpty(roomRent) ? "0" : roomRent)                       
                       );

                   if (!_message.Equals("No Error"))
                   {
                       MessageBox.Show(_message);
                   }
                   else
                   {
                       MessageBox.Show("Sucess");
                   }
                   clearData();
                    
                }
                else
                {
                    #region set Guest Details
                    firstName = txtFirstName.Text;
                    lastName = txtLastName.Text;
                    middleName = txtMiddleName.Text;
                    mobileNo = txtMobileNo.Text;
                    landLineNo = txtLandLineNo.Text;
                    emailId = txtEmailId.Text;
                    address1 = txtAddress1.Text;
                    address2 = txtAddress2.Text;
                    address3 = txtAddress3.Text;
                    area = txtArea.Text;
                    country = cmbCountries.Text;
                    city = cmbCities.Text;
                    postalCode = txtPostalCode.Text;
                    typeOfIdProof = cmbIdProof.Text;
                    idProofNumber = txtIdProofNo.Text;
                    corporateClientId = cmbGuest.SelectedValue.ToString();
                    roomRent = txtRoomCharges.Text;
                    if (rbtRegClientY.Checked == true)
                    {

                        guestType = _clsGlobalConstants.guestCorparateTypeId;
                        corporateClientId = cmbGuest.SelectedValue.ToString();
                    }
                    else
                    {
                        guestType = _clsGlobalConstants.guestIndividualTypeId;
                        corporateClientId = "";
                    }
                    // setting guest details 

                    _clsGuest = new ClsGuest
                    {

                        typeOfGuest = guestType,
                        corporateClientId = corporateClientId,
                        firstName = firstName,
                        middleName = middleName,
                        lastName = lastName,
                        mobile = mobileNo,
                        landLine = landLineNo,
                        email = emailId,
                        add1 = address1,
                        add2 = address2,
                        add3 = address3,
                        area = area,
                        country = country,
                        city = city,
                        postalCode = postalCode,
                        typeOfIdProof = typeOfIdProof,
                        idProofNumber = idProofNumber

                    };

                    #endregion

                    #region Set Reservation Details
                    checkInDate = dtpCheckin.Text;
                    // checkOutDate = dtpCheckOut.Text;

                    totalMale = txtMaleGuest.Text;
                    totalFemale = txtFemaleGuest.Text;
                    totalMinor = txtMinor.Text;
                    totalExtraGuest = txtExtraGuest.Text;
                    totGuest = txtTotalGuest.Text;
                    advanceAmt = txtRoomCharges.Text;

                    if (chkMultipleRooms.Checked == true)
                    {
                        roomId = setRoomIdByRoomNo(txtMultipleRooms);
                    }
                    else
                    {
                        roomId = cmbRoomNo.SelectedValue.ToString();
                    }

                    _clsReservationDetails = new ClsReservationDetails
                    {
                        fromDate = checkInDate,
                        toDate = checkOutDate,
                        roomId = roomId,
                        noOfMaleGuest = totalMale,
                        noOfFemaleGuest = totalFemale,
                        noOfMinorGuest = totalMinor,
                        noOfExtraGuest = totalExtraGuest,
                        advanceAmount = advanceAmt,
                        totalGuest = totGuest,
                        pricePerNight = roomRent
                    };
                    #endregion

                    #region Set Reservation
                    visitPurpose = txtVisitPurpose.Text;
                    referanceName = txtReferance.Text;
                    gstnNo = txtGstn.Text;
                    _clsReservation = new ClsReservation
                    {
                        visitPurpose = visitPurpose,
                        references = referanceName,
                        gstnNumber = gstnNo
                    };
                    #endregion
                    var data = new Dictionary<string, object>();
                    //adding guest to key value pair
                    data.Add(_clsGlobalConstants.globalGuestHeadstring, _clsGuest);

                    data.Add(_clsGlobalConstants.globalReservationHeadString, _clsReservation);
                    data.Add(_clsGlobalConstants.globalReservationDetailsHeadString, _clsReservationDetails);

                    _clsRestService.PostRequestMultiDimesionJsonString(_clsGlobalConstants.checkIn, data);
                }


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cmbRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
              
              txtRoomCharges.Text= getRoomPriceByRoomNo(cmbRoomNo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); 
                
            }
           
        }

        private void cmbGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbGuest.Text))
            {
               setCorporateClientDetails(cmbGuest.SelectedValue.ToString());
            }
        }

        private void txtBookingAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            _clsGeneralLibrary.isNumericValue(sender, e);
        }

        private void chkAdvPaymentMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAdvPaymentMode.Checked)
            {
                rbtCard.Checked = false;
                rbtCash.Checked = false;
                panel1.Enabled = false;
            }
            else
            {
                panel1.Enabled = true;
            }
        }

      

                                
                                                                     
    }
}
