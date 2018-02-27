using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Elfisk.ECS.Core;

namespace Elfisk.ECS.Core
{
  public class GameEngine
  {
    protected GameEnvironment Environment { get; private set; }

    protected ISystem[] Systems { get; set; }

    protected IGameLoopEventQueue EventQueue { get; set; }


    public GameEngine(GameEnvironment environment)
    {
      Condition.Requires(environment, nameof(environment)).IsNotNull();

      Environment = environment;
    }


    public async Task RunGameLoop()
    {
      Systems = Environment.DependencyContainer.ResolveAll<ISystem>().ToArray();
      EventQueue = Environment.DependencyContainer.Resolve<IGameLoopEventQueue>();

      while (true)
      {
        EventQueue.InvokeEvents(Environment);

        foreach (ISystem system in Systems)
          await system.Update(Environment);

        await Task.Delay(Environment.GameLoopPeriod);
      }
    }
  }
}
