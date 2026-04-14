using Microsoft.Xna.Framework;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Scenes;
using System.Collections.Generic;


namespace MonoGameLibrary.nodes
{
    public abstract class Node : DrawableGameComponent //inherits from drawableGameComponent so that Updat(GameTime gameTime) and Draw(GameTime gameTime) can be overridden in the node class and is called on the game instance update and draw calls automaticaly
    {

        public Node() : base(Core.Instance)
        {
            GameManager.Instance.game.Components.Add(this);
            Scene = GameManager.Instance.ActiveScene;

        }

        public Node(Scene _scene) : base(Core.Instance)
        {
            GameManager.Instance.game.Components.Add(this);
            Scene = _scene;
        }


        private Node _Parent;

        /// <summary>
        /// the parent node of this node, when set the node is added the the parents children
        /// </summary>
        public Node Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                if (_Parent != null) // in the situation where there is already a parent make sure to remove self from children before setting new parent and adding self to new parents children collection
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

        private Scene _Scene; // the scene this node is associated with, when a scene is disposed the node is also disposed.


        public Scene Scene
        {
            get
            {
                return _Scene;
            }

            set
            {
                if (_Scene != null)
                {
                    UnregisterNode(_Scene.GameComponents);
                }


                if (value != null) // this check is here mainly due to the scene class where in their situation Scene is set to null, however a null value wont have a GameComponents collection so this check is necessary to avoid a null reference exception when trying to register the node to the new scene's GameComponents collection.
                {
                    RegisterNode(value.GameComponents);
                }

                _Scene = value;
            }

        }

        /// <summary>
        /// Add this node to the new scene's GameComponents collection.
        /// </summary>
        /// <param name="gameComponents"></param>
        private void RegisterNode(GameComponentCollection gameComponents)
        {
            gameComponents.Add(this);
        }


        /// <summary>
        /// Remove this node from the old scene's GameComponents collection.
        /// </summary>
        /// <param name="gameComponents"></param>
        private void UnregisterNode(GameComponentCollection gameComponents)
        {
            gameComponents.Remove(this);
        }

        protected override void Dispose(bool disposing) // when the node is disposed any assets it is using are disposed of. dispose and visible are also set to false, which stops the update and draw functions from being called
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);

            Enabled = false;
            Visible = false;

            foreach (Node Child in Children ) 
            {
                Child.Dispose(disposing);
            }



        }
    }
}
