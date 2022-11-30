using System;
using System.Collections.Generic;
using System.Linq;
using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public class ComponentsRegistrator : RegistratorBase
    {
        [SerializeField]
        private List<ComponentData> _componentData = new();

        private void Awake()
        {
            // Register this component to the container
            foreach (var data in _componentData.Where(d => d.TargetComponent != null)) {
                RegisterToTargetContainer(data.TargetComponent, data.Method);
            }
        }

        [Serializable]
        private class ComponentData
        {
            public Component TargetComponent;
            public RegisterMethod Method = RegisterMethod.AllInterfaces;
        }
    }
}
