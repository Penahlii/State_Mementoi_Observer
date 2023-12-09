using System;
using System.Collections.Generic;

class AccountMemento
{
    public decimal Balance { get; private set; }
    public List<string> TransactionHistory { get; private set; }

    public AccountMemento(decimal balance, List<string> transactionHistory)
    {
        Balance = balance;
        TransactionHistory = new List<string>(transactionHistory);
    }
}

class FinancialAccount
{
    private decimal balance;
    private List<string> transactionHistory = new List<string>();
    private Stack<AccountMemento> mementos = new Stack<AccountMemento>();

    public FinancialAccount(decimal initialBalance)
    {
        balance = initialBalance;
        SaveState();
    }

    public void Deposit(decimal amount)
    {
        balance += amount;
        string transaction = $"Deposited {amount:C}. New balance: {balance:C}";
        transactionHistory.Add(transaction);
        SaveState();
        Console.WriteLine(transaction);
    }

    public void Withdraw(decimal amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            string transaction = $"Withdrawn {amount:C}. New balance: {balance:C}";
            transactionHistory.Add(transaction);
            SaveState();
            Console.WriteLine(transaction);
        }
        else
        {
            Console.WriteLine("Insufficient funds");
        }
    }

    public void DisplayTransactionHistory()
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine($"Current Balance: {balance:C}");
    }

    public void Undo()
    {
        if (mementos.Count > 1)
        {
            mementos.Pop(); 
            var previousState = mementos.Peek();
            balance = previousState.Balance;
            transactionHistory = new List<string>(previousState.TransactionHistory);
            Console.WriteLine("Undo");
        }
        else
        {
            Console.WriteLine("Cannot undo further");
        }
    }

    private void SaveState()
    {
        mementos.Push(new AccountMemento(balance, transactionHistory));
    }
}

class Program
{
    static void Main()
    {
        var account = new FinancialAccount(1000);

        account.Deposit(500);
        account.Withdraw(200);
        account.Deposit(1000);
        account.DisplayTransactionHistory();

        Console.WriteLine("\nUndoing last transaction:");
        account.Undo();
        account.DisplayTransactionHistory();
    }
}