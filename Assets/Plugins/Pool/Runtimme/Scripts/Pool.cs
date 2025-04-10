using System.Collections.Generic;

namespace Pool
{
    public class Pool<T> : IPool where T : IPoolObject, new()
    {
        private readonly List<T> _objects = new();

        public Pool(int initialSize = 0)
        {
            for (int i = 0; i < initialSize; i++)
            {
                Create();
            }
        }

        public T Get()
        {
            foreach (var obj in _objects)
            {
                if (obj.IsActive == false)
                {
                    obj.IsActive = true;
                    return obj;
                }
            }
            var newObj = Create();
            newObj.IsActive = true;
            return newObj;
        }

        public void Release(T obj)
        {
            obj.IsActive = false;
        }

        private T Create()
        {
            var obj = new T();
            _objects.Add(obj);
            return obj;
        }
    }
}