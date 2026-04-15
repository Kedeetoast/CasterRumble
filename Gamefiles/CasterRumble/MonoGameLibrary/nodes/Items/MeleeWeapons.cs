using Microsoft.Xna.Framework;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Items
{
    public class Melee : Weapon
    {

        public Meleetype Type { get; set; }

        public float DashRange { get; set; } = 0;

        public Melee(ref World _world, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 0) : base(ref _world, _ID, _position, _rotation, _despawntime)
        {


        }
    }

    public enum Meleetype
    {
        Pierce,
        swing,
        heavy,
        blunt
    }
}
