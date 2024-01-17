//Title:Employee Leave Management System Using WebAPI
//Author:Pavun Kavitha
//Created At:(20/3/2023)
//Updated At:(10/6/2023)
//Reviewd By:Anitha
//Reviewd At:(19/6/2023)
using Microsoft.EntityFrameworkCore;
using ELMSWebAPI.Models;
var builder = WebApplication.CreateBuilder(args);

string? connection=builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
//builder.Services.AddController();
//builder.Services.AddSession();
//builder.Services.AddDbContext<ELMSWebAPIContext>(opt =>
   // opt.UseInMemoryDatabase("ELMS"));
   builder.Services.AddDbContext<ELMSWebAPIContext>(options =>{
    options.UseSqlServer(connection);
});
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
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
