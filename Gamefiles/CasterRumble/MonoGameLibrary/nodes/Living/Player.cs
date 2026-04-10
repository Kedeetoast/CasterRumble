using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Dynamics;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Living
{
    public class Player : Living
    {



        public InputManager Input => InputManager.Instance;

        private float MovementDirection => Input.Check_Action_signal("Move_Right") - Input.Check_Action_signal("Move_Left");

        private float MovementSpeed = 100f;

        private float Deceleration = 0.1f;


        public Player(ref World _world, EntityList _entityList,float _health, string _ID, Vector2 _position, float _rotation = 0) : base(ref _world, _health, _ID, _position, _rotation, SpriteType.Animated_SpriteSet)
        {
            
        }


        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("[Logger] Event received! clicked at {e._ClickedAt:HH:mm:ss}");
            Actions(gameTime);

        }


        private void Actions(GameTime _Gametime)
        {
            float DeltaTime = (float)_Gametime.ElapsedGameTime.TotalSeconds;

            if (MovementDirection != 0)
                Velocity = new Vector2(MovementDirection * MovementSpeed * DeltaTime, Velocity.Y);
            else 
                Velocity = new Vector2(MoveTowards(Velocity.X,0,Deceleration), Velocity.Y);

            if (Input.Check_Action_Just_Pressed("Jump"))
            {

            }
        }


        private float MoveTowards(float current, float target, float maxDelta)
        {
            if (Math.Abs(target - current) <= maxDelta)
                return target;
            return current + Math.Sign(target - current) * maxDelta;

        }
    }
}
