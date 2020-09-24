module Game

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open DrawingCanvasModule
open PaintModule
open GameCamera
open PencilPreviewModule

type MyGame () as this =
    inherit Game()
 
    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let Camera = Camera()

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable editor = Unchecked.defaultof<DrawingCanvas>
    let mutable btn = Unchecked.defaultof<Texture2D>
    let mutable pixel = Unchecked.defaultof<Texture2D>
    let mutable pencilPreview = Unchecked.defaultof<PencilPreview>
    let paperColor = Color(30,30,30)

    override this.Initialize() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        editor <- DrawingCanvas(this.GraphicsDevice)
        pencilPreview <- PencilPreview(this.GraphicsDevice, Camera)

        loadFile(editor, this.GraphicsDevice)
        this.IsMouseVisible <- true;
        base.Initialize()
        ()

    override this.LoadContent() =
        btn <- this.Content.Load<Texture2D>("btn")
        pixel <- new Texture2D(this.GraphicsDevice, 1,1)
        pixel.SetData([|Color.White|])        
        ()
 
    override this.Update (gameTime) =
        paintOnMouseClick(Camera, editor)
        pencilPreview.update()
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
            pixel
            , editor.Texture.Bounds
            , paperColor
        )               

        spriteBatch.Draw(
            editor.Texture
            , editor.Texture.Bounds
            , Color.White
        )

        spriteBatch.Draw(
            pencilPreview.Texture
            , pencilPreview.Area
            , Color.White
        )
       
        spriteBatch.End()
        ()

    override this.OnExiting(sender, args) =
        saveFile(editor)
        base.OnExiting(sender, args);

