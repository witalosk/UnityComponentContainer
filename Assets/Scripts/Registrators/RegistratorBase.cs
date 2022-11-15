using System;
using System.Collections.Generic;
using ComponentContainer.Internal;
using ComponentContainer.Container;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public abstract class RegistratorBase : MonoBehaviour
    {
        [SerializeField]
        protected ContainerBase _targetContainer;

        protected void RegisterToTargetContainer(Component component, RegisterMethod method)
        {
            var componentType = component.GetType();
            var targetTypes = new List<Type>();

            if (method.HasFlag(RegisterMethod.AllInterfaces)) {
                targetTypes.AddRange(componentType.GetInterfaces());
            }
            if (method.HasFlag(RegisterMethod.BaseType)) {
                targetTypes.AddRange(GetBaseTypes(componentType));
            }
            if (method.HasFlag(RegisterMethod.Self)) {
                targetTypes.Add(componentType);
            }

            foreach (var type in targetTypes) {
                _targetContainer.RegisterInstance(type, component);
            }
        }
        
        private static IEnumerable<Type> GetBaseTypes(Type self)
        {
            for (var baseType = self.BaseType; null != baseType; baseType = baseType.BaseType) {
                yield return baseType;
            }
        }
        
    }
}
