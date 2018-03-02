using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsCorporateClients
    {
        public ClsCorporateClients ()
	    {
            name = "name";
            mobileNo = "mobile";
            email = "email";
            landLineNo = "landLine";
            address1 = "add1";
            address2 = "add2";
            address3 = "add3";
            area = "area";
            city = "city";
            state = "state";
            country = "country";
            postalCode = "postalCode";
            GstnNo = "gstin";
            contactPerson = "contactPerson";
	    }

        public string name { get; set; }
        public string mobileNo { get; set; }
        public string landLineNo { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string GstnNo { get; set; }
        public string contactPerson { get; set; }
    }
}
