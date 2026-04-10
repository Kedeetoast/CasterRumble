using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Utility;
using MonoGameLibrary.Graphics;
using System;
//using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public Dictionary<string, Actions> ActionList { get; private set; }

        public readonly List<string> SystemActions;

        

        private KeyboardState keyboardstate;

        private GamePadState gamepadstate;

        private MouseState mousestate;

        private KeyboardState Previous_keyboardstate;

        private GamePadState Previous_gamepadstate;

        private MouseState Previous_mousestate;



        public Vector2 MousePosition => new Vector2(mousestate.X, mousestate.Y);

        public InputManager()
        {
            ActionList = new Dictionary<string, Actions>();

            SystemActions = new List<string>
            {"Sys_Up","Sys_Down","Sys_Left","Sys_Right","Sys_Confirm","Sys_Cancel","Sys_Menu"};
            SystemActions.ForEach(Add_Action);

            Add_input("Sys_Up", Keys.Up);
            Add_input("Sys_Up", Buttons.DPadUp);
            Add_input("Sys_Up", Buttons.LeftThumbstickUp);
            Add_input("Sys_Down", Keys.Down);
            Add_input("Sys_Down", Buttons.DPadDown);
            Add_input("Sys_Down", Buttons.LeftThumbstickDown);
            Add_input("Sys_Left", Keys.Left);
            Add_input("Sys_Left", Buttons.DPadLeft);
            Add_input("Sys_Left", Buttons.LeftThumbstickLeft);
            Add_input("Sys_Right", Keys.Right);
            Add_input("Sys_Right", Buttons.DPadRight);
            Add_input("Sys_Right", Buttons.LeftThumbstickRight);
            Add_input("Sys_Confirm", Keys.Z);
            Add_input("Sys_Confirm", Buttons.A);
            Add_input("Sys_Cancel", Keys.X);
            Add_input("Sys_Cancel", Buttons.B);
            Add_input("Sys_Cancel", Keys.C);
            Add_input("Sys_Cancel", Keys.Escape);
            Add_input("Sys_Cancel", Buttons.Start);

            keyboardstate = Keyboard.GetState();
            gamepadstate = GamePad.GetState(0, GamePadDeadZone.Circular);
            mousestate = Mouse.GetState();



        }

        public override void Update(GameTime gametime)
        {
          

            Previous_keyboardstate = keyboardstate;

            Previous_gamepadstate = gamepadstate;

            Previous_mousestate = mousestate;


            keyboardstate = Keyboard.GetState();
            gamepadstate = GamePad.GetState(0, GamePadDeadZone.Circular);
            mousestate = Mouse.GetState();

        }




        public void Add_Action(string _Name)
        {
            if (ActionList.ContainsKey(_Name))
            {
                Console.WriteLine($"Error: Action with name \"{_Name}\" already exist.");
                return;
            }

            ActionList.Add(_Name, new Actions(_Name));
        }

        public void Add_input(string _Name, Keys key)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {
                value.Add_button(key);
            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
            }
        }

        public void Add_input(string _Name, Buttons button)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {
                value.Add_button(button);
            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
            }
        }

        public void Add_input(string _Name, MouseButtons Mousebutton)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {
                value.Add_button(Mousebutton);
            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
            }
        }

        public bool Check_Action_pressed(string _Name)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {

                return value.Is_action_pressed(keyboardstate, gamepadstate, mousestate);

            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
                return false;
            }
        }

        public bool Check_Action_Just_Pressed(string _Name)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {

                var current = value.Is_action_pressed(keyboardstate, gamepadstate, mousestate);
                    if (current)
                    {
                        var previous = value.Is_action_pressed(Previous_keyboardstate, Previous_gamepadstate, Previous_mousestate);
                    return !previous;

                    }
                    else
                    {
                        return false;
                    }

            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
                return false;
            }
        }



        /// <summary>
        /// return if an action is press as a float between 0 and 1. used for analouge inputs such as triggers and thumbsticks, while representing digital inputs as 1 for pressed and 0 for not pressed.
        /// </summary>
        /// <param name="_Name"></param>
        /// <returns></returns>
        public float Check_Action_signal(string _Name)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {


                return value.Is_action_pressed_signal(keyboardstate, gamepadstate, mousestate);

            }
            else
            {
                Console.WriteLine($"Error: No Action exist with name \"{_Name}\"");
                return 0;
            }
        }



    }
}

