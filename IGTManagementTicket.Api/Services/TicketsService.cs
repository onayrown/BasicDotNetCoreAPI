using IGTManagementTicket.Api.Interfaces.Repository;
using IGTManagementTicket.Api.Interfaces.Service;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IStatusDBRepository _statusDBRepository;
        private readonly ITicketsInfoRepository _ticketsInfoRepository;

        public TicketsService(IStatusDBRepository statusDBRepository, 
            ITicketsInfoRepository ticketsInfoRepository)
        {
            _statusDBRepository = statusDBRepository;
            _ticketsInfoRepository = ticketsInfoRepository;
        }

        public EnvironmentType GetEnvironmentType(string environment)
        {
            switch (environment)
            {
                case "CONF":
                    return EnvironmentType.Config;
                case "STAG":
                    return EnvironmentType.Staging;
                case "PROD":
                    return EnvironmentType.Prod;
                default:
                    return EnvironmentType.Unknow; 
            }
        }

        public TicketsInfo DatabaseExists(int jobId, EnvironmentType environment)
        {
            return _ticketsInfoRepository.DatabaseExists(jobId, environment);
        }

        public StatusDB DatabaseCreate(int jobId, EnvironmentType environment)
        {
            return _statusDBRepository.DatabaseCreate(jobId, environment);
        }

        public StatusDB DatabaseDelete(int jobId, EnvironmentType environment)
        {
            return _statusDBRepository.DatabaseDelete(jobId, environment);
        }

        public StatusDB DatabaseClear(int jobId, EnvironmentType environment)
        {
            return _statusDBRepository.DatabaseClear(jobId, environment);
        }
    }
}
