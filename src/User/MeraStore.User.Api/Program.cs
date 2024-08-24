using MeraStore.Shared.Common.Http;
using MeraStore.Shared.Common.Logging;
using MeraStore.Shared.Common.WebApi;
using MeraStore.User.Shared.Common;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Host.UseLogging(Constants.ApplicationNames.User);

// Add services to the container.

builder.Services.AddDefaultServices(builder.Configuration);
builder.Services.AddApiCallerServices(builder.Configuration);


var app = builder.Build();

app.MapDefaultEndpoints();

app.UseResponseCompression();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/heath");
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCommonMiddlewares();
app.MapControllers();
app.Run();
