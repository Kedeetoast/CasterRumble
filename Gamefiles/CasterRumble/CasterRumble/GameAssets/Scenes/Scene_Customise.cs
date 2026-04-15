using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.General.Utility;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_Customise : Scene
    {
        private Sprite FaceSprite;
        private Sprite PlayerSprite;
        private Panel Panel_Main;

        public override void Initialize()
        {

            GumService.Default.Root.Children.Clear();


            BackgroundColor = new Color(255, 200, 112);
            InitializeUI();
            base.Initialize();

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GumService.Default.Draw();
        }

        public void InitializeUI()
        {
            CreatePanel_Main();
        }

        public override void Update(GameTime gameTime)
        {
            GumService.Default.Update(gameTime);
            PlayerSprite.Color = GameManager.Instance.PlayerColor;
            FaceSprite.ChangeActive($"Face-{GameManager.Instance.Face}");
        }

        private void CreatePanel_Main()
        {
            Panel_Main = new Panel();
            Panel_Main.Name = "Panel_Main";
            Panel_Main.Dock(Gum.Wireframe.Dock.Fill);
            Panel_Main.AddToRoot();

            var Textbox_Face = new TextBox();
            Textbox_Face.Anchor(Gum.Wireframe.Anchor.TopLeft);
            Textbox_Face.X = 25f;
            Textbox_Face.Y = 40f;
            Textbox_Face.Width = 120;
            Textbox_Face.Height = 20;
            Textbox_Face.Text = GameManager.Instance.Face.ToString();
            Textbox_Face.Placeholder = "enter number to select a face";
            Textbox_Face.TextChanged += FaceNum;
            Panel_Main.AddChild(Textbox_Face);

            var Textbox_Hex = new TextBox();
            Textbox_Hex.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            Textbox_Hex.X = 25f;
            Textbox_Hex.Y = -40f;
            Textbox_Hex.Width = 120;
            Textbox_Hex.Height = 20;
            Textbox_Hex.Text = $"#{GameManager.Instance.PlayerColor.R:X2}{GameManager.Instance.PlayerColor.G:X2}{GameManager.Instance.PlayerColor.B:X2}"; 
            Textbox_Hex.Placeholder = "Select Colour (HEX)";
            Textbox_Hex.TextChanged += HexNum;
            Panel_Main.AddChild(Textbox_Hex);

            var TitleButtonBack = new Button();
            TitleButtonBack.Anchor(Gum.Wireframe.Anchor.Bottom);
            TitleButtonBack.Text = "BACK";
            TitleButtonBack.Width = 70;
            TitleButtonBack.Height = -10;
            TitleButtonBack.X = 0f;
            TitleButtonBack.Y = -10f;
            TitleButtonBack.Click += TitleButtonBackPressed;
            Panel_Main.AddChild(TitleButtonBack);
        }

        private void TitleButtonBackPressed(object sender, EventArgs e)
        {

            SceneManager.Instance.ChangeScene(new Scene_Title());
        }

        protected override void LoadContent()
        {
            var FaceAtlas = TextureAtlas.FromFile(Content, "Images/Spritesheet/Atlas_definition/defSpr_Faces");
            var Atlas = TextureAtlas.FromFile(Content, "Images/Spritesheet/Atlas_definition/defSpr_player");
            FaceSprite = FaceAtlas.CreateSprite_spriteset();
            PlayerSprite = Atlas.CreateAnimatedSprite("playerIdle-animation");
            PlayerSprite.Position = new Vector2(960, 360);
            PlayerSprite.Scale = Vector2.One * 5;
            PlayerSprite.Color = GameManager.Instance.PlayerColor;
            FaceSprite.ChangeActive($"Face-{GameManager.Instance.Face}");
            FaceSprite.LayerDepth = 1f;
            FaceSprite.Position = new Vector2(960, 360);
            FaceSprite.Scale = Vector2.One * 5;
        }



        private void FaceNum(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = Utility.KeepOnlyNumbers(textbox.Text);
            if (textbox.Text != "")
            { 
                int x = Int32.Parse(textbox.Text);
                if (x < 1)
                {
                    textbox.Text = "1";
                }
                else if (x > 8)
                {
                    textbox.Text = "8";
                }
                x = Int32.Parse(textbox.Text);
                GameManager.Instance.Face = x;
            }
        }

        private void HexNum(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;

            textbox.Text = CleanHex(textbox.Text);

            if (textbox.Text.Length > 6)
            {

            }

            if (textbox.Text.Length == 6)
            {
                var colour = Utility.FromHex(textbox.Text);
                GameManager.Instance.PlayerColor = colour;
            }

        }

        static string CleanHex(string s) =>
            Regex.Replace(s, @"[^0-9A-Fa-f]", "");


    }
}
