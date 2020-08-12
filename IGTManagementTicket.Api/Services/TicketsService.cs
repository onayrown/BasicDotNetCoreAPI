using IGTManagementTicket.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Services
{
    public static class TicketsService
    {
        public static Items GetTicketCount(int jobId, string environment)
        {
            var items = new Items()
            {
                TicketCount = 5,
                BookCount = 3,
                ErrorMessage = null
            };

            if (jobId == 9753 && environment.ToUpper().Equals("STAG"))
            {
                return items;
            }

            return null;            
        }

        public static StatusDB CreateDB(int jobId, string environment)
        {
            var statusDB = new StatusDB()
            {
                JobId = jobId,
                Environment = environment
            };
            
            return statusDB;
        }

        public static StatusDB DeleteDB(int jobId, string environment)
        {
            var statusDB = new StatusDB()
            {
                JobId = jobId,
                Environment = environment
            };

            return statusDB;
        }

        public static StatusDB GetStatusDBByJob(int jobId, string environment)
        {
            var statusDB = new StatusDB()
            {
                JobId = jobId,
                Environment = environment.ToUpper()
            };

            if (jobId == 9753 && environment.ToUpper().Equals("STAG"))
            {
                return statusDB;
            }            

            return null;
        }
    }
}
