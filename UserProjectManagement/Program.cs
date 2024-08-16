using Infrastructure.Persistence;
using Infrastructure.ServiceConfiguration;
using Microsoft.OpenApi.Models;
using Serilog;
using UserProjectManagement;
using UserProjectManagement.Configuration;

static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
               .UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration))
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  var env = hostingContext.HostingEnvironment;
                  config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                  config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                  config.AddEnvironmentVariables();
                  config.AddDatabaseConfiguration();
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                  logging.AddDebug();
              })
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.ConfigureServices((context, services) =>
                  {
                      services.AddLogging();
                      services.AddControllers();

                      //customize service
                      services.AddPresentationLayerServices();
                      services.AddInfrastructureServices(context.Configuration);

                      //services.AddApiVersioning();
                      services.AddEndpointsApiExplorer();
                      services.AddSwaggerGen(c =>
                      {
                          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                          {
                              Name = "Authorization",
                              Type = SecuritySchemeType.Http,
                              Scheme = "Bearer",
                              BearerFormat = "JWT",
                              In = ParameterLocation.Header,
                              Description = "enter jwt token"

                          });

                          c.AddSecurityRequirement(new OpenApiSecurityRequirement
                          {
                              {
                                  new OpenApiSecurityScheme
                                  {
                                  Reference=new OpenApiReference
                                  {
                                      Type=ReferenceType.SecurityScheme,
                                      Id="Bearer"
                                  }
                                  },
                                  new string[] {}
                              }
                          });
                      });

                      services.AddCors(options =>
                      {
                          options.AddPolicy("EnableCORS", builder =>
                          {
                              builder.WithOrigins("http://localhost:30067")
                              .AllowAnyHeader().AllowAnyMethod()
                              .AllowCredentials()
                              .Build();
                          });
                      });
                  });
                  webBuilder.Configure((context, app) =>
                  {
                      app.UseCors("EnableCORS");
                      if (context.HostingEnvironment.IsDevelopment())
                      {
                          app.UseDeveloperExceptionPage();
                          app.UseSwagger();
                          app.UseSwaggerUI();
                      }
                      else
                      {
                          app.UseHsts();
                      }

                      var basePath = context.Configuration.GetValue<string>("BasePath", "");
                      app.UsePathBase(basePath);
                      app.UseRouting();

                      app.UseAuthentication();
                      app.UseAuthorization();

                      app.UseEndpoints(endpoints =>
                      {
                          endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
                      });

                  });
              });

var builder = CreateHostBuilder(args);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var env = scope.ServiceProvider.GetService<IHostEnvironment>();
        new ApplicationDbContextFactory(env).Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.Run();
Console.WriteLine("App is running...");

public partial class Program { }
