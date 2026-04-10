using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Utility;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Living
{
    public abstract class NPC : Living
    {
        

        protected NPC(ref World _world, EntityList entityList, float health, string _ID, Vector2 _position, float _rotation = 0, SpriteType _spriteType = SpriteType.Static) : base(ref _world, health, _ID, _position, _rotation, _spriteType)
        {
        }
    }
}
