using System;
using MonoGameLibrary.General.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGameLibrary.General.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public readonly GraphicsManager Graphics = GraphicsManager.Instance; 

        public readonly InputManager Input = InputManager.Instance;

        public readonly NetManager Network = NetManager.Instance;

        public readonly AudioManager Audio = AudioManager.Instance;



        public GameManager() 
        {
         
        }



    }

}
