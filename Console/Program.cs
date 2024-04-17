using System.ServiceModel;
using WcfService;

// wait for WCF service to spin-up
Console.WriteLine("Waiting 2 seconds for service to become available...\n");
Thread.Sleep(2000);

var endpoint = new EndpointAddress("http://localhost:5189/service.svc");
var factory = new ChannelFactory<IService>(new BasicHttpBinding(), endpoint);

var channel = factory.CreateChannel();

var requestTasks = new List<Task<Guid>>();

for (var i = 0; i < 5; i++)
{
    var task = channel.GetInstanceIdAsync();

    requestTasks.Add(task);

    Thread.Sleep(1000);
}

await Task.WhenAll(requestTasks);

var ids = requestTasks.Select(t => t.Result).ToList();

foreach (var id in ids)
{
    Console.WriteLine(id);
}

Console.WriteLine($"\nDistinct IDs: {ids.Distinct().Count()}\n");