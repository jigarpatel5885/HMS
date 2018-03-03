using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsGuest
    {
        public ClsGuest()
        {
            typeOfGuest = "typeOfGuest";
            corporateClientId = "corporateClientId";            
            firstName = "firstName";
            middleName = "middleName";
            lastName = "lastName";
            mobile = "mobile";
            landLine = "landLine";
            email = "email";
            add1 = "add1";
            add2 = "add2";
            add3 = "add3";
            area = "area";
            city = "city";
            state = "state";
            country = "country";
            postalCode = "postalCode";
            typeOfIdProof = "typeOfIdProof";
            idProofNumber = "idProofNumber";
           
           
        }
        public string typeOfGuest { get; set; }
        public string corporateClientId { get; set; }       
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string landLine { get; set; }
        public string email { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string add3 { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string typeOfIdProof { get; set; }
        public string idProofNumber { get; set; }
       
    }

   
}
