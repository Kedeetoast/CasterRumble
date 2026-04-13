using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.nodes;

namespace MonoGameLibrary.General.Scenes
{
    public class PlayableScene : Scene
    {
        public World World;

        Entity Entity;

        public PlayableScene() : base()
        {
            // Initialize the scene
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            World.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            World = new World(GameManager.Instance.Gravity);

            //Entity = new Entity(ref World, "Block", new Vector2(100, 100));
        }
    }
}
