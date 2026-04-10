using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes
{
    public class Node2D : Node
    {
        public virtual float Rotation { get; set; } = 0.0f;

        public virtual float GlobalRotation
        {
            get
            {
                if (Parent is Node2D parentNode2D)
                {
                    return (Rotation + parentNode2D.GlobalRotation);
                }
                return Rotation;
            }
        }


        public Node2D(Node _parent = null ) : base() 
        { 

        }



        /// <summary>
        /// Gets or Sets the scale factor to apply to the x- and y-axes when rendering this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is Vector2.One
        /// </remarks>
        public virtual Vector2 Scale { get; set; } = Vector2.One;

        public virtual Vector2 GlobalScale
        {
            get
            {
                if (Parent is Node2D parentNode2D)
                {
                    return (Scale * parentNode2D.GlobalScale);
                }
                return Scale;
            }
        }

        /// <summary>
        /// Gets or Sets the scale factor to apply to the x- and y-axes when rendering this sprite.
        /// </summary>


        /// <remarks>
        /// Default value is Vector2.One
        /// </remarks>
        public virtual Vector2 Position { get; set; } = Vector2.Zero;

        /// <remarks>
        /// Default value is Vector2.One
        /// </remarks>
        public virtual Vector2 GlobalPosition
        {
            get
            {
                if (Parent is Node2D parentNode2D)
                { 
                    return (Vector2.RotateAround(Position, parentNode2D.GlobalPosition, parentNode2D.Rotation)* GlobalScale + parentNode2D.GlobalPosition);
                }
                return Position;
            }
        }

        /// <summary>
        /// Gets or Sets the xy-coordinate origin point, relative to the top-left corner, of this sprite.
        /// </summary>
        /// <remarks>
        /// Default value is Vector2.Zero
        /// </remarks>
        public virtual Vector2 Origin { get; set; } = Vector2.Zero;

    }
}
