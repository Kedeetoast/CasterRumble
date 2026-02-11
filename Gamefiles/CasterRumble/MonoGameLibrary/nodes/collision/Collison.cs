using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace MonoGameLibrary.nodes.collision
{
    public class Collison : Entity
    {



        public bool Hitbox;

        /// <summary>
        /// the layer the collision object is on.
        /// </summary>
        public int CollisonLayer;

        /// <summary>
        /// the layer the collision object detects on.
        /// </summary>
        public int CollisonMask;
    } 
}