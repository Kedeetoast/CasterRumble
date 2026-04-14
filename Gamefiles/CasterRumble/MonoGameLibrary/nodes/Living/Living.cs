using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Utility; 
using MonoGameLibrary.nodes.Items;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Living
{
    public abstract class Living : Entity
    {
        protected float Health;

        protected float MaxHealth;

        protected float GrabRange = 192;

        private Vector2 _grabPosition;

        protected virtual Vector2 GrabPosition
        {
            get { return _grabPosition; }
            set { _grabPosition = value; }
        }

        protected bool Alive => Health > 0;

        protected Item Equiped;

        public Living(ref World _world, float health, string _ID, Vector2 _position, float _rotation = 0, SpriteType _spriteType = SpriteType.Static) : base(ref _world, _ID, _position, _rotation, _spriteType)
        {
            Health = health;
            MaxHealth = health;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }


        protected void Die()
        {

        }

        protected virtual void Grab()
        {
            Item closest = null;
            float closestDist = float.MaxValue;

            foreach (Body b in world.BodyList)
            {
                // Skip if this body belongs to the living entity itself
                if (b == body) continue;

                // Only care about bodies whose UserData is an unequipped Item
                if (b.Tag is not Item item) continue;
                if (item.Held) continue;

                float dist = Vector2.Distance(body.Position, b.Position);
                if (dist <= GrabRange && dist < closestDist)
                {
                    closestDist = dist;
                    closest = item;
                }
            }

            if (closest != null)
                Equip(closest);
        }

        protected virtual void Equip(Item item)
        {
            item.PickUp(this);
            Equiped = item;
        }

    }
}
