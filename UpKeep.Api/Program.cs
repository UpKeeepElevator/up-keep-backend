using Microsoft.EntityFrameworkCore;
using UpKeep.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<UpKeepDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("upKeep") ?? ""));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();
