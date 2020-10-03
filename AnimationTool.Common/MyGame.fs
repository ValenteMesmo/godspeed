module Game

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open DrawingCanvasModule
open PaintModule
open CameraModule
open PencilPreviewModule

type MyGame (runningOnAndroid) as this =
    inherit Game()
 
    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let Camera = Camera(runningOnAndroid)

    let mutable batch = Unchecked.defaultof<SpriteBatch>
    
    let mutable pencilPreview = Unchecked.defaultof<PencilPreview>

    let objects = System.Collections.Generic.List<GameObjectModule.GameObject>()

    override this.Initialize() =
        batch <- new SpriteBatch(this.GraphicsDevice)
        pencilPreview <- PencilPreview(this.GraphicsDevice, Camera)
        
        this.IsMouseVisible <- true;
        base.Initialize()
        ()

    override this.LoadContent() =

        Textures.btn <- this.Content.Load<Texture2D>("btn")        
        Textures.pencil <- this.Content.Load<Texture2D>("pencil")
        Textures.eraser <- this.Content.Load<Texture2D>("eraser")
        Textures.save <- this.Content.Load<Texture2D>("save")

        Textures.pixel <- new Texture2D(this.GraphicsDevice, 1, 1)
        Textures.pixel.SetData([| Color.White |])

        objects.Add(Buttons.pencil)
        objects.Add(Buttons.eraser)
        objects.Add(Buttons.save)
        objects.Add(DrawingCanvasModule.create(this.GraphicsDevice))

        ()
 
    override this.Update (gameTime) =
        Input.update(Camera)        
        pencilPreview.update()
        updatePencilSize()

        for object in objects do
            object.Update()
        ()
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear(Color.Black);

        batch.Begin(
            SpriteSortMode.Deferred
            , BlendState.NonPremultiplied
            , SamplerState.PointClamp
            , DepthStencilState.Default
            , RasterizerState.CullNone
            , null
            , Camera.GetTransformation(this.GraphicsDevice)
        )

        for object in objects do
            object.Draw(batch)
        ()

        batch.Draw(
            pencilPreview.Texture
            , pencilPreview.Area
            , Color.White
        )

        batch.End()
        ()

