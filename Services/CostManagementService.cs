using ERPCostManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPCostManagement.Services
{
    public class CostManagementService
    {
        private List<Invoice> _invoices = new List<Invoice>
        {
            new Invoice { Id = 1, ClientId = 1, Amount = 1000m, DueDate = DateTime.Now.AddDays(-5), Status = InvoiceStatus.Overdue },
            new Invoice { Id = 2, ClientId = 1, Amount = 500m, DueDate = DateTime.Now.AddDays(10), Status = InvoiceStatus.Unpaid },
            new Invoice { Id = 3, ClientId = 2, Amount = 750m, DueDate = DateTime.Now.AddDays(5), Status = InvoiceStatus.Unpaid }
        };

        private List<Payment> _payments = new List<Payment>
        {
            new Payment { Id = 1, InvoiceId = 1, Amount = 500m, PaymentMethod = "Cash", PaymentDate = DateTime.Now.AddDays(-2) }
        };

        private int _nextPaymentId = 2;
        private int _nextReceiptNumber = 1;

        public Payment LogPayment(int invoiceId, decimal amount, string paymentMethod)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            var payment = new Payment
            {
                Id = _nextPaymentId++,
                InvoiceId = invoiceId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                PaymentDate = DateTime.Now
            };
            _payments.Add(payment);
            invoice.Payments.Add(payment);

            UpdateInvoiceStatus(invoice);
            return payment;
        }

        public PaymentReceipt GenerateReceipt(int paymentId)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == paymentId);
            if (payment == null) throw new Exception("Payment not found");

            return new PaymentReceipt
            {
                PaymentId = payment.Id,
                InvoiceId = payment.InvoiceId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                ReceiptNumber = $"REC-{_nextReceiptNumber++:D4}"
            };
        }

        public void UpdateInvoiceStatus(int invoiceId, InvoiceStatus status)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");
            invoice.Status = status;
        }

        public List<Payment> GetPaymentHistory(int invoiceId)
        {
            return _payments.Where(p => p.InvoiceId == invoiceId).ToList();
        }

        public InvoiceSummaryReport GenerateSummaryReport(int? clientId, InvoiceStatus? status, DateTime? startDate, DateTime? endDate)
        {
            var filteredInvoices = _invoices.AsQueryable();

            if (clientId.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.ClientId == clientId.Value);
            if (status.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.Status == status.Value);
            if (startDate.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.DueDate >= startDate.Value);
            if (endDate.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.DueDate <= endDate.Value);

            var invoices = filteredInvoices.ToList();
            return new InvoiceSummaryReport
            {
                ClientId = clientId ?? 0,
                Status = status,
                StartDate = startDate,
                EndDate = endDate,
                Invoices = invoices,
                TotalAmount = invoices.Sum(i => i.Amount)
            };
        }

        private void UpdateInvoiceStatus(Invoice invoice)
        {
            var totalPaid = invoice.Payments.Sum(p => p.Amount);
            if (totalPaid >= invoice.Amount)
                invoice.Status = InvoiceStatus.Paid;
            else if (invoice.DueDate < DateTime.Now)
                invoice.Status = InvoiceStatus.Overdue;
            else
                invoice.Status = InvoiceStatus.Unpaid;
        }
    }
}