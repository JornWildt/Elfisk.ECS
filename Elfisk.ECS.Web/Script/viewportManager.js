var Elfisk = Elfisk || {};

Elfisk.ViewPortManager = function () {
  var displayElementId = null;
  
  var width = 100;

  var height = 100;

  var pixelWidth = 100;

  var pixelHeight = 100;

  var registerDisplayElement = function(id) 
  { 
    displayElementId = id; 
    pixelWidth = $(id).innerWidth();
    pixelHeight = $(id).innerHeight();
  }

  var positionToPixelX = function (x) {
    return x * (pixelWidth / width);
  }


  var positionToPixelY = function (y) {
    return y * (pixelHeight / height);
  }


  var module =
    {
      registerDisplayElement: registerDisplayElement,
      getPixelWidth: function () { return pixelWidth; },
      getPixelHeight: function () { return pixelHeight; },
      positionToPixelX: positionToPixelX,
      positionToPixelY: positionToPixelY
    };

  return module;
}();
