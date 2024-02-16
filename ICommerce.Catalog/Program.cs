using ICommerce.Catalog.Middleware;
using ICommerce.Catalog.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();
Log.Information("Starting web application");
try
{
    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Host.UseSerilog();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IProductService, ProductService>();
    }


    var app = builder.Build();
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseExceptionHandingMiddleware();
        app.Run();
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}




