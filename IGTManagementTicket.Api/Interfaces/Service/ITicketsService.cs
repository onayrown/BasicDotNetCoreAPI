using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Interfaces.Service
{
    public interface ITicketsService
    {
        TicketsInfo DatabaseExists(int jobId, EnvironmentType environment);
        StatusDB DatabaseCreate(int jobId, EnvironmentType environment);
        StatusDB DatabaseDelete(int jobId, EnvironmentType environment);
        StatusDB DatabaseClear(int jobId, EnvironmentType environment);
        EnvironmentType GetEnvironmentType(string environment);
    }
}
