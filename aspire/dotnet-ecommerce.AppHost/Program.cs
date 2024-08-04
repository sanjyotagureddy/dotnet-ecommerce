var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BelleZone_User_Api>("bellezone-user-api");



builder.AddProject<Projects.BelleZone_Product_Api>("bellezone-product-api");



builder.Build().Run();
