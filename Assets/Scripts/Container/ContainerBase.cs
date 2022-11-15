using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using ComponentContainer.Internal;
using ComponentContainer.Internal.InstanceProviders;
using UnityEngine;

namespace ComponentContainer.Container
{
    public abstract class ContainerBase : MonoBehaviour, IContainer
    {
        private readonly Injector _injector = new();

        private readonly ConcurrentDictionary<Type, List<IInstanceProvider>> _containerData = new();

        public object Resolve<T>()
        {
            return Resolve(typeof(T));
        }
        
        public object Resolve(Type type)
        {
            if (type.IsConstructedGenericType 
                && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                // if IEnumerable<T> is requested
                if (_containerData.ContainsKey(type.GenericTypeArguments[0]) && _containerData[type.GenericTypeArguments[0]].Count > 0)
                {
                    int instanceCount = _containerData[type.GenericTypeArguments[0]].Count;
                    var array = Array.CreateInstance(type.GenericTypeArguments[0], instanceCount);
                    for (int i = 0; i < instanceCount; i++)
                    {
                        array.SetValue(_containerData[type.GenericTypeArguments[0]][i].GetInstance(), i);
                    }
                    return array;
                }
                if (_containerData.ContainsKey(type) && _containerData[type].Count > 0)
                {
                    return _containerData[type][0].GetInstance();
                }
            }
            else
            {
                // for not enumerable type
                if (_containerData.ContainsKey(type) && _containerData[type].Count > 0)
                {
                    return _containerData[type][0].GetInstance();
                }
            }

            return GetDefault(type);
        }

        public void RegisterInstance(Type type, object obj)
        {
            if (_containerData.ContainsKey(type)) {
                _containerData[type].Add(new ExistingInstanceProvider(obj));
            }
            else {
                _containerData.TryAdd(type, new List<IInstanceProvider>(){ new ExistingInstanceProvider(obj) });
            }
        }
        
        public void RegisterInstance<T>(T obj)
        {
            RegisterInstance(typeof(T), obj);
        }
        
        public void Inject(object instance)
        {
            var targetMethods = TargetSearcher.Search(instance.GetType());
            _injector.Inject(instance, this, targetMethods);
        }

        private static object GetDefault(Type type)
        {
            if(type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
