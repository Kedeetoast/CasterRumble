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
using CasterRumble.GameAssets.Scenes;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameLibrary;
using MonoGameGum;
using Microsoft.Xna.Framework.Media;
//using System.Drawing;

namespace CasterRumble

{
    public class Game1 : Core
    {




        public Game1() : base("Dungeon Slime", 1280, 720, false)
        {

        }

        protected override void Initialize()
        {
            ImgDirectory = "Images/Graphics/";
            FntDirectory = "Fonts/";
            MusDirectory = "Audio/Music/";
            SfxDirectory = "Audio/Sounds/";
            GameManager.Instance.port = 8888;

            base.Initialize();
            SetActions();
        }

        protected override void LoadContent()
        {
            InitializeGum();
            System.Diagnostics.Debug.WriteLine("[Debug] Loading content for Game1.");
            SceneManager.Instance.ChangeScene(new Scene_Tutorial());


        }

        private void InitializeGum()
        {
            // Initialize the Gum service. The second parameter specifies
            // the version of the default visuals to use. V3 is the latest
            // version.
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);

            // Tell the Gum service which content manager to use. We will tell it to
            // use the global content manager from our Core.
            GumService.Default.ContentLoader.XnaContentManager = Core.Content;

            // Register keyboard input for UI control.
            FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);

            // Register gamepad input for Ui control.
            FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);

            // Customize the tab reverse UI navigation to also trigger when the keyboard
            // Up arrow key is pushed.
            FrameworkElement.TabReverseKeyCombos.Add(
               new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Up });

            // Customize the tab UI navigation to also trigger when the keyboard
            // Down arrow key is pushed.
            FrameworkElement.TabKeyCombos.Add(
               new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Down });

            // The assets created for the UI were done so at 1/4th the size to keep the size of the
            // texture atlas small.  So we will set the default canvas size to be 1/4th the size of
            // the game's resolution then tell gum to zoom in by a factor of 4.
            GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 4.0f;
            GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 4.0f;
            GumService.Default.Renderer.Camera.Zoom = 4.0f;
        }


        private void SetActions()
        {
            System.Diagnostics.Debug.WriteLine("actions");
            InputManager.Instance.Add_Action("Move_Left");
            InputManager.Instance.Add_Action("Move_Right");
            InputManager.Instance.Add_Action("Jump");
            InputManager.Instance.Add_Action("Menu");
            InputManager.Instance.Add_Action("Use_Item");
            InputManager.Instance.Action_AltName("Use_Item", "PickUp");
            InputManager.Instance.Action_AltName("Use_Item", "Atk_Light");
            InputManager.Instance.Add_Action("Atk_Heavy");
            InputManager.Instance.Add_Action("Atk_Block");
            InputManager.Instance.Add_Action("Skill_0");
            InputManager.Instance.Add_Action("Skill_1");
            InputManager.Instance.Add_Action("Skill_2");
            InputManager.Instance.Add_Action("Skill_3");
            InputManager.Instance.Add_Action("Discard_Skill");
            InputManager.Instance.Add_Action("Discard_Item");

            InputManager.Instance.Add_input("Move_Left", Keys.A);
            InputManager.Instance.Add_input("Move_Left", Buttons.LeftThumbstickLeft);
            InputManager.Instance.Add_input("Move_Right", Keys.D);
            InputManager.Instance.Add_input("Move_Right", Buttons.LeftThumbstickRight);
            InputManager.Instance.Add_input("Jump", Keys.Space);
            InputManager.Instance.Add_input("Jump", Buttons.A);
            InputManager.Instance.Add_input("Menu", Keys.Escape);
            InputManager.Instance.Add_input("Menu", Buttons.Start);
            InputManager.Instance.Add_input("Use_Item", MouseButtons.Left);
            InputManager.Instance.Add_input("Use_Item", Buttons.X);
            InputManager.Instance.Add_input("Atk_Heavy", MouseButtons.Middle);
            InputManager.Instance.Add_input("Atk_Heavy", Buttons.Y);
            InputManager.Instance.Add_input("Atk_Block", MouseButtons.Right);
            InputManager.Instance.Add_input("Atk_Block", Buttons.RightStick);
            InputManager.Instance.Add_input("Skill_0", Keys.D1);
            InputManager.Instance.Add_input("Skill_0", Buttons.DPadUp);
            InputManager.Instance.Add_input("Skill_1", Keys.D2);
            InputManager.Instance.Add_input("Skill_1", Buttons.DPadLeft);
            InputManager.Instance.Add_input("Skill_2", Keys.D3);
            InputManager.Instance.Add_input("Skill_2", Buttons.DPadRight);
            InputManager.Instance.Add_input("Skill_3", Keys.D4);
            InputManager.Instance.Add_input("Skill_3", Buttons.DPadDown);
            InputManager.Instance.Add_input("Discard_Skill", Keys.LeftControl);
            InputManager.Instance.Add_input("Discard_Skill", Buttons.RightShoulder);
            InputManager.Instance.Add_input("Discard_Item", Keys.X);
            InputManager.Instance.Add_input("Discard_Item", Buttons.LeftShoulder);

        }
    }
}
