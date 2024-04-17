using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

builder.Services.AddSingleton<ObjectPool<Service>>(serviceProvider =>
{
    var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();

    var policy = new DefaultPooledObjectPolicy<Service>();

    return provider.Create(policy);
});

builder.Services.AddTransient<Service>(serviceProvider =>
{
    var pool = serviceProvider.GetService<ObjectPool<Service>>();

    var instance = pool.Get();

    //instance.ServicePool = pool;

    try
    {
        return instance;
    }
    finally
    {
        pool.Return(instance);
    }
    
});

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

// Applies additional service behaviors
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>(); // enables retrieval of metadata address info from request headers

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<Service>();

    serviceBuilder.AddServiceEndpoint<Service, IService>(new BasicHttpBinding(), "/Service.svc");

    // Added via Services.AddServiceModelMetadata();
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();

    // Configure WSDL to be available over http
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();
