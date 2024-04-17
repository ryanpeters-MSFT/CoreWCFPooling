using Microsoft.Extensions.ObjectPool;

namespace WcfService
{
    public class WcfPoolPolicy(ObjectPool<Service> servicePool) : PooledObjectPolicy<Service>
    {
        public override Service Create()
        {
            return new Service();
        }

        public override bool Return(Service obj)
        {
            if (obj is IResettable resettable)
            {
                return resettable.TryReset();
            }

            return true;
        }
    }
}
