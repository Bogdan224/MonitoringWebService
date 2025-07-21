using Serilog;
using MonitoringApi.Services;

namespace MonitoringApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<StorageService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Angular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200",
                                       "https://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging(options =>
                {
                    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                    {
                        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                        diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent);
                    };
                });

            app.UseCors("Angular");

            app.MapControllers();

            app.Run();

            
        }
    }
}
