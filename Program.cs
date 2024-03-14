using GlobalExceptionHandling.Data;
using GlobalExceptionHandling.Middleware;
using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using NLog.Extensions.Logging;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog();
});
builder.Services.AddScoped<IProductService,ProductServicecs>();
builder.Services.AddDbContext<DbContextClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GlobalExcept")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseHttpsRedirection();
logger.Info("Started");
app.UseAuthorization();

app.MapControllers();

app.Run();
