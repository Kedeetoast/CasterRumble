using System;
using MonoGameLibrary.General.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.nodes;
using MonoGameLibrary.General.Scenes;


namespace MonoGameLibrary.General.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GraphicsManager Graphics => GraphicsManager.Instance; 

        public InputManager Input => InputManager.Instance;

        public NetworkManager Network => NetworkManager.Instance;

        public AudioManager Audio => AudioManager.Instance;

        public ContentManager Content => Core.Content;

        public Scene ActiveScene => Core.S_activeScene;

        public ContentManager Scene_Content => Core.S_activeScene.Content;

        public static string ImgDirectory => Core.Instance.ImgDirectory;
        public static string FntDirectory => Core.Instance.FntDirectory;
        public static string MusDirectory => Core.Instance.MusDirectory;
        public static string SfxDirectory => Core.Instance.SfxDirectory;

        public int port { get; set; } = 9050; // Default port for network communication

        public Vector2 Gravity { get; set; } = new Vector2(0, 9.8f); // Default gravity pointing downwards

        //public event EventHandler<UpdateEventArgs> UpdateEvent;

        public string EntityListFilePath = "Entity_List/EntityList.json";

        public readonly EntityList EntityList;

        public Game game => Core.Instance;

        


        public GameManager() 
        {
            EntityList = new EntityList(Content, EntityListFilePath);
        }


        
        public void ChangeBackGroundColor(Color color)
        {
            Core.Instance.BackgroundColor = color;
        }

    }

    //public class UpdateEventArgs : EventArgs
    //{
    //    public GameTime GameTime { get; set; }
    //}
}
