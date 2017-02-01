#I "../node_modules/"
#r "fable-core/Fable.Core.dll"
#load "fable-import-pixi/Fable.Import.Pixi.fs"

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.PIXI
open Fable.Import.Browser

importAll "core-js"

let options = [
    BackgroundColor ( float 0x1099bb )
    Resolution 1.
]
let renderer = 
    Globals.autoDetectRenderer(500.,500., options)
    |> unbox<SystemRenderer>
let gameDiv = document.getElementById("game")
gameDiv.appendChild(renderer.view)

let stage = Container()

let texture = Texture.fromImage("./build/assets/bunny.png")
let bunny = Sprite(texture)
bunny.anchor.x <- 0.5
bunny.anchor.y <- 0.5
bunny.position.x <- 250.
bunny.position.y <- 250.
stage.addChild(bunny) |> ignore


let rec animate (dt:float) =
    window.requestAnimationFrame(FrameRequestCallback animate) |> ignore
    bunny.rotation <- bunny.rotation + 0.1
    bunny.position.x <- sin(dt / 1000.) * 100. + 250.
    bunny.position.y <- -cos(dt / 1000.) * 100. + 250.
    renderer.render(stage)

animate 0.