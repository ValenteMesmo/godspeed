module PaintModule

open Microsoft.Xna.Framework.Input
open GameCamera
open Microsoft.Xna.Framework
open DrawingCanvasModule
open System

let mutable previousWorldPosition = Point.Zero 

let pointsBetween(a:Point, b:Point) =
    let mutable A = a;

    let w = b.X - a.X
    let h = b.Y - a.Y
    let mutable dx1 = 0
    let mutable dy1 = 0
    let mutable dx2 = 0
    let mutable dy2 = 0

    if (w < 0) then
        dx1 <- -1 
    else if (w > 0) then 
        dx1 <- 1
    if (h < 0) then
         dy1 <- -1
    else if (h > 0) then
         dy1 <- 1 
    if (w < 0) then
        dx2 <- -1
    else if (w > 0) then
        dx2 <- 1

    let mutable longest = Math.Abs(w)
    let mutable shortest = Math.Abs(h)
    if not (longest > shortest) then
        longest <- Math.Abs(h)
        shortest <- Math.Abs(w)
        if (h < 0) then
            dy2 <- -1
        else if (h > 0) then
            dy2 <- 1
        dx2 <- 0

    let mutable numerator = shortest

    [
        for i in 0..longest do
            yield A
            numerator <- numerator + shortest
            if not (numerator < longest) then
                numerator <- numerator - longest
                A.X <- A.X + dx1
                A.Y <- A.Y + dy1
            else
                A.X <- A.X + dx2
                A.Y <- A.Y + dy2
    ]

let paintOnMouseClick(camera:Camera, editor: DrawingCanvas) =     
    let mouse = Mouse.GetState()
    
    if mouse.LeftButton = ButtonState.Pressed then
        let mousePosition = camera.GetWorldPosition(mouse.Position)

        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- mousePosition

        for point in pointsBetween(previousWorldPosition, mousePosition) do
            if editor.Texture.Bounds.Contains(point) then
                editor.SetColor(point, Color.Red)
        editor.UpdateTexture()
        previousWorldPosition <- mousePosition
    else
        previousWorldPosition <- Point.Zero
    ()