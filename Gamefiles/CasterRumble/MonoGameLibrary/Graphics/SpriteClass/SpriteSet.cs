using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Graphics;

namespace MonoGameLibrary.Graphics.SpriteClass
{
    public class SpriteSet
    {
        public string Playing { get; protected set; }
        public TextureRegion ActiveRegion { get; set; }

        public Dictionary<string,TextureRegion> AvailableRegions;

        public TextureRegion Default;

        public SpriteSet(Dictionary<string, TextureRegion> Sprites)
        {
            AvailableRegions = Sprites;

            Playing = AvailableRegions.ElementAt(0).Key;
            Default = AvailableRegions.ElementAt(0).Value;
            ActiveRegion = Default;
        }

        public SpriteSet(Dictionary<string, TextureRegion> Sprites, String Active)
        {
            AvailableRegions = Sprites;

            Default = AvailableRegions.ElementAt(0).Value;
            ActiveRegion = Default;

            ChangeActive(Active);
        }

        public SpriteSet(TextureRegion Reigon)
        {
            AvailableRegions = new Dictionary<string, TextureRegion>();
            AvailableRegions.Add("default", Reigon);

            Playing = "default";
            Default = AvailableRegions.ElementAt(0).Value;
            ActiveRegion = Default;
        }

        public SpriteSet() { }


        public virtual void ChangeActive(string Newsprite)
        {
            if (AvailableRegions.TryGetValue(Newsprite, out TextureRegion value))
            {
                Playing = Newsprite;
                ActiveRegion = value;
            }
            else
            {
                Console.WriteLine($"Error: No animation with the name {Newsprite} found in the avaiable animations for this animated sprite");
            }
        }
    }
}
