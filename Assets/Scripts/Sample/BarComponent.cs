using System;
using System.Collections.Generic;
using System.Linq;
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

        private IEnumerable<IFooComponent> _fooComponents;
        private IContainer _container;
        
        [Inject]
        public void Construct([Nullable] IFooComponent fooComponent, IEnumerable<IFooComponent> fooComponents, IContainer container)
        {
            _fooComponent = fooComponent;
            _fooComponents = fooComponents;
            _container = container;
        }

        private void Start()
        {
            if (_fooComponent != null)
            {
                Debug.Log(_fooComponent.Test());
            }
            
            Debug.Log($"fooComponentsNum: {_fooComponents.Count()}");
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
