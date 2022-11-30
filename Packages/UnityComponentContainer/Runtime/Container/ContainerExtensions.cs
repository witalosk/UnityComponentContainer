using ComponentContainer.Internal;
using UnityEngine;

namespace ComponentContainer.Container
{
    public static class ContainerExtensions
    {
        public static T Instantiate<T>(this IContainer container, T prefab) where T : UnityEngine.Object
        {
            var instance = UnityEngine.Object.Instantiate(prefab);
            
            if (instance is GameObject gameObject)
            {
                container.InjectToGameObject(gameObject);
            }
            else
            {
                container.Inject(instance);                
            }
            
            return instance;
        }
        
        public static T Instantiate<T>(this IContainer container, T prefab, Transform parent, bool worldPositionStays = false)
            where T : UnityEngine.Object
        {
            var instance = UnityEngine.Object.Instantiate(prefab, parent, worldPositionStays);

            if (instance is GameObject gameObject)
            {
                container.InjectToGameObject(gameObject);
            }
            else
            {
                container.Inject(instance);                
            }
            
            return instance;
        }

        
        public static void InjectToGameObject(this IContainer container, GameObject gameObject)
        {
            InjectGameObjectRecursive(container, gameObject);
        }
        
        private static void InjectGameObjectRecursive(IContainer container, GameObject current)
        {
            if (current == null) return;
            
            var components =  current.GetComponents<Component>();
            foreach (var component in components)
            {
                container.Inject(component);
            }

            var transform = current.transform;
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                InjectGameObjectRecursive(container, child.gameObject);
            }
        }

    }
}
