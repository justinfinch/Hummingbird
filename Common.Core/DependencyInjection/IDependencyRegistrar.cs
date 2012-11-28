namespace Common.Core.DependencyInjection
{
    public interface IDependencyRegistrar
    {
        void Register<TDependency, T>()
            where T : TDependency;
    }
}
