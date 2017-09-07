using CuttingEdge.Conditions;
using Elfisk.Commons;

namespace Elfisk.ECS.Core
{
  public class GameEnvironment
  {
    public IDependencyContainer DependencyContainer { get; protected set; }


    public GameEnvironment(IDependencyContainer dependencies)
    {
      Condition.Requires(dependencies, nameof(dependencies)).IsNotNull();

      DependencyContainer = dependencies;
    }
  }
}
