using ComponentContainer.Registrator;
using UnityEngine;

namespace ComponentContainer.Samples.GeneralClassSample
{
    public class MySampleRegistrator : GeneralRegistratorBase
    {
        [SerializeField]
        private LifeTime _registerLifetimeMethod = LifeTime.Singleton;
        
        /// <summary>
        /// Write your rules to generate instance
        /// </summary>
        protected override void BuildContainer(IContainerBuilder builder)
        {
            builder.Register<AccumulationLogger>(_registerLifetimeMethod).As<ILogger>();
        }
    }
}
