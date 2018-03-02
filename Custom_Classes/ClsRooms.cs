using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsRooms
    {
        public ClsRooms()
        {
            roomNumber = "Room_No";
            floorNumber = "Floor_Number";
            maxAdults = "Max_Adult";
            maxChildren = "Max_Children";
            pricePerNight = "Price_Per_Night";
            roomType = "Room_Type";
            active = "Active";
            id = "ID";
        }
        public string roomNumber { get; set; }
        public string floorNumber { get; set; }
        public string maxAdults { get; set; }
        public string maxChildren { get; set; }
        public string pricePerNight { get; set; }
        public string roomType { get; set; }
        public string active { get; set; }
        public string id { get; set; }
    }

    
}
