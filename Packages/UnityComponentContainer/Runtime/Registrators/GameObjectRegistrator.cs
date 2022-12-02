using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public class GameObjectRegistrator : InstanceRegistratorBase
    {
        [SerializeField]
        private RegisterMethod _registerMethod = RegisterMethod.AllInterfaces;
        
        protected override void RegisterToContainer()
        {
            // Register this component to the container
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is Transform or InstanceRegistratorBase) continue;
                
                RegisterInstanceToTargetContainer(component, _registerMethod);
            }
        }
    }
}
