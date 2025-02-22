using System;

namespace bankkonto
{
    class BankAccount
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public int MonthlySaving { get; set; }
        public string Shares { get; set; }

        public BankAccount(string name, int balance)
        {
            Name = name;
            Balance = balance;
        }

        public void CheckBalance()
        {
            Console.WriteLine($"You have {Balance:C} in your bank account."); // Use currency formatting
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void Deposit(int value)
        {
            if (value <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }
            Balance += value;
            Console.WriteLine($"Deposited {value:C}. New balance: {Balance:C}");
        }

        public void Withdraw(int value)
        {
            if (value <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return;
            }
            if (Balance < value)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }
            Balance -= value;
            Console.WriteLine($"Withdrew {value:C}. New balance: {Balance:C}");
        }

        public void SetMonthlySaving(int savings)
        {
            if (savings < 0)
            {
                Console.WriteLine("Monthly saving must be non-negative.");
                return;
            }
            MonthlySaving = savings;
            Console.WriteLine($"Monthly saving set to {savings:C}.");
        }

        public void SetShare(string share)
        {
            Shares = share;
            Console.WriteLine($"Shares updated to {Shares}.");
        }

        public override string ToString()
        {
            return $"Congratulations, {Name}! You have opened an account with a balance of {Balance:C}."; // Use string interpolation
        }
    }

    class Banking
    {
        public void Run()
        {
            BankAccount account = null;

            while (true)
            {
                if (account == null)
                {
                    account = CreateAccount();
                    if (account == null) continue; //restart the loop
                }

                DisplayMenu();
                string userInput = GetUserInput();

                ProcessInput(account, userInput);
            }
        }

        private BankAccount CreateAccount()
        {
            string tempName;
            int tempBalance;

            Console.WriteLine("Hello and welcome! To use this system, please enter the following:");

            Console.Write("Name: ");
            tempName = Console.ReadLine();

            while (true)
            {
                try
                {
                    Console.Write("Balance: ");
                    tempBalance = int.Parse(Console.ReadLine());
                    if (tempBalance < 0)
                    {
                        Console.WriteLine("Balance cannot be negative.  Please try again.");
                        continue; //restart balance prompt
                    }
                    break; //valid balance entered
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }

            BankAccount account = new BankAccount(tempName, tempBalance);
            Console.WriteLine(account.ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return account;
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the awesome banking system");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("[C] Check Balance");
            Console.WriteLine("[D] Deposit");
            Console.WriteLine("[W] Withdraw");
            Console.WriteLine("[M] Set monthly saving");
            Console.WriteLine("[S] Set your stock");
            Console.WriteLine("[Q] Quit");
            Console.Write("Type: ");
        }

        private string GetUserInput()
        {
            return Console.ReadLine().ToUpper();
        }

        private void ProcessInput(BankAccount account, string userInput)
        {
            switch (userInput)
            {
                case "C":
                    account.CheckBalance();
                    break;
                case "D":
                    DepositFunds(account);
                    break;
                case "W":
                    WithdrawFunds(account);
                    break;
                case "M":
                    SetMonthlySavings(account);
                    break;
                case "S":
                    SetStock(account);
                    break;
                case "Q":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid option.  Please try again.");
                    break;
            }
        }

        private void DepositFunds(BankAccount account)
        {
            Console.Write("Enter the amount to deposit: ");
            try
            {
                int depositAmount = int.Parse(Console.ReadLine());
                account.Deposit(depositAmount);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        private void WithdrawFunds(BankAccount account)
        {
            Console.Write("Enter the amount to withdraw: ");
            try
            {
                int withdrawalAmount = int.Parse(Console.ReadLine());
                account.Withdraw(withdrawalAmount);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        private void SetMonthlySavings(BankAccount account)
        {
            Console.Write("How much do you want to set as your monthly saving? ");
            try
            {
                int savingAmount = int.Parse(Console.ReadLine());
                account.SetMonthlySaving(savingAmount);
                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        private void SetStock(BankAccount account)
        {
            Console.Write("What do you want to add to your stocks? ");
            string stockName = Console.ReadLine();
            account.SetShare(stockName);
            Console.ReadKey(true);
        }

        private void ExitProgram()
        {
            Console.WriteLine("Bye! See you again!");
            Environment.Exit(0);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Banking bank = new Banking();
            bank.Run();

            Console.ReadKey();
        }
    }
}