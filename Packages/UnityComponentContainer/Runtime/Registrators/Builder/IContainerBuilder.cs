namespace ComponentContainer.Registrator
{
    public interface IContainerBuilder
    {
        IRegisterData Register<TConcrete>(LifeTime lifeTime);

        IRegisterData Register<TConcrete, TInterface>(LifeTime lifeTime);
    }
}
