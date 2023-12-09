using System;
using System.Collections.Generic;


// Observer (Interface)
interface IInvestor
{
    void Update(Stock stock);
}

class Stock
{
    private string symbol;
    private double price;
    private List<IInvestor> investors = new List<IInvestor>();

    public Stock(string symbol, double price)
    {
        this.symbol = symbol;
        this.price = price;
    }

    public void Attach(IInvestor investor)
    {
        investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        investors.Remove(investor);
    }

    public void Notify()
    {
        foreach (var investor in investors)
        {
            investor.Update(this);
        }
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (price != value)
            {
                price = value;
                Notify();
            }
        }
    }

    public string Symbol => symbol;
}

// Concrete Observer
class Investor : IInvestor
{
    private string name;

    public Investor(string name)
    {
        this.name = name;
    }

    public void Update(Stock stock)
    {
        Console.WriteLine($"Notified {name} of {stock.Symbol}'s change to {stock.Price}$");
    }
}

// Concrete Observer 2
class FinancialNews : IInvestor
{
    public void Update(Stock stock)
    {
        Console.WriteLine($"Breaking News: {stock.Symbol} has changed to {stock.Price}$");
    }
}


class Program
{
    static void Main()
    {
        Stock appleStock = new Stock("APPLE", 150.0);

        IInvestor investor1 = new Investor("Penahli Ibrahim");
        IInvestor investor2 = new Investor("Orxan Memmedli");
        IInvestor newsService = new FinancialNews();

        appleStock.Attach(investor1);
        appleStock.Attach(investor2);
        appleStock.Attach(newsService);

        appleStock.Price = 155;
        appleStock.Price = 160;

        appleStock.Detach(investor2);


        appleStock.Price = 158;
        appleStock.Price = 152;
    }
}