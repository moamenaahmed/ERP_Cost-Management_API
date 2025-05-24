# ERP Cost Management API

## Overview
This project is an ASP.NET Core Web API implementing a Cost Management module for an Enterprise Resource Planning (ERP) course assignment. It focuses on managing invoices and payments using in-memory data storage, designed for simplicity and beginner-friendly learning.

## Features
- **Payment Logging**: Log payments for invoices with details like amount and payment method.
- **Payment Receipt Generation**: Generate PDF receipts for payments.
- **Invoice Status Tracking**: Update and track invoice statuses (Paid, Unpaid, Overdue).
- **Payment History Log**: View all payments associated with an invoice.
- **Invoice Summary Report**: Generate reports filtering invoices by client, status, or date range.

## Technologies
- **.NET 8.0**: Backend framework for building the RESTful API.
- **ASP.NET Core**: Provides the Web API structure.
- **Swagger UI**: Interactive API documentation and testing.
- **iText7**: Used for PDF receipt generation.
- **In-Memory Data**: Uses lists for data storage instead of a database.

## Getting Started
1. **Prerequisites**: Install .NET 8.0 SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com).
2. **Clone the Repository**:
   ```bash
   git clone https://github.com/moamenaahmed/Cost-Management-API.git
   cd ERPCostManagement
   ```
3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```
4. **Run the Application**:
   ```bash
   dotnet run
   ```
5. **Access the API**: The API will be hosted at `https://localhost:<port>` (check the console for the exact port).

## Using Swagger UI
Swagger UI provides an interactive interface to explore and test the API endpoints.

1. **Access Swagger UI**:
   - After running the application, open a browser and navigate to `https://localhost:<port>/swagger` (replace `<port>` with the port shown in the console, e.g., 5001).
   - You’ll see a list of all API endpoints with their descriptions.

2. **Test Endpoints**:
   - Expand an endpoint (e.g., `POST /api/costmanagement/payments`).
   - Click “Try it out” to enable editing.
   - Enter the required parameters (e.g., for `POST /api/costmanagement/payments`, provide a JSON body like `{"invoiceId": 1, "amount": 200, "paymentMethod": "Credit"}`).
   - Click “Execute” to send the request.
   - View the response in the Swagger UI, including status codes and response bodies.
   - For `POST /api/costmanagement/receipts`, download the PDF receipt by providing a `paymentId` (e.g., `1`).

3. **Explore All Endpoints**:
   - Use Swagger UI to test other endpoints like retrieving payment history or generating summary reports with query parameters.

## API Endpoints
- `POST /api/costmanagement/payments`: Log a payment.
- `POST /api/costmanagement/receipts`: Generate a PDF payment receipt.
- `PUT /api/costmanagement/invoices/{invoiceId}/status`: Update invoice status.
- `GET /api/costmanagement/invoices/{invoiceId}/payments`: Get payment history.
- `GET /api/costmanagement/reports`: Generate a summary report with optional filters (clientId, status, startDate, endDate).

## Project Structure
- **Controllers/**: API endpoints for cost management functionalities.
- **Models/**: Data models for invoices, payments, and reports.
- **Services/**: Business logic with in-memory data storage.

## Notes
- This project uses dummy data for simplicity, suitable for educational purposes.
- Built for an ERP course assignment, focusing on .NET framework skills.
- For production, consider adding a database and validation.