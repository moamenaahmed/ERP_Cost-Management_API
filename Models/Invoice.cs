namespace ERPCostManagement.Models
{
    public enum InvoiceStatus
    {
        Paid,
        Unpaid,
        Overdue
    }

    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}