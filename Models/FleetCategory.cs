using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOM_FLEET_SYSTEM.Models
{
    public class FleetCategory
    {
        [Key]
        public int FleetCategoryId { get; set; }

        [Required]
        [Display(Name = "Plate")]
        [StringLength(50)]
        public string NumberPlate { get; set; }

        [Required]
        [Display(Name = "Model")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Year")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Year { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Tag")]
        [StringLength(50)]
        public string TagNumber { get; set; }

        [Required]
        [Display(Name = "Mileage")]
        [StringLength(50)]
        public string Mileage { get; set; }


        public int? COFId { get; set; }
        [Display(Name = "Number")]
        public COF COFNumber { get; set; }

        //public int? DepartmentId { get; set; }
        //[Display(Name = "Department")]
        //public Department Department { get; set; }

        public int? InsuranceId { get; set; }
        [Display(Name = "Insurance")]
        public Insurance InsuranceNumber { get; set; }

        public int? StationId { get; set; }
        //[Required]
        [Display(Name = "Station")]
       // [StringLength(50)]
        public Station Station { get; set; }

        public int? LocationId { get; set; }
        //[Required]
        [Display(Name = "Location")]
       // [StringLength(50)]
        public Location LocationName  { get; set; }
        public int? DistrictId { get; set; }
       // [Required]
        [Display(Name = "District")]
        //[StringLength(50)]
        public District DistrictName { get; set; }
        public int? RegionId { get; set; }
       // [Required]
        [Display(Name = "Region")]
       // [StringLength(50)]
        public Region RegionName { get; set; }

        public int? DepartmentId { get; set; }
        //[Required]
        [Display(Name = "Department")]
       // [StringLength(50)]
        public  Department DepartmentName { get; set; }
        public ICollection<FleetCustodian> FleetCustodians { get; set; }
     
        public ICollection<Grounded> Grounded { get; set; }
       // public ICollection<Station> Stations { get; set; }

    }
}
