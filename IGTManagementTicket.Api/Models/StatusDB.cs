using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Models
{
    public class StatusDB
    {
        public int JobId { get; set; }
        public string Environment { get; set; }
        public string ErrorMessage { get; set; }
    }
}
