namespace WcfService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Guid GetInstanceId();
    }
}
