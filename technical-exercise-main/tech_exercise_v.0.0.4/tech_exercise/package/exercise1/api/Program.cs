using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Interfaces;
using StargateAPI.Business.Services;
using StargateAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonService, PersonService>();

var connectionString = builder.Configuration.GetConnectionString("StarbaseApiDatabase");
if (builder.Environment.IsDevelopment() && connectionString!.Contains(":memory:"))
{
    // In-memory SQLite is destroyed when its connection closes, so keep one open for the app lifetime.
    var keepAlive = new SqliteConnection(connectionString);
    keepAlive.Open();
    builder.Services.AddSingleton(keepAlive);
    builder.Services.AddDbContext<StargateContext>(options => options.UseSqlite(keepAlive));
}
else
{
    builder.Services.AddDbContext<StargateContext>(options => options.UseSqlite(connectionString));
}

builder.Services.AddMediatR(cfg =>
{
    cfg.AddRequestPreProcessor<CreateAstronautDutyPreProcessor>();
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<StargateContext>();
        db.Database.Migrate();
        StargateSeeder.Seed(db);
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


