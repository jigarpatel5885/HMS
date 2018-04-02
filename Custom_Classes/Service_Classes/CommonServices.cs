using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HMS.Custom_Classes.Service_Classes
{
    class CommonServices
    {
        #region Fields

        ClsDataAccess _clsDataAccess = new ClsDataAccess();
        public string _message="";
        #endregion

        #region Room Type Master
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getRoomTypeMaster()
        {
            var dataset = new DataSet();
            var sqlParams = new List<SqlParameter>();
            var outPutParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000

            });

            dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETROOMTYPE", sqlParams, out outPutParameter);

            if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
            {
                if (outPutParameter["@Error"] == "NoError")
                {
                }
                else
                {
                    _message = outPutParameter["@Error"];
                }
            }
            return dataset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomType"></param>
        /// <param name="price"></param>
        /// <param name="Active"></param>
        /// <returns></returns>
        public string setRoomTypeMaster(string userId,
                                string roomType,
                                decimal price,
                                string Active                               
                                )
        {
            var sqlParams = new List<SqlParameter>();
            var outPutParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@User_Id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = userId
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Price",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = price
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Room_Type",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = roomType
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Active",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = Active
            });
            
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000

            });

            var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETROOMTYPE", sqlParams, out outPutParameter);
            if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
            {               
                    _message = outPutParameter["@Error"];                
            }

            return _message;
        }
        #endregion

        #region Room Master
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getRoomMaster()
        {
            var dataset = new DataSet();
            var sqlParams = new List<SqlParameter>();
            var outPutParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000

            });

            dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETROOMMASTER", sqlParams, out outPutParameter);

            if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
            {
                if (outPutParameter["@Error"] == "NoError")
                {
                }
                else
                {
                    _message = outPutParameter["@Error"];
                }
            }
            return dataset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomNo"></param>
        /// <param name="floorNo"></param>
        /// <param name="maxAdult"></param>
        /// <param name="maxChildren"></param>
        /// <param name="pricePerNight"></param>
        /// <param name="roomTypeId"></param>
        /// <param name="Active"></param>
        /// <returns></returns>
        public string setRoomMaster(string userId,
                               string roomNo,
                               string floorNo,
                               int maxAdult,
                               int maxChildren,
                               decimal pricePerNight,
                               int roomTypeId,                                
                               string Active
                               )
        {
            var sqlParams = new List<SqlParameter>();
            var outPutParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@User_Id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = userId
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Room_No",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = roomNo
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Floor_No",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = floorNo
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Max_Adult",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = maxAdult
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Max_Children",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = maxChildren
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Price_Per_Night",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = pricePerNight
            });
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Room_Type_Id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = roomTypeId
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Active",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = Active
            });

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000

            });

            var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETROOMMASTER", sqlParams, out outPutParameter);
            if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
            {
                _message = outPutParameter["@Error"];
            }

            return _message;
        }
        #endregion

        #region Service Master

          public DataSet getServiceMaster()
        {
            var dataset = new DataSet();
            var sqlParams = new List<SqlParameter>();
            var outPutParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000

            });

            dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETSERVICEMASTER", sqlParams, out outPutParameter);

            if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
            {
                if (outPutParameter["@Error"] == "NoError")
                {
                }
                else
                {
                    _message = outPutParameter["@Error"];
                }
            }
            return dataset;
        }

          public string setServiceMaster(string userId,
                                        int serviceId,
                                        string serviceName,
                                        string serviceDescription,
                                        decimal price,                              
                                        string Active
                                )
          {
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = serviceId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Name",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = serviceName
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Description",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = serviceDescription
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@price",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = price
              });
             

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Active",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = Active
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETSERVICEMASTER", sqlParams, out outPutParameter);
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
          }
        #endregion

        #region Get Countries>>States>>Cities
        

          public DataSet getStatesByCountryId(string countryId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Country_Id",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = countryId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETSTATEBYCOUNTRYID", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }

          public DataSet getCitiesByStateId(string stateId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@State_Id",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = stateId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCITYBYSTATEID", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
        #endregion

        #region Corporate Client Master
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
          public DataSet getCorporateClient()
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCORPORATECLIENT", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <param name="name"></param>
        /// <param name="mobileNo"></param>
        /// <param name="lanlineNo"></param>
        /// <param name="emailId"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="address3"></param>
        /// <param name="area"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="postalCode"></param>
        /// <param name="Gstn"></param>
        /// <param name="ContactPerson"></param>
        /// <param name="Active"></param>
        /// <returns></returns>
          public string setCorporateClient(string userId,
                                 int clientId,
                                 string name,
                                 decimal mobileNo,
                                 decimal lanlineNo,
                                 string emailId,
                                 string address1,
                                 string address2,
                                 string address3,       
                                 string area,
                                 string country,
                                 string state,
                                 string city,
                                 decimal postalCode,
                                 string Gstn,
                                 string ContactPerson,                                 
                                 string Active
                                 )
          {
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Corporate_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = clientId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Name",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = name
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Mobile_No",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = mobileNo
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Lanline_No",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = lanlineNo
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Email_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = emailId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address1",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = address1
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address2",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = address2
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address3",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = address3
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Area",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = area
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Country",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = country
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@State",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = state
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@City",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = city
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Postal_Code",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = postalCode
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@GSTN",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = Gstn
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Contact_Person",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = ContactPerson
              });


              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Active",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = Active
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETCORPORATECLIENTMASTER", sqlParams, out outPutParameter);
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
          }
        #endregion

          #region Fill dropdown dataset
          public DataSet getComboDetails(string userId,string KeyId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = KeyId

              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Key_Code",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = KeyId

              });
              sqlParams.Add(new SqlParameter()
              { 
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCOMBODETAILS", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
          #endregion

        #region getCorporateGstn
          public DataSet getCorporateDetailByClienId(string ClienId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Client_Id",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = ClienId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCORPORATECLIENTDETAIL", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
        #endregion

        #region set checkin data

          public string setChekinData(string userId,
                                       string firstName,
                                       string middleName,
                                       string lastName,
                                       decimal mobileNo,
                                       decimal lanlineNo,
                                       string emailId,
                                       string add1,
                                       string add2,
                                       string add3,
                                       string area,
                                       string country,
                                       string state,
                                       string city,
                                       decimal postalCode,
                                       string referance,
                                       string visitPurpose,
                                       string idProofType,
                                       string  idProofNumber,
                                       int totMinor,
                                        int totFemale,
                                        int totMale,
                                        int advanceReciept,
                                        int extraGuest,
                                        int totalGuest,
                                        int stayDays,
                                        string paymentMode,
                                        decimal advBookingAmount,
                                        int RoomNo,
                                        string checkinDate,
                                        string checkinTime,
                                       string multipleRooms,
                                       string expectedCheckOutDate,
                                       string gstin,
                                       int corporateId,
                                       decimal roomRent,
                                        string trnMode
              )

          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@First_Name",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = firstName

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Middle_Name",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = middleName

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Last_Name",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = lastName

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Mobile_Number",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = mobileNo

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Lanline_Number",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = lanlineNo

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Email_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = emailId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address1",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = add1

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address2",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = add2

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Address3",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = add3

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Area",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = area

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Country",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = country

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@State",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = state

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@City",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = city

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Postal_Code",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = postalCode

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Referance",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = referance

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Visit_Purpose",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = visitPurpose

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Id_Proof_Type",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = idProofType

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Id_Proof_Number",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = idProofNumber

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Minor_Guest",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = totMinor

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Male_Guest",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = totMale

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Female_Guest",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = totFemale

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Advance_Reciept",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = advanceReciept

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Extra_Guest",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = extraGuest

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Total_Guest",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = totalGuest

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Stay_Days",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = stayDays

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Booking_Amount",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = advBookingAmount

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Checkin_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = checkinDate

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Checkin_Time",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = checkinTime

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Payment_Mode",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = paymentMode

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_No",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = RoomNo

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Multiple_Rooms",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = multipleRooms

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Expected_Checkout_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = expectedCheckOutDate

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Corporate_Client_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = corporateId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Gstn",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = gstin

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Rent",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = roomRent

              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Trn_Mode",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = trnMode

              });
              var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETCHECKINDETAILS_NEW", sqlParams, out outPutParameter);
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
                          
          }
        #endregion

          #region get Room Rent By id
          public DataSet getRoomPriceById(string roomId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = roomId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETROOMCHARGESBYROOMID", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
          #endregion

          #region get Service Price By id
          public DataSet getServicePriceById(string serviceId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = serviceId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETSERVICEPRICEBYID", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }


          public DataSet getServiceByRoomId(string roomId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_No",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = roomId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCUSTOMERSERVICELIST", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }


          public DataSet getServiceByRoomIdRid(string roomId,string reservation_Id)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_No",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = roomId

              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Reservation_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = reservation_Id

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCUSTOMERSERVICELIST_MOD", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
          #endregion

        #region Customer Service


          public DataSet getCustomerCheckinDetailsForMod(string roomId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = roomId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCHECKINDETAILSBYROOMID", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
          public DataSet getCustomerCheckinByRoomId(string roomId)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = roomId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCHECKINCUSTOMERDETAILS", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="serviceId"></param>
        /// <param name="reservationId"></param>
        /// <param name="roomId"></param>
        /// <param name="serviceTime"></param>
        /// <param name="serviceDate"></param>
        /// <param name="remarks"></param>
        /// <param name="price"></param>
        /// <param name="trnMode"></param>
        /// <returns></returns>
          public string setCustomerService(string userId,
                                      int serviceId,
                                      string reservationId,
                                      int roomId,
                                      string serviceTime,
                                      string serviceDate,
                                      string remarks,
                                      decimal price,
                                      string trnMode
                              )
          {
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = serviceId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Reservation_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = reservationId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = roomId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Time",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = serviceTime
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Service_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = serviceDate
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Remarks",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = remarks
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Trn_Mode",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = trnMode
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@price",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = price
              });



              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              var dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETCUSTOMERSERVICES", sqlParams, out outPutParameter);
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
          }
        #endregion


        #region Occupancy 

        

          public DataSet getOccupancyList(string userId,string date)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Current_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = date

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETOCCUPANCYLIST", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
        #endregion

        #region setCustomerCheckout

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentMode"></param>
        /// <param name="roomNo"></param>
        /// <param name="checkOutDt"></param>
        /// <param name="checkOutTime"></param>
        /// <param name="reservationId"></param>
        /// <param name="discount"></param>
        /// <param name="sgst"></param>
        /// <param name="cgst"></param>
        /// <param name="totalAmount"></param>
        /// <param name="trnMode"></param>
        /// <returns></returns>
          public string setCustomerCheckOut(string userId,
                                      string paymentMode,
                                      int roomNo,
                                      string checkOutDt,
                                      string checkOutTime,
                                      string reservationId,
                                      decimal discount,
                                      decimal sgst,
                                      decimal cgst,  
                                      decimal totalAmount,
                                      string trnMode,
                                      decimal totServiceAmt,  
                                      Boolean param1
             
                              )
          {
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId
              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Payment_Mode",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = paymentMode
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Reservation_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = reservationId
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Discount",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = discount
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@SGST",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = sgst
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@CGST",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = cgst
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@CheckOut_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = checkOutDt
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@CheckOut_Time",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = checkOutTime
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Total_Amount",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = totalAmount
              });
            
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Trn_Mode",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = trnMode
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_No",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = roomNo
              });
            



              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Total_Service_Amount",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = totServiceAmt
              });

              var dataTable = new DataSet();
              if (param1)
              {
                  dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETCHECKOUTRECHECKIN_NEW", sqlParams, out outPutParameter);
              }
              else
              {
                 
                  dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETCHECKOUT_NEW", sqlParams, out outPutParameter);
              }
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
          }
        #endregion

        #region get invoice details
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reservation_Id"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
          public DataSet getInvoiceDetails(string userId, string reservation_Id,int roomNo)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();


              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Id",
                  SqlDbType = SqlDbType.Int,
                  Direction = ParameterDirection.Input,
                  Value = roomNo

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Reservation_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = reservation_Id

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETINVOICEDATA", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
          public DataSet getInvoiceByNumber(string userId, string invoiceNo)
          {
              var dataset = new DataSet();
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();


              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId

              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Invoice_No",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = invoiceNo

              });
              
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });

              dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETINVOICEBYNUMBER", sqlParams, out outPutParameter);

              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  if (outPutParameter["@Error"] == "NoError")
                  {
                  }
                  else
                  {
                      _message = outPutParameter["@Error"];
                  }
              }
              return dataset;
          }
        #endregion

        #region Room Shifting 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomNo"></param>
        /// <param name="OldroomNo"></param>
        /// <param name="shiftDate"></param>
        /// <param name="ShiftTime"></param>
        /// <param name="reservationId"></param>
        /// <param name="roomRent"></param>
        /// <returns></returns>
         public string setRoomShifting(string userId,
                                      int roomNo,
                                      int OldroomNo,
                                      string shiftDate,
                                      string ShiftTime,
                                      string reservationId,                                      
                                      decimal roomRent
             
                              )
          {
              var sqlParams = new List<SqlParameter>();
              var outPutParameter = new Dictionary<string, string>();

              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@User_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = userId
              });

             
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Reservation_Id",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = reservationId
              });
              
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Shifting_Date",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = shiftDate
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Shifting_Time",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Input,
                  Value = ShiftTime
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_Rent",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = roomRent
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@New_Room_No",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = roomNo
              });
              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Room_No",
                  SqlDbType = SqlDbType.Decimal,
                  Direction = ParameterDirection.Input,
                  Value = OldroomNo
              });

              
            



              sqlParams.Add(new SqlParameter()
              {
                  ParameterName = "@Error",
                  SqlDbType = SqlDbType.VarChar,
                  Direction = ParameterDirection.Output,
                  Size = 5000

              });
              var dataTable = new DataSet();


              dataTable = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_SETROOMSHIFTING_NEW", sqlParams, out outPutParameter);
             
              if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
              {
                  _message = outPutParameter["@Error"];
              }

              return _message;
          }
        #endregion

        #region CheckoutModification
         public DataSet getCustomerCheckinByInvoice(string invoiceNo)
         {
             var dataset = new DataSet();
             var sqlParams = new List<SqlParameter>();
             var outPutParameter = new Dictionary<string, string>();

             sqlParams.Add(new SqlParameter()
             {
                 ParameterName = "@Invoice_No",
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Input,
                 Value = invoiceNo

             });
             sqlParams.Add(new SqlParameter()
             {
                 ParameterName = "@Error",
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Output,
                 Size = 5000

             });

             dataset = _clsDataAccess.ExecuteStoredProcedure("SP_HMS_GETCUSTOMERDETAILBYINVOICE", sqlParams, out outPutParameter);

             if (outPutParameter.Keys.Count > 0 && outPutParameter.ContainsKey("@Error"))
             {
                 if (outPutParameter["@Error"] == "NoError")
                 {
                 }
                 else
                 {
                     _message = outPutParameter["@Error"];
                 }
             }
             return dataset;
         }

        #endregion
    }




}
