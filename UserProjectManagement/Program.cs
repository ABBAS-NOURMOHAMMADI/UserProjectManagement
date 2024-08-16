using Domain.Interfaces;
using Infrastructure.Persistence;
using UserProjectManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddPresentationLayerServices();

builder.Services.AddApiVersioning();


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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.Run();
Console.WriteLine("App is running...");
