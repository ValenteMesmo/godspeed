module Game
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open CameraModule
open Config

type MyGame (env : GameEnviroment, mode: ScreenMode) as this =
    inherit Game()

    do 
        this.Content.RootDirectory <- "Content"
        
    let CameraWorld = Camera(mode)
    let CameraGui = Camera(mode)

    let graphics = new GraphicsDeviceManager(this)
    let mutable batchGui = Unchecked.defaultof<SpriteBatch>
    let mutable batchWorld = Unchecked.defaultof<SpriteBatch>

    let objects = System.Collections.Generic.List<GameObjectModule.GameObject>()

    override this.Initialize() =
        batchGui <- new SpriteBatch(this.GraphicsDevice)
        batchWorld <- new SpriteBatch(this.GraphicsDevice)        
       
        if env = Config.Desktop && mode = ScreenMode.Portrait then
            graphics.PreferredBackBufferHeight <- int CameraModule.DESKTOP_PORTRAIT_HEIGHT
            graphics.PreferredBackBufferWidth <- int CameraModule.DESKTOP_PORTRAIT_WIDTH
            graphics.ApplyChanges()

        this.IsMouseVisible <- true;
        base.Initialize()
        ()

    override this.LoadContent() =
        CameraWorld.SetLocation(Vector2(50.0f, 50.0f))
        CameraWorld.SetZoom(5.0f)

        Textures.btn <- this.Content.Load<Texture2D>("btn")        
        Textures.pencil <- this.Content.Load<Texture2D>("pencil")
        Textures.eraser <- this.Content.Load<Texture2D>("eraser")
        Textures.save <- this.Content.Load<Texture2D>("save")

        Textures.pixel <- new Texture2D(this.GraphicsDevice, 1, 1)
        Textures.pixel.SetData([| Color.White |])

        objects.Add(DrawingCanvasModule.create(this.GraphicsDevice))
        objects.Add(Buttons.createPencilLight(mode))
        objects.Add(Buttons.createPencilGray(mode))        
        objects.Add(Buttons.createPencilDark(mode))
        objects.Add(Buttons.createEraser(mode))
        objects.Add(Buttons.createSave(mode))
        objects.Add(Buttons.createIncreaseSizeButton(mode))
        objects.Add(Buttons.createDecreaseSizeButton(mode))
        
        objects.Add(PencilPreviewModule.create(this.GraphicsDevice))
        objects.Add(PencilOptions.create())

        ()
 
    override this.Update (gameTime) =
        Input.update(CameraWorld, CameraGui)

        for object in objects do
            object.Update()

        ()
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear(Color.Black);
        
        batchWorld.Begin(
            SpriteSortMode.Deferred
            , BlendState.NonPremultiplied
            , SamplerState.PointClamp
            , DepthStencilState.Default
            , RasterizerState.CullNone
            , null
            , CameraWorld.GetTransformation(this.GraphicsDevice)
        )
        batchGui.Begin(
            SpriteSortMode.Deferred
            , BlendState.NonPremultiplied
            , SamplerState.PointClamp
            , DepthStencilState.Default
            , RasterizerState.CullNone
            , null
            , CameraGui.GetTransformation(this.GraphicsDevice)
        )

        for object in objects do
            object.Draw(batchWorld, batchGui)
        ()

        batchWorld.End()
        batchGui.End()
        ()

