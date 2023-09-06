namespace SumUp.Services.Printer
{
    using System.Collections.Generic;

    using SumUp.Contracts.Models;
    using SumUp.Contracts.Printer;
    
    using ConsoleTables;

    public class InvoiceTablePrinter : IPrinter
    {
        private readonly string[] _columns = { "Id", "Date", "Total" };
        private readonly Format _consoleFormat = Format.Default;
        private readonly ConsoleTable _consoleTable;

        public InvoiceTablePrinter()
        {
            _consoleTable = new ConsoleTable(_columns);
            _consoleTable.Options.EnableCount = false;
        }

        public void Print(Invoice invoice)
        {
            _consoleTable.AddRow(invoice.Id, invoice.Date, invoice.Total);
            _consoleTable.AddRow("", "Total:", invoice.Total);

            _consoleTable.Write(_consoleFormat);
        }

        public void Print(IEnumerable<Invoice> invoices)
        {
            float total = 0.0f;

            foreach (Invoice invoice in invoices)
            {
                _consoleTable.AddRow(invoice.Id, invoice.Date, invoice.Total);
                total += invoice.Total;
            }

            _consoleTable.AddRow("", "Total:", total);

            _consoleTable.Write(_consoleFormat);
        }
    }
}
