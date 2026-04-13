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
using nkast.Aether.Physics2D.Common;
using MonoGameLibrary.nodes.skills;
using MonoGameLibrary.nodes.Items;

namespace MonoGameLibrary.nodes.Living
{
    public class Player : Living
    {



        public InputManager Input => InputManager.Instance;

        private float MovementDirection => Input.Check_Action_signal("Move_Right") - Input.Check_Action_signal("Move_Left");

        private float MovementSpeed = 100f;

        private float Deceleration = 0.1f;

        private FixedArray4<Skill> Skills = new FixedArray4<Skill>();



        public Player(ref World _world, float _health, Vector2 _position, float _rotation = 0) : base(ref _world, _health, "Player", _position, _rotation, SpriteType.Animated_SpriteSet)
        {
            
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Console.WriteLine("[Logger] Event received! clicked at {e._ClickedAt:HH:mm:ss}");
            Actions(gameTime);
            itemLogic();
            System.Diagnostics.Debug.WriteLine($"pos ({Position.X},{Position.Y})");

        }


        private void Actions(GameTime _Gametime)
        {
            float DeltaTime = (float)_Gametime.ElapsedGameTime.TotalSeconds;

            if (MovementDirection != 0)
            {
                //System.Diagnostics.Debug.WriteLine("should be moving");
                Velocity = new Vector2(MovementDirection * MovementSpeed * DeltaTime, Velocity.Y);
            }

            else
            {
                Velocity = new Vector2(MoveTowards(Velocity.X, 0, Deceleration), Velocity.Y);
            }

            if (Input.Check_Action_Just_Pressed("Jump"))
            {

            }
        }

        private void itemLogic()
        {
            if (Input.Check_Action_Just_Pressed("Use_Item"))
            {
                if (Equiped is Weapon heldWeapon)
                {
                    heldWeapon.LightAttack();
                }
                else  if (Equiped != null)
                {
                    Equiped.Use();
                    }
            }

            if (Input.Check_Action_Just_Pressed("Atk_Heavy"))
            {
                if (Equiped is Weapon heldWeapon)
                {
                    heldWeapon.HeavyAttack();
                }
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
