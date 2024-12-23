using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.Json.Serialization;
using ToDo.Domain.Database;
using ToDo.WebAPI.Endpoints;


var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<TodoService>();

var app = builder.Build();


app.UseWebApi();
app.Run();


