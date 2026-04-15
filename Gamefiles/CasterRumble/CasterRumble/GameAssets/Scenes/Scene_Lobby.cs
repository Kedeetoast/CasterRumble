using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_Lobby : Scene
    {
        private Panel MainPanel;
        private Label KeyLabel;
        private Panel PlayerListPanel;
        private List<Label> PlayerLabels = new List<Label>();

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
            RefreshPlayerList();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GumService.Default.Draw();
        }

        private void InitializeUI()
        {
            GumService.Default.Root.Children.Clear();
            CreateMainPanel();
        }

        private void CreateMainPanel()
        {
            MainPanel = new Panel();
            MainPanel.Dock(Gum.Wireframe.Dock.Fill);
            MainPanel.AddToRoot();

            // Title
            var TitleLabel = new Label();
            TitleLabel.Anchor(Gum.Wireframe.Anchor.Top);
            TitleLabel.X = 0;
            TitleLabel.Y = 20;
            TitleLabel.Text = "Lobby";
            MainPanel.AddChild(TitleLabel);

            // Key display
            KeyLabel = new Label();
            KeyLabel.Anchor(Gum.Wireframe.Anchor.Top);
            KeyLabel.X = 0;
            KeyLabel.Y = 50;
            KeyLabel.Text = $"Key: {NetworkManager.Instance.Key}";
            MainPanel.AddChild(KeyLabel);

            // Player list container
            PlayerListPanel = new Panel();
            PlayerListPanel.Anchor(Gum.Wireframe.Anchor.Center);
            PlayerListPanel.X = 0;
            PlayerListPanel.Y = 0;
            PlayerListPanel.Width = 200;
            PlayerListPanel.Height = 300;
            MainPanel.AddChild(PlayerListPanel);

            // Start button (server only)
            if (NetworkManager.Instance.Authority == Authority.Server)
            {
                var StartButton = new Button();
                StartButton.Anchor(Gum.Wireframe.Anchor.Bottom);
                StartButton.X = 0;
                StartButton.Y = -20;
                StartButton.Width = 100;
                StartButton.Height = -2;
                StartButton.Text = "Start Game";
                StartButton.Click += StartButtonPressed;
                MainPanel.AddChild(StartButton);
            }

            // Back button
            var BackButton = new Button();
            BackButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            BackButton.X = 18;
            BackButton.Y = -10;
            BackButton.Width = 70;
            BackButton.Height = -10;
            BackButton.Text = "BACK";
            BackButton.Click += BackButtonPressed;
            MainPanel.AddChild(BackButton);

            // Key display
            KeyLabel = new Label();
            KeyLabel.Anchor(Gum.Wireframe.Anchor.Top);
            KeyLabel.X = 0;
            KeyLabel.Y = 50;
            KeyLabel.Text = $"Key: {NetworkManager.Instance.Key}";
            MainPanel.AddChild(KeyLabel);

            // IP display
            var IPLabel = new Label();
            IPLabel.Anchor(Gum.Wireframe.Anchor.Top);
            IPLabel.X = 0;
            IPLabel.Y = 75;
            IPLabel.Text = $"IP: {System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)}";
            MainPanel.AddChild(IPLabel);

            // Port display
            var PortLabel = new Label();
            PortLabel.Anchor(Gum.Wireframe.Anchor.Top);
            PortLabel.X = 0;
            PortLabel.Y = 100;
            PortLabel.Text = $"Port: {GameManager.Instance.port}";
            MainPanel.AddChild(PortLabel);
        }

        private void RefreshPlayerList()
        {
            // Clear old labels
            foreach (var label in PlayerLabels)
                PlayerListPanel.RemoveChild(label);
            PlayerLabels.Clear();

            // Rebuild from current player list
            var players = NetworkManager.Instance.ConnectedPlayers;
            for (int i = 0; i < players.Count; i++)
            {
                var label = new Label();
                label.Anchor(Gum.Wireframe.Anchor.Top);
                label.X = 0;
                label.Y = i * 30;
                label.Text = $"Player {i + 1}: {players[i]}";
                PlayerListPanel.AddChild(label);
                PlayerLabels.Add(label);
            }
        }

        private void StartButtonPressed(object sender, EventArgs e)
        {
            // SceneManager.Instance.ChangeScene(new Scene_Game());
        }

        private void BackButtonPressed(object sender, EventArgs e)
        {
            NetworkManager.Instance.Stop();
            SceneManager.Instance.ChangeScene(new Scene_GameMenu());
        }
    }
}
