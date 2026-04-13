using MonoGameLibrary.General.Scenes;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class SceneManager : Singleton<SceneManager>
    {
        // public Scene ActiveScene => Core.S_activeScene;

        public Scene ActiveScene
        {
            get
            {
                return Core.S_activeScene;
            }
        }

        public void ChangeScene(Scene _newScene)
        {
            if (_newScene == ActiveScene)
                    return;


            if (ActiveScene != null)
            {
                ActiveScene.Dispose();
            }

            Core.TransitionScene(_newScene);
        }
    }
}
