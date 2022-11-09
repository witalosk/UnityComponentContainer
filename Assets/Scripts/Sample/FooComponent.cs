using UnityEngine;
namespace ComponentContainer.Sample
{
    public class FooComponent : MonoBehaviour, IFooComponent
    {
        public string Test()
        {
            return $"Foo: {gameObject.name}";
        }
    }
}
