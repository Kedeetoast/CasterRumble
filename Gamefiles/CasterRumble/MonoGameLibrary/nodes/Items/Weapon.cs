using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Utility;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Items;

public abstract class Weapon : Item
{
    private float Damage;

    private string LightCooldown;

    private string HeavyCooldown;

    private string Knockback;

    protected Weapon(ref World _world, string _ID, Vector2 _position, float _rotation = 0, float _despawntime = 60) : base(ref _world, _ID, _position, _rotation, _despawntime)
    {

        
    }

    public virtual void LightAttack()
    {
        // Implement light attack logic here
    }

    public virtual void HeavyAttack()
    {
        // Implement heavy attack logic here
    }

}






