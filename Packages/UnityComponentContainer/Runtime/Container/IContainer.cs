using System;

namespace ComponentContainer.Container
{
    public interface IContainer
    {
        void Register(Type concreteType, Type registerType);

        void Register<TConcrete, TRegister>();
        
        void RegisterInstance(Type registerType, object obj);
        
        void RegisterInstance<TRegister>(TRegister obj);
        
        object Resolve<T>(bool notNull = false);
        
        object Resolve(Type type, bool nullable = false);

        void Inject(object instance);
    }
}
