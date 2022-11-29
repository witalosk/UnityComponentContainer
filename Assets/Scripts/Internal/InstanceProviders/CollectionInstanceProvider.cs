using System;
using System.Collections;
using System.Collections.Generic;

namespace ComponentContainer.Internal.InstanceProviders
{
    /// <summary>
    /// Provide Collection Of Instance
    /// </summary>
    public class CollectionInstanceProvider : IInstanceProvider
    {
        private readonly List<IInstanceProvider> _instanceProviders = new();
        private IList _instanceList;

        public Type ElementType { get; }
        public Type ListType { get; }

        public CollectionInstanceProvider(Type elementType, params IInstanceProvider[] providers)
        {
            ElementType = elementType;
            ListType = typeof(List<>).MakeGenericType(elementType);
            _instanceList = (IList)Activator.CreateInstance(ListType);

            foreach (var provider in providers)
            {
                AddProvider(provider);
            }
        }

        /// <summary>
        /// Register a provider to this collection
        /// </summary>
        public void AddProvider(IInstanceProvider provider)
        {
            if (provider is ExistingInstanceProvider eiProvider)
            {
                _instanceList.Add(eiProvider.GetInstance());
            }
            else
            {
                _instanceList = null;
            }
            
            _instanceProviders.Add(provider);
        }
        
        public object GetInstance()
        {
            if (_instanceList is { })
            {
                return _instanceList;
            }

            int instanceCount = _instanceProviders.Count;
            var array = Array.CreateInstance(ElementType, instanceCount);
            for (int i = 0; i < instanceCount; i++)
            {
                array.SetValue(_instanceProviders[i].GetInstance(), i);
            }
            return array;
        }
    }
}
