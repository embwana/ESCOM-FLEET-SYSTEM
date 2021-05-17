using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(15)]
        public string Comment { get; set; }
       
       // public ICollection<Station> Station { get; set; }
        public ICollection<FleetCustodian> FleetCustodians { get; set; }
        public ICollection<FleetCategory> FleetCategory { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
    }
}
