using System;
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

        private void Awake()
        {
            // Register Self
            RegisterInstance(this as IContainer);
            
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
    }
    
}
