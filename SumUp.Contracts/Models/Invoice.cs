namespace SumUp.Contracts.Models
{
    public class Invoice
    {
        public string Id { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public float Total { get; set; } = 0.0f;
    }
}
