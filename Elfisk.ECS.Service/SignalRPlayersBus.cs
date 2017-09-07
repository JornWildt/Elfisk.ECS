using Elfisk.ECS.Core;
using System;
using System.Threading.Tasks;
using Elfisk.ECS.Service.Hubs;

namespace Elfisk.ECS.Service
{
  public class SignalRPlayersBus : IPlayersBus
  {
    public async Task Send(string player, object msg)
    {
      Console.WriteLine($"[{player}] {msg}");
      await GameMessageHub.SendMessage(player, msg);
    }
  }
}
