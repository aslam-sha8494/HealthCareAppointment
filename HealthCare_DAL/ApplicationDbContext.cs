using HealthCareAppointment.HealthCare_BLL.AccountModels;
using HealthCareAppointment.HealthCare_BLL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HealthCareAppointment.HealthCare_DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserRegisters> UserRegisters { get; set; }
       
        public ApplicationDbContext()
            : base("HealthcareAppointmentConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}