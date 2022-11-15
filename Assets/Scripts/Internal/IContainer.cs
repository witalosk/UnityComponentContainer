using System;
namespace ComponentContainer.Internal
{
    public interface IContainer
    {
        void RegisterInstance(Type type, object obj);
        
        void RegisterInstance<T>(T obj);

        object Resolve<T>(bool notNull = false);
        
        object Resolve(Type type, bool notNull = false);

        void Inject(object instance);
    }
}
