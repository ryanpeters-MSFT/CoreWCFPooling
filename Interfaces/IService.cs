using System.ServiceModel;

namespace Interfaces
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Guid GetInstanceId();
    }
}
