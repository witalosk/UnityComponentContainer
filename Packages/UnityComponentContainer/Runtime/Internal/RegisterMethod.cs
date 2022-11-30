using System;
namespace ComponentContainer.Internal
{
    [Flags]
    public enum RegisterMethod
    {
        None = 0,
        AllInterfaces = 1,
        BaseType = 2,
        Self = 4
    }
}
