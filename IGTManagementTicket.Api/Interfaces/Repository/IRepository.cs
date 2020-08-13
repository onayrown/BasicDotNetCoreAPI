using IGTManagementTicket.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
    }
}
