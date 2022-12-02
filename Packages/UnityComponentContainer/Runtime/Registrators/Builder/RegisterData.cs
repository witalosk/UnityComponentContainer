using System;
using System.Collections.Generic;
using ComponentContainer.Internal;
namespace ComponentContainer.Registrator
{
    public class RegisterData : IRegisterData
    {
        public LifeTime LifeTime { get; }
        public Type ConcreteType { get; }
        public List<Type> TargetTypes { get; } = new();

        public RegisterData(LifeTime lifeTime, Type concreteType)
        {
            LifeTime = lifeTime;
            ConcreteType = concreteType;
        }

        public IRegisterData As(Type type)
        {
            AddTargetType(type);
            return this;
        }

        public IRegisterData As<T>()
        {
            return As(typeof(T));
        }
        
        public IRegisterData AsSelf()
        {
            AddTargetType(ConcreteType);
            return this;
        }

        public IRegisterData AsBaseTypes()
        {
            foreach (var type in ConcreteType.GetBaseTypes())
            {
                AddTargetType(type);
            }

            return this;
        }

        public IRegisterData AsAllInterfaces()
        {
            foreach (var type in ConcreteType.GetInterfaces())
            {
                AddTargetType(type);
            }

            return this;
        }

        private void AddTargetType(Type targetType)
        {
            if (!targetType.IsAssignableFrom(ConcreteType))
            {
                throw new InvalidCastException($"{targetType} is not assignable from {ConcreteType}");
            }

            if (TargetTypes.Contains(targetType)) return;
            
            TargetTypes.Add(targetType);
        }
    }
}
