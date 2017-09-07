using System.Collections.Generic;

namespace Elfisk.ECS.Core.Implementation
{
  public class GameLoopEventQueue : IGameLoopEventQueue
  {
    public Queue<IGameLoopEvent> QueuedActions { get; set; }


    public GameLoopEventQueue()
    {
      QueuedActions = new Queue<IGameLoopEvent>();
    }


    public void Enqueue(IGameLoopEvent e)
    {
      QueuedActions.Enqueue(e);
    }


    public void InvokeEvents(GameEnvironment environment)
    {
      while (QueuedActions.Count > 0)
        QueuedActions.Dequeue().Invoke(environment);
    }
  }
}
