using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MonoGameLibrary.Graphics
{
    internal class Texture : Canvas
    {

        // The texture used for the background pattern.
        private Texture2D _backgroundPattern;

        // The destination rectangle for the background pattern to fill.
        private Rectangle _backgroundDestination;

        // The offset to apply when drawing the background pattern so it appears to
        // be scrolling.
        private Vector2 _backgroundOffset;

        // The speed that the background pattern scrolls.
        private float _scrollSpeed = 50.0f;

        public Texture(string textureName)
        {
            // Load the background pattern texture.
            _backgroundPattern = Game.Content.Load<Texture2D>(GameManager.ImgDirectory + textureName);
            // Set the destination rectangle to fill the entire screen.
            _backgroundDestination = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
        }

        public override void Draw(GameTime gameTime)
        {

            _SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _SpriteBatch.Draw(_backgroundPattern, _backgroundDestination, new Rectangle(_backgroundOffset.ToPoint(), _backgroundDestination.Size), Color.White * 0.5f);
            _SpriteBatch.End();
        }
    }
}
