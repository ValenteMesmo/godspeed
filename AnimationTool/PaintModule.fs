module PaintModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open DrawingCanvasModule
open GameCamera
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

let poitsFromPencilArea(radiusSquared: int, point: Point) =
    [
        for i in point.X - radiusSquared .. point.X + radiusSquared do
            for j in point.Y - radiusSquared .. point.Y + radiusSquared do
                let deltaX = i - point.X
                let deltaY = j - point.Y
                let distanceSquared = 
                    Math.Pow(float deltaX, 2.0) 
                    + Math.Pow(float deltaY, 2.0)

                if distanceSquared <= float radiusSquared then
                    yield Point(i, j)
    ]

let paintOnMouseClick(camera:Camera, editor: DrawingCanvas) =     
    let mouse = Mouse.GetState()
    
    if mouse.LeftButton = ButtonState.Pressed 
        || mouse.RightButton = ButtonState.Pressed then

        let mousePosition = camera.GetWorldPosition(mouse.Position)

        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- mousePosition

        for point in pointsBetween(previousWorldPosition, mousePosition) do
            for pencilPoint in poitsFromPencilArea(6, point) do
                if editor.Texture.Bounds.Contains(pencilPoint) then
                    if mouse.RightButton = ButtonState.Pressed then
                        editor.SetColor(pencilPoint, Color.Transparent)
                    else 
                        editor.SetColor(pencilPoint, Color.Red)

        editor.UpdateTexture()
        previousWorldPosition <- mousePosition
    else
        previousWorldPosition <- Point.Zero
    
let convertColor(color:Color) =
    System.Drawing.Color.FromArgb(
        int color.A
        , int color.R
        , int color.G
        , int color.B
    )

let fileName = "savefile.png"

let FromArrayIndexToPoint(index: int, width: int) =
    Point((index / width), index % width)

let loadFile(editor: DrawingCanvas, device: Graphics.GraphicsDevice) =
    if System.IO.File.Exists(fileName) then
        let texture = Texture2D.FromFile(device, fileName)
    
        texture.GetData(editor.Pixels)
        editor.UpdateTexture()

        texture.Dispose()
    ()

let saveFile(editor: DrawingCanvas) =
    let pic = new System.Drawing.Bitmap(
        editor.Texture.Width
        , editor.Texture.Height
        , System.Drawing.Imaging.PixelFormat.Format32bppArgb
    )    
    
    for i in 0..editor.Pixels.Length-1 do
        let position = FromArrayIndexToPoint(i, editor.Texture.Width)
        pic.SetPixel(position.Y, position.X, convertColor editor.Pixels.[i])

    pic.Save(fileName, Drawing.Imaging.ImageFormat.Png)
    pic.Dispose()
    ()