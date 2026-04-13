using Microsoft.Xna.Framework;
using MonoGameLibrary.nodes.Items;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterRumble.Entities.Items.Weapon
{
    public class Weapon_Melee_Sword : Melee
    {
        public Weapon_Melee_Sword(ref World _world, Vector2 position) : base(ref _world, "Weapon_Melee_Sword", position)
        {
        }
    }
}
