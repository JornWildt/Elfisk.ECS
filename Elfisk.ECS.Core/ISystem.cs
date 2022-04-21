using System;
using System.Threading.Tasks;

namespace Elfisk.ECS.Core
{
  public interface ISystem
  {
    Task Update(GameEnvironment environment, TimeSpan elapsedTime);
  }
}
