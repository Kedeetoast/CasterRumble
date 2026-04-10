using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.Graphics.SpriteClass;
using MonoGameLibrary.nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.Scenes
{
    public class TitleScene : Scene
    {

        
        

        public override void Initialize()
        {
            base.Initialize();

            
        }

        protected override void LoadContent() 
        {
            TextItem Caster = new TextItem("Caster", "04B_30_5x");
                Caster.Position = new Vector2(100, 100);
            Caster.Color = Color.Red;

            TextItem Rumble = new TextItem("Rumble", "04B_30_5x");
            Rumble.Position = new Vector2(100, 150);

        }
    }
}
