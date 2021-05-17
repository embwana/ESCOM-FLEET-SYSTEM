using ESCOM_FLEET_SYSTEM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static ESCOM_FLEET_SYSTEM.Controllers.LicenceController;

namespace ESCOM_FLEET_SYSTEM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Licence> Licences { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Location> Location { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.COF> COF { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Department> Department { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.District> District { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.FleetCategory> FleetCategory { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.FleetCustodian> FleetCustodian { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Grounded> Grounded { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Insurance> Insurance { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.InsuranceProvider> InsuranceProvider { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Region> Region { get; set; }
        public DbSet<ESCOM_FLEET_SYSTEM.Models.Station> Station { get; set; }
    }
}
