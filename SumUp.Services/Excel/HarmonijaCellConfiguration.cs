namespace SumUp.Services.Excel
{
    internal class HarmonijaCellConfiguration
    {
        public static readonly HarmonijaCellConfiguration Id = new HarmonijaCellConfiguration(9, 3, "ФАКТУРА БР.:");
        public static readonly HarmonijaCellConfiguration Date = new HarmonijaCellConfiguration(7, 3, "Датум:");
        public static readonly HarmonijaCellConfiguration Total = new HarmonijaCellConfiguration(-5, -1, "");

        private HarmonijaCellConfiguration(int row, int column, string cellPrefix)
        {
            Row = row;
            Column = column;
            CellPrefix = cellPrefix;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public string CellPrefix { get; set; }
    }
}
