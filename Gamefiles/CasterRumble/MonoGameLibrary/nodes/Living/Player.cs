using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using MonoGameLibrary.nodes.Casts;
using MonoGameLibrary.nodes.Items;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Dynamics.Contacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Timers;

namespace MonoGameLibrary.nodes.Living
{
    public class Player : Living
    {

        public override Vector2 Gravity
        {
            get
            {
                if (IsOnGround())
                    return Vector2.Zero;

                if (Velocity.Y <= 0)
                {
                    // Rising — apply reduced gravity for floatier jump arc
                    return new Vector2(0, GameManager.Instance.Gravity.Y * 0.2f);
                }
                else
                {
                    // Falling — apply stronger gravity for snappy landing
                    return new Vector2(0, GameManager.Instance.Gravity.Y * 0.035f);
                }
            }
        }

        private float _lastAimAngle = 0f;


        public float Aiming
        {
            get
            {
                if (Input.State == InputState.Keyboard)
                {
                    _lastAimAngle = MathF.Atan2(Input.MouseWorldPos.Y - Position.Y, Input.MouseWorldPos.X - Position.X);
                    return _lastAimAngle;
                }

                if (Input.State == InputState.Gamepad)
                {
                    Vector2 stick = Input.GetThumbstickDirection(ThumbStick.Right);
                    if (stick == Vector2.Zero) return _lastAimAngle;
                    _lastAimAngle = MathF.Atan2(stick.Y, stick.X);
                    return _lastAimAngle;
                }

                return _lastAimAngle;
            }
        }

        protected override Vector2 GrabPosition
        {
            get 
            {
                Vector2 direction = new Vector2(
                (float)Math.Cos(Aiming),
                (float)Math.Sin(Aiming)
                );
                return direction* GrabRange; 
            }
        }

        public InputManager Input => InputManager.Instance;

        private float MovementDirection => Input.Check_Action_signal("Move_Right") - Input.Check_Action_signal("Move_Left");

        private float MovementSpeed = 1.7f;
        private float Jumpforce = 4f;
        private float Deceleration = 0.15f;
        private float CyoteTimeLength = 1f;

        private Boolean CyoteCheck = true;
        private Boolean CyoteActive = false;
        private Timer CyoteTimer = new Timer();

        private Sprite Arms;
        private Sprite FaceSprite;

        private FixedArray4<Cast> Casts = new FixedArray4<Cast>();



        public Player(ref World _world, float _health, Vector2 _position, float _rotation = 0) : base(ref _world, _health, "Player", _position, _rotation, SpriteType.Animated_SpriteSet)
        {
            Mass = 1;
            body.FixedRotation = true;
            body.IgnoreGravity = true;
            CyoteTimer.Elapsed += CyoteTimeout;
        }


        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
            ApplyGravity();
            UpdateAnimation();
            Inputs(gameTime);
            //ApplyGravity();

            if (!IsOnGround() && !CyoteCheck)
            {
                CyoteTime();
            }

            if (IsOnGround())
            {
                CyoteActive = false;
                CyoteCheck = false;
                CyoteTimer.Enabled = false;

            }

            if (Equiped != null)
            {
                Equiped.Rotation = Aiming;
            }

        }

        private void ApplyGravity()
        {
            if (!IsOnGround() && !CyoteActive)
            {
                body.ApplyLinearImpulse(Gravity * Mass);
            }
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
                Velocity = new Vector2(MoveTowards(Velocity.X, 0, Deceleration), Velocity.Y); // zero Y on ground
            }

            if (Input.Check_Action_Just_Pressed("Jump") &&( IsOnGround() || CyoteActive))
            {
                //System.Diagnostics.Debug.WriteLine("Jump");
                CyoteActive = false;
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

        public void SetCustomization()
        {

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
            sprite.Color = GameManager.Instance.PlayerColor;
            TextureAtlas atlas = TextureAtlas.FromFile(GameManager.Instance.Scene_Content, GameManager.Instance.EntityList.entityList[ID].SpriteAtlasPath);
            var FaceAtlas = TextureAtlas.FromFile(GameManager.Instance.Scene_Content, "Images/Spritesheet/Atlas_definition/defSpr_Faces");
            var lst = new List<string> { "playerIdle_Arms-animation", "playerRunning_Arms-animation", "playerThrow_Arms-animation", "playerFall_Arms-animation", "playerJump_Arms-animation" };

            Arms = atlas.CreateAnimatedSprite_spriteset(lst);
            Arms.Parent = this;
            Arms.Color = GameManager.Instance.PlayerColor;
            FaceSprite = FaceAtlas.CreateSprite_spriteset();
            FaceSprite.ChangeActive($"Face-{GameManager.Instance.Face}");
            FaceSprite.LayerDepth = 1f;
            FaceSprite.Parent = this;

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

        private void CyoteTime()
        {
            CyoteCheck = true;
            CyoteActive = true;
            CyoteTimer.Interval = CyoteTimeLength*1000;
            CyoteTimer.Enabled = true;

        }

        private void CyoteTimeout(Object source, ElapsedEventArgs e) 
        {
            CyoteActive = false;
        }


    }
}
