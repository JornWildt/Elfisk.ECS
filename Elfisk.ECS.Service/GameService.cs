using System;
using System.Threading;
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
    protected static ILog Logger = LogManager.GetLogger(typeof(GameService));

    protected bool DoRunWebHost { get; set; }

    protected TimeSpan LoopPeriod { get; set; }

    protected IDisposable WebHost { get; set; }

    protected IWindsorContainer CastleContainer { get; set; }

    protected Task GameLoopTask { get; set; }

    protected CancellationTokenSource GameLoopCancellationTokenSource { get; set; }


    public GameService(IWindsorContainer container, TimeSpan loopPeriod, bool doRunWebHost)
    {
      Condition.Requires(container, nameof(container)).IsNotNull();
      CastleContainer = container;
      LoopPeriod = loopPeriod;
      DoRunWebHost = doRunWebHost;
    }


    public bool Start(HostControl hostControl)
    {
      Initialize();
      StartGameLoop();
      return true;
    }


    protected virtual void Initialize()
    {
      Logger.Debug("Installing dependencies");
      CastleContainer.Install(FromAssembly.This());

      if (DoRunWebHost)
      {
        Logger.Debug("Starting web host");
        WebHost = WebApp.Start(ServiceAppSettings.SignalRUrl);
      }
    }


    private void StartGameLoop()
    {
      CastleDependencyContainer gameContainer = new CastleDependencyContainer(CastleContainer);
      GameEnvironment env = new GameEnvironment(gameContainer, LoopPeriod);
      GameEngine engine = new GameEngine(env);

      Logger.Debug("Start engine loop");

      GameLoopCancellationTokenSource = new CancellationTokenSource();
      GameLoopTask = Task.Run(async () =>
      {
        try
        {
          await engine.RunGameLoop();
        }
        catch (Exception ex)
        {
          Logger.Error(ex);
        }
      },
      GameLoopCancellationTokenSource.Token);
    }


    public virtual bool Stop(HostControl hostControl)
    {
      Logger.Info("Stopping service");

      if (GameLoopCancellationTokenSource != null)
        GameLoopCancellationTokenSource.Cancel();

      if (WebHost != null)
        WebHost.Dispose();

      CastleContainer.Dispose();

      return true;
    }
  }
}
