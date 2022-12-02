using UnityEngine;
namespace ComponentContainer.Sample.BasicSample
{
    public class FooComponent : MonoBehaviour, IFooComponent
    {
        [SerializeField]
        private string _message = "Hello!";

        public string Test()
        {
            return $"Foo: {gameObject.name} says \"{_message}\"";
        }
    }
}
