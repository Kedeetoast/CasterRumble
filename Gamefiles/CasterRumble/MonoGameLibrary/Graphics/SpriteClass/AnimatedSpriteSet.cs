using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics.SpriteClass
{
    public class AnimatedSpriteSet : SpriteSet
    {

        /// <summary>
        /// current Frame of the current animation playing
        /// </summary>
        //public TextureRegion ActiveFrame { get; set; }

        /// <summary>
        /// current animation playing
        /// </summary>
        public Animation ActiveAnimation;


        /// <summary>
        /// all animations able to be used by the sprite
        /// </summary>
        public Dictionary<string, Animation> AvailableAnimations;

        /// <summary>
        /// fallback animation incase wanted sprite is unavailable
        /// </summary>
        public Animation DefaultAnimation;

        public AnimatedSpriteSet(Dictionary<string, Animation> Sprites)
        {
            AvailableAnimations = Sprites;

            Playing = AvailableAnimations.ElementAt(0).Key;
            DefaultAnimation = AvailableAnimations.ElementAt(0).Value;
            ActiveAnimation = DefaultAnimation;
        }

        public AnimatedSpriteSet(Dictionary<string, Animation> Sprites, String Active)
        {
            AvailableAnimations = Sprites;

            DefaultAnimation = AvailableAnimations.ElementAt(0).Value;
            ActiveAnimation = DefaultAnimation;
            ChangeActive(Active);
        }

        public AnimatedSpriteSet(Animation animation)
        {
            AvailableAnimations = new Dictionary<string, Animation>();
            AvailableAnimations.Add("DefaultAnimation", animation);

            Playing = "DefaultAnimation";
            DefaultAnimation = AvailableAnimations.ElementAt(0).Value;
            ActiveAnimation = DefaultAnimation;
        }

        public override void ChangeActive(string Newsprite)
        {
            if (AvailableAnimations.TryGetValue(Newsprite, out Animation value))
            {
                Playing = Newsprite;
                ActiveAnimation = value;
            }
            else
            {
                Console.WriteLine($"Error: No animation with the name {Newsprite} found in the avaiable animations for this animated sprite");
            }
        }


    }
}
