#I "../node_modules/"
#r "fable-core/Fable.Core.dll"
#load "fable-import-pixi/Fable.Import.Pixi.fs"

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.PIXI
open Fable.Import.Browser

importAll "core-js"
let pixiparticles = importDefault<obj> "pixi-particles"

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

let emitOptions = createObj [
                                "alpha" ==> createObj ["start" ==> 0.8; "end" ==> 0.1]
                                "spawnType" ==> "circle"
                                "spawnCircle" ==> createObj ["x" ==> 0; "y" ==> 0; "r" ==> 10]
                                "lifetime" ==> createObj ["min" ==> 0.5; "max" ==> 1.5]
                                "pos" ==> createObj ["x" ==> 0.; "y" ==> 0.]
                                "scale" ==> createObj ["start" ==> 1.0; "end" ==> 0.3]
                                "startRotation" ==> createObj ["min" ==> -15; "max" ==> 315]
                                "color" ==> createObj ["start" ==> "fb1010"; "end" ==>"f5b830"]
                                "speed" ==> createObj ["start" ==> 200; "end" ==> 100]
                                "maxParticles" ==> 10000
                                "frequency" ==> 0.02
]
(*       

        startRotation: {
            min: 0,
            max: 360
        },
        rotationSpeed: {
            min: 0,
            max: 0
        },
        emitterLifetime: 0.31,
        maxParticles: 1000,
        pos: {
            x: 0,
            y: 0
        },
        addAtBack: false,
        spawnType: "circle",
        spawnCircle: {
            x: 0,
            y: 0,
            r: 10
        }
        *)
let emitter = createNew pixiparticles?Emitter (stage,
                                               [|texture|],
                                               emitOptions
                                               )
let mutable lastT = 0.0
let rec animate (t:float) =
    let dt = t - lastT;
    window.requestAnimationFrame(FrameRequestCallback animate) |> ignore
    bunny.rotation <- bunny.rotation + 0.1
    bunny.position.x <- sin(t / 1000.) * 100. + 250.
    bunny.position.y <- -cos(t / 1000.) * 100. + 250.
    emitter?ownerPos <- bunny.position
    emitter?ownerRot <- bunny.rotation
    emitter?update((dt) * 0.001) |> ignore
    renderer.render(stage)
    lastT <- t

emitter?emit <- true
animate 0.