using System.Collections.Generic;
using System.Linq;
using Banks.BanksStructure;
using Banks.BanksStructure.Implementations;
using Spectre.Console;

namespace Banks.ConsoleInterfaceStructure
{
    public class ConsoleInterface
    {
        private InputTransformer _inputTransformer = new ();

        public DataForNewAccount RegisterAccount(List<string> banks, List<string> accountTypes)
        {
            string bankName = GetWantedBank(banks);
            string accountType = GetWantedAccount(accountTypes);
            float amountOfMoney = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter [green]amount of money[/]")
                    .PromptStyle("red")
                    .Secret());
            return new DataForNewAccount(bankName, accountType, amountOfMoney);
        }

        public string CloseAccount(List<string> personalAccounts)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Which account do you want to close?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(personalAccounts));
        }

        public DataForOneWayTransaction WithdrawMoney(List<string> personalAccounts)
        {
            string accountId = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]From which account do you want to withdraw money?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(personalAccounts));
            float amountOfMoney = AnsiConsole.Ask<float>("[green]What amount of money do you want to withdraw[/]?");
            return new DataForOneWayTransaction(accountId, amountOfMoney);
        }

        public DataForOneWayTransaction ReplenishMoney(List<string> personalAccounts)
        {
            string accountId = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]From which account do you want to replenish money?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(personalAccounts));
            float amountOfMoney = AnsiConsole.Ask<float>("[green]What amount of money do you want to replenish[/]?");
            return new DataForOneWayTransaction(accountId, amountOfMoney);
        }

        public DataForTwoWaysTransactions TransferMoney(List<string> personalAccounts)
        {
            string accountToWithdrawId = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]From which account do you want to transfer money?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(personalAccounts));
            string accountToReplenishId = AnsiConsole.Ask<string>("[green]Type target account Id[/]?");
            float amountOfMoney = AnsiConsole.Ask<float>("[green]What amount of money do you want to transfer[/]?");
            return new DataForTwoWaysTransactions(accountToWithdrawId, accountToReplenishId, amountOfMoney);
        }

        public int CancelTransaction(List<int> personalTransactions)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("[green]Which transaction do you want to cancel?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(personalTransactions));
        }

        public void GetTransactions(List<Transaction> transactions)
        {
            if (!transactions.Any()) return;
            var table = new Table();
            table.AddColumn("[yellow]Transaction Id[/]");
            table.AddColumn("[red]Withdrawal account[/]");
            table.AddColumn(new TableColumn("[green]Replenishment account[/]"));
            table.AddColumn(new TableColumn("[blue]Amount of money[/]"));
            foreach (Transaction transaction in transactions)
            {
                switch (transaction)
                {
                    case WithdrawalTransaction withdrawalTransaction:
                        table.AddRow(
                            $"[yellow]{withdrawalTransaction.GetId()}[/]",
                            $"[red]{withdrawalTransaction.AccountToWithdraw.GetId()}[/]",
                            "-",
                            $"[green]{withdrawalTransaction.GetAmountOfMoney()}[/]");
                        break;
                    case ReplenishmentTransaction replenishmentTransaction:
                        table.AddRow(
                            $"[yellow]{replenishmentTransaction.GetId()}[/]",
                            "-",
                            $"[green]{replenishmentTransaction.AccountToReplenish.GetId()}[/]",
                            $"[blue]{replenishmentTransaction.GetAmountOfMoney()}[/]");
                        break;
                    case TransferTransaction transferTransaction:
                        table.AddRow(
                            $"[yellow]{transferTransaction.GetId()}[/]",
                            $"[red]{transferTransaction.AccountToWithdraw.GetId()}[/]",
                            $"[green]{transferTransaction.AccountToReplenish.GetId()}[/]",
                            $"[blue]{transferTransaction.GetAmountOfMoney()}[/]");
                        break;
                }
            }

            AnsiConsole.Write(table);
        }

        public int ScrollDays()
        {
            return AnsiConsole.Ask<int>("[green]How many days do you want to scroll[/]?");
        }

        public void OnEnd()
        {
            AnsiConsole.Markup("[red]See you later![/]");
        }

        public void Start()
        {
            AnsiConsole.Markup("[red]Welcome to the Ivan's Bank application![/]");
            AnsiConsole.WriteLine(string.Empty);
            AnsiConsole.Markup("[blue]Type \"commands\" to get full list of available commands.[/]");
            AnsiConsole.WriteLine(string.Empty);
        }

        public Client RegisterClient()
        {
            string clientName = AnsiConsole.Ask<string>("What's your [green]name[/]?");
            string clientSurname = AnsiConsole.Ask<string>("What's your [green]surname[/]?");
            string clientAddress = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]][/] What's your [green]address[/]?")
                    .AllowEmpty());
            string clientPassportNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]][/] What's your [green]passport number[/]?")
                    .AllowEmpty());
            AnsiConsole.Markup($"[red]{clientPassportNumber}[/]");
            AnsiConsole.WriteLine("\n");
            return _inputTransformer.CreateClient(clientName, clientSurname, clientAddress, clientPassportNumber);
        }

        public string GetCommand()
        {
            return AnsiConsole.Ask<string>("[blue]What do you want me to do for you[/]?");
        }

        public void GetAvailableCommands()
        {
            var table = new Table();
            table.AddColumn("[yellow]Command[/]");
            table.AddColumn(new TableColumn("[green]Description[/]"));
            table.AddRow("[yellow]commands[/]", "[green]Shows the list of all available commands[/]");
            table.AddRow("[yellow]getAccounts[/]", "[green]Shows the list of your accounts[/]");
            table.AddRow("[yellow]getTransactions[/]", "[green]Shows the list of your transactions[/]");
            table.AddRow("[yellow]registerClient[/]", "[green]Gives you an opportunity to register if you are not registered yet[/]");
            table.AddRow("[yellow]registerAccount[/]", "[green]Adds new account[/]");
            table.AddRow("[yellow]closeAccount[/]", "[green]Closes one of your accounts[/]");
            table.AddRow("[yellow]withdraw[/]", "[green]Withdraws money from your account[/]");
            table.AddRow("[yellow]replenish[/]", "[green]Replenishes money to your account[/]");
            table.AddRow("[yellow]transfer[/]", "[green]Transfers money from your account to the other account[/]");
            table.AddRow("[yellow]cancel[/]", "[green]Cancels transaction[/]");
            table.AddRow("[yellow]scroll[/]", "[green]Shows what will happen with your accounts in number of days[/]");
            table.AddRow("[yellow]quit[/]", "[green]Closes this application[/]");
            AnsiConsole.Write(table);
        }

        public void GetAccountsStatus(IReadOnlyList<Account> accounts)
        {
            if (!accounts.Any()) return;
            var table = new Table();
            table.AddColumn("[yellow]Account Id[/]");
            table.AddColumn(new TableColumn("[green]Amount of money[/]"));
            foreach (Account account in accounts)
            {
                table.AddRow($"[yellow]{account.GetId()}[/]", $"[green]{account.GetAmountOfMoney()}[/]");
            }

            AnsiConsole.Write(table);
        }

        public string GetWantedBank(List<string> banks)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]In which bank do you want to open new account?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(banks));
        }

        public string GetWantedAccount(List<string> accountTypes)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Which account do you want to open?[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(accountTypes));
        }
    }
}