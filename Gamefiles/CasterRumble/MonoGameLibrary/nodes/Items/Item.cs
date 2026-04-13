using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Common;
using MonoGameLibrary.General.Utility;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Xml;
using System.Xml.Linq;

namespace MonoGameLibrary.nodes.Items
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







        public Item(ref World _world, string _ID, Vector2 _position, float _rotation = 0) : base(ref _world, _ID, _position, _rotation)
        {
            DespawnTime = Attributes.DespawnTime;
            Timer = new Timer(DespawnTime * 1000);
            Timer.Elapsed += DespawnTimeout;
            Timer.Enabled = true;
        }

        public Item(ref World _world, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 0) : base(ref _world, _ID, _position, _rotation)
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

        public virtual void PickUp(Entity _holder)
        {
            Parent = _holder;
            Holder = _holder;
            Held = true;
            Timer.Enabled = false;
        }
        
        public virtual void Drop()
        {
            Holder = null;
            Held = false;
            Timer.Enabled = true;
        }

        public virtual void Use()
        {
            // Implement item usage logic here
        }

    }
}
