using System.Text.Json;
using System.Text.Json.Serialization;

using MeraStore.Shared.Common.Logging;
using MeraStore.Shared.Common.WebApi;
using MeraStore.User.Shared.Common;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Host.UseLogging(Constants.ServiceIdentifiers.User);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
  options.JsonSerializerOptions.WriteIndented = true;
  options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCommonMiddlewares();
app.MapControllers();
app.Run();
