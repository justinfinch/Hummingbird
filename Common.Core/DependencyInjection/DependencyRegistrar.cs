using System;

namespace Common.Core.DependencyInjection
{
    public class DependencyRegistrar
    {
        private IDependencyRegistrar _registrar;

        public IDependencyRegistrar Current
        {
            get { return _registrar; }
        }

        private static readonly Lazy<DependencyRegistrar> _instance = new Lazy<DependencyRegistrar>(() => new DependencyRegistrar());
        public static DependencyRegistrar Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private DependencyRegistrar()
        {

        }

        public void SetRegistrar(IDependencyRegistrar registrar)
        {
            _registrar = registrar;
        }

        

    }
}
