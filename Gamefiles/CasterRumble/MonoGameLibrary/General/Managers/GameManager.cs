using System;
using MonoGameLibrary.General.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.nodes;


namespace MonoGameLibrary.General.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GraphicsManager Graphics => GraphicsManager.Instance; 

        public InputManager Input => InputManager.Instance;

        public NetManager Network => NetManager.Instance;

        public AudioManager Audio => AudioManager.Instance;

        public ContentManager Content => Core.Content;

        public string EntityListFilePath = "Entity_List/EntityList";

        public readonly EntityList EntityList;


        public GameManager() 
        {
            EntityList = new EntityList(Content, EntityListFilePath);
        }



    }

}
