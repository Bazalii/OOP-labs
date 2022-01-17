using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task Create([FromQuery] string description)
        {
            return _service.Create(description);
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int)HttpStatusCode.BadRequest);
            Task result = _service.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/getAll")]
        public IActionResult GetAll()
        {
            List<Task> result = _service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/getByLatestTime")]
        public IActionResult GetByLatestTime()
        {
            Task result = _service.GetByLatestTime();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/getTasksForTheWeek")]
        public IActionResult GetTasksForTheWeek()
        {
            List<Task> result = _service.GetTasksForTheWeek();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/getTasksChangedByEmployee")]
        public IActionResult GetTasksChangedByEmployee([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
            List<Task> result = _service.GetByEmployeeChanges(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/load")]
        public IActionResult Load()
        {
            _service.Load();
            return Ok();
        }

        [HttpPut]
        [Route("/tasks/save")]
        public IActionResult Save()
        {
            _service.Save();
            return Ok();
        }

        [HttpPatch]
        [Route("/tasks/changeStatus")]
        public IActionResult ChangeStatus([FromQuery] Guid id, [FromQuery] string status)
        {
            if (id == Guid.Empty || string.IsNullOrWhiteSpace(status)) return StatusCode((int)HttpStatusCode.BadRequest);
            switch (status)
            {
                case "Open":
                    _service.ChangeStatus(id, TaskStatus.Open);
                    break;
                case "Active":
                    _service.ChangeStatus(id, TaskStatus.Active);
                    break;
                case "Resolved":
                    _service.ChangeStatus(id, TaskStatus.Resolved);
                    break;
            }

            return Ok();
        }

        [HttpPatch]
        [Route("/tasks/changeTask")]
        public IActionResult ChangeStatus([FromQuery] Guid employeeId, [FromQuery] Guid taskId, [FromQuery] string change)
        {
            if (employeeId == Guid.Empty || taskId == Guid.Empty || string.IsNullOrWhiteSpace(change))
                return StatusCode((int)HttpStatusCode.BadRequest);
            Task result = _service.ChangeTask(employeeId, taskId, change);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPatch]
        [Route("/tasks/addDescription")]
        public IActionResult AddCommentary([FromQuery] Guid id, [FromQuery] string description)
        {
            _service.FindById(id).Description = description;
            return NoContent();
        }
    }
}