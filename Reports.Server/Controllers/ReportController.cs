using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        public Report Create([FromQuery] Guid id, string commentary)
        {
            return _service.Create(id, commentary);
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Report result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/reports/getAllFinishedDailyReports")]
        public IActionResult GetAllFinishedDailyReports()
        {
            List<Guid> result = _service.GetAllFinishedDailyReports();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}