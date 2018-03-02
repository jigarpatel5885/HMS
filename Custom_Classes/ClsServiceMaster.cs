using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Custom_Classes
{
    class ClsServiceMaster
    {
        public ClsServiceMaster()
        {
            serviceId = "Service_Id";
            serviceName = "Service_Name";
            serviceDescr = "Service_Description";
            price = "Price";
            active = "Active";
        }

        public string serviceId { get; set; }
        public string serviceName { get; set; }
        public string serviceDescr { get; set; }
        public string price{get;set;}
        public string active { get; set; }
    }
}
