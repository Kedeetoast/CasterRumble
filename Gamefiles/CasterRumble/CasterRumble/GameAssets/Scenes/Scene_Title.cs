using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_Title : Scene
    {
        private TitleState _currentTitleState = TitleState.TitleScreen;
        private string _uiSoundEffect = "Sfx_ui_select";
        private Panel _titleScreenButtonsPanel;
        private Panel _optionsPanel;
        private Panel _CastopediaPanel;
        private Button _SettingsButton;
        private Button _CastopediaButton;
        private Button _optionsBackButton;
        private TextItem Caster;
        private TextItem Rumble;


        public override void Initialize()
        {
            BackgroundColor = new Color(255, 200, 112);
            base.Initialize();
            InitializeUI();
        }

        protected override void LoadContent()
        {
            Caster = new TextItem("Caster", "Fnt_04B_30_5x");
            Caster.Position = new Vector2(50, 40);
            Caster.Color = Color.Red;

            Rumble = new TextItem("Rumble", "Fnt_04B_30_5x");
            Rumble.Position = new Vector2(100, 140);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_currentTitleState == TitleState.TitleScreen)
            {
                Caster.IsVisible = true;
                Rumble.IsVisible = true;
            }
            else if (_currentTitleState == TitleState.Castopedia)
            {
                Caster.IsVisible = false;
                Rumble.IsVisible = false;
                _CastopediaPanel.IsVisible = true;
                _optionsPanel.IsVisible = false;
            }

            else if (_currentTitleState == TitleState.Settings)
            {
                Caster.IsVisible = false;
                Rumble.IsVisible = false;
                _CastopediaPanel.IsVisible = false;
                _optionsPanel.IsVisible = true;
            }

            GumService.Default.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_currentTitleState == TitleState.TitleScreen)
            {
                Caster.IsVisible = true;
                Rumble.IsVisible = true;
                _titleScreenButtonsPanel.IsVisible = true;
                _CastopediaPanel.IsVisible = false;
                _optionsPanel.IsVisible = false;
            }
            else if (_currentTitleState == TitleState.Castopedia)
            {
                Caster.IsVisible = false;
                Rumble.IsVisible = false;
                _titleScreenButtonsPanel.IsVisible = false;
                _CastopediaPanel.IsVisible = true;
                _optionsPanel.IsVisible = false;
            }

            else if (_currentTitleState == TitleState.Settings)
            {
                Caster.IsVisible = false;
                Rumble.IsVisible = false;
                _titleScreenButtonsPanel.IsVisible = false;
                _CastopediaPanel.IsVisible = false;
                _optionsPanel.IsVisible = true;
            }

            GumService.Default.Draw();
        }



        private void CreateTitlePanel()
        {
            // Create a container to hold all of our buttons
            _titleScreenButtonsPanel = new Panel();
            _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _titleScreenButtonsPanel.AddToRoot();

            var startButton = new Button();
            startButton.Anchor(Gum.Wireframe.Anchor.Left);
            startButton.X = 15;
            startButton.Y = -12;
            startButton.Width = 100;
            startButton.Height = -2;
            startButton.Text = "Start";
            startButton.Click += HandleStartClicked;
            _titleScreenButtonsPanel.AddChild(startButton);

            var exitButton = new Button();
            exitButton.Anchor(Gum.Wireframe.Anchor.Left);
            exitButton.X = 30;
            exitButton.Y = 69;
            exitButton.Width = 100;
            exitButton.Height = -2;
            exitButton.Text = "Exit";
            exitButton.Click += HandleExitClicked;
            _titleScreenButtonsPanel.AddChild(exitButton);

            _SettingsButton = new Button();
            _SettingsButton.Anchor(Gum.Wireframe.Anchor.Left);
            _SettingsButton.X = 25;
            _SettingsButton.Y = 42;
            _SettingsButton.Width = 100;
            _SettingsButton.Height = -2;
            _SettingsButton.Text = "Options";
            _SettingsButton.Click += HandleSettingsClicked;
            _titleScreenButtonsPanel.AddChild(_SettingsButton);

            _CastopediaButton = new Button();
            _CastopediaButton.Anchor(Gum.Wireframe.Anchor.Left);
            _CastopediaButton.X = 20;
            _CastopediaButton.Y = 15;
            _CastopediaButton.Width = 100;
            _CastopediaButton.Height = -2;
            _CastopediaButton.Text = "Castopedia";
            _CastopediaButton.Click += HandleCastopediaClicked;
            _titleScreenButtonsPanel.AddChild(_CastopediaButton);

            startButton.IsFocused = true;
        }

        private void HandleStartClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);

            // Change to the game scene to start the game.
            SceneManager.ChangeScene(new Scene_GameMenu());
        }

        private void HandleExitClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);

            // Exit game
            Core.Instance.Exit();
        }

        private void HandleSettingsClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);
            _currentTitleState = TitleState.Settings;
        //    // Set the title panel to be invisible.
        //    _titleScreenButtonsPanel.IsVisible = false;

        //    // Set the options panel to be visible.
        //    _optionsPanel.IsVisible = true;

        //    // Give the back button on the options panel focus.
        //    _optionsBackButton.IsFocused = true;
        }

        private void HandleCastopediaClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);
            _currentTitleState = TitleState.Castopedia;
            //// Set the title panel to be invisible.
            //_titleScreenButtonsPanel.IsVisible = false;

            //// Set the options panel to be visible.
            //_optionsPanel.IsVisible = true;

            //// Give the back button on the options panel focus.
            //_optionsBackButton.IsFocused = true;
        }

        private void HandleCustomizeButton(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);

            // Change to the game scene to start the game.
            SceneManager.ChangeScene(new Scene_Customise());
        }

        private void CreateOptionsPanel()
        {
            _optionsPanel = new Panel();
            _optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _optionsPanel.IsVisible = false;
            _optionsPanel.AddToRoot();

            var colouredRectangle = new ColoredRectangleRuntime();
            colouredRectangle.Anchor(Gum.Wireframe.Anchor.TopLeft);
            colouredRectangle.X = 10;
            colouredRectangle.Y = 10;
            colouredRectangle.Width = 200;
            colouredRectangle.Height = 100;
            colouredRectangle.Color = Microsoft.Xna.Framework.Color.Gray;
            _optionsPanel.AddChild(colouredRectangle);

            var optionsText = new TextRuntime();
            optionsText.FontSize = 12;
            optionsText.X = 10;
            optionsText.Y = 10;
            optionsText.Text = "OPTIONS";
            _optionsPanel.AddChild(optionsText);

            var musicLabel = new Label();
            musicLabel.Text = "Music";
            musicLabel.X = 20;
            musicLabel.Y = 35;
            _optionsPanel.AddChild(musicLabel);

            var MasterSlider = new Slider();
            MasterSlider.Anchor(Gum.Wireframe.Anchor.Top);
            MasterSlider.Y = 30f;
            MasterSlider.Minimum = 0;
            MasterSlider.Maximum = 1;
            MasterSlider.Value = AudioManager.Instance.Volume_Master;
            MasterSlider.SmallChange = .1;
            MasterSlider.LargeChange = .2;
            MasterSlider.ValueChanged += HandleMasterSliderValueChanged;
            MasterSlider.ValueChangeCompleted += HandleMasterSliderValueChangeCompleted;
            _optionsPanel.AddChild(MasterSlider);

            var musicSlider = new Slider();
            musicSlider.Anchor(Gum.Wireframe.Anchor.Top);
            musicSlider.Y = 30f;
            musicSlider.Minimum = 0;
            musicSlider.Maximum = 1;
            musicSlider.Value = AudioManager.Instance.Volume_Music;
            musicSlider.SmallChange = .1;
            musicSlider.LargeChange = .2;
            musicSlider.ValueChanged += HandleMusicSliderValueChanged;
            musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
            _optionsPanel.AddChild(musicSlider);

            var sfxLabel = new Label();
            sfxLabel.Text = "SFX";
            sfxLabel.X = 20;
            sfxLabel.Y = 80;
            _optionsPanel.AddChild(sfxLabel);

            var sfxSlider = new Slider();
            sfxSlider.Anchor(Gum.Wireframe.Anchor.Top);
            sfxSlider.Y = 93;
            sfxSlider.Minimum = 0;
            sfxSlider.Maximum = 1;
            sfxSlider.Value = AudioManager.Instance.Volume_SFX;
            sfxSlider.SmallChange = .1;
            sfxSlider.LargeChange = .2;
            sfxSlider.ValueChanged += HandleSfxSliderChanged;
            sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;
            _optionsPanel.AddChild(sfxSlider);

            _optionsBackButton = new Button();
            _optionsBackButton.Text = "BACK";
            _optionsBackButton.Width = 70;
            _optionsBackButton.Height = -10;
            _optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            _optionsBackButton.X = 18f;
            _optionsBackButton.Y = -10f;
            _optionsBackButton.Click += HandleOptionsButtonBack;
            _optionsPanel.AddChild(_optionsBackButton);

            var CustomizeButton = new Button();
            CustomizeButton.Text = "Customize";
            CustomizeButton.Width = 70;
            CustomizeButton.Height = -10;
            CustomizeButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            CustomizeButton.X = -18f;
            CustomizeButton.Y = -10f;
            CustomizeButton.Click += HandleCustomizeButton;
            _optionsPanel.AddChild(CustomizeButton);

            var textbox = new TextBox();
            textbox.Anchor(Gum.Wireframe.Anchor.Bottom);
            textbox.X = 0f;
            textbox.Y = -10f;
            textbox.Width = 120;
            textbox.Height = 20;
            textbox.Placeholder = "Enter Port...";
            textbox.TextChanged += textbox_TextChanged;

            _optionsPanel.AddChild(textbox);
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = KeepOnlyNumbers(textbox.Text);
            if (textbox.Text.Length > 5)
            {
                textbox.Text = textbox.Text.Substring(0,5);

                
                
            }

            int x;
            if (textbox.Text.Length == 0)
            {
                x = 0;
            }
            else
            {
                x = int.Parse(textbox.Text);
            }

            GameManager.Instance.port = SafeParsePort(x);
            textbox.Text = GameManager.Instance.port.ToString(); // Update the textbox to reflect the actual port being used
            System.Diagnostics.Debug.WriteLine($"Port set to: {textbox.Text}");
        }

        private int SafeParsePort(int input)
        {

            if (input >= 0 && input <= 65535)
                {
                    if ((input <= 1023 || input > 49151) && input.ToString().Length >=4 ) 
                    {
                        System.Diagnostics.Debug.WriteLine($"Port set to unsafe port {input}");
                        return GameManager.Instance.port; // Return a default safe port if the input is out of range
                }

                return input;
                }
            System.Diagnostics.Debug.WriteLine($"Port set to unsafe port {input}");
            return GameManager.Instance.port; // Return current port if parsing fails or out of range
        }

        public static string KeepOnlyNumbers(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

        private void HandleSfxSliderChanged(object sender, EventArgs args)
        {
            // Intentionally not playing the UI sound effect here so that it is not
            // constantly triggered as the user adjusts the slider's thumb on the
            // track.

            // Get a reference to the sender as a Slider.
            var slider = (Slider)sender;

            // Set the global sound effect volume to the value of the slider.;
            AudioManager.Instance.Volume_SFX = (float)slider.Value;
        }

        private void HandleSfxSliderChangeCompleted(object sender, EventArgs e)
        {
            // Play the UI Sound effect so the player can hear the difference in audio.
            PlaySFX(_uiSoundEffect);
        }

        private void HandleMusicSliderValueChanged(object sender, EventArgs args)
        {
            // Intentionally not playing the UI sound effect here so that it is not
            // constantly triggered as the user adjusts the slider's thumb on the
            // track.

            // Get a reference to the sender as a Slider.
            var slider = (Slider)sender;

            // Set the global song volume to the value of the slider.
            AudioManager.Instance.Volume_Music = (float)slider.Value;
        }

        private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs args)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);
        }

        private void HandleMasterSliderValueChanged(object sender, EventArgs args)
        {
            // Intentionally not playing the UI sound effect here so that it is not
            // constantly triggered as the user adjusts the slider's thumb on the
            // track.

            // Get a reference to the sender as a Slider.
            var slider = (Slider)sender;

            // Set the global master volume to the value of the slider.
            AudioManager.Instance.Volume_Master = (float)slider.Value;
        }

        private void HandleMasterSliderValueChangeCompleted(object sender, EventArgs args)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);
        }

        private void HandleOptionsButtonBack(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            PlaySFX(_uiSoundEffect);
            _currentTitleState = TitleState.TitleScreen;
            //// Set the title panel to be visible.
            //_titleScreenButtonsPanel.IsVisible = true;

            //// Set the options panel to be invisible.
            //_optionsPanel.IsVisible = false;

            //// Give the options button on the title panel focus since we are coming
            //// back from the options screen.
            //_SettingsButton.IsFocused = true;
        }

        private void CreateCastopediaPanel()
        {
            _CastopediaPanel = new Panel();
            _optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _optionsPanel.IsVisible = false;
            _optionsPanel.AddToRoot();
        }
        private void InitializeUI()
        {
            // Clear out any previous UI in case we came here from
            // a different screen:
            GumService.Default.Root.Children.Clear();

            CreateTitlePanel();
            CreateOptionsPanel();
            CreateCastopediaPanel();

        }

        private enum TitleState
        {
            TitleScreen,
            Settings,
            Castopedia

        }

        private enum CastopediaState
        {
            Weapons,
            Skills

        }
    }
}
