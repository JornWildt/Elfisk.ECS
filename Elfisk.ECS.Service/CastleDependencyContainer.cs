using System.Collections.Generic;
using Castle.Windsor;
using CuttingEdge.Conditions;
using Elfisk.Commons;
using Reg = Castle.MicroKernel.Registration;


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


    public void Register<T1, T2>() 
      where T2 : T1
      where T1 : class
    {
      Container.Register(Reg.Component.For<T1>().ImplementedBy<T2>());
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
