namespace ERPCostManagement.Models
{
    public class PaymentReceipt
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }= string.Empty;
        public DateTime PaymentDate { get; set; }
        public string ReceiptNumber { get; set; }= string.Empty;
    }
}