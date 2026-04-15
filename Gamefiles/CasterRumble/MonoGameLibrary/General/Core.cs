using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System;

namespace MonoGameLibrary.General;

public class Core : Game
{
    internal static Core s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static Core Instance => s_instance;

    // The scene that is currently active.
    public static Scene S_activeScene {private set; get; }

    /// <summary>
    /// Gets the graphics device manager to control the presentation of graphics.
    /// </summary>
    public static GraphicsDeviceManager Graphics { get; private set; }

    /// <summary>
    /// Gets the graphics device used to create graphical resources and perform primitive rendering.
    /// </summary>
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    /// <summary>
    /// Gets the sprite batch used for all 2D rendering.
    /// </summary>
    public static SpriteBatch SpriteBatch { get; private set; }

    private BasicEffect SpriteBatchEffect;

    /// <summary>
    /// Gets the content manager used to load global assets.
    /// </summary>
    public static new ContentManager Content { get; private set; }

    public virtual string ImgDirectory { get; protected set; }
    public virtual string FntDirectory { get; protected set; }
    public virtual string MusDirectory { get; protected set; }
    public virtual string SfxDirectory { get; protected set; }

    private Color _backgroundColor = Color.CornflowerBlue;

    public Color BackgroundColor 
    { 
        get
        {
            return _backgroundColor;
        }
        set
        {
            if (value != null)
            {
                _backgroundColor = value;
            }
        }
    }

    /// <summary>
    /// Creates a new Core instance.
    /// </summary>
    /// <param name="title">The title to display in the title bar of the game window.</param>
    /// <param name="width">The initial width, in pixels, of the game window.</param>jj
    /// <param name="height">The initial height, in pixels, of the game window.</param>
    /// <param name="fullScreen">Indicates if the game should start in fullscreen mode.</param>
    public Core(string title, int width, int height, bool fullScreen)
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;

        // Create a new graphics device manager.
        Graphics = new GraphicsDeviceManager(this);

        // Set the graphics defaults.
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;

        // Apply the graphic presentation changes.
        Graphics.ApplyChanges();

        // Set the window title.
        Window.Title = title;

        // Set the core's content manager to a reference of the base Game's
        // content manager.
        Content = base.Content;

        // Set the root directory for content.
        Content.RootDirectory = "Content";

        // Mouse is visible by default.
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Set the core's graphics device to a reference of the base Game's
        // graphics device.
        GraphicsDevice = base.GraphicsDevice;

        // Create the sprite batch instance.
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();

    }



    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);


        //if (S_nextScene != null)
        //{
        //    TransitionScene();
        //}



        //System.Diagnostics.Debug.WriteLine($"[Debug] Active Scene: {(S_activeScene != null ? S_activeScene.GetType().Name : "None")}");
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the back buffer.
        GraphicsDevice.Clear(BackgroundColor);

        // Apply the camera transform when a camera exists; render normally otherwise.
        Matrix? cameraTransform = CameraManager.HasCamera
            ? CameraManager.ActiveCamera.Transform
            : (Matrix?)null;

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);



        base.Draw(gameTime);

        // Always end the sprite batch when finished.
        SpriteBatch.End();
    }


    public static void TransitionScene(Scene newScene)
    {
        // Change the currently active scene to the new scene.
        S_activeScene = newScene;

        S_activeScene.Initialize();


    }
}
