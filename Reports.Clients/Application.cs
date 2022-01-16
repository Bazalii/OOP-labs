using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    public class Application
    {
        private ConsoleInterface _console = new ();

        public void Process()
        {
            _console.Start();
            while (true)
            {
                string command = _console.GetCommand();
                switch (command)
                {
                    case "commands":
                        _console.GetAvailableCommands();
                        break;
                    case "createEmployee":
                        CreateEmployee(_console.AskForName());
                        break;
                    case "writeEmployeesTree":
                        WriteEmployeesTree(_console.AskForTeamleadName());
                        break;
                    case "findEmployeeById":
                        FindEmployeeById(_console.AskForEmployeeId());
                        break;
                    case "findEmployeeByName":
                        FindEmployeeByName(_console.AskForName());
                        break;
                    case "deleteEmployee":
                        DeleteEmployee(_console.AskForEmployeeId());
                        break;
                    case "addSubordinate":
                        AddSubordinate(_console.AskForSupervisorId(), _console.AskForSubordinateId());
                        break;
                    case "getAllTasks":
                        GetAllTasks();
                        break;
                    case "getTaskById":
                        GetTaskById(_console.AskForTaskId());
                        break;
                    case "getEmployeeTasks":
                        GetEmployeeTasks(_console.AskForEmployeeId());
                        break;
                    case "getTasksChangedByEmployee":
                        GetTasksChangedByEmployee(_console.AskForEmployeeId());
                        break;
                    case "createTask":
                        CreateTask(_console.AskForDescription());
                        break;
                    case "changeStatusOfTheTask":
                        ChangeStatusOfTheTask(_console.AskForTaskId(), _console.AskStatus());
                        break;
                    case "changeAssignedEmployee":
                        ChangeAssignedEmployee(_console.AskForEmployeeId(), _console.AskForTaskId());
                        break;
                    case "addTaskDescription":
                        AddTaskDescription(_console.AskForTaskId(), _console.AskForDescription());
                        break;
                    case "createReport":
                        CreateReport(_console.AskForEmployeeId(), _console.AskForDescription());
                        break;
                    case "findReportById":
                        FindReportById(_console.AskForReportId());
                        break;
                    case "addTasksToReport":
                        ChangeStatusOfTheTask(_console.AskForEmployeeId(), _console.AskForReportId());
                        break;
                    case "getAllFinishedDailyReports":
                        GetAllFinishedDailyReports();
                        break;
                    case "getTasksForTheWeek":
                        GetTasksForTheWeek();
                        break;
                    case "save":
                        Save();
                        break;
                    case "load":
                        Load();
                        break;
                    case "quit":
                        _console.OnEnd();
                        return;
                }
            }
        }

        public void CreateEmployee(string name)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            _console.WriteEmployee(JsonConvert.DeserializeObject<Employee>(responseString));
        }

        public void WriteEmployeesTree(string name)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            _console.WriteEmployeesTree(JsonConvert.DeserializeObject<Employee>(responseString));
        }

        public Employee FindEmployeeById(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);
                _console.WriteEmployee(employee);
                return employee;
            }
            catch (WebException e)
            {
                _console.WriteLine($"Employee was not found: {e}");
                return null;
            }
        }

        public Employee FindEmployeeByName(string name)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                _console.WriteEmployee(JsonConvert.DeserializeObject<Employee>(responseString));
                return JsonConvert.DeserializeObject<Employee>(responseString);
            }
            catch (WebException e)
            {
                _console.WriteLine($"Employee was not found: {e}");
                return null;
            }
        }

        public void DeleteEmployee(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/deleteEmployee/?id={id}");
            request.Method = "DELETE";
            request.GetResponse();
        }

        public void AddSubordinate(string supervisorId, string subordinateId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/addSubordinate/?supervisorId={supervisorId}&subordinateId={subordinateId}");
            request.Method = "PATCH";
            request.GetResponse();
            FindEmployeeById(supervisorId);
        }

        public List<Task> GetAllTasks()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/getAll");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                _console.WriteTasks(JsonConvert.DeserializeObject<List<Task>>(responseString));
                return JsonConvert.DeserializeObject<List<Task>>(responseString);
            }
            catch (WebException e)
            {
                _console.WriteLine($"Tasks were not found: {e}");
                return null;
            }
        }

        public Task GetTaskById(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                List<Task> taskToWrite = new () { JsonConvert.DeserializeObject<Task>(responseString) };
                _console.WriteTasks(taskToWrite);
                return taskToWrite[0];
            }
            catch (WebException e)
            {
                _console.WriteLine($"Task was not found: {e}");
                return null;
            }
        }

        public Task GetLatestTask()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/getByLatestTime");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                List<Task> taskToWrite = new () { JsonConvert.DeserializeObject<Task>(responseString) };
                _console.WriteTasks(taskToWrite);
                return taskToWrite[0];
            }
            catch (WebException e)
            {
                _console.WriteLine($"Task was not found: {e}");
                return null;
            }
        }

        public IReadOnlyList<Task> GetEmployeeTasks(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);
                _console.WriteTasks(employee.Tasks);
                return employee.Tasks;
            }
            catch (WebException e)
            {
                _console.WriteLine($"Employee was not found: {e}");
                return null;
            }
        }

        public List<Task> GetTasksChangedByEmployee(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/getTasksChangedByEmployee/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(responseString);
                _console.WriteTasks(tasks);
                return tasks;
            }
            catch (WebException e)
            {
                _console.WriteLine($"Tasks were not found: {e}");
                return null;
            }
        }

        public void CreateTask(string description)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/?description={description}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            List<Task> taskToWrite = new () { JsonConvert.DeserializeObject<Task>(responseString) };
            _console.WriteTasks(taskToWrite);
        }

        /* dd*/
        public void ChangeStatusOfTheTask(string id, string status)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/changeStatus/?id={id}&status={status}");
            request.Method = "PATCH";
            request.GetResponse();
        }

        /* dd*/
        public void ChangeAssignedEmployee(string employeeId, string taskId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/changeAssignedEmployee/?employeeId={employeeId}&taskId={taskId}");
            request.Method = "PATCH";
            request.GetResponse();
        }

        /* dd*/
        public void AddTaskDescription(string id, string description)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/addDescription/?id={id}&commentary={description}");
            request.Method = "PATCH";
            request.GetResponse();
            _console.WriteLine("Commentary added!");
        }

        public void CreateReport(string authorId, string description)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/?authorId={authorId}&description={description}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            _console.WriteReport(JsonConvert.DeserializeObject<Report>(responseString));
        }

        public Report FindReportById(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                Report report = JsonConvert.DeserializeObject<Report>(responseString);
                return report;
            }
            catch (WebException e)
            {
                _console.WriteLine($"Report was not found: {e}");
                return null;
            }
        }

        /* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        public void AddTasksToReport(string employeeId, string reportId)
        {
            Employee employee = FindEmployeeById(employeeId);
            Report report = FindReportById(reportId);
            foreach (Task task in employee.Tasks)
            {
                report.AddTask(task);
            }
        }

        public void GetAllFinishedDailyReports()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/getAllFinishedDailyReports");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(responseString);
            }
            catch (WebException e)
            {
                _console.WriteLine($"Reports were not found: {e}");
            }
        }

        public List<Task> GetTasksForTheWeek()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/getTasksForTheWeek");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(responseString);
                return tasks;
            }
            catch (WebException e)
            {
                _console.WriteLine($"Report was not found: {e}");
                return null;
            }
        }
    }
}