module Game
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open CameraModule

type MyGame (runningOnAndroid) as this =
    inherit Game()
 
    do 
        this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let Camera = Camera(runningOnAndroid)

    let mutable batch = Unchecked.defaultof<SpriteBatch>

    let objects = System.Collections.Generic.List<GameObjectModule.GameObject>()

    override this.Initialize() =
        batch <- new SpriteBatch(this.GraphicsDevice)
        if runningOnAndroid then
            graphics.PreferredBackBufferHeight <- 
                int CameraModule.DESKTOP_PORTRAIT_HEIGHT
            graphics.PreferredBackBufferWidth <- 
                int CameraModule.DESKTOP_PORTRAIT_WIDTH
            graphics.ApplyChanges()
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

        objects.Add(DrawingCanvasModule.create(this.GraphicsDevice))
        objects.Add(PencilPreviewModule.create(this.GraphicsDevice, Camera))
        objects.Add(Buttons.createPencil(runningOnAndroid))
        objects.Add(Buttons.createEraser(runningOnAndroid))
        objects.Add(Buttons.createSave(runningOnAndroid))

        ()
 
    override this.Update (gameTime) =
        Input.update(Camera)

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

        batch.End()
        ()

