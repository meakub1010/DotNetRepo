public class Invoice
{
    public void AddItem(string itemName, decimal price)
    {
        // Logic to add an item to the invoice        
    }
    public decimal GetTotal()
    {
        // Logic to calculate the total amount of the invoice
        return 0.0m; // Placeholder return value
    }

    /*
    dfining print and save methods here in Invoice class violates SRP
    SRP - Single Responsibility Principle states that a class should have only one reason to change.
    */

}

public class InvoicePrinter
{
    public void Print(Invoice invoice)
    { 
        // print invoice details
    }
}

public class InvoiceSaver
{
    public void Save(Invoice invoice)
    {
        // save invoice to database or file
    }
 }