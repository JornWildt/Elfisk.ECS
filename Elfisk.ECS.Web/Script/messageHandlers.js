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


  var handleRemoveSpriteMessage = function (msg) {
    //$('#discussion').append('<li>Remove: ' + htmlEncode(msg.SpriteId) + '</li>');

    Elfisk.SpriteManager.removeSprite(msg.SpriteId);
  };


  function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
  }


  var module =
    {
      handleRenderMessage: handleRenderMessage,
      handleRemoveSpriteMessage: handleRemoveSpriteMessage
    };

  return module;
}();
