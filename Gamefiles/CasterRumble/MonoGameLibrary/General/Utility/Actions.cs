using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.General.Utility
{
    /// <summary>
    /// links multiple inputs to a single action, no matter the input device
    /// </summary>
    public class Actions
    {
        public string Name { get; set; }

        private List<Keys> Input_keys { get; set; }

        private List<Buttons> Input_Buttons { get; set; }

        private List<MouseButtons> Input_Mouse { get; set; }


        public Actions(string _Name)
        {
            Name = _Name;
            Input_keys = new List<Keys>();
            Input_Buttons = new List<Buttons>();
            Input_Mouse = new List<MouseButtons>();
        }

        /// <summary>
        /// adds the specified key to the collection of input keys.
        /// </summary>
        /// <param name="_Key"></param>
        public void Add_button(Keys _Key)
        {
            Input_keys.Add(_Key);
        }

        /// <summary>
        /// Adds the specified button to the collection of input buttons.
        /// </summary>
        /// <param name="_Button">The button to add to the input buttons collection. Cannot be null.</param>
        public void Add_button(Buttons _Button)
        {
            Input_Buttons.Add(_Button);
        }

        /// <summary>
        /// Adds the specified mouse button to the input collection.
        /// </summary>
        /// <param name="_Mouse">The mouse button to add to the input collection.</param>
        public void Add_button(MouseButtons _Mouse)
        {
            Input_Mouse.Add(_Mouse);
        }

        public void Clear_buttons()
        {
            Input_keys.Clear();
            Input_Buttons.Clear();
            Input_Mouse.Clear();
        }

        public void Remove_button(Keys _Key)
        {
            Input_keys.Remove(_Key);
        }

        public void Remove_button(Buttons _Button)
        {
            Input_Buttons.Remove(_Button);
        }

        public void Remove_button(MouseButtons _Mouse)
        {
            Input_Mouse.Remove(_Mouse);
        }


        /// <summary>
        /// mimics the is key down method but for mouse buttons, returns the state of the mouse button you ask for
        /// </summary>

        public ButtonState Is_Mouse_Button_Pressed(MouseState _MouseState, MouseButtons _Mouse)
        {
            switch (_Mouse)
            {
                case MouseButtons.Left:
                    return _MouseState.LeftButton ;
                case MouseButtons.Middle:
                    return _MouseState.MiddleButton;
                case MouseButtons.Right:
                    return _MouseState.RightButton;
                case MouseButtons.X_1:
                    return _MouseState.XButton1;   
                case MouseButtons.X_2:
                    return _MouseState.XButton2;
            }
            return ButtonState.Released;
        }

        public Boolean Is_action_pressed(KeyboardState _KeyState, GamePadState _GamePadState, MouseState _MouseState)
        {
            foreach (Keys key in Input_keys)
            {
                if (_KeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            foreach (Buttons button in Input_Buttons)
            {
                if (_GamePadState.IsButtonDown(button))
                {
                    return true;
                }
            }
            foreach (MouseButtons mouseButton in Input_Mouse)
            {
                if (Is_Mouse_Button_Pressed(_MouseState, mouseButton) == ButtonState.Pressed)
                {
                    return true;
                }
            }

            return false;
        }

        public float Is_action_pressed_signal(KeyboardState _KeyState, GamePadState _GamePadState, MouseState _MouseState)
        {
            foreach (Keys key in Input_keys)
            {
                if (_KeyState.IsKeyDown(key))
                {
                    return 1;
                }
            }
            foreach (Buttons button in Input_Buttons)
            {
                if (button == Buttons.LeftTrigger || button == Buttons.RightTrigger)
                {
                    if (_GamePadState.IsButtonDown(button))
                    {
                        return Get_Trigger_Signal(_GamePadState, button);
                    }
                }
                else if ((button == Buttons.LeftThumbstickUp || button == Buttons.LeftThumbstickDown || button == Buttons.LeftThumbstickLeft || button == Buttons.LeftThumbstickRight) || (button == Buttons.RightThumbstickUp || button == Buttons.RightThumbstickDown || button == Buttons.RightThumbstickLeft || button == Buttons.RightThumbstickRight))
                {
                    return Get_Thumbstick_Signal(_GamePadState, button);
                }
                else { 
                    if (_GamePadState.IsButtonDown(button))
                    {
                        return 1;
                    }
                }   
            }
            foreach (MouseButtons mouseButton in Input_Mouse)
            {
                if (Is_Mouse_Button_Pressed(_MouseState, mouseButton) == ButtonState.Pressed)
                {
                    return 1;
                }
            }

            return 0;
        }

        private float Get_Trigger_Signal(GamePadState _GamePadState, Buttons _Button)
        {
            if (_Button == Buttons.LeftTrigger)
            {
                return _GamePadState.Triggers.Left;
            }
            else if (_Button == Buttons.RightTrigger)
            {
                return _GamePadState.Triggers.Right;
            }
            return 0;
        }

        private float Get_Thumbstick_Signal(GamePadState _GamePadState, Buttons _Button)
        {
            if (_Button == Buttons.LeftThumbstickUp)
            {
                return Math.Max(0, _GamePadState.ThumbSticks.Left.Y);
            }
            else if (_Button == Buttons.LeftThumbstickDown)
            {
                return Math.Max(0, -_GamePadState.ThumbSticks.Left.Y);
            }
            else if (_Button == Buttons.LeftThumbstickLeft)
            {
                return Math.Max(0, -_GamePadState.ThumbSticks.Left.X);
            }
            else if (_Button == Buttons.LeftThumbstickRight)
            {
                return Math.Max(0, _GamePadState.ThumbSticks.Left.X);
            }
            else if (_Button == Buttons.RightThumbstickUp)
            {
                return Math.Max(0, _GamePadState.ThumbSticks.Right.Y);
            }
            else if (_Button == Buttons.RightThumbstickDown)
            {
                return Math.Max(0, -_GamePadState.ThumbSticks.Right.Y);
            }
            else if (_Button == Buttons.RightThumbstickLeft)
            {
                return Math.Max(0, -_GamePadState.ThumbSticks.Right.X);
            }
            else if (_Button == Buttons.RightThumbstickRight)
            {
                return Math.Max(0, _GamePadState.ThumbSticks.Right.X);
            }
            return 0;
        }
    }




    public enum MouseButtons
    {
        Left,
        Middle,
        Right,
        X_1,
        X_2
    }
}
