namespace ERPCostManagement.Models
{
    public class InvoiceSummaryReport
    {
        public int ClientId { get; set; }
        public InvoiceStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public decimal TotalAmount { get; set; }
    }
}