using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public class GameObjectRegistrator : RegistratorBase
    {
        [SerializeField]
        private RegisterMethod _registerMethod = RegisterMethod.AllInterfaces;
        
        private void Awake()
        {
            // Register this component to the container
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is Transform or RegistratorBase) continue;
                
                RegisterToTargetContainer(component, _registerMethod);
            }
        }
    }
}
