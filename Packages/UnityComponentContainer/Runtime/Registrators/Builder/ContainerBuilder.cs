using System.Collections.Generic;
using ComponentContainer.Internal.InstanceProviders;
namespace ComponentContainer.Registrator
{
    public class ContainerBuilder : IContainerBuilder
    {
        public List<RegisterData> RegisteredData { get; } = new();

        public IRegisterData Register<TConcrete>(LifeTime lifeTime)
        {
            var registerData = new RegisterData(lifeTime, typeof(TConcrete));
            RegisteredData.Add(registerData);
            
            return registerData;
        }
        
        public IRegisterData Register<TConcrete, TInterface>(LifeTime lifeTime)
        {
            var registerData = new RegisterData(lifeTime, typeof(TConcrete));
            RegisteredData.Add(registerData);
            
            return registerData.As<TInterface>();
        }
    }

    public enum LifeTime
    {
        Transient = 0,
        Singleton = 10
    }
}
