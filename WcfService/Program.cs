using Microsoft.Extensions.ObjectPool;
using System.Diagnostics;

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
    var stopwatch = new Stopwatch();

    var pool = serviceProvider.GetService<ObjectPool<Service>>();

    stopwatch.Start();

    var instance = pool.Get();

    instance.Pool = pool;

    stopwatch.Stop();

    Console.WriteLine($"RETRIEVED instance => \tTime: {stopwatch.ElapsedMilliseconds}ms\tID: {instance.GetInstanceId()}");

    return instance;
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
