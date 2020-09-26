module PointUtils

open Microsoft.Xna.Framework
open System

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