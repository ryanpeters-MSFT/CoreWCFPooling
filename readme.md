# CoreWCF Pooling Sample

This simple example demonstrates a client and server implementation of WCF services using [CoreWCF](https://github.com/CoreWCF/CoreWCF). This example includes Basic HTTP (`BasicHttpBinding`) and leverages the `ObjectPool<T>` functionality to pool service instances. 

In this example, the client/console application merely makes several concurrent requests to the endopint to simulate a high load and outputs the IDs of the service instance to demonstrate the reuse of the WCF service instances. The constructor of the `Service` WCF class contains `Thread.Sleep(2000)` to simulate load in creating the instance. The client/console makes an asynchronous request every second. 

- **Client** - Console/client application
- **Interfaces** - Common object types and service interfaces
- **Server** - WCF service supporting Basic binding of endpoints and using `ObjectPool<T>` to generate and return instances of the WCF service.

## CoreWCF Packages Used

- **CoreWCF.Primitives** - Common/shared types
- **CoreWCF.Http** - Supports HTTP bindings

## Running the Demo

In VS Code or Visual Studio, run both the Console and WcfService projects. The console application will make several concurrent requests to the WCF service and output the IDs from the service instance using the `.GetInstanceId()` RPC call.

## Links

- [CoreWCF Samples](https://github.com/CoreWCF/samples/tree/main)