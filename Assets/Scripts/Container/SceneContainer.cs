using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Container
{
    [DefaultExecutionOrder(-999)]
    public class SceneContainer : ContainerBase, IContainer
    {
        private enum GameObjectSearchMethod
        {
            Scene = 0,
            SpecifiedGameObjects = 10
        }

        [SerializeField]
        private GameObjectSearchMethod _searchMethod = GameObjectSearchMethod.Scene;

        [SerializeField]
        private List<GameObject> _targetGameObjects = new();

        private readonly Injector _injector = new();

        private readonly ConcurrentDictionary<Type, object> _containerData = new();

        private void Awake()
        {
            // Register Self
            Register(this as IContainer);
            
            // シーンが対象の場合はシーンを検索
            if (_searchMethod == GameObjectSearchMethod.Scene) {
                _targetGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject))
                    .Select(c => c as GameObject)
                    .Where(c => c != null && c.hideFlags != HideFlags.NotEditable && c.hideFlags != HideFlags.HideAndDontSave).ToList();
            }

            foreach (var gameObject in _targetGameObjects) {
                foreach (var component in gameObject.GetComponents<Component>()) {
                    Inject(component);
                }
            }
        }

        public override object Resolve<T>()
        {
            return Resolve(typeof(T));
        }
        
        public override object Resolve(Type type)
        {
            return _containerData.ContainsKey(type) ? _containerData[type] : null;
        }

        public override void Register(Type type, object obj)
        {
            _containerData.TryAdd(type, obj);
        }
        
        public override void Register<T>(T obj)
        {
            _containerData.TryAdd(typeof(T), obj);

            List<T> list = _containerData.ContainsKey(typeof(List<T>)) 
                ? _containerData[typeof(List<T>)] as List<T> 
                : new List<T>();

            list?.Add(obj);
            _containerData[typeof(List<T>)] = list;
            _containerData[typeof(T[])] = list?.ToArray();
            _containerData[typeof(IEnumerable<T>)] = list;
        }
        
        public override void Inject(object instance)
        {
            var targetMethods = TargetSearcher.Search(instance.GetType());
            _injector.Inject(instance, this, targetMethods);
        }
    }

}
