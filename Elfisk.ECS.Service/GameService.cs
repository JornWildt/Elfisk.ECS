using System;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CuttingEdge.Conditions;
using Elfisk.ECS.Core;
using log4net;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Elfisk.ECS.Service
{
  public class GameService : ServiceControl
  {
    static ILog Logger = LogManager.GetLogger(typeof(GameService));

    IDisposable SignalRHost { get; set; }

    IWindsorContainer CastleContainer { get; set; }


    public GameService(IWindsorContainer container)
    {
      Condition.Requires(container, nameof(container)).IsNotNull();
      CastleContainer = container;
    }


    public bool Start(HostControl hostControl)
    {
      CastleContainer.Install(FromAssembly.This());

      SignalRHost = WebApp.Start(ServiceAppSettings.SignalRUrl);

      CastleDependencyContainer gameContainer = new CastleDependencyContainer(CastleContainer);
      GameEnvironment env = new GameEnvironment(gameContainer);
      GameEngine engine = new GameEngine(env);

      Task.Run(async () =>
      {
        try
        {
          await engine.RunGameLoop();
        }
        catch (Exception ex)
        {
          Logger.Error(ex);
        }
      });
      return true;
    }


    public bool Stop(HostControl hostControl)
    {
      SignalRHost.Dispose();
      CastleContainer.Dispose();
      return true;
    }
  }
}
