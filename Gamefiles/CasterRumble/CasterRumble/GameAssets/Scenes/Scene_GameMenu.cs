using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_GameMenu : Scene
    {

        private MenuState _menuState = MenuState.Menu;
        private Panel MenuPanel;
        private Button JoinServerButton;
        private Panel JoinServerPanel;

        private string IPAddress;
        private string IPPort;

        public override void Initialize()
        {
            BackgroundColor = new Color(255, 200, 112);
            InitializeUI();
            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GumService.Default.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_menuState == MenuState.Menu)
            {
                MenuPanel.IsVisible = true;
                JoinServerPanel.IsVisible = false;
            }
            else if (_menuState == MenuState.JoinMenu)
            {
                MenuPanel.IsVisible = false;
                JoinServerPanel.IsVisible = true;

            }


            GumService.Default.Draw();
        }


        private void InitializeUI()
        {
            // Clear out any previous UI in case we came here from
            // a different screen:
            GumService.Default.Root.Children.Clear();


            CreateMenuPanel();
            CreateJoinServerPanel();
        }


        private void CreateMenuPanel()
        {
            MenuPanel = new Panel();
            MenuPanel.Name = "MenuPanel";
            MenuPanel.Dock(Gum.Wireframe.Dock.Fill);
            MenuPanel.AddToRoot();

            var _CreateServer = new Button();
            _CreateServer.Anchor(Gum.Wireframe.Anchor.Left);
            _CreateServer.X = 25;
            _CreateServer.Y = 0;
            _CreateServer.Width = 100;
            _CreateServer.Height = -2;
            _CreateServer.Text = "Create Server";
            _CreateServer.Click += HandleCreateServerClicked;
            MenuPanel.AddChild(_CreateServer);

            JoinServerButton = new Button();
            JoinServerButton.Anchor(Gum.Wireframe.Anchor.Right);
            JoinServerButton.X = -25;
            JoinServerButton.Y = 0;
            JoinServerButton.Width = 100;
            JoinServerButton.Height = -2;
            JoinServerButton.Text = "Join Server";
            JoinServerButton.Click += HandleJoinServerClicked;
            MenuPanel.AddChild(JoinServerButton);

            var TitleButtonBack = new Button();
            TitleButtonBack.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            TitleButtonBack.Text = "BACK";
            TitleButtonBack.Width = 70;
            TitleButtonBack.Height = -10;
            TitleButtonBack.X = 18f;
            TitleButtonBack.Y = -10f;
            TitleButtonBack.Click += TitleButtonBackPressed;
            MenuPanel.AddChild(TitleButtonBack);


            var TutorialButton = new Button();
            TutorialButton.Anchor(Gum.Wireframe.Anchor.Bottom);
            TutorialButton.Text = "Tutorial";
            TutorialButton.Width = 70;
            TutorialButton.Height = -10;
            TutorialButton.X = 0f;
            TutorialButton.Y = -20f;
            TutorialButton.Click += TutorialButtonPressed;
            MenuPanel.AddChild(TutorialButton);

        }

        private void TutorialButtonPressed(object sender, EventArgs e)
        {
            SceneManager.Instance.ChangeScene(new Scene_Tutorial());
        }

        private void HandleCreateServerClicked(object sender, EventArgs e)
        {
            NetworkManager.Instance.CreateServer(GameManager.Instance.port, 8);
            SceneManager.Instance.ChangeScene(new Scene_Lobby());
        }

        private void CreateJoinServerPanel()
        {
            JoinServerPanel = new Panel();
            JoinServerPanel.Name = "JoinServerPanel";
            JoinServerPanel.Dock(Gum.Wireframe.Dock.Fill);
            JoinServerPanel.AddToRoot();

            var _JoinServer = new Button();
            _JoinServer.Anchor(Gum.Wireframe.Anchor.Left);
            _JoinServer.X = 15;
            _JoinServer.Y = -12;
            _JoinServer.Width = 100;
            _JoinServer.Height = -2;
            _JoinServer.Text = "Join Server";
            _JoinServer.Click += HandleJoinServerClicked;
            JoinServerPanel.AddChild(_JoinServer);

            var Textbox_IP = new TextBox();
            Textbox_IP.Anchor(Gum.Wireframe.Anchor.TopRight);
            Textbox_IP.X = -25f;
            Textbox_IP.Y = 40f;
            Textbox_IP.Width = 120;
            Textbox_IP.Height = 20;
            Textbox_IP.Placeholder = "Enter IP...";
            Textbox_IP.TextChanged += Textbox_IPChanged;
            JoinServerPanel.AddChild(Textbox_IP);

            var Textbox_Port = new TextBox();
            Textbox_Port.Anchor(Gum.Wireframe.Anchor.BottomRight);
            Textbox_Port.X = -25f;
            Textbox_Port.Y = -40f;
            Textbox_Port.Width = 120;
            Textbox_Port.Height = 20;
            Textbox_Port.Placeholder = "Enter Port...";
            Textbox_Port.TextChanged += Textbox_PortChanged;
            JoinServerPanel.AddChild(Textbox_Port);

            var JoinButtonBack = new Button();
            JoinButtonBack = new Button();
            JoinButtonBack.Text = "BACK";
            JoinButtonBack.Width = 70;
            JoinButtonBack.Height = -10;
            JoinButtonBack.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            JoinButtonBack.X = 18f;
            JoinButtonBack.Y = -10f;
            JoinButtonBack.Click += JoinButtonBackPressed;
            JoinServerPanel.AddChild(JoinButtonBack );
        }

        private void Textbox_IPChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            IPPort = textbox.Text;
        }

        private void Textbox_PortChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = KeepOnlyNumbers(textbox.Text);
            if (textbox.Text.Length > 5)
            {
                textbox.Text = textbox.Text.Substring(0, 5);



            }
            IPPort = textbox.Text;
        }

        public static string KeepOnlyNumbers(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

        private void HandleJoinServerClicked(object sender, EventArgs e)
        {
            _menuState = MenuState.JoinMenu;
        }

        private void JoinButtonBackPressed(object sender, EventArgs e)
        {

            _menuState = MenuState.Menu;
        }

        private void TitleButtonBackPressed(object sender, EventArgs e)
        {

            SceneManager.Instance.ChangeScene(new Scene_Title());
        }

        private enum MenuState
        {
            Menu,
            JoinMenu,

        }


    }


}