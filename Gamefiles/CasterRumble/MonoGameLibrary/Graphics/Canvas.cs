using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    public abstract class Canvas :  Node2D

    {
        protected SpriteBatch spriteBatch => GraphicsManager.Instance.SpriteBatch;

        /// <summary>
        /// Gets or Sets the color mask to apply when rendering this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is Color.White
        /// </remarks>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Gets or Sets the sprite effects to apply when rendering this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is SpriteEffects.None
        /// </remarks>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        /// <summary>
        /// Gets or Sets the layer depth to apply when rendering this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is 0.0f
        /// </remarks>
        public float LayerDepth { get; set; } = 0.0f;

        public Canvas() : base()
        {
        }

    }
}
