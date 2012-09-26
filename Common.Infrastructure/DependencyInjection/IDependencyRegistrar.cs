using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Infrastructure.DependencyInjection
{
    public interface IDependencyRegistrar
    {
        void Register<TDependency, T>()
            where T : TDependency;
    }
}
