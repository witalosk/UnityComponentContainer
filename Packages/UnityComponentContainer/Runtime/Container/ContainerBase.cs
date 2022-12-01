using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using ComponentContainer.Internal;
using ComponentContainer.Internal.InstanceProviders;
using UnityEngine;

namespace ComponentContainer.Container
{
    public abstract class ContainerBase : MonoBehaviour, IContainer
    {
        private readonly Injector _injector = new();

        private readonly ConcurrentDictionary<Type, IInstanceProvider> _containerData = new();

        public object Resolve<T>(bool notNull = false)
        {
            return Resolve(typeof(T));
        }
        
        public object Resolve(Type type, bool nullable = false)
        {
            if (_containerData.ContainsKey(type))
            {
                return _containerData[type].GetInstance();
            }

            if (!nullable)
            {
                throw new ArgumentNullException($"\"{type}\" was requested, but the corresponding type is not registered in the container.");
            }

            return GetDefault(type);
        }

        public void RegisterInstance(Type type, object obj)
        {
            RegisterProvider(type, new ExistingInstanceProvider(obj));
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

        private void RegisterProvider(Type type, IInstanceProvider provider)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
               throw new InvalidOperationException($"The listed object cannot be registered. Wrap it in an appropriate class and register it. (Type: {type}) {provider.GetInstance()}");
            }
            
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(type);
            if (_containerData.ContainsKey(type))
            {
                _containerData[type] = provider;
                ((CollectionInstanceProvider)_containerData[collectionType]).AddProvider(provider);
            }
            else {
                _containerData.TryAdd(type, provider);
                _containerData.TryAdd(collectionType, new CollectionInstanceProvider(type, provider));
            }
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
