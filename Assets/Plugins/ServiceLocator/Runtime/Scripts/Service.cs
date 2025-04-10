namespace ServiceLocator
{
    public class Service<TService> where TService : IService
    {
        public static TService Instance;
    }
}