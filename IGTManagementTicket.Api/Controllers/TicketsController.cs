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
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "I am UP and Running";
        }
        // GET: api/<TicketsController>
        [HttpGet("{job}/{environment}")]
        public ActionResult<IEnumerable<Items>> GetTicketCounts(int job, string environment)
        {
            try
            {
                var items = TicketsService.GetTicketCount(job, environment);

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
                var result = TicketsService.CreateDB(payload.Job, payload.Environment);

                return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{job}/{environment}")]
        public IActionResult DeleteDB(int job, string environment)
        {
            try
            {
                var result = TicketsService.GetPayloadByJob(job, environment);

                if (result == null)
                {
                    return NotFound();
                }

                TicketsService.DeleteDB(job, environment);

                return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
