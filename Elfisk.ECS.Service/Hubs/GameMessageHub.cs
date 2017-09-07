using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Microsoft.AspNet.SignalR;

namespace Elfisk.ECS.Service.Hubs
{
  public class GameMessageHub : Hub
  {
    public async Task Send(string name, string message)
    {
      await SendText(name, message);
    }


    internal static async Task SendText(string name, string text)
    {
      IHubContext context = GlobalHost.ConnectionManager.GetHubContext<GameMessageHub>();
      await context.Clients.All.addNewMessageToPage(name, text);
      return;
    }


    internal static async Task SendMessage(string name, object msg)
    {
      Condition.Requires(name, nameof(name)).IsNotNullOrEmpty();
      Condition.Requires(msg, nameof(msg)).IsNotNull();

      string handlerName = "handle" + msg.GetType().Name;

      IHubContext context = GlobalHost.ConnectionManager.GetHubContext<GameMessageHub>();
      await context.Clients.All.Invoke(handlerName, msg);
    }
  }
}
