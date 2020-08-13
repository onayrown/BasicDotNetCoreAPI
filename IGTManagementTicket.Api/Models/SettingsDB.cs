using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Models
{
    public class SettingsDB
    {
        public int JobId { get; set; }
        public EnvironmentType Environment { get; set; }
    }
}
