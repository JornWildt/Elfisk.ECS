var Elfisk = Elfisk || {};

Elfisk.SpriteManager = function () {
  var sprites = {};

  var textures = {};

  var initialize = function (cfg, callback) {
    PIXI.loader
      .add(cfg.rootUrl + "game/images/bear.png")
      .add(cfg.rootUrl + "game/tiles/tiles1.bmp")
      .load(function () { onTexturesLoaded(cfg, callback); });
  }

  var onTexturesLoaded = function (cfg, callback) {
    textures["B"] =
    {
      texture: PIXI.loader.resources[cfg.rootUrl + "game/images/bear.png"].texture,
      frame: new PIXI.Rectangle(0, 0, 32, 32)
    };

    textures["T1"] =
      {
        texture: PIXI.loader.resources[cfg.rootUrl + "game/tiles/tiles1.bmp"].texture,
        frame: new PIXI.Rectangle(0, 0, 32, 32)
      };

    textures["T2"] =
      {
        texture: PIXI.loader.resources[cfg.rootUrl + "game/tiles/tiles1.bmp"].texture,
        frame: new PIXI.Rectangle(32, 0, 32, 32)
      };

    textures["T3"] =
      {
        texture: PIXI.loader.resources[cfg.rootUrl + "game/tiles/tiles1.bmp"].texture,
        frame: new PIXI.Rectangle(64, 0, 32, 32)
      };

    textures["T4"] =
      {
        texture: PIXI.loader.resources[cfg.rootUrl + "game/tiles/tiles1.bmp"].texture,
        frame: new PIXI.Rectangle(96, 0, 32, 32)
      };

    callback(cfg);
  }


  var getTexture = function (name) {
    return textures[name];
  }


  var getOrCreateSprite = function (spriteId, textureId) {
    if (!(spriteId in sprites)) {
      var texture = new PIXI.Texture(textures[textureId].texture, textures[textureId].frame);
      //texture.frame = textures[textureId].frame;
      sprites[spriteId] = new PIXI.Sprite(texture);
      sprites[spriteId].scale.set(1, 1);
      Elfisk.Game.getStage().addChild(sprites[spriteId]);
    }

    return sprites[spriteId];
  }


  var module =
  {
    initialize: initialize,
    getTexture: getTexture,
    getOrCreateSprite: getOrCreateSprite
  };

  return module;
}();
