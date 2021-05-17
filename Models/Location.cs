using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        [Required]
        [Display(Name = "Location")]
        [StringLength(30)]
        public string LocationName { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(15)]
        public string Comment { get; set; }
       // public ICollection<Station> Station { get; set; }
        //public ICollection<Region> Region{get;set; }
        //public ICollection<District> District { get; set; }
        public ICollection<Department> Department { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
        public ICollection<FleetCustodian> FleetCustodian { get; set; }
        public ICollection<FleetCategory> FleetCategory { get; set; }
    }
}
