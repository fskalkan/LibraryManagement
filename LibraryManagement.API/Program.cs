using Hangfire;
using Hangfire.SqlServer;
using LibraryManagement.Api.ExceptionHandlers;
using LibraryManagement.Application;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.BackgroundJobs;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new SqlServerStorageOptions());
});

builder.Services.AddHangfireServer();

// Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<LoanOverdueJob>(
    recurringJobId: "mark-overdue-loans",
    methodCall: job => job.ExecuteAsync(),
    cronExpression: Cron.Daily);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();