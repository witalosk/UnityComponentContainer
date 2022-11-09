using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Container
{
    public abstract class ContainerBase : MonoBehaviour, IContainer
    {
        private readonly Injector _injector = new();

        private readonly ConcurrentDictionary<Type, List<object>> _containerData = new();

        public object Resolve<T>()
        {
            return Resolve(typeof(T));
        }
        
        public object Resolve(Type type)
        {
            Debug.Log($"{type} is requested!");
            if (type.IsConstructedGenericType) {
                Debug.Log($"IsConstructedGenericType: {type.IsConstructedGenericType}, GetGenericTypeDefinition: {type.GetGenericTypeDefinition()},  GenericTypeArguments[0]: {type.GenericTypeArguments[0]}");
            }

            if (type.IsConstructedGenericType 
                && (type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                // if List<T> or IEnummerable<T> is requested
                if (_containerData.ContainsKey(type.GenericTypeArguments[0]) && _containerData[type.GenericTypeArguments[0]].Count > 0)
                {
                    // Todo: List<object> を List<T>に詰め込む
                    return Convert.ChangeType(_containerData[type.GenericTypeArguments[0]], type);
                }
                else if (_containerData.ContainsKey(type) && _containerData[type].Count > 0)
                {
                    return _containerData[type][0];
                }
            }
            else
            {
                // for not enummerable type
                if (_containerData.ContainsKey(type) && _containerData[type].Count > 0)
                {
                    return _containerData[type][0];
                }
            }

            return GetDefault(type);
        }

        public void RegisterInstance(Type type, object obj)
        {
            if (_containerData.ContainsKey(type)) {
                _containerData[type].Add(obj);
            }
            else {
                _containerData.TryAdd(type, new List<object>(){ obj });
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
