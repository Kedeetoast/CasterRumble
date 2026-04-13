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
        protected SpriteBatch _SpriteBatch => GraphicsManager.Instance.SpriteBatch;

        /// <summary>
        /// Gets or Sets the color mask to apply when rendering this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is Color.White
        /// </remarks>
        /// 

        private Color _color = Color.White;

        public Color Color 
        { 
            get
            {
                return _color* Visibility;
            }

            set
            {
                _color = value;
            }
            
        }

        private float _Visibility = 1.0f;

        public float Visibility
        {
            get
            {
                if (!IsVisible)
                {
                    return 0.0f;
                }
                else
                {
                    return _Visibility;
                }
            }
            set{ _Visibility = Math.Clamp(value, 0.0f, 1.0f); }
        }

        public bool IsVisible { get; set; } = true;

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
