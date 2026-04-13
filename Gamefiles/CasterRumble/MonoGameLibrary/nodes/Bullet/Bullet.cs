using Microsoft.Xna.Framework;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Bullet
{
    public class Bullet : Entity
    {
        private float speed = 500f; // Speed of the bullet in pixels per second

        private Vector2 direction; // Direction the bullet is moving in

        private float distanceTraveled = 0f; // Distance the bullet has traveled so far

        private BulletAtributes attributes; // Additional attributes for the bullet


        public Bullet(ref World _world, Vector2 _position,int _rotation, Vector2 _direction) : base(ref _world,"Bullet",_position, _rotation)
        {
            this.direction = Vector2.Normalize(_direction);
        }
    }
}

public enum BulletTraits
{
    None,
    Piercing,
    Explosive, // Causes an explosion on impact
    Incendiary, // Sets targets on fire
    HighVelocity, // Travels faster than normal bullets
    Corrosive = 1 << 7, // Deals damage over time by corroding targets
    Electric = 1 << 8, // Deals electric damage and can stun targets
}