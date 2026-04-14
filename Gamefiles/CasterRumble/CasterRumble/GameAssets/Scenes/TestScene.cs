using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;

namespace CasterRumble.GameAssets.Scenes
{
    public class TestScene : Scene
    {

        Sprite Sprite_SLIME { get; set; }

        Sprite Sprite_BAT { get; set; }
        TextureAtlas Atlas { get; set; }



        public TestScene() : base()
        {
            // Initialize the scene
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            base.LoadContent();

            Atlas = TextureAtlas.FromFile(Content, "Images/Spritesheet/Atlas_definition/defSpr_atlas");

            Sprite_SLIME = Atlas.CreateAnimatedSprite("slime-animation");
            Sprite_SLIME.Scale = new Vector2(4.0f, 4.0f); 
            Sprite_SLIME.Position = new Vector2(100, 100);  

            Sprite_BAT = Atlas.CreateAnimatedSprite("bat-animation");
            Sprite_BAT.Scale = new Vector2(4.0f, 4.0f);
            Sprite_BAT.Position = new Vector2(300, 100);
        }
    }
}
