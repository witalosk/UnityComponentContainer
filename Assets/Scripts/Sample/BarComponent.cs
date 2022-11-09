using System;
using ComponentContainer.Internal;
using ComponentContainer.Container;
using UnityEngine;

namespace ComponentContainer.Sample
{
    public class BarComponent : MonoBehaviour
    {
        [SerializeField]
        private BarComponent _prefab;

        private float _timer = 0f;
        private bool _isInstantiated = false;
        private IFooComponent _fooComponent;
        private IContainer _container;
        
        [Inject]
        public void Construct(IFooComponent fooComponent, IContainer container)
        {
            _fooComponent = fooComponent;
            _container = container;
        }

        private void Start()
        {
            Debug.Log(_fooComponent.Test());
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (!_isInstantiated && _timer > 5f)
            {
                _container.Instantiate(_prefab);
                _isInstantiated = true;
            }
        }
    }
}
