using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class GraphicsManager : Singleton<GraphicsManager>
    {


        public event EventHandler<DrawEventArgs> DrawEvent;

        public event EventHandler<UpdateEventArgs> UpdateEvent;

        public void DrawEventCall(SpriteBatch _spriteBatch)
        {
            Console.WriteLine($"DrawEvent Occured.");

            // Fire the event — notify all listeners
            OnDraw(new DrawEventArgs
            {
                SpriteBatch = _spriteBatch,
            });
        }

        protected virtual void OnDraw(DrawEventArgs args)
        {
            DrawEvent?.Invoke(this, args); // '?.' safely handles zero listeners
        }

        public void UpdateEventCall(GameTime _gameTime)
        {
            Console.WriteLine($"DrawEvent Occured.");

            // Fire the event — notify all listeners
            OnUpdate(new UpdateEventArgs
            {
                GameTime = _gameTime,
            });
        }

        protected virtual void OnUpdate(UpdateEventArgs args)
        {
            UpdateEvent?.Invoke(this, args); // '?.' safely handles zero listeners
        }
    }

    public class DrawEventArgs : EventArgs
    {
        public SpriteBatch SpriteBatch { get; set; }
    }

    public class UpdateEventArgs : EventArgs
    {
        public GameTime GameTime { get; set; }
    }

}
