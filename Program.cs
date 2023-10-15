using IdProvider.DataAccess;
using IdProvider.Interfaces;
using IdProvider.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
builder.Services.AddDbContext<DataContext>(opt =>
                            opt.UseSqlite(configuration.GetConnectionString("WebApiDatabase")));

//My services
builder.Services.AddScoped<IIdsService, IdsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();    
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();