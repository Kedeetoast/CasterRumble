using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers;

public class GraphicsManager : Singleton<GraphicsManager>
{
    public SpriteBatch SpriteBatch => Core.SpriteBatch;

}

        
