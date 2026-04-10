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



        //public event EventHandler<UpdateEventArgs> UpdateEvent;

        public string EntityListFilePath = "Entity_List/EntityList.json";

        public readonly EntityList EntityList;

        public Game game => Core.Instance;


        public GameManager() 
        {
            EntityList = new EntityList(Content, EntityListFilePath);
        }


        ///// <summary>
        ///// Runs on an Update event. notifies listeners that an update occured, passing along the GameTime information.
        ///// </summary>
        ///// <param name="_gameTime"></param>
        //public void UpdateEventCall(GameTime _gameTime)
        //{
        //    //Console.WriteLine($"UpdateEvent Occured.");

        //    // Fire the event — notify all listeners
        //    OnUpdate(new UpdateEventArgs
        //    {
        //        GameTime = _gameTime,
        //    });
        //}

        //protected virtual void OnUpdate(UpdateEventArgs args)
        //{
        //    UpdateEvent?.Invoke(this, args); // '?.' safely handles zero listeners
        //}

    }

    //public class UpdateEventArgs : EventArgs
    //{
    //    public GameTime GameTime { get; set; }
    //}
}
