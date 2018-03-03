using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsRoomTypes
    {
        public ClsRoomTypes()
        {
            roomId = "id";
            roomName = "name";
            price = "pricePerNight";
        }
        public string roomName { get; set; }
        public string price { get; set; }
        public string roomId { get; set; }
    }
}
