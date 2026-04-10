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

    }
}
