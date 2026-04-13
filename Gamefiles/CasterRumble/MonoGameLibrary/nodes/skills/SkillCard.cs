using Microsoft.Xna.Framework;
using MonoGameLibrary.nodes.Items;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.skills
{
    internal class SkillCard : Item
    {

        public Skill Skill { get; set; }   


        public SkillCard(ref World _world, string _ID, Vector2 _position, float _rotation = 0) : base(ref _world, _ID, _position, _rotation)
        {
        }   



    }
}
