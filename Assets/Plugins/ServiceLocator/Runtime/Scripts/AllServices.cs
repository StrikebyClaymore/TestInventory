namespace ServiceLocator
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterService<TService>(TService service) where TService : IService
        {
            Service<TService>.Instance = service;
        }
        
        public TService GetService<TService>() where TService : IService
        {
            return Service<TService>.Instance;
        }
    }
}