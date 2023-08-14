using Infrastructure.Automapper;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//connection to database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(conf => conf.UseNpgsql(connection));

builder.Services.AddScoped<IQuoteService, QuoteService>();

//automapper
builder.Services.AddAutoMapper(typeof(ServiceProfile));



builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//auto update database
try
{
    var datacontext = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    await datacontext.Database.MigrateAsync();
}
catch (Exception e)
{
    // ignored
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
