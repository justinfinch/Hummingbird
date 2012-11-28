using Ninject;

namespace Common.Core.DependencyInjection.Ninject
{
    public class NinjectDependencyRegistrar : IDependencyRegistrar
    {
        IKernel _kernel;

        public NinjectDependencyRegistrar(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Register<TDependency, T>()
            where T : TDependency
        {
            _kernel.Bind<TDependency>().To<T>();
        }
    }
}
