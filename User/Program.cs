using ErrandPay_test;
using Microsoft.EntityFrameworkCore;
using UserAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite());
// Add services to the container.
// C:\Users\hidde\source\repos\ErrandPay test\ErrandPay test\Application.db
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton(x => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7055/")
});

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
