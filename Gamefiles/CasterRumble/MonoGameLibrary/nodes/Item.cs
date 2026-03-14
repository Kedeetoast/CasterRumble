using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Common;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;

namespace MonoGameLibrary.nodes
{
    public class Item : Entity
    {

        public Boolean Held { get; set; }



        /// <summary >
        ///How long it takes an item to despawn 
        /// </summary >
        public float DespawnTime { get; set; }







        public Item(ref World _world, EntityList _entityList, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = -1) : base(ref _world, _entityList, _ID, _position, _rotation)
        {
            DespawnTime = _despawntime;

        }



        public void DespawnTimeout()
        {
            world.Remove(body);
        }


    }
}
