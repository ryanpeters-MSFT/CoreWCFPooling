using Microsoft.Extensions.ObjectPool;

namespace WcfService
{
    public class Service : IService, IDisposable
    { 
        private readonly Guid id = Guid.NewGuid();
        //public ObjectPool<Service> ServicePool { get; set; }

        public Service(/*ObjectPool<Service> servicePool*/)
        {
            //this.ServicePool = servicePool;

            // simulate a long instance construction time
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            //ServicePool.Return(this);
        }

        public Guid GetInstanceId()
        {
            //Thread.Sleep(10000);

            return id;
        }
    }
}
