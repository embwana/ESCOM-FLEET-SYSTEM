using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        [Required]
        [Display(Name = "District")]
        [StringLength(50)]
        public string DistrictName { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(15)]
        public string Comment { get; set; }

        public ICollection<Station> Station { get; set; }
        //public ICollection<Location> Location { get; set; }
        public ICollection<Department> Department { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
        public ICollection<FleetCustodian> FleetCustodian { get; set; }
        public ICollection<FleetCategory> FleetCategory { get; set; }
    }
}
