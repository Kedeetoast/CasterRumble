using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Utility;
using MonoGameLibrary.Graphics;
using System;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public Dictionary<string, Actions> ActionList { get; private set; }

        public InputManager()
        {
            ActionList = new Dictionary<string, Actions>();
        }



        public void Add_Action(string _Name)
        {
            ActionList.Add(_Name, new Actions(_Name));
        }

        public void Add_input(string _Name, Keys key)
        {
            if (ActionList.TryGetValue(_Name, out Actions? value))
            {
                value.Add_button(key);
            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
            }
        }

        public bool Check_Action(string _Name)
        {
            if (ActionList.TryGetValue(_Name, out Actions? value))
            {
                var keyState = Keyboard.GetState();
                var gamePadState = GamePad.GetState(0, GamePadDeadZone.Circular);
                var mouseState = Mouse.GetState();


                return value.Is_action_pressed(keyState, gamePadState, mouseState);

            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
                return false;
            }
        }
    }
}

