using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.OpenApi.Models;
using Rainfall.Contracts.Interface;
using Rainfall.ReferenceAPI;
using Rainfall.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo { Title ="Rainfall API", Version = "1.0.0" })
);

builder.Services.AddHttpClient();
//creates a named httpclient for the api
builder.Services.AddHttpClient("rainfallapi", client =>
{
    client.BaseAddress = new Uri(UriHelper.Encode(new Uri(builder.Configuration.GetSection("RainfallSetting:url").Value!)));
});

//declaring DI 
builder.Services.AddScoped<IRainfallApiLib, RainfallApiLib>();
builder.Services.AddScoped<IRainfallServices, RainfallServices>();

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
