using System;
namespace ComponentContainer.Registrator
{
    public interface IRegisterData
    {
        /// <summary>
        /// Register as type
        /// </summary>
        IRegisterData As(Type type);

        /// <summary>
        /// Register as T
        /// </summary>
        IRegisterData As<T>();

        /// <summary>
        /// Register as concrete type
        /// </summary>
        IRegisterData AsSelf();

        /// <summary>
        /// Register as base types
        /// </summary>
        IRegisterData AsBaseTypes();

        /// <summary>
        /// Register as all Implemented interfaces
        /// </summary>
        IRegisterData AsAllInterfaces();
    }
}
