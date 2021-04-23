using NetTopologySuite.Geometries;
using oxygen_tracker.Settings.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oxygen_tracker.Models
{
    [Table("UserLocations")]
    public class UserLocation
    {
        [Required]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public string Pincode { get; set; }
        public Point Location => new(Lat, Long, 4326);
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string UserGuid { set; get; }
        [NotMapped]
        public double Lat { get; set; }
        [NotMapped]
        public double Long { get; set; }
    }
}