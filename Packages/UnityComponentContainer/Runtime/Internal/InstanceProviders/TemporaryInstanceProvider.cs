using System;
using ComponentContainer.Container;
namespace ComponentContainer.Internal.InstanceProviders
{
    public class TemporaryInstanceProvider : IInstanceProvider
    {
        private readonly Type _concreteType;
        private readonly IContainer _container;

        public TemporaryInstanceProvider(Type concreteType, IContainer container)
        {
            _concreteType = concreteType;
            _container = container;
        }

        public object GetInstance()
        {
            object instance = Activator.CreateInstance(_concreteType);
            _container.Inject(instance);
            
            return instance;
        }
    }
}
