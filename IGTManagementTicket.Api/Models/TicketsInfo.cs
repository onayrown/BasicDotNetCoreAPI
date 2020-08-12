using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Models
{
    public class TicketsInfo
    {
        public int Tickets { get; set; }
        public int Books { get; set; }
        public string ErrorMessage { get; set; }
    }
}
