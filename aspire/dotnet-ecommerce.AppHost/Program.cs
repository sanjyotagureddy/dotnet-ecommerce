var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BelleZone_User_Api>("bellezone-user-api");



builder.Build().Run();
