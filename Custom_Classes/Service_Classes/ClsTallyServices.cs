using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS.Custom_Classes.Service_Classes
{
    class ClsTallyServices
    {

        #region Utilities

        //public void LedgerCreateXml(ClsCorporateClients ledger) // request xml and response for ledger creation
        //{
        //    try
        //    {
        //        String xmlstc = "";
        //        xmlstc = "<ENVELOPE>\r\n";
        //        xmlstc = xmlstc + "<HEADER>\r\n";
        //        xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
        //        xmlstc = xmlstc + "</HEADER>\r\n";
        //        xmlstc = xmlstc + "<BODY>\r\n";
        //        xmlstc = xmlstc + "<IMPORTDATA>\r\n";
        //        xmlstc = xmlstc + "<REQUESTDESC>\r\n";
        //        xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
        //        xmlstc = xmlstc + "</REQUESTDESC>\r\n";
        //        xmlstc = xmlstc + "<REQUESTDATA>\r\n";
        //        xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";

        //        xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + ledger.name + "\" Action =" + "\"" + "Create" + "\">\r\n";
        //        xmlstc = xmlstc + "<NAME>" + ledger.name + "</NAME>\r\n";
        //        xmlstc = xmlstc + "<PARENT>" + "Sales Account" + "</PARENT>\r\n";
        //        //xmlstc = xmlstc + "<OPENINGBALANCE>" + ledger.openingBalance + "</OPENINGBALANCE>\r\n";
        //        xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>\r\n";
        //        xmlstc = xmlstc + "</LEDGER>\r\n";

        //        xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
        //        xmlstc = xmlstc + "</REQUESTDATA>\r\n";
        //        xmlstc = xmlstc + "</IMPORTDATA>\r\n";
        //        xmlstc = xmlstc + "</BODY>";
        //        xmlstc = xmlstc + "</ENVELOPE>";
        //        String xml = xmlstc;
        //        String lLedgerResponse = SendReqst(xml);
        //        MessageBox.Show(lLedgerResponse);
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        public void LedgerCreateXml(ClsCorporateClients ledger) // request xml and response for ledger creation
        {
            try
            {
                String xmlstc = setTallyHeaderXmlString();
                

                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + ledger.name + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME>" + ledger.name + "</NAME>\r\n";
                xmlstc = xmlstc + "<PARENT>" + "Sales Account" + "</PARENT>\r\n";
                //xmlstc = xmlstc + "<OPENINGBALANCE>" + ledger.openingBalance + "</OPENINGBALANCE>\r\n";
                xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";

                xmlstc = xmlstc + setTallyFooterXmlString();
               
                String xml = xmlstc;
                String lLedgerResponse = SendReqst(xml);
                MessageBox.Show(lLedgerResponse);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string SendReqst(string pWebRequstStr)
        {
            String lResponseStr = "";
            String lResult = "";

            try
            {
                String lTallyLocalHost = "http://localhost:9000";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
                lStrmWritr.Write(pWebRequstStr);
                lStrmWritr.Close();
                HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream lreceiveStream = lhttpResponse.GetResponseStream();
                StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
                lResponseStr = lStreamReader.ReadToEnd();
                lhttpResponse.Close();
                lStreamReader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            lResult = lResponseStr;
            return lResult;
        }

        public string setTallyHeaderXmlString()
        {
            String xmlstc = "";
            xmlstc = "<ENVELOPE>\r\n";
            xmlstc = xmlstc + "<HEADER>\r\n";
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
            xmlstc = xmlstc + "</HEADER>\r\n";
            xmlstc = xmlstc + "<BODY>\r\n";
            xmlstc = xmlstc + "<IMPORTDATA>\r\n";
            xmlstc = xmlstc + "<REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
            xmlstc = xmlstc + "</REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REQUESTDATA>\r\n";
            xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
            return xmlstc;
        }


        public string setTallyFooterXmlString()
        {
            String xmlstc = "" ;
            xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
            xmlstc = xmlstc + "</REQUESTDATA>\r\n";
            xmlstc = xmlstc + "</IMPORTDATA>\r\n";
            xmlstc = xmlstc + "</BODY>\r\n";
            xmlstc = xmlstc + "</ENVELOPE>\r\n";
            return xmlstc;
        }

        //public void createVoucher()
        //{
        //<ISDEEMEDPOSITIVE> tag must be set as Yes for DEBIT Amounts. ie 'By' in tally

        //    String xmlstc = "";
        //    xmlstc = "<ENVELOPE>\r\n";
        //    xmlstc = xmlstc + "<HEADER>\r\n";
        //    xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
        //    xmlstc = xmlstc + "</HEADER>\r\n";
        //    xmlstc = xmlstc + "<BODY>\r\n";
        //    xmlstc = xmlstc + "<IMPORTDATA>\r\n";
        //    xmlstc = xmlstc + "<REQUESTDESC>\r\n";
        //    xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
        //    xmlstc = xmlstc + "</REQUESTDESC>\r\n";
        //    xmlstc = xmlstc + "<REQUESTDATA>\r\n";
        //    xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
        //    xmlstc = xmlstc + "<VOUCHER REMOTEID=" + "\"" + " 0613b33a-c0b9-4b3aab64-091bb154504f-00000006" + "\"" + " VCHTYPE=" + "\"" + "Journal" + "\"" + " ACTION=" + "\"" + "Create" + "\"> \r\n";
        //    xmlstc = xmlstc + "<DATE>20171201</DATE>\r\n";
        //    xmlstc = xmlstc + "<VOUCHERTYPENAME>Journal</VOUCHERTYPENAME>\r\n";
        //    xmlstc = xmlstc + "<ISINVOICE>No</ISINVOICE>\r\n";
        //    xmlstc = xmlstc + "<REFERENCE>6</REFERENCE>\r\n";
        //    xmlstc = xmlstc + "<VOUCHERNUMBER>6</VOUCHERNUMBER>\r\n";

        //    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>\r\n";
        //    xmlstc = xmlstc + "<LEDGERNAME>jigar</LEDGERNAME>\r\n";
        //    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>\r\n";
        //    xmlstc = xmlstc + "<AMOUNT>100.00</AMOUNT>\r\n";
        //    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>\r\n";

        //    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>\r\n";
        //    xmlstc = xmlstc + "<LEDGERNAME>Central Tax UGST</LEDGERNAME>\r\n";
        //    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>\r\n";
        //    xmlstc = xmlstc + "<AMOUNT>70.00</AMOUNT>\r\n";
        //    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>\r\n";

        //    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>\r\n";
        //    xmlstc = xmlstc + "<LEDGERNAME>cash</LEDGERNAME>\r\n";
        //    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>\r\n";
        //    xmlstc = xmlstc + "<AMOUNT>-170.00</AMOUNT>\r\n";
        //    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>\r\n";
        //    xmlstc = xmlstc + " </VOUCHER>\r\n";

        //    xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
        //    xmlstc = xmlstc + "</REQUESTDATA>\r\n";
        //    xmlstc = xmlstc + "</IMPORTDATA>\r\n";
        //    xmlstc = xmlstc + "</BODY>\r\n";
        //    xmlstc = xmlstc + "</ENVELOPE>\r\n";
        //    String xml = xmlstc;
        //    String lLedgerResponse = SendReqst(xml);
        //    MessageBox.Show(lLedgerResponse);

        //}
        #endregion
    }

   
}
