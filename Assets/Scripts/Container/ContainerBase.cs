using System;
using ComponentContainer.Internal;
using UnityEngine;
namespace ComponentContainer.Container
{
    public abstract class ContainerBase : MonoBehaviour, IContainer
    {
        public abstract void Register(Type type, object obj);
        
        public abstract void Register<T>(T obj);
        
        public abstract object Resolve<T>();
        
        public abstract object Resolve(Type type);
        
        public abstract void Inject(object instance);
    }
}
