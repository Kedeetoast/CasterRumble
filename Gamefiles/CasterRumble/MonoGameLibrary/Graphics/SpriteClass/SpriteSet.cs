using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Graphics;

namespace MonoGameLibrary.Graphics.SpriteClass
{
    public class SpriteSet
    {
        public TextureRegion ActiveSprite;

        public Dictionary<string,TextureRegion> AvailableSprites;

        public TextureRegion Default;

        public SpriteSet(Dictionary<string, TextureRegion> Sprites)
        {
            AvailableSprites = Sprites;

            Default = AvailableSprites.ElementAt(0).Value;
            ActiveSprite = Default;
        }

        public SpriteSet(Dictionary<string, TextureRegion> Sprites, String Active)
        {
            AvailableSprites = Sprites;

            Default = AvailableSprites.ElementAt(0).Value;
            ActiveSprite = Default;
            ChangeActive(Active);
        }

        public SpriteSet(TextureRegion Reigon)
        {
            AvailableSprites.Add("default", Reigon);

            Default = AvailableSprites.ElementAt(0).Value;
            ActiveSprite = Default;
        }


        public void ChangeActive(string Newsprite)
        {
            if (AvailableSprites.TryGetValue(Newsprite, out TextureRegion? value))
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
