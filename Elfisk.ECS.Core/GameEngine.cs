using System.Linq;
using System.Threading.Tasks;
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
      Environment = environment;
    }


    public async Task RunGameLoop()
    {
      Systems = Environment.DependencyContainer.ResolveAll<ISystem>().ToArray();
      EventQueue = Environment.DependencyContainer.Resolve<IGameLoopEventQueue>();

      while (true)
      {
        EventQueue.InvokeEvents(Environment);

        // OBS: Not correct usage of Environment.GameLoopPeriod (should calculate actual spent time)

        foreach (ISystem system in Systems)
          await system.Update(Environment, Environment.GameLoopPeriod);

        await Task.Delay(Environment.GameLoopPeriod);
      }
    }
  }
}
