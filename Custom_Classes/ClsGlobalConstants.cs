using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsGlobalConstants
    {
        
        public ClsGlobalConstants()
        {
            mainUrl = "http://default-environment.srjbvgwytq.us-east-1.elasticbeanstalk.com";
            roomType  = mainUrl + "/rest/secured/roomtypes";
            rooms = mainUrl + "/rest/secured/rooms";
            countries = mainUrl + "/rest/secured/location/countries";
            states = mainUrl + "/rest/secured/location/states/";
            cities = mainUrl + "/rest/secured/location/cities/";
            idProofs = mainUrl + "/rest/secured/app/idproofs";
            corporateClients = mainUrl + "/rest/secured/corporateclients";
            checkIn = mainUrl + "/rest/secured/reservation";
            visitPurpose = mainUrl + "/rest/secured/app/visitpurpose";
            login = mainUrl + "rest/secured/users/login";

            globalReservationDetailsHeadString = "reservationDetails";
            globalReservationHeadString = "reservation";
            globalGuestHeadstring = "guestDetails";
            guestIndividualTypeId = "81";
            guestCorparateTypeId = "82";
            globalID = "id";
            globalName = "name";
            globalTransModePost = "Post";
            globalTransModePut = "Put";
            globalRentUploadStatus = false;
            tallyUrl = "";


            glvUserId = "test";

            roomRentSersvice = 1;
            
        }


        public string glvUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string mainUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomType { get; set; }
        public string rooms { get; set; }
        public string countries { get; set; }
        public string states { get; set; }
        public string cities { get; set; }
        public string idProofs { get; set; }
        public string corporateClients { get; set; }
        public string checkIn { get; set; }
        public string globalReservationDetailsHeadString { get; set; }
        public string globalReservationHeadString { get; set; }
        public string globalGuestHeadstring { get; set; }
        public string guestIndividualTypeId { get; set; }
        public string guestCorparateTypeId { get; set; }
        public string globalID { get; set; }
        public string globalName { get; set; }
        public string visitPurpose { get; set; }
        public string login { get; set; }
        public string tallyUrl { get; set; }

        public string globalTransModePost { get; set; }
        public string globalTransModePut { get; set; }
        public Boolean globalRentUploadStatus { get; set; }

        public int roomRentSersvice { get; set; }
    }

   
}
