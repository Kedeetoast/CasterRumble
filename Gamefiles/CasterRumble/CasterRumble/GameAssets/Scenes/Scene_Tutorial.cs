using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.nodes;
using MonoGameLibrary.nodes.Living;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.GameAssets.Scenes
{
    public class Scene_Tutorial : PlayableScene
    {

        protected override void LoadContent()
        {
            base.LoadContent();

            var player = new Player(ref World, 100, new Vector2(100, 100));
            var entity = new Entity(ref World, "Block", new Vector2(100, 200));
            var entity2 = new Entity(ref World, "Block", new Vector2(228, 200));
            var entity3 = new Entity(ref World, "Block", new Vector2(356, 200));
            var entity4 = new Entity(ref World, "Block", new Vector2(484, 200));
            var entity5 = new Entity(ref World, "Block", new Vector2(612, 200));
            var entity6 = new Entity(ref World, "Block", new Vector2(740, 200));
            var entity7 = new Entity(ref World, "Block", new Vector2(868, 200));
            var Camera = new Camera();
            Camera.Parent = player;
            //System.Diagnostics.Debug.WriteLine($"");
        }
    }
}
