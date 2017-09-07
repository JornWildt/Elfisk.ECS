namespace Elfisk.ECS.Core
{
  public interface IGameLoopEventQueue
  {
    void Enqueue(IGameLoopEvent e);

    void InvokeEvents(GameEnvironment environment);
  }
}
