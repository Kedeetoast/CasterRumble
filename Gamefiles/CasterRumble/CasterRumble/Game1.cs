using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using MonoGameLibrary.General.Utility;
using MonoGameLibrary.General.Scenes;
using nkast.Aether.Physics2D.Dynamics;
using System;
using CasterRumble.Scenes;
//using System.Drawing;

namespace CasterRumble

{
    public class Game1 : Core
    {

        private BasicEffect _spriteBatchEffect;



        // Defines the slime animated sprite.
        private Sprite _slime;

        // Defines the bat animated sprite.
        private Sprite _bat;

        public Game1() : base("Dungeon Slime", 1280, 720, false)
        {
            
        }

        protected override void Initialize()
        {


            base.Initialize();
            SetActions();

;
        }

        protected override void LoadContent()
        {

            System.Diagnostics.Debug.WriteLine("[Debug] Loading content for Game1.");
            ChangeScene(new TestScene());

            //// Create the texture atlas from the XML configuration file
            //TextureAtlas atlas = TextureAtlas.FromFile(Content, "Images/Spritesheet/Atlas_definition/defSpr_atlas");

            //// Create the slime animated sprite from the atlas.
            //_slime = atlas.CreateAnimatedSprite("slime-animation");
            //_slime.Scale = new Vector2(4.0f, 4.0f);

            //// Create the bat animated sprite from the atlas.
            //_bat = atlas.CreateAnimatedSprite("bat-animation");
            //_bat.Position = new Vector2(_slime.Width + 10, 0); // Position the bat 10px to the right of the slime.
            //_bat.Scale = new Vector2(4.0f, 4.0f);
        }

        //protected override void Update(GameTime gameTime)
        //{
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //        Exit();


        //    // TODO: Add your update logic here
        //    GameManager.Instance.UpdateEventCall(gameTime);

        //    base.Update(gameTime);
        //}

        //protected override void Draw(GameTime gameTime)
        //{
        //    // Clear the back buffer.
        //    GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // Begin the sprite batch to prepare for rendering.
        //    SpriteBatch.Begin(samplerState: SamplerState.PointClamp);



        //    base.Draw(gameTime);

        //    // Always end the sprite batch when finished.
        //    SpriteBatch.End();
        //}

        private void SetActions()
        {

            InputManager.Instance.Add_Action("Move_Left");
            InputManager.Instance.Add_Action("Move_Right");
            InputManager.Instance.Add_Action("Jump");
            InputManager.Instance.Add_Action("Menu");
            InputManager.Instance.Add_Action("Atk_Light");
            InputManager.Instance.Add_Action("Atk_Heavy");
            InputManager.Instance.Add_Action("Atk_Block");
            InputManager.Instance.Add_Action("Skill_1");
            InputManager.Instance.Add_Action("Skill_2");
            InputManager.Instance.Add_Action("Skill_3");
            InputManager.Instance.Add_Action("Skill_4");
            InputManager.Instance.Add_Action("Discard_Skill");
            InputManager.Instance.Add_Action("Discard_Weapon");

            InputManager.Instance.Add_input("Move_Left", Keys.A);
            InputManager.Instance.Add_input("Move_Left", Buttons.LeftThumbstickLeft);
            InputManager.Instance.Add_input("Move_Right", Keys.D);
            InputManager.Instance.Add_input("Move_Right", Buttons.LeftThumbstickRight);
            InputManager.Instance.Add_input("Jump", Keys.Space);
            InputManager.Instance.Add_input("Jump", Buttons.A);
            InputManager.Instance.Add_input("Menu", Keys.Escape);
            InputManager.Instance.Add_input("Menu", Buttons.Start);
            InputManager.Instance.Add_input("Atk_Light", MouseButtons.Left);
            InputManager.Instance.Add_input("Atk_Light", Buttons.X);
            InputManager.Instance.Add_input("Atk_Heavy", MouseButtons.Middle);
            InputManager.Instance.Add_input("Atk_Heavy", Buttons.Y);
            InputManager.Instance.Add_input("Atk_Block", MouseButtons.Right);
            InputManager.Instance.Add_input("Atk_Block", Buttons.RightStick);
            InputManager.Instance.Add_input("Skill_1", Keys.D1);
            InputManager.Instance.Add_input("Skill_1", Buttons.DPadUp);
            InputManager.Instance.Add_input("Skill_2", Keys.D2);
            InputManager.Instance.Add_input("Skill_2", Buttons.DPadLeft);
            InputManager.Instance.Add_input("Skill_3", Keys.D3);
            InputManager.Instance.Add_input("Skill_3", Buttons.DPadRight);
            InputManager.Instance.Add_input("Skill_4", Keys.D4);
            InputManager.Instance.Add_input("Skill_4", Buttons.DPadDown);
            InputManager.Instance.Add_input("Discard_Skill", Keys.LeftControl);
            InputManager.Instance.Add_input("Discard_Skill", Buttons.RightShoulder);
            InputManager.Instance.Add_input("Discard_Weapon", Keys.R);
            InputManager.Instance.Add_input("Discard_Weapon", Buttons.LeftShoulder);

        }
    }
}
