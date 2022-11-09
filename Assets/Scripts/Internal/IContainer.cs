using System;
namespace ComponentContainer.Internal
{
    public interface IContainer
    {
        void RegisterInstance(Type type, object obj);
        
        void RegisterInstance<T>(T obj);

        object Resolve<T>();
        
        object Resolve(Type type);

        void Inject(object instance);
    }
}
