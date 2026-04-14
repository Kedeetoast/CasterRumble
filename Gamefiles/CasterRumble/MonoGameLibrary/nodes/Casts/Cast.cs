using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes.Casts
{
    public abstract class Cast
    {
        public float Cooldown { get; set; }

        private Timer Timer { get; set; }

        public bool On_Cooldown { get; private set; } = false;

        public virtual void UseSkill() 
        {
            var x = Cooldown * 1000;
            Timer = new Timer(x);

            Timer.Elapsed += CooldownTimeout;
            Timer.Enabled = true;
            On_Cooldown = true;
        }

        private void CooldownTimeout(Object source, ElapsedEventArgs e)
        {
            On_Cooldown = false;
        }


    }
}
