using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SuperMarket.Models;
//using Pomelo.EntityFrameworkCore.MySql;




var builder = WebApplication.CreateBuilder(args);
//add services here
builder.Services.AddAuthorization(Options =>
{
    Options.AddPolicy("AdminOnly", policy =>
    policy.RequireRole("Admin"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SMDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 26))));


builder.Services.AddControllers();

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
