using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    public class TextItem : Canvas
    {

        public ContentManager Content => GameManager.Instance.Content;

        public string Text { get; set; }   

        public SpriteFont Font { get; set; }

        public TextItem(string _Text,string _Font)
        {
            Text = _Text;
            Font = Content.Load<SpriteFont>(GameManager.FntDirectory + _Font);
        }

        public TextItem(string _Text, SpriteFont _Font)
        {
            Text = _Text;
            Font = _Font;
        }

        public override void Draw(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine($"{Color.R}, {Color.G}, {Color.B}, {Color.A}");
            _SpriteBatch.DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);  
        }

    }
}
