using System.Collections.Generic;
using Castle.Windsor;
using CuttingEdge.Conditions;
using Elfisk.Commons;

namespace Elfisk.ECS.Service
{
  public class CastleDependencyContainer : IDependencyContainer
  {
    protected IWindsorContainer Container { get; set; }


    public CastleDependencyContainer(IWindsorContainer container)
    {
      Condition.Requires(container, nameof(container)).IsNotNull();
      Container = container;
    }


    public T Resolve<T>()
    {
      return Container.Resolve<T>();
    }


    public IEnumerable<T> ResolveAll<T>()
    {
      return Container.ResolveAll<T>();
    }
  }
}
