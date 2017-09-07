using System.Threading.Tasks;

namespace Elfisk.ECS.Core
{
  public interface IPlayersBus
  {
    Task Send(string player, object msg);
  }
}
