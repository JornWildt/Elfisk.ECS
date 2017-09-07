using Elfisk.Commons;

namespace Elfisk.ECS.Service
{
  public static class ServiceAppSettings
  {
    public static readonly AppSetting<string> SignalRUrl = new AppSetting<string>("Service.SignalRUrl");
  }
}
