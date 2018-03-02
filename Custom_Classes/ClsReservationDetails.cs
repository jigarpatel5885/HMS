using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsReservationDetails
    {
        public ClsReservationDetails()
        {
            fromDate = "fromDate";
            toDate = "toDate";
            roomId = "roomId";
            noOfExtraGuest = "noOfExtraGuest";
            noOfMaleGuest = "noOfMaleGuest";
            noOfFemaleGuest = "noOfFemaleGuest";
            noOfMinorGuest = "noOfMinorGuest";
            totalGuest = "totalGuest";
            advanceAmount = "advanceAmount";
            pricePerNight = "pricePerNight";
            
        }    
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string roomId { get; set; }
        public string noOfExtraGuest { get; set; }
        public string noOfMaleGuest { get; set; }
        public string noOfFemaleGuest { get; set; }
        public string noOfMinorGuest { get; set; }
        public string totalGuest { get; set; }
        public string advanceAmount { get; set; }
        public string pricePerNight { get; set; }
       
    }
   
}
