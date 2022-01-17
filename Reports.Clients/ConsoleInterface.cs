using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;
using Spectre.Console;

namespace Reports.Clients
{
    public class ConsoleInterface
    {
        public void Start()
        {
            AnsiConsole.Markup("[red]Welcome to the Ivan's Reports application![/]");
            AnsiConsole.WriteLine(string.Empty);
            AnsiConsole.Markup("[green]This application helps you to automate the process of reports generating.[/]");
            AnsiConsole.WriteLine(string.Empty);
        }

        public void OnEnd()
        {
            AnsiConsole.Markup("[red]See you later![/]");
            AnsiConsole.WriteLine(string.Empty);
        }

        public void WriteLine(string line)
        {
            AnsiConsole.Markup($"[red]{line}[/]");
            AnsiConsole.WriteLine(string.Empty);
        }

        public string AskForName()
        {
            return AnsiConsole.Ask<string>("[green]What is wanted name?[/]");
        }

        public string AskForTeamleadName()
        {
            return AnsiConsole.Ask<string>("[green]What is teamlead name?[/]");
        }

        public string AskForEmployeeId()
        {
            return AnsiConsole.Ask<string>("[green]What is wanted employee id?[/]");
        }

        public string AskForSubordinateId()
        {
            return AnsiConsole.Ask<string>("[green]What is subordinate id?[/]");
        }

        public string AskForSupervisorId()
        {
            return AnsiConsole.Ask<string>("[green]What is supervisor id?[/]");
        }

        public string AskForTaskId()
        {
            return AnsiConsole.Ask<string>("[green]What is wanted task id?[/]");
        }

        public string AskForReportId()
        {
            return AnsiConsole.Ask<string>("[green]What is wanted report id?[/]");
        }

        public string AskForDescription()
        {
            return AnsiConsole.Ask<string>("[green]Please type the description:[/]");
        }

        public string AskStatus()
        {
            return AnsiConsole.Ask<string>("[green]Please type the status:[/]");
        }

        public void WriteEmployee(Employee employee, List<Task> tasks)
        {
            AnsiConsole.Markup($"[green]{employee.Name} with id: {employee.Id}[/]");
            AnsiConsole.WriteLine(string.Empty);
            if (employee.Subordinates.Any())
            {
                AnsiConsole.Markup("[yellow]Subordinates:[/]");
                AnsiConsole.WriteLine(string.Empty);
                foreach (Employee subordinate in employee.Subordinates)
                {
                    AnsiConsole.Markup($"[green]{subordinate.Name} with id: {subordinate.Id}[/]");
                    AnsiConsole.WriteLine(string.Empty);
                }
            }

            WriteTasks(tasks);
        }

        public void WriteEmployeesTree(Employee teamlead)
        {
            Tree root = new ($"{teamlead.Name}");
            DepthFirstSearch(root, teamlead);
            AnsiConsole.Write(root);
        }

        public void WriteChanges(IEnumerable<Change> changes)
        {
            foreach (Change change in changes)
            {
                AnsiConsole.Markup($"[green]{change.EmployeeId} : {change.Time} : {change.Commentary}[/]");
                AnsiConsole.WriteLine(string.Empty);
            }
        }

        public void WriteTasks(IEnumerable<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                AnsiConsole.Markup($"[red]{task.Id}[/]");
                AnsiConsole.WriteLine(string.Empty);
                AnsiConsole.Markup($"[green]{task.Status}[/]");
                AnsiConsole.WriteLine(string.Empty);
                AnsiConsole.Markup($"[blue]{task.TimeOfCreation}[/]");
                AnsiConsole.WriteLine(string.Empty);
                if (task.Description != null)
                {
                    AnsiConsole.Markup($"[yellow]{task.Description}[/]");
                    AnsiConsole.WriteLine(string.Empty);
                }

                WriteChanges(task.Changes);
            }
        }

        public void WriteReport(Report report, List<Task> tasks)
        {
            AnsiConsole.Markup($"[green]{report.Id} : {report.Status} : {report.TimeOfCreation} : {report.AuthorId} : {report.Commentary}[/]");
            AnsiConsole.WriteLine(string.Empty);
            WriteTasks(tasks);
        }

        public void GetAvailableCommands()
        {
            var table = new Table();
            table.AddColumn("[yellow]Command[/]");
            table.AddColumn(new TableColumn("[green]Description[/]"));
            table.AddRow("[yellow]commands[/]", "[green]Shows the list of all available commands[/]");
            table.AddRow("[yellow]createEmployee[/]", "[green]Creates an employee[/]");
            table.AddRow("[yellow]writeEmployeesTree[/]", "[green]Show the hierarchy of employees[/]");
            table.AddRow("[yellow]findEmployeeById[/]", "[green]Finds an employee using an id[/]");
            table.AddRow("[yellow]findEmployeeByName[/]", "[green]Finds an employee using a name[/]");
            table.AddRow("[yellow]DeleteEmployee[/]", "[green]Deletes employee using an id[/]");
            table.AddRow("[yellow]addSubordinate[/]", "[green]Adds a subordinate to a supervisor[/]");
            table.AddRow("[yellow]getAllTasks[/]", "[green]Shows all the tasks[/]");
            table.AddRow("[yellow]getTaskById[/]", "[green]Finds the task using the id[/]");
            table.AddRow("[yellow]getEmployeeTasks[/]", "[green]Shows employee's tasks[/]");
            table.AddRow("[yellow]getTasksChangedByEmployee[/]", "[green]Shows tasks that were changed by an employee[/]");
            table.AddRow("[yellow]createTask[/]", "[green]Creates tasks with description[/]");
            table.AddRow("[yellow]changeStatusOfTheTask[/]", "[green]Changes status of the task[/]");
            table.AddRow("[yellow]changeAssignedEmployee[/]", "[green]Changes employee that was assigned for the task[/]");
            table.AddRow("[yellow]addTaskDescription[/]", "[green]Adds description to the task using id[/]");
            table.AddRow("[yellow]createReport[/]", "[green]Creates a report with author and description[/]");
            table.AddRow("[yellow]findReportById[/]", "[green]Finds a report using an id[/]");
            table.AddRow("[yellow]addTasksToReport[/]", "[green]Add employee's tasks to the report[/]");
            table.AddRow("[yellow]getAllFinishedDailyReports[/]", "[green]Shows all the reports that were finished not more than 24h ago[/]");
            table.AddRow("[yellow]getTasksForTheWeek[/]", "[green]Shows all the tasks that were made not more than 7 days ago[/]");
            table.AddRow("[yellow]save[/]", "[green]Saves the current state of the server[/]");
            table.AddRow("[yellow]load[/]", "[green]Loads the previous state of the server[/]");
            table.AddRow("[yellow]quit[/]", "[green]Closes this application[/]");
            AnsiConsole.Write(table);
        }

        public string GetCommand()
        {
            return AnsiConsole.Ask<string>("[blue]What do you want me to do for you?[/]");
        }

        private void DepthFirstSearch(Tree tree, Employee employee)
        {
            foreach (Employee subordinate in employee.Subordinates)
            {
                tree.AddNode($"{subordinate.Name}");
                DepthFirstSearch(tree, subordinate);
            }
        }
    }
}