using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.nodes;
using MonoGameLibrary.General.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Scenes
{
    public class Scene : Node
    {
        /// <summary>
        /// Gets the ContentManager used for loading scene-specific assets.
        /// </summary>
        /// <remarks>
        /// Assets loaded through this ContentManager will be automatically unloaded when this scene ends.
        /// </remarks>
        public ContentManager Content { get; protected set; }

        public GameComponentCollection GameComponents { get;  set; } 

        /// <summary>
        /// Gets a value that indicates if the scene has been disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }

        protected Dictionary<string, Node> SceneAssets = new Dictionary<string, Node>();

        protected Dictionary<string, Song> SceneSongs = new Dictionary<string, Song>();

        protected Dictionary<string, SoundEffect> SceneSoundEffects = new Dictionary<string, SoundEffect>();



    


        /// <summary>
        /// Creates a new scene instance.
        /// </summary>
        public Scene() : base()
        {
            // Create a content manager for the scene
            Content = new ContentManager(Core.Content.ServiceProvider);

            // Set the root directory for content to the same as the root directory
            // for the game's content.
            Content.RootDirectory = Core.Content.RootDirectory;
        }

        // Finalizer, called when object is cleaned up by garbage collector.
        ~Scene() => Dispose(false);

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        /// <remarks>
        /// When overriding this in a derived class, ensure that base.Initialize()
        /// still called as this is when LoadContent is called.
        /// </remarks>
        public override void Initialize()
        {
            LoadContent();
        }

        /// <summary>
        /// Override to provide logic to load content for the scene.
        /// </summary>
        protected override void LoadContent() { }

        /// <summary>
        /// Unloads scene-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Updates this scene.
        /// </summary>
        /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
        public override void Update(GameTime gameTime) { }

        /// <summary>
        /// Draws this scene.
        /// </summary>
        /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
        public override void Draw(GameTime gameTime) { }

        /// <summary>
        /// Disposes of this scene.
        /// </summary>
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        /// <summary>
        /// Disposes of this scene.
        /// </summary>
        /// <param name="disposing">'
        /// Indicates whether managed resources should be disposed.  This value is only true when called from the main
        /// Dispose method.  When called from the finalizer, this will be false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                UnloadContent();
                Content.Dispose();
            }
            IsDisposed = true;
        }

        public void PlaySFX(string key)
        {
            if (SceneSoundEffects.TryGetValue(key, out SoundEffect soundEffect))
            {
                AudioManager.Instance.PlaySoundEffect(soundEffect, 0.0f, 0.0f, false);
            }
            else
            {
                try
                {
                    SceneSoundEffects.Add("key", Content.Load<SoundEffect>(key));
                    AudioManager.Instance.PlaySoundEffect(SceneSoundEffects[key], 0.0f, 0.0f, false);
                }
                catch (ContentLoadException)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: Sound effect with key '{key}' not found in content.");
                }

            }
        }
    }
}
