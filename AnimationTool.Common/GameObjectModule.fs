module GameObjectModule

open Microsoft.Xna.Framework.Graphics

type GameObject() =
     member val Update = fun () -> () with get, set
     member val Draw = fun (world:SpriteBatch, gui:SpriteBatch) -> () with get, set