namespace SumUp.Core
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using SumUp.Core.Logic;
    using SumUp.Core.Options;
    using SumUp.Services.Excel;
    using SumUp.Contracts.Excel;
    using SumUp.Contracts.Models;
    using SumUp.Services.Printer;
    using SumUp.Contracts.Printer;
    using SumUp.Contracts.Startup;

    internal class Program : IStartup
    {
        private readonly IExcelParser _excelParser;
        private readonly IPrinter _printer;

        public Program(IExcelParser excelParser, IPrinter printer)
        {
            _excelParser = excelParser;
            _printer = printer;
        }

        public void Start(StartMode mode, string path)
        {
            switch (mode)
            {
                case StartMode.File:
                    FileInvoice(path);
                    break;

                case StartMode.Directory:
                    DirectoryInvoice(path);
                    break;
            }
        }

        private void FileInvoice(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File doesn't exists");
                return;
            }

            _printer.Print(_excelParser.GetInvoice(path));
        }

        private void DirectoryInvoice(string path)
        {
            if (!Directory.Exists(path))
            {
                if(File.Exists(path))
                {
                    path = Path.GetDirectoryName(path)!;
                }
                else
                {
                    Console.WriteLine("Directory doesn't exists");
                    return;
                }
            }

            IEnumerable<string> files =
                Directory.EnumerateFiles(path, "*.xlsx", SearchOption.AllDirectories);

            IEnumerable<Invoice> invoices = files.Select(x => _excelParser.GetInvoice(x));

            _printer.Print(invoices);
        }

        static void Main(string[] args)
        {
            SumUpOptions? options = Parser.Parse(args);

            if (options == null)
            {
                return;
            }

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IStartup, Program>();
                    services.AddSingleton<IExcelParser, HarmonijaExcelParser>();
                    services.AddSingleton<IPrinter, InvoiceTablePrinter>();
                })
                .Build();

            StartMode mode = options.FilePath != null ? StartMode.File : StartMode.Directory;
            string? path = options.FilePath ?? options.DirectoryPath;

            if (path == null)
            {
                Console.WriteLine("Invalid argument provided");
                return;
            }

            IStartup startup = host.Services.GetRequiredService<IStartup>();
            startup.Start(mode, path);
        }
    }
}