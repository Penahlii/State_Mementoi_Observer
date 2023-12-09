using System;


interface IDocumentState
{
    void EditDocument(Document document, string content);
    void ReviewDocument(Document document);
    void ApproveDocument(Document document);
    void PrintDocument(Document document);
}

class DraftState : IDocumentState
{
    public void EditDocument(Document document, string content)
    {
        document.Content = content;
        Console.WriteLine("Document edited and saved as draft");
    }

    public void ReviewDocument(Document document)
    {
        Console.WriteLine("Cannot review, document is in draft state");
    }

    public void ApproveDocument(Document document)
    {
        document.State = new ApprovedState();
        Console.WriteLine("Document approved");
    }

    public void PrintDocument(Document document)
    {
        Console.WriteLine("Cannot print, document is in draft state");
    }
}


class PendingApprovalState : IDocumentState
{
    public void EditDocument(Document document, string content)
    {
        Console.WriteLine("Cannot edit, document is pending approval");
    }

    public void ReviewDocument(Document document)
    {
        document.State = new ApprovedState();
        Console.WriteLine("Document reviewed and approved");
    }

    public void ApproveDocument(Document document)
    {
        Console.WriteLine("Cannot approve, document is already pending approval");
    }

    public void PrintDocument(Document document)
    {
        Console.WriteLine("Cannot print, document is pending approval");
    }
}


class ApprovedState : IDocumentState
{
    public void EditDocument(Document document, string content)
    {
        Console.WriteLine("Cannot edit, document is already approved");
    }

    public void ReviewDocument(Document document)
    {
        Console.WriteLine("Cannot review, document is already approved");
    }

    public void ApproveDocument(Document document)
    {
        Console.WriteLine("Cannot approve, document is already approved");
    }

    public void PrintDocument(Document document)
    {
        Console.WriteLine($"Printing document: {document.Content}");
    }
}


class Document
{
    private string content;

    public IDocumentState State { get; set; }

    public Document()
    {
        State = new DraftState();
    }

    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    public void EditDocument(string newContent)
    {
        State.EditDocument(this, newContent);
    }

    public void ReviewDocument()
    {
        State.ReviewDocument(this);
    }

    public void ApproveDocument()
    {
        State.ApproveDocument(this);
    }

    public void PrintDocument()
    {
        State.PrintDocument(this);
    }
}

class Program
{
    static void Main()
    {
        var document = new Document();
        document.EditDocument("This is the initial content.");
        document.ReviewDocument();
        document.ApproveDocument();
        document.EditDocument("This is the revised content.");
        document.PrintDocument();
    }
}