module Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open GameCamera
open DrawingCanvasModule
open PaintModule

type MyGame () as this =
    inherit Game()
 
    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let Camera = Camera()
    let area = new Rectangle(0, 0, 100, 100)

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable editor = Unchecked.defaultof<DrawingCanvas>
    let mutable btn = Unchecked.defaultof<Texture2D>

    override this.Initialize() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        editor <- DrawingCanvas(this.GraphicsDevice)
        do  base.Initialize()
            this.IsMouseVisible <- true;
        ()

    override this.LoadContent() =
        btn <- this.Content.Load<Texture2D>("btn")
        ()
 
    override this.Update (gameTime) =
        paintOnMouseClick(Camera, editor)
        ()
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(
            SpriteSortMode.Deferred
            , BlendState.NonPremultiplied
            , SamplerState.PointClamp
            , DepthStencilState.Default
            , RasterizerState.CullNone
            , null
            , Camera.GetTransformation(this.GraphicsDevice)
        )

        spriteBatch.Draw(
            editor.Texture
            , area
            , Color.White
        )

        spriteBatch.End()
        ()

