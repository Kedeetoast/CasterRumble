using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Utility;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Items
{
    internal class SkillCard : Item
    {

        public SkillCard(ref World _world, EntityList _entityList, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 0) : base(ref _world, _ID, _position, _rotation, _despawntime)
        {
        }
    }
}
