using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxygen_tracker.Models
{
   public class PostOffice
    {
        public string Name { get; set; }
        public object Description { get; set; }
        public string BranchType { get; set; }
        public string DeliveryStatus { get; set; }
        public string Circle { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string Region { get; set; }
        public string Block { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
    }

    public class PostalResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public List<PostOffice> PostOffice { get; set; }
    }

}
