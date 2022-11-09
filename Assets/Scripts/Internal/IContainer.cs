using System;
namespace ComponentContainer.Internal
{
    public interface IContainer
    {
        void Register(Type type, object obj);
        
        void Register<T>(T obj);

        object Resolve<T>();
        
        object Resolve(Type type);

        void Inject(object instance);
    }
}
