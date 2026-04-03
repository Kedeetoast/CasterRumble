using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Dynamics;
using System;
//using System.Drawing;

namespace CasterRumble

{
    public class Game1 : Core
    {
        public event EventHandler EventName;

        private BasicEffect _spriteBatchEffect;

        public World _world;

        // Defines the slime animated sprite.
        private Sprite _slime;

        // Defines the bat animated sprite.
        private Sprite _bat;

        public Game1() : base("Dungeon Slime", 1280, 720, false)
        {

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create the texture atlas from the XML configuration file
            TextureAtlas atlas = TextureAtlas.FromFile(Content, "Images/Spritesheet/Atlas_definition/defSpr_atlas");

            // Create the slime animated sprite from the atlas.
            _slime = atlas.CreateAnimatedSprite("slime-animation");
            _slime.Scale = new Vector2(4.0f, 4.0f);

            // Create the bat animated sprite from the atlas.
            _bat = atlas.CreateAnimatedSprite("bat-animation");
            _bat.Position = new Vector2(_slime.Width + 10, 0); // Position the bat 10px to the right of the slime.
            _bat.Scale = new Vector2(4.0f, 4.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the slime animated sprite.
            //_slime.Update(gameTime);

            // Update the bat animated sprite.
            //_bat.Update(gameTime);

            // TODO: Add your update logic here
            GraphicsManager.Instance.UpdateEventCall(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //var vp = GraphicsDevice.Viewport;
            //_spriteBatchEffect.View = Matrix.CreateLookAt(Camera.Instance.Position, Camera.Instance.Position + Vector3.Forward, Vector3.Up);
           // _spriteBatchEffect.Projection = Matrix.CreateOrthographic(Camera.Instance.CameraViewWidth, Camera.Instance.CameraViewWidth / vp.AspectRatio, 0f, -1f);

            // Clear the back buffer.
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Begin the sprite batch to prepare for rendering.
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            GraphicsManager.Instance.DrawEventCall(SpriteBatch);

            // Always end the sprite batch when finished.
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
