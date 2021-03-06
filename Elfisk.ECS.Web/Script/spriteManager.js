﻿var Elfisk = Elfisk || {};

Elfisk.SpriteManager = function () {
  var images = []

  var sprites = {};

  var textures = {};

  var registerImages = function(img)
  {
    images = img;
  }

  var initialize = function (cfg, callback) {
    images.forEach(function (img) {
      PIXI.loader.add(img.url);
    })

    PIXI.loader
      .load(function () { onTexturesLoaded(cfg, callback); });
  }

  var onTexturesLoaded = function (cfg, callback) {
    images.forEach(function (img) {
      img.frames.forEach(function (frame) {
        textures[frame.name] =
        {
          texture: PIXI.loader.resources[img.url].texture,
          frame: new PIXI.Rectangle(frame.box[0],frame.box[1],frame.box[2],frame.box[3])
        };
      })
    });

    callback(cfg);
  }


  var getTexture = function (name) {
    return textures[name];
  }


  var getOrCreateSprite = function (spriteId, textureId, label) {
    if (!(spriteId in sprites)) {
      var texture = new PIXI.Texture(textures[textureId].texture, textures[textureId].frame);
      sprites[spriteId] = new PIXI.Sprite(texture);
      sprites[spriteId].scale.set(1, 1);
      if (label != null) {
        var text = new PIXI.Text(label, { fill: "#FFFFFF", fontSize: 12 })
        text.x = -10;
        text.y = -10;
        sprites[spriteId].addChild(text);
      }
      Elfisk.Game.getStage().addChild(sprites[spriteId]);
    }

    return sprites[spriteId];
  }

  var removeSprite = function (spriteId) {
    Elfisk.Game.getStage().removeChild(sprites[spriteId]);
  }


  var module =
  {
    registerImages: registerImages,
    removeSprite: removeSprite,
    initialize: initialize,
    getTexture: getTexture,
    getOrCreateSprite: getOrCreateSprite
  };

  return module;
}();
