using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Interfaces.Repository
{
    public interface IStatusDBRepository : IRepository<StatusDB>
    {
        StatusDB DatabaseCreate(int jobId, EnvironmentType environment);
        StatusDB DatabaseDelete(int jobId, EnvironmentType environment);
        StatusDB DatabaseClear(int jobId, EnvironmentType environment);
    }
}
