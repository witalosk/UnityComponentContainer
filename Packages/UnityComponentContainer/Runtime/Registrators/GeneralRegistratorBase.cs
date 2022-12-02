using System;
using ComponentContainer.Internal.InstanceProviders;

namespace ComponentContainer.Registrator
{
    public abstract class GeneralRegistratorBase : RegistratorBase
    {
        /// <summary>
        /// Install the rules
        /// </summary>
        protected abstract void BuildContainer(IContainerBuilder builder);

        protected override void RegisterToContainer()
        {
            var builder = new ContainerBuilder();
            BuildContainer(builder);

            foreach (var data in builder.RegisteredData)
            {
                switch (data.LifeTime)
                {
                    case LifeTime.Singleton:
                        object singletonObj = Activator.CreateInstance(data.ConcreteType);
                        foreach (var registerType in data.TargetTypes)
                        {
                            _targetContainer.RegisterInstance(registerType, singletonObj);
                        }
                        break;
                    
                    case LifeTime.Transient:
                        foreach (var registerType in data.TargetTypes)
                        {
                            _targetContainer.Register(data.ConcreteType, registerType);
                        }
                        break;    
                }
            }
        }
    }
}
