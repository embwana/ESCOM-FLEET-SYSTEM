using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }

        [Required]
        [Display(Name = "Station Name")]
        [StringLength(50)]
        public string StationName { get; set; }
        public int? LocationId { get; set; }
        [Required]
        [Display(Name = "Location")]
       // [StringLength(50)]
        public Location LocationName { get; set; }

        public int? DepartmentId { get; set; }
        [Required]
        [Display(Name = "Deptment")]
        //[StringLength(50)]
        public Department DepartmentName { get; set; }

        public int? DistrictId { get; set; }

        [Display(Name = "District")]
        //[StringLength(50)]
        public District DistrictName { get; set; }
        public int? RegionId { get; set; }
        [Display(Name = "Region")]
        //[StringLength(50)]
        public Region RegionName { get; set; }

       



        //public int FleetNumber { get; set; }
        //public ICollection<Region> Region { get; set; }
        //public ICollection<District> District { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
        public ICollection<FleetCustodian> FleetCustodian { get; set; }
        public ICollection<FleetCategory> FleetCategory { get; set; }
        //public ICollection<Region> Region { get; set; }
    }
}
