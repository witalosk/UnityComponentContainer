using ComponentContainer.Container;

namespace ComponentContainer.Registrator
{
    public static class RegistratorExtensions
    {
        /// <summary>
        /// Register a instance as a TInterface
        /// </summary>
        public static void RegisterInstance<TInterface, TConcrete>(this IContainer container, TConcrete obj)
            where TConcrete : TInterface
        {
            container.RegisterInstance(typeof(TInterface), obj);
        }
    }
}
