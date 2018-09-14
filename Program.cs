using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.IO;

namespace SerilogSQLTest
{
    public class Program
    {
        static string connectionString;
        static string logTableName;


        public static void Main(string[] args)
        {

            connectionString = Configuration["Logging:ConnectionString"];
            logTableName = Configuration["Logging:TableName"];
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(Configuration)
            //    .MinimumLevel.ControlledBy(LoggingLevel.Switch)
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //    //.Enrich.WithExceptionDetails()
            //    .Enrich.WithProperty("Application", "SerilogSQLTest")
            //    .Enrich.WithMachineName()
            //    .Enrich.WithEnvironmentUserName()
            //    .Enrich.WithAspnetcoreHttpcontext()
            //    .WriteTo.MSSqlServer(connectionString, logTableName)
            //    .CreateLogger();
            try
            {
                Log.Information("Starting web host");

                CreateWebHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .Build();


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((provider, context, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(Configuration)
                        .MinimumLevel.ControlledBy(LoggingLevel.Switch)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithProperty("Application", "SerilogSQLTest")
                        //.Enrich.WithMachineName()
                        //.Enrich.WithEnvironmentUserName()
                        //.Enrich.WithAspnetcoreHttpcontext(provider)
                        .WriteTo.MSSqlServer(connectionString, logTableName);
                });
    }
}
