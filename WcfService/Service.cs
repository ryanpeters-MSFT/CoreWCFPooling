namespace WcfService
{
    public class Service : PooledService<Service>, IService
    { 
        private readonly Guid id = Guid.NewGuid();

        public Service()
        {
            // simulate a long instance construction time
            Thread.Sleep(2000);
        }

        public Guid GetInstanceId() => id;
    }
}
