namespace SumUp.Contracts.Printer
{
    using System.Collections.Generic;

    using SumUp.Contracts.Models;

    public interface IPrinter
    {
        public void Print(Invoice invoice);

        public void Print(IEnumerable<Invoice> invoices);
    }
}
