using HealthCareAppointment.HealthCare_BLL;
using HealthCareAppointment.HealthCare_BLL.Repositories;
using HealthCareAppointment.HealthCare_DAL.Repositories;

namespace HealthCareAppointment.HealthCare_DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            UserRegisters = new RegisterRepository(_applicationDbContext);
            UserRoles = new RoleRepository(_applicationDbContext);
        }

        public IRegisterRepository UserRegisters { get; private set; }
        public IRoleRepository UserRoles { get; private set; }

        public void Complete()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}