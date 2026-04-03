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
using System.Timers;

namespace MonoGameLibrary.nodes
{
    public class Item : Entity
    {

        public Boolean Held { get; private set;}
        public Entity Holder { get; private set;}



        /// <summary >
        ///How long it takes an item to despawn 
        /// </summary >
        private float DespawnTime { get; set; }

        private Timer Timer { get; set; }







        public Item(ref World _world, EntityList _entityList, string _ID, Vector2 _position, float _rotation = 0) : base(ref _world, _entityList, _ID, _position, _rotation)
        {
            DespawnTime = Attributes.DespawnTime;
            Timer = new Timer(DespawnTime * 1000);
            Timer.Elapsed += DespawnTimeout;
            Timer.Enabled = true;
        }

        public Item(ref World _world, EntityList _entityList, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 0) : base(ref _world, _entityList, _ID, _position, _rotation)
        {
            DespawnTime = _despawntime;
            Timer = new Timer(DespawnTime * 1000);
            Timer.Elapsed += DespawnTimeout;
            Timer.Enabled = true;
        }


        public void DespawnTimeout(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Item ", Attributes.Id, " Despawned");
            world.Remove(body);
            Timer.Enabled = false;
        }

        public void PickUp(Entity _holder)
        {
            Holder = _holder;
            Held = true;
            Timer.Enabled = false;
        }


    }
}
