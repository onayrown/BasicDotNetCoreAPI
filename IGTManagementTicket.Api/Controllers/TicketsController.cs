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

        // POST api/<TicketsController>
        public ActionResult<Payload> CreateDB(Payload payload)
        {
            try
            {
                var result = TicketsService.CreateDB(payload.JobId, payload.Environment);

                return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{jobId}/{environment}")]
        public IActionResult DeleteDB(int jobId, string environment)
        {
            try
            {
                var result = TicketsService.GetPayloadByJob(jobId, environment);

                if (result == null)
                {
                    return NotFound();
                }

                TicketsService.DeleteDB(jobId, environment);

                return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
