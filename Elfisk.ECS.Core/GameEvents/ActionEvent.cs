using System;

namespace Elfisk.ECS.Core.GameEvents
{
  public class ActionEvent : IGameLoopEvent
  {
    Action F { get; set; }


    public ActionEvent(Action f)
    {
      F = f;
    }

    public void Invoke(GameEnvironment environment)
    {
      F();
    }
  }
}
