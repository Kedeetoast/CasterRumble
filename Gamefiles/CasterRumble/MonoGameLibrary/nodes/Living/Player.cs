using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using MonoGameLibrary.nodes.Casts;
using MonoGameLibrary.nodes.Items;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.nodes.Living
{
    public class Player : Living
    {



        public InputManager Input => InputManager.Instance;

        private float MovementDirection => Input.Check_Action_signal("Move_Right") - Input.Check_Action_signal("Move_Left");

        private float MovementSpeed = 125f;
        private float Jumpforce = 6000000f;
        private float Deceleration = 5f;

        private Sprite Arms;

        private FixedArray4<Cast> Casts = new FixedArray4<Cast>();



        public Player(ref World _world, float _health, Vector2 _position, float _rotation = 0) : base(ref _world, _health, "Player", _position, _rotation, SpriteType.Animated_SpriteSet)
        {
            Mass = 1;
            body.FixedRotation = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateAnimation();
            Inputs(gameTime);

        }


        private void Inputs(GameTime gameTime)
        {
            Actions(gameTime);
            itemLogic();
            SkillUsage();

        }

        private void Actions(GameTime _Gametime)
        {
            float DeltaTime = (float)_Gametime.ElapsedGameTime.TotalSeconds;

            if (MovementDirection != 0f)
            {

                Velocity = new Vector2(MovementDirection * MovementSpeed, Velocity.Y);
            }

            else if (MovementDirection == 0f && IsOnGround()) 
            {
                Velocity = new Vector2(MoveTowards(Velocity.X, 0, Deceleration), Velocity.Y);
            }

            if (Input.Check_Action_Just_Pressed("Jump") && IsOnGround())
            {
                System.Diagnostics.Debug.WriteLine("Jump");
                Velocity = new Vector2(Velocity.X, -Jumpforce); ;
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
                else
                {
                    Grab();
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

        private void SkillUsage()
        {
            if (Input.Check_Action_Just_Pressed("Skill_0") && Casts[0] != null && !Casts[0].On_Cooldown)
            {
                Casts[0].UseSkill();
            }
            if (Input.Check_Action_Just_Pressed("Skill_1") && Casts[1] != null && !Casts[1].On_Cooldown)
            {
                Casts[1].UseSkill();
            }
            if (Input.Check_Action_Just_Pressed("Skill_2") && Casts[2] != null && !Casts[2].On_Cooldown)
            {
                Casts[2].UseSkill();
            }
            if (Input.Check_Action_Just_Pressed("Skill_3") && Casts[3] != null && !Casts[3].On_Cooldown)
            {
                Casts[3].UseSkill();
            }
        }



        private float MoveTowards(float current, float target, float maxDelta)
        {
            if (Math.Abs(target - current) <= maxDelta)
                return target;
            return current + Math.Sign(target - current) * maxDelta;

        }

        protected override void LoadSubSprites()
        {
            TextureAtlas atlas = TextureAtlas.FromFile(GameManager.Instance.Scene_Content, GameManager.Instance.EntityList.entityList[ID].SpriteAtlasPath);

            var lst = new List<string> { "playerIdle_Arms-animation", "playerRunning_Arms-animation", "playerThrow_Arms-animation", "playerFall_Arms-animation", "playerJump_Arms-animation" };
            Arms = atlas.CreateAnimatedSprite_spriteset(lst);
            Arms.Parent = this;

        }

        public void UpdateAnimation()
        {

            if (Velocity.Y < 0 && sprite.SpriteSet.Playing != "playerJump-animation" && !IsOnGround())
            {
                sprite.ChangeActive("playerJump-animation");
                Arms.ChangeActive("playerJump_Arms-animation");
            }
            else if (Velocity.Y > 0 && sprite.SpriteSet.Playing != "playerFall-animation" && !IsOnGround())
            {
                sprite.ChangeActive("playerFall-animation");
                Arms.ChangeActive("playerFall_Arms-animation");
            }

            else if (Velocity.X != 0 && sprite.SpriteSet.Playing != "playerRunning-animation" && IsOnGround())
            {
                sprite.ChangeActive("playerRunning-animation");
                Arms.ChangeActive("playerRunning_Arms-animation");
            }
            else if (Velocity.X == 0 && sprite.SpriteSet.Playing != "playerIdle-animation" && IsOnGround())
            {
                sprite.ChangeActive("playerIdle-animation");
                Arms.ChangeActive("playerIdle_Arms-animation");
            }

            if (Velocity.X > 0)
            {
                sprite.Effects = SpriteEffects.None;
                Arms.Effects = SpriteEffects.None;
            }
            else if (Velocity.X < 0)
            {
                sprite.Effects = SpriteEffects.FlipHorizontally;
                Arms.Effects = SpriteEffects.FlipHorizontally;
            }

        }


    }
}
