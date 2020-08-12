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

            if (jobId == 1 && environment.ToLower().Equals("usa"))
            {
                return items;
            }

            return null;            
        }

        public static Payload CreateDB(int jobId, string environment)
        {
            var payload = new Payload()
            {
                JobId = jobId,
                Environment = environment
            };
            
            return payload;
        }

        public static Payload DeleteDB(int jobId, string environment)
        {
            var payload = new Payload()
            {
                JobId = jobId,
                Environment = environment
            };

            return payload;
        }

        public static Payload GetPayloadByJob(int jobId, string environment)
        {
            var payload = new Payload()
            {
                JobId = jobId,
                Environment = environment
            };

            if (jobId == 1 && environment.ToLower().Equals("usa"))
            {
                return payload;
            }

            return null;
        }
    }
}
