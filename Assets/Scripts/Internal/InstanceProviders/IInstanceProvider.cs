namespace ComponentContainer.Internal.InstanceProviders
{
    /// <summary>
    /// Provide a instance by registered method
    /// </summary>
    public interface IInstanceProvider
    {
        object GetInstance();
    }
}
