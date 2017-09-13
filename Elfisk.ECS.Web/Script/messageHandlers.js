var Elfisk = Elfisk || {};

Elfisk.MessageHandlers = function () {
  var handleRenderMessage = function (msg) {
    //$('#discussion').append('<li>Render: ' + htmlEncode(msg.Texture) + '</li>');

    var x = Elfisk.ViewPortManager.positionToPixelX(msg.X);
    var y = Elfisk.ViewPortManager.positionToPixelY(msg.Y);

    var sprite = Elfisk.SpriteManager.getOrCreateSprite(msg.SpriteId, msg.Texture);
    sprite.x = x;
    sprite.y = y;
  };


  function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
  }


  var module =
    {
      handleRenderMessage: handleRenderMessage
    };

  return module;
}();
