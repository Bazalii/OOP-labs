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

        [HttpGet]
        [Route("/reports/load")]
        public IActionResult Load()
        {
            _service.Load();
            return Ok();
        }

        [HttpPut]
        [Route("/reports/save")]
        public IActionResult Save()
        {
            _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("/reports/deleteReport")]
        public IActionResult DeleteReport([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Report result = _service.Delete(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPatch]
        [Route("/reports/changeStatus")]
        public IActionResult ChangeStatus([FromQuery] Guid id, [FromQuery] string status)
        {
            if (id == Guid.Empty || string.IsNullOrWhiteSpace(status)) return StatusCode((int)HttpStatusCode.BadRequest);
            switch (status)
            {
                case "Draft":
                    _service.ChangeStatus(id, ReportStatus.Draft);
                    break;
                case "Ready":
                    _service.ChangeStatus(id, ReportStatus.Ready);
                    break;
                case "Closed":
                    _service.ChangeStatus(id, ReportStatus.Closed);
                    break;
            }

            return Ok();
        }

        [HttpPatch]
        [Route("/reports/addTask")]
        public IActionResult AddTasks([FromQuery] Guid reportId, [FromQuery] Guid taskId)
        {
            _service.FindById(reportId).AddTask(taskId);
            return Ok();
        }
    }
}