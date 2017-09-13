var Elfisk = Elfisk || {};

Elfisk.Game = function () {
  var startSignalRCommunication = function (url) {
    $.connection.hub.url = url;

    // Reference the auto-generated proxy for the hub.
    var msgHub = $.connection.gameMessageHub;

    msgHub.client.handleRenderMessage = Elfisk.MessageHandlers.handleRenderMessage;

    $.connection.hub.start();
  };


  var stage = null;

  var renderer = null;

  var startRender = function (cfg) {
    // Create a container object called the `stage`
    stage = new PIXI.Container();

    // stage.scale.set(3, 3);

    // Create the renderer
    renderer = PIXI.autoDetectRenderer(Elfisk.ViewPortManager.getPixelWidth(), Elfisk.ViewPortManager.getPixelHeight());

    // Add the canvas to the HTML document
    $(cfg.viewElement).append(renderer.view);

    // Tell the `renderer` to `render` the `stage`
    renderer.render(stage);
  };


  var initialize = function (cfg) {
    Elfisk.SpriteManager.initialize(cfg, onLoaded);
  };


  var onLoaded = function (cfg) {
    startSignalRCommunication(cfg.signalRUrl);
    startRender(cfg);
    if (cfg.onReady != null)
      cfg.onReady();
  }


  var gameLoop = function () {
    //Loop this function 60 times per second
    requestAnimationFrame(gameLoop);

    //Render the stage
    renderer.render(stage);
  }


  var module =
    {
      initialize: initialize,
      getStage: function () { return stage; },
      runGameLoop: gameLoop
    };

  return module;
}();

