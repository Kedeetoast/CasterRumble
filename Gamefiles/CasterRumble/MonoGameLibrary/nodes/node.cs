using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.nodes
{



    public abstract class Node : DrawableGameComponent
    {

        public Node() : base(GameManager.Instance.game)
        {
            GameManager.Instance.game.Components.Add(this);

        }

        //public Node(Scene _scene,Node _parent = null) : base(GameManager.Instance.game)
        //{
        //    GameManager.Instance.game.Components.Add(this);
        //    Scene = _scene;
        //    Parent = _parent;

        //    if (_parent != null)
        //    {
        //        Parent = _parent;
        //    }
        //    else
        //    {
        //        Parent = _scene;
        //    }
        //}



        private Node _Parent;

        protected Node Parent
        {
            get
            {
                    return _Parent;
            }
            set
            {
                if (_Parent != null)
                {
                    _Parent.Children.Remove(this); // Remove this node from the old parent's children collection, if it exists.
                    _Parent = value;
                    value.Children.Add(this);
                }
                else
                {
                    _Parent = value;
                    value.Children.Add(this);
                }
            }

        }
            


        protected List<Node> Children = new List<Node>();

        //private Scene _Scene;


        //public Scene Scene
        //{
        //    get
        //    {
        //        return _Scene;
        //    }

        //    set
        //    {
        //        GameManager.Instance.ActiveScene.GameComponents.Add(this); // Add this node to the new scene's GameComponents collection.
        //        _Scene = value;
        //    }

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
