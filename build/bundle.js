(function (exports,PIXI) {
'use strict';

var options = {
    backgroundColor: 1087931,
    resolution: 1
};
var renderer = PIXI.autoDetectRenderer(500, 500, options);
var gameDiv = document.getElementById("game");
gameDiv.appendChild(renderer.view);
var stage = new PIXI.Container();
var texture = PIXI.Texture.fromImage("./build/assets/bunny.png");
var bunny = new PIXI.Sprite(texture);
bunny.anchor.x = 0.5;
bunny.anchor.y = 0.5;
bunny.position.x = 250;
bunny.position.y = 250;
stage.addChild(bunny);
function animate(dt) {
    window.requestAnimationFrame(function (delegateArg0) {
        animate(delegateArg0);
    });
    bunny.rotation = bunny.rotation + 0.1;
    bunny.position.x = Math.sin(dt / 1000) * 100 + 250;
    bunny.position.y = -Math.cos(dt / 1000) * 100 + 250;
    renderer.render(stage);
}
animate(0);

exports.options = options;
exports.renderer = renderer;
exports.gameDiv = gameDiv;
exports.stage = stage;
exports.texture = texture;
exports.bunny = bunny;
exports.animate = animate;

}((this.game = this.game || {}),PIXI));

//# sourceMappingURL=bundle.js.map