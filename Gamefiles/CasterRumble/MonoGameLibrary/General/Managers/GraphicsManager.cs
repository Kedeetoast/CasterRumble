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

        public void DrawEventCall(SpriteBatch spriteBatch)
        {
            Console.WriteLine($"DrawEvent Occured.");

            // Fire the event — notify all listeners
            OnDraw(new DrawEventArgs
            {
                _spriteBatch = spriteBatch,
            });
        }

        protected virtual void OnDraw(DrawEventArgs args)
        {
            DrawEvent?.Invoke(this, args); // '?.' safely handles zero listeners
        }
    }

    public class DrawEventArgs : EventArgs
    {
        public SpriteBatch _spriteBatch { get; set; }
    }

}
