using System;
using log4net;
using Topshelf;

namespace Elfisk.ECS.Service
{
  public class ServiceControlWithErrorHandling : ServiceControl
  {
    private ServiceControl ServiceControl { get; set; }

    private ILog Logger { get; set; }

    public string ServiceName { get; private set; }


    public ServiceControlWithErrorHandling(ServiceControl serviceControl, ILog logger)
    {
      ServiceControl = serviceControl;
      Logger = logger;

      ServiceName = ServiceControl.GetType().Assembly.GetName().Name;
    }


    public bool Start(HostControl hostControl)
    {
      try
      {
        return ServiceControl.Start(hostControl);
      }
      catch (Exception ex)
      {
        Logger.Fatal($"Unhandled exception occurred while starting the service: {ServiceName}", ex);
        throw;
      }
    }

    public bool Stop(HostControl hostControl)
    {
      try
      {
        return ServiceControl.Stop(hostControl);
      }
      catch (Exception ex)
      {
        // Log the error and force shutdown. Human intervention is needed at this point.
        Logger.Fatal($"Unhandled exception occurred while stopping the service: {ServiceName}", ex);
        return true;
      }
    }
  }
}
