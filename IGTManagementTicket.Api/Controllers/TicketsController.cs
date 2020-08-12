using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IGTManagementTicket.Api.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public string HealthCheck()
        {
            return "I am UP and Running";
        }
        // GET: api/<TicketsController>
        [HttpGet("{jobId}/{environment}")]
        public ActionResult<IEnumerable<Items>> TicketCount(int jobId, string environment)
        {
            try
            {
                var items = TicketsService.GetTicketCount(jobId, environment);

                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{jobId}/{environment}")]
        public ActionResult<StatusDB> CreateDB(int jobId, string environment)
        {
            try
            {
                var result = TicketsService.CreateDB(jobId, environment);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{jobId}/{environment}")]
        public IActionResult DeleteDB(int jobId, string environment)
        {
            try
            {
                var result = TicketsService.GetStatusDBByJob(jobId, environment);

                if (result == null)
                {
                    return NotFound();
                }

                TicketsService.DeleteDB(jobId, environment);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
