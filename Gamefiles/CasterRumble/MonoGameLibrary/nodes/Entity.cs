using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoGameLibrary.nodes
{
    public class Entity
    {
        /*
        public Entity(Entity ParentNode, ) 
        {
           parent = ParentNode;
        }
        */

        /// <summary>
        /// specifies the postion of a given entity relative to orgin (0,0).
        /// </summary>
        public Entity parent;

        /// <summary>
        /// specifies the postion of a given entity relative to orgin (0,0).
        /// </summary>
        public Point globalPosition;


        /// <summary>
        /// specifies the postion of a given entity relative to parents orgin.
        /// </summary>
        private Point position;

        /// <summary>
        /// rotation of object with prarents rotation considered .
        /// </summary>
        public float globalRotation;

        /// <summary>
        /// rotation relative to parent.
        /// </summary>
        public float rotation;

        /// <summary>
        /// Gets the height, in pixels, of this texture region.
        /// </summary>
        public Vector2 scale;
    }
}
