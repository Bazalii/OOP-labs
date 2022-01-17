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
            if (id != Guid.Empty)
            {
                Task result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
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
        public void ChangeStatus([FromQuery] Guid id, [FromQuery] string status)
        {
            if (id == Guid.Empty || string.IsNullOrWhiteSpace(status)) return;
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