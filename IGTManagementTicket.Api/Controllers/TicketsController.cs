﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGTManagementTicket.Api.Interfaces.Service;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using IGTManagementTicket.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IGTManagementTicket.Api.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet]
        public string HealthCheck()
        {
            return "I am UP and Running";
        }
        // GET: api/<TicketsController>
        [HttpGet("{jobId}/{environment}")]
        public ActionResult<IEnumerable<TicketsInfo>> TicketCount(int jobId, string environment)
        {
            try
            {
                var environmentType = _ticketsService.GetEnvironmentType(environment);

                if (environmentType == EnvironmentType.Unknow)
                {
                    return NotFound();
                }

                var items = _ticketsService.DatabaseExists(jobId, environmentType);

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
                var environmentType = _ticketsService.GetEnvironmentType(environment);

                if (environmentType == EnvironmentType.Unknow)
                {
                    return NotFound();
                }

                var result = _ticketsService.DatabaseCreate(jobId, environmentType);

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
                var environmentType = _ticketsService.GetEnvironmentType(environment);

                if (environmentType == EnvironmentType.Unknow)
                {
                    return NotFound();
                }

                var result = _ticketsService.DatabaseDelete(jobId, environmentType);                

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{jobId}/{environment}")]
        public IActionResult ClearDB(int jobId, string environment)
        {
            try
            {
                var environmentType = _ticketsService.GetEnvironmentType(environment);

                if (environmentType == EnvironmentType.Unknow)
                {
                    return NotFound();
                }

                var result = _ticketsService.DatabaseClear(jobId, environmentType);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
