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

        public void Register(Type concreteType, Type registerType)
        {
            RegisterProvider(registerType, new TemporaryInstanceProvider(concreteType, this));
        }
        
        public void Register<TConcrete, TRegister>()
        {
            Register(typeof(TConcrete), typeof(TRegister));
        }
        
        public void RegisterInstance(Type registerType, object obj)
        {
            RegisterProvider(registerType, new ExistingInstanceProvider(obj));
        }

        public void RegisterInstance<TRegister>(TRegister obj)
        {
            RegisterInstance(typeof(TRegister), obj);
        }
        
        public void Inject(object instance)
        {
            Injector.Inject(instance, this, TargetSearcher.Search(instance.GetType()));
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
