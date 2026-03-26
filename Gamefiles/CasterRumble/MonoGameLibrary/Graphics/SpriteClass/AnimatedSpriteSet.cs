using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics.SpriteClass
{
    public class AnimatedSpriteSet
    {

        /// <summary>
        /// current animation playing
        /// </summary>
        public Animation ActiveSprite;


        /// <summary>
        /// all animations able to be used by the sprite
        /// </summary>
        public Dictionary<string, Animation> AvailableSprites;

        /// <summary>
        /// fallback animation incase wanted sprite is unavailable
        /// </summary>
        public Animation Default;

        public AnimatedSpriteSet(Dictionary<string, Animation> Sprites)
        {
            AvailableSprites = Sprites;

            Default = AvailableSprites.ElementAt(0).Value;
            ActiveSprite = Default;
        }

        public AnimatedSpriteSet(Dictionary<string, Animation> Sprites, String Active)
        {
            AvailableSprites = Sprites;

            Default = AvailableSprites.ElementAt(0).Value;
            ActiveSprite = Default;
            ChangeActive(Active);
        }


        public void ChangeActive(string Newsprite)
        {
            if (AvailableSprites.TryGetValue(Newsprite, out Animation? value))
            {
                ActiveSprite = value;
            }
            else
            {
                Console.WriteLine($"Error: No animation with the name {Newsprite} found in the avaiable animations for this animated sprite");
            }
        }


    }
}
