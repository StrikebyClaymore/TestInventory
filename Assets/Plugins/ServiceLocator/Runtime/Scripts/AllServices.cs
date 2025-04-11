using System.Collections.Generic;
using System.Linq;

namespace ServiceLocator
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Instance => _instance ??= new AllServices();
        private readonly List<IService> _services = new();
        public IReadOnlyCollection<IService> Services => _services;

        public void RegisterService<TService>(TService service) where TService : IService
        {
            _services.Add(service);
        }

        public TService GetService<TService>() where TService : class, IService
        {
            var type = typeof(TService);
            var result = _services.FirstOrDefault(type.IsInstanceOfType);
            if (result is null)
                return null;
            return (TService)result;
        }
    }
}