namespace SumUp.Core.Options
{
    using Commander.NET.Attributes;

    internal class SumUpOptions
    {
        [Parameter("-f", "--file", Description = "Path to .xlsx file for generating info", Regex = @".*\.xlsx", Required = Required.No)]
        public string? FilePath { get; set; } = null;

        [Parameter("-d", "--directory", Description = "Path to folder containing .xlsx files for generating info", Required = Required.No)]
        public string? DirectoryPath { get; set; } = null;
    }
}
