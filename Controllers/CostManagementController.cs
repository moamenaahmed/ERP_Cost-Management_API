using ERPCostManagement.Models;
using ERPCostManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERPCostManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostManagementController : ControllerBase
    {
        private readonly CostManagementService _service;

        public CostManagementController(CostManagementService service)
        {
            _service = service;
        }

        [HttpPost("payments")]
        public IActionResult LogPayment([FromBody] LogPaymentRequest request)
        {
            try
            {
                var payment = _service.LogPayment(request.InvoiceId, request.Amount, request.PaymentMethod);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("receipts")]
        public IActionResult GenerateReceipt([FromBody] GenerateReceiptRequest request)
        {
            try
            {
                var receipt = _service.GenerateReceipt(request.PaymentId);
                return Ok(receipt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("invoices/{invoiceId}/status")]
        public IActionResult UpdateInvoiceStatus(int invoiceId, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                _service.UpdateInvoiceStatus(invoiceId, request.Status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("invoices/{invoiceId}/payments")]
        public IActionResult GetPaymentHistory(int invoiceId)
        {
            try
            {
                var payments = _service.GetPaymentHistory(invoiceId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("reports")]
        public IActionResult GenerateSummaryReport([FromQuery] int? clientId, [FromQuery] InvoiceStatus? status,
            [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var report = _service.GenerateSummaryReport(clientId, status, startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class LogPaymentRequest
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class GenerateReceiptRequest
    {
        public int PaymentId { get; set; }
    }

    public class UpdateStatusRequest
    {
        public InvoiceStatus Status { get; set; }
    }
}