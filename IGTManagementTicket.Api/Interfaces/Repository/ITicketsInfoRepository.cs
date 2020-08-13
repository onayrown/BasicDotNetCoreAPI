using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Interfaces.Repository
{
    public interface ITicketsInfoRepository : IRepository<TicketsInfo>
    {
        TicketsInfo DatabaseExists(int jobId, EnvironmentType environment);
    }
}
