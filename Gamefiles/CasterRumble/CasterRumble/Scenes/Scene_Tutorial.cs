using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.nodes;
using MonoGameLibrary.nodes.Living;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.Scenes
{
    public class Scene_Tutorial : PlayableScene
    {

        protected override void LoadContent()
        {
            base.LoadContent();

            var player = new Player(ref World, 100, new Vector2(100, 100));
            var entity = new Entity(ref World, "Block", new Vector2(100, 200));
        }
    }
}
