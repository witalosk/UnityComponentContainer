using System;
using System.Collections.Generic;
using ComponentContainer.Internal;

namespace ComponentContainer.Registrator
{
    public abstract class InstanceRegistratorBase : RegistratorBase
    {
        protected void RegisterInstanceToTargetContainer(object obj, RegisterMethod method)
        {
            var componentType = obj.GetType();
            var targetTypes = new List<Type>();

            if (method.HasFlag(RegisterMethod.AllInterfaces))
            {
                targetTypes.AddRange(componentType.GetInterfaces());
            }
            if (method.HasFlag(RegisterMethod.BaseType))
            {
                targetTypes.AddRange(componentType.GetBaseTypes());
            }
            if (method.HasFlag(RegisterMethod.Self))
            {
                targetTypes.Add(componentType);
            }

            foreach (var type in targetTypes)
            {
                _targetContainer.RegisterInstance(type, obj);
            }
        }
        
    }
}
