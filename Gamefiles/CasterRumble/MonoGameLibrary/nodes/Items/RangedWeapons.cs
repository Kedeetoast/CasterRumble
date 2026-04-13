using Microsoft.Xna.Framework;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Items
{
    public class Ranged : Weapon
    {
        private int bulletAmt;

        private float Spread;

        private int maxAmmo;

        private int _AmmoTemp;

        private int AmmoTemp
        {
            get
            {
                return _AmmoTemp;
            }
            set
            {
                _AmmoTemp = Math.Clamp(value, 0, maxAmmo);
            }
        }

        private float recoil;

        private int HeavyCost;





        public Ranged(ref World _world, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 0) : base(ref _world, _ID, _position, _rotation, _despawntime)
        {
        }

        public override void LightAttack()
        {
           if (AmmoTemp > 0)
            {
                // Implement light attack logic here
                AmmoTemp--;
            }

            else
            {
                Reload();
            }
        }

        public override void HeavyAttack()
        {
            if (AmmoTemp > 0)
            {
                // Implement heavy attack logic here
                AmmoTemp--;
            }

            else
            {
                Reload();
            }
        }

        private void Reload()
        {
            AmmoTemp = maxAmmo;
        }
    }
}
