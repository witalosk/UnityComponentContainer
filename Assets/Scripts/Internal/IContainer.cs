using System;
namespace ComponentContainer.Internal
{
    public interface IContainer
    {
        void RegisterInstance(Type type, object obj);
        
        void RegisterInstance<T>(T obj);

        object Resolve<T>(bool notNull = false);
        
        object Resolve(Type type, bool nullable = false);

        void Inject(object instance);
    }
}
