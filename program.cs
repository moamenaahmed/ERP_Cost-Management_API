using ERPCostManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<CostManagementService>();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ERP Cost Management API",
        Version = "v1",
        Description = "API for managing invoices and payments in an ERP system."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP Cost Management API V1");
    c.RoutePrefix = "swagger"; 
});

app.MapControllers();

app.MapGet("/", () => "ERP Cost Management API is running");

app.Run();