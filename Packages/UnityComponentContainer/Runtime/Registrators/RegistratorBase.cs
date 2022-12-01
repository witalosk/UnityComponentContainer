using ComponentContainer.Container;
using UnityEngine;

namespace ComponentContainer.Registrator
{
    [DefaultExecutionOrder(-9999)]
    public abstract class RegistratorBase : MonoBehaviour
    {
        [SerializeField]
        protected ContainerBase _targetContainer;
    }
}
