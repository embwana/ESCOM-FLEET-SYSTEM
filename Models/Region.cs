using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class Region
    {
        [Key]
        public int RegionId { get; set; }
        [Required]
        [Display(Name = "Region")]
        [StringLength(30)]
        public string RegionName { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(15)]
        public string Comment { get; set; }
        ////public int? StationId { get; set; }
        ////[Required]
        ////[Display(Name = "Station")]
        ////[StringLength(30)]
        ////public Station StationName { get; set; }
        ////public int? LocationId { get; set; }
        ////[Required]
        ////[Display(Name = "Location")]
        ////[StringLength(30)]
        ////public Location LocationName { get; set; }
        ////public int? DistrictId { get; set; }
        ////[Required]
        ////[Display(Name = "District")]
        ////[StringLength(30)]
        ////public District DistrictName { get; set; }
        //public ICollection<District> District { get; set; }
        public ICollection<Department> Department { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
        public ICollection<FleetCustodian> FleetCustodian { get; set; }
        public ICollection<FleetCategory> FleetCategory { get; set; }
    }
}
