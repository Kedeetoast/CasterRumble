using Microsoft.Xna.Framework;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MonoGameLibrary.nodes.Bullet
{
    public class Bullet : Entity
    {
        private float speed = 500f; // Speed of the bullet in pixels per second

        private Vector2 _direction;

        private Vector2 Direction // Direction the bullet is moving in
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = Vector2.Normalize(value);
            }

        }

        private float distanceTraveled = 0f; // Distance the bullet has traveled so far

        private BulletAttributes BulletAttribute; // Additional attributes for the bullet

        private Boolean RangeReached = false;

        private float MaxTime = 5f;


        public Bullet(ref World _world, Vector2 _position,float _rotation, BulletAttributes _BulletAttribute) : base(ref _world,"Bullet",_position, _rotation)
        {

            Direction = new Vector2(
                (float)Math.Cos(_rotation),
                (float)Math.Sin(_rotation)
                );

            BulletAttribute = _BulletAttribute;
            body.IgnoreGravity = true;
            Velocity = Direction * speed;
            body.OnCollision += OnCollision;
        }

        public override void Update(GameTime gametime)
        {
            float DeltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            base.Update(gametime);
            distanceTraveled += DeltaTime * speed;
            if (distanceTraveled >= BulletAttribute.range && !RangeReached)
            {
                HandleRangeReached();
            }



        }

        private void HandleRangeReached()
        {
            RangeReached = true;
            body.IgnoreGravity = false;

            var time = new Timer(MaxTime * 1000);
            time.Elapsed += BulletTimeout;
            time.Enabled = true;

        }
        private void BulletTimeout(Object source, ElapsedEventArgs e)
        {
            Dispose();
        }

        private bool OnCollision(Fixture sender, Fixture other, nkast.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            // Check if the other body's tag is a Living 
            if (other.Body.Tag is Living.Living living)
            {
                living.TakeDamage(BulletAttribute.Damage);

                if (!BulletAttribute.Piercing)
                {
                    Dispose(); // Destroys the bullet
                }
            }

            // Return true to process the collision normally, false to ignore it
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                body.OnCollision -= OnCollision;
            }
            base.Dispose(disposing);
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