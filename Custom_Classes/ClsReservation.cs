using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsReservation
    {
        public ClsReservation()
        {
            visitPurpose = "visitPurpose";
            references = "references";
            gstnNumber = "gstnNumber";
           
        }
        public string visitPurpose { get; set; }
        public string references { get; set; }
        public string gstnNumber { get; set; }
        
    }
   
}
