using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareAppointment.HealthCare_BLL.Repositories;

namespace HealthCareAppointment.HealthCare_BLL
{
    public interface IUnitOfWork : IDisposable
    {
        IRegisterRepository UserRegisters { get; }
        IRoleRepository UserRoles { get; }
        void Complete();
    }
}
