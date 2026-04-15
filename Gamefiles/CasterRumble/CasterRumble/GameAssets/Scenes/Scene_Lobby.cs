using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameLibrary.General.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_Lobby: Scene
    {

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


            GumService.Default.Draw();
        }


        private void InitializeUI()
        {
   
            GumService.Default.Root.Children.Clear();


            CreateMainPanel();
        }

        private void CreateMainPanel()
        {

        }

    }
}
