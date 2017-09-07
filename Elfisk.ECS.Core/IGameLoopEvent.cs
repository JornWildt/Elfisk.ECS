namespace Elfisk.ECS.Core
{
  public interface IGameLoopEvent
  {
    void Invoke(GameEnvironment environment);
  }
}
