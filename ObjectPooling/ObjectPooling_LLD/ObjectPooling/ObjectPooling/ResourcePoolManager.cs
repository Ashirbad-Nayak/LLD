using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPooling
{
    public class ResourcePoolManager
    {
        int _maxPoolSize;
        List<Resource> _resourcePool = new List<Resource>();
        List<Resource> _inUseResourcePool = new List<Resource>();
        private static readonly Object _lock = new Object();
        private static ResourcePoolManager _instance;
        private ResourcePoolManager(int _initialPoolSize) { 
            for(int i= 0; i < _initialPoolSize; i++)
            {
                Resource resource = new Resource();
                _resourcePool.Add(resource);
            }
        }
        public static ResourcePoolManager GetInstance(int _initialPoolSize)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ResourcePoolManager(_initialPoolSize);
                    }
                }
            }
            return _instance;
        }
        public Resource GetResource()
        {
            lock (_lock)
            {
                if (_resourcePool.Count > 0)
                {
                    Resource resource = _resourcePool[0];
                    _resourcePool.RemoveAt(0);
                    _inUseResourcePool.Add(resource);
                    Console.WriteLine("Resource allocated");
                    return resource;
                }
                else
                {
                    Console.WriteLine("No resources available");
                    return null;
                }
            }
        }
        public void ReleaseResource(Resource resource)
        {
            lock (_lock)
            {
                if (_inUseResourcePool.Contains(resource))
                {
                    _inUseResourcePool.Remove(resource);
                    _resourcePool.Add(resource);
                    Console.WriteLine("Resource released");
                }
                else
                {
                    Console.WriteLine("No resources available");
                }
            }
        }


    }
}
