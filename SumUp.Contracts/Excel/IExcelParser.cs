namespace SumUp.Contracts.Excel
{
    using SumUp.Contracts.Models;

    public interface IExcelParser
    {
        public Invoice GetInvoice(string filename);
    }
}
