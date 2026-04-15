using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Utility;
using System.Collections.Generic;

namespace MonoGameLibrary.General.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public Dictionary<string, Actions> ActionList { get; private set; }
        //public readonly List<string> SystemActions;

        private KeyboardState keyboardstate;
        private GamePadState gamepadstate;
        private MouseState mousestate;

        private KeyboardState Previous_keyboardstate;
        private GamePadState Previous_gamepadstate;
        private MouseState Previous_mousestate;

        public InputState State { get; private set; } = InputState.Keyboard;

        public Vector2 MousePosition => new Vector2(mousestate.X, mousestate.Y);
        public Vector2 MouseWorldPos => MouseWorldPosition();

        public InputManager()
        {
            ActionList = new Dictionary<string, Actions>();

            var SystemActions = new List<string>
        { "Sys_Up", "Sys_Down", "Sys_Left", "Sys_Right", "Sys_Confirm", "Sys_Cancel", "Sys_Menu" };
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
            Add_input("Sys_Cancel", Keys.C);
            Add_input("Sys_Cancel", Keys.Escape);
            Add_input("Sys_Cancel", Buttons.B);
            Add_input("Sys_Cancel", Buttons.Start);

            Add_input("Sys_Menu", Keys.Escape);
            Add_input("Sys_Menu", Buttons.Start);


            keyboardstate = Keyboard.GetState();
            gamepadstate = GamePad.GetState(0, GamePadDeadZone.Circular);
            mousestate = Mouse.GetState();
            Previous_keyboardstate = keyboardstate;
            Previous_gamepadstate = gamepadstate;
            Previous_mousestate = mousestate;
        }

        public override void Update(GameTime gametime)
        {
            //System.Diagnostics.Debug.WriteLine($"{State}");
            Previous_keyboardstate = keyboardstate;
            Previous_gamepadstate = gamepadstate;
            Previous_mousestate = mousestate;

            keyboardstate = Keyboard.GetState();
            gamepadstate = GamePad.GetState(0, GamePadDeadZone.Circular);
            mousestate = Mouse.GetState();
            UpdateInputState();



        }


        private void UpdateInputState()
        {
            if (keyboardstate.GetPressedKeyCount() > 0 ||
                mousestate.LeftButton == ButtonState.Pressed ||
                mousestate.RightButton == ButtonState.Pressed ||
                mousestate.MiddleButton == ButtonState.Pressed)
            {
                State = InputState.Keyboard;
            }
            else if (gamepadstate.Buttons != new GamePadButtons(Buttons.None) ||
                     gamepadstate.Triggers.Left > 0f ||
                     gamepadstate.Triggers.Right > 0f ||
                     gamepadstate.ThumbSticks.Left != Vector2.Zero ||
                     gamepadstate.ThumbSticks.Right != Vector2.Zero)
            {
                State = InputState.Gamepad;
            }
        }


        public void Remove_Action(string _Name)
        {
            if (ActionList.ContainsKey(_Name))
                ActionList.Remove(_Name);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public void Add_Action(string _Name)
        {
            if (ActionList.ContainsKey(_Name))
            {
                System.Diagnostics.Debug.WriteLine($"Error: Action with name \"{_Name}\" already exists.");
                return;
            }
            ActionList.Add(_Name, new Actions(_Name));
        }


        public void Action_AltName(string _Name, string _AltName)
        {
            if (!ActionList.TryGetValue(_Name, out Actions value))
            {
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
                return;
            }
            if (ActionList.ContainsKey(_AltName))
            {
                System.Diagnostics.Debug.WriteLine($"Error: Action with name \"{_AltName}\" already exists.");
                return;
            }
            ActionList.Add(_AltName, value);
        }

        public void Add_input(string _Name, Keys key)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Add_button(key);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public void Add_input(string _Name, Buttons button)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Add_button(button);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public void Add_input(string _Name, MouseButtons Mousebutton)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Add_button(Mousebutton);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public void Remove_input(string _Name, Keys key)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Remove_button(key);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }


        public void Remove_input(string _Name, Buttons button)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Remove_button(button);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public void Remove_input(string _Name, MouseButtons Mousebutton)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
                value.Remove_button(Mousebutton);
            else
                System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
        }

        public bool Check_Action_pressed(string _Name)
        {
            //System.Diagnostics.Debug.WriteLine($"{_Name} is being checked");
            if (ActionList.TryGetValue(_Name, out Actions value))
                return value.Is_action_pressed(keyboardstate, gamepadstate, mousestate);

            System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
            return false;
        }

        public bool Check_Action_Just_Pressed(string _Name)
        {
            if (ActionList.TryGetValue(_Name, out Actions value))
            {
                bool current = value.Is_action_pressed(keyboardstate, gamepadstate, mousestate);
                bool previous = value.Is_action_pressed(Previous_keyboardstate, Previous_gamepadstate, Previous_mousestate);
                return current && !previous;
            }

            System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
            return false;
        }

        /// <summary>
        /// Returns how strongly an action is pressed as a float between 0 and 1.
        /// Used for analogue inputs such as triggers and thumbsticks;
        /// digital inputs return 1 when pressed, 0 otherwise.
        /// </summary>
        public float Check_Action_signal(string _Name)
        {


            if (ActionList.TryGetValue(_Name, out Actions value))
            {
                var x = value.Is_action_pressed_signal(keyboardstate, gamepadstate, mousestate);
                //System.Diagnostics.Debug.WriteLine($"{_Name} is being checked and returned {x}");
                return x;
            }


            System.Diagnostics.Debug.WriteLine($"Error: No Action exists with name \"{_Name}\"");
            return 0f;
        }

        public Vector2 GetThumbstickDirection(ThumbStick stick)
        {
            Vector2 raw = stick == ThumbStick.Left
                ? gamepadstate.ThumbSticks.Left
                : gamepadstate.ThumbSticks.Right;

            // MonoGame's Y axis is inverted relative to screen space — positive Y is up on a stick.
            raw.Y = -raw.Y;

            if (raw == Vector2.Zero) return Vector2.Zero;
            return Vector2.Normalize(raw);
        }

        private Vector2 MouseWorldPosition()
        {
            Vector2 screenPos = MousePosition;

            if (!CameraManager.HasCamera)
                return screenPos;

            // Invert the camera transform to unproject from screen space to world space.
            Matrix inverseTransform = Matrix.Invert(CameraManager.ActiveCamera.Transform);
            return Vector2.Transform(screenPos, inverseTransform);
        }


    }

    public enum InputState
    {
        Keyboard = 0,
        Gamepad = 1
    }

    public enum ThumbStick
    {
        Left,
        Right
    }
}

