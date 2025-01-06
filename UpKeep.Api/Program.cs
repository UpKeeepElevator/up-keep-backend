using Microsoft.EntityFrameworkCore;
using UpKeep.Data.Context;
using UpKeepApi.Extensions;
using UpKeepApi.Extensions.Config;
using UpKeepApi.Extensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecifiOrigins = "_MyAllowSpecifiOrigins";

builder.Services.ConfigurarCORS(MyAllowSpecifiOrigins);
builder.Services.ConfigurarWebAPI(builder.Configuration);

//Servicios
builder.Services.ConfigurarServicios(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseCors(MyAllowSpecifiOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();