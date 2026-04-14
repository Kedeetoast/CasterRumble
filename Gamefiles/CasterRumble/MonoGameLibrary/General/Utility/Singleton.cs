using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Utility
{
    public abstract class Singleton<T> : GameComponent where T : Singleton<T>, new()
    {
        protected Singleton() : base(Core.Instance)
        {
            Core.Instance.Components.Add(this);
        }

        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance => _instance.Value;
    }
}
