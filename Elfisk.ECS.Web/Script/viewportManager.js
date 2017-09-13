var Elfisk = Elfisk || {};

Elfisk.ViewPortManager = function () {
  var width = 100;

  var height = 100;

  var pixelWidth = $("#gameDisplay").innerWidth();

  var pixelHeight = $("#gameDisplay").innerHeight();


  var positionToPixelX = function (x) {
    return x * (pixelWidth / width);
  }


  var positionToPixelY = function (y) {
    return y * (pixelHeight / height);
  }


  var module =
    {
      getPixelWidth: function () { return pixelWidth; },
      getPixelHeight: function () { return pixelHeight; },
      positionToPixelX: positionToPixelX,
      positionToPixelY: positionToPixelY
    };

  return module;
}();
