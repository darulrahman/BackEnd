using krediku_be.Data;
using krediku_be.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Config untuk datacontext Entityframework
builder.Services.AddDbContext<KredikuContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("KredikuConnectionString"));
});

//Config Interface Repositories
builder.Services.AddScoped<ILocationRepo, LocationRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
