using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public Employee Create([FromQuery] string name)
        {
            return _service.Create(name);
        }

        [HttpGet]
        public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Employee result = _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (id != Guid.Empty)
            {
                Employee result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/employee/getAll")]
        public IActionResult GetAll()
        {
            List<Employee> result = _service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/employees/getEmployeeTasks")]
        public IActionResult GetEmployeeTasks([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                IReadOnlyList<Guid> result = _service.GetEmployeeTasks(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/employees/getAssignedEmployee")]
        public IActionResult GetAssignedEmployee([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Employee result = _service.GetAssignedEmployee(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/employees/getSubordinatesTasks")]
        public IActionResult GetSubordinatesTasks([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                List<Guid> result = _service.GetSubordinatesTasks(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/employees/load")]
        public IActionResult Load()
        {
            _service.Load();
            return Ok();
        }

        [HttpPut]
        [Route("/employees/save")]
        public IActionResult Save()
        {
            _service.Save();
            return Ok();
        }

        [HttpPatch]
        [Route("/employees/assignEmployee")]
        public IActionResult AssignEmployee([FromQuery] Guid employeeId, [FromQuery] Guid taskId)
        {
            if (employeeId != Guid.Empty && taskId != Guid.Empty)
            {
                Employee result = _service.AssignEmployee(employeeId, taskId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPatch]
        [Route("/employees/changeAssignedEmployee")]
        public IActionResult ChangeAssignedEmployee([FromQuery] Guid employeeId, [FromQuery] Guid taskId)
        {
            if (employeeId != Guid.Empty && taskId != Guid.Empty)
            {
                Employee result = _service.ChangeAssignedEmployee(employeeId, taskId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPatch]
        [Route("/employees/addSubordinate")]
        public IActionResult AddSubordinate([FromQuery] Guid supervisorId, [FromQuery] Guid subordinateId)
        {
            if (supervisorId != Guid.Empty && subordinateId != Guid.Empty)
            {
                Employee result = _service.AddSubordinate(supervisorId, subordinateId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [Route("/employees/deleteEmployee")]
        public IActionResult DeleteEmployee([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Employee result = _service.Delete(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}