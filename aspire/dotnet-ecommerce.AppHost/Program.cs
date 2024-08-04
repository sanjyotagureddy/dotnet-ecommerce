var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MeraStore_User_Api>("merastore-user-api");



builder.AddProject<Projects.MeraStore_Product_Api>("merastore-product-api");



builder.Build().Run();
