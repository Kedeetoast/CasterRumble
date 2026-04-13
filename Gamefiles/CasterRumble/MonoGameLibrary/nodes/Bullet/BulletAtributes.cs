using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Bullet
{
    public class BulletAtributes
    {
        public bool Piercing { get; set; }

        public int Explosive { get; set; }

        public int Incendiary { get; set; } = 0;

        public int SpeedMultiplier { get; set; } = 0;

        public int  Corrosive { get; set; } = 0;

        public int Electric { get; set; } = 0;

        public int Bouncy { get; set; } = 0;

        public float Damage { get; set; } = 10f; // Damage the bullet will inflict on impact

        public float range = 1000f; // Maximum distance the bullet can travel before gravity affects it


    }
}
