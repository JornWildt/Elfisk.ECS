using System;
using Elfisk.Commons;

namespace Elfisk.ECS.Core
{
  public class GameEnvironment
  {
    public IDependencyContainer DependencyContainer { get; protected set; }

    public IEntityRepository Entities { get; protected set; }

    public ISystem[] Systems { get; protected set; }

    public TimeSpan GameLoopPeriod { get; protected set; }


    public GameEnvironment(IDependencyContainer dependencies, TimeSpan gameLoopPeriod)
    {
      DependencyContainer = dependencies;
      Entities = dependencies.Resolve<IEntityRepository>();
      Systems = dependencies.ResolveAll<ISystem>();
      GameLoopPeriod = gameLoopPeriod;
    }
  }
}
