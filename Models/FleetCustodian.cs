using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class FleetCustodian
    {
        [Key]
        public int FleetCustodianId { get; set; }
        public int? LicenceId { get; set; }
        public int? COFId { get; set; }
        [Display(Name = "COF")]
        public COF COFNumber { get; set; }

        [Display(Name = "Licence")]
        public Licence LicenceNumber { get; set; }
        public int? FleetCategoryId { get; set;}
        
        [Display(Name = "Number Plate")]
        public FleetCategory NumberPlate { get; set; }
        public int? StationId { get; set; }
        
        [Display(Name = "Station")]
        public Station StationName { get; set; }
        public int? DepartmentId { get; set; }
        
        [Display(Name = "Department")]
        public Department DepartmentName { get; set; }
        [Required]
        [Display(Name = "Collected On")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime CollectedOn { get; set; }
        [Required]
        [Display(Name = "Returned On")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime ReturnedOn { get; set; }

        
    }
}
