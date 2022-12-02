using System;
using ComponentContainer.Container;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public abstract class RegistratorBase : MonoBehaviour
    {
        [SerializeField]
        protected bool _isEnable = true;
        
        [SerializeField]
        protected ContainerBase _targetContainer;

        protected abstract void RegisterToContainer();
        
        private void Awake()
        {
            if (!_isEnable) return;
            
            RegisterToContainer();
        }
    }
}
