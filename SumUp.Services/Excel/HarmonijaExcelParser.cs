namespace SumUp.Services.Excel
{
    using System.Linq;

    using SumUp.Contracts.Excel;
    using SumUp.Contracts.Models;

    using ClosedXML.Excel;

    public class HarmonijaExcelParser : IExcelParser
    {
        public Invoice GetInvoice(string filename)
        {
            using(XLWorkbook workbook = new XLWorkbook(filename))
            {
                IXLWorksheet defaultSheet = workbook.Worksheets.FirstOrDefault();

                return new Invoice()
                {
                    Id = GetInvoiceCell(defaultSheet, HarmonijaCellConfiguration.Id),
                    Date = GetInvoiceCell(defaultSheet, HarmonijaCellConfiguration.Date),
                    Total = GetInvoiceTotal(defaultSheet)
                };
            }
        }

        private string GetInvoiceCell(IXLWorksheet worksheet, HarmonijaCellConfiguration cellConfiguration)
        {
            string data = worksheet.Cell(
                cellConfiguration.Row,
                cellConfiguration.Column).GetValue<string>();

            return data
                .Replace(cellConfiguration.CellPrefix, "")
                .Trim();
        }

        private float GetInvoiceTotal(IXLWorksheet worksheet)
        {
            int rowIndex = worksheet.LastRowUsed().RowNumber() + HarmonijaCellConfiguration.Total.Row;
            int columnIndex = worksheet.LastColumnUsed().ColumnNumber() + HarmonijaCellConfiguration.Total.Column;

            return worksheet.Cell(rowIndex, columnIndex).GetValue<float>();
        }
    }
}
