using Microsoft.Extensions.ObjectPool;

namespace WcfService
{
    public abstract class PooledService<T> : IDisposable where T : class
    {
        private ObjectPool<T> pool;

        public ObjectPool<T> Pool
        {
            set { pool = value; }
        }

        public void Dispose()
        {
            if (pool is null)
            {
                throw new InvalidOperationException($"Pool must be set when using {nameof(PooledService<T>)}");
            }

            if (this is T instance)
            {
                pool.Return(instance);
            }
        }
    }
}
