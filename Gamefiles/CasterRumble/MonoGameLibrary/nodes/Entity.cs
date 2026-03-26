using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Common;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;
using MonoGameLibrary.Graphics.SpriteClass;

namespace MonoGameLibrary.nodes
{
    public class Entity

    {
        public string ID { get; set; }
        public Vector2 Position => body.Position;



        public float Rotation => body.Rotation;



        public float Mass { get; set; }



        public Vector2 Velocity => body.LinearVelocity;



        public Body body;

        public World world;


        public Sprite sprite;

        public EntityData Attributes;




        public Entity(ref World _world, EntityList entityList, string _ID, Vector2 _position, float _rotation = 0)
        {
            ID = _ID;

            Attributes = entityList.entityList[ID];

            var X = BodyType.Static;

            if (Attributes.BodyType == "Kinematic")
            {
                X = BodyType.Kinematic;
            }
            else if (Attributes.BodyType == "Dynamic")
            {
                X = BodyType.Dynamic;
            }
            else if (Attributes.BodyType == "Static")
            {
                X = BodyType.Static;
            }

            world = _world;

            body = _world.CreateBody(_position, _rotation, X );



            string shape = Attributes.HitboxShape;

            //TextureAtlas atlas = TextureAtlas.FromFile(base.Content, "images/atlas_definition/atlas-definition.xml")

            //sprite = TextureAtlas.CreateSprite(entityList.entityList[ID].Sprite);



            Fixture fixture = HitboxShape(body, Attributes, shape);
        } 


        public Fixture HitboxShape(Body body, EntityData Attributes, string shape)
        {
            if (shape == "Rectangle")
            {
                Console.WriteLine("Rectangle");
                return body.CreateRectangle(Attributes.Hitbox["x"], Attributes.Hitbox["y"], 1, new Vector2(Attributes.Hitbox["xOffset"], Attributes.Hitbox["yOffset"]));
            }
            else if (shape == "Circle")
            {
                Console.WriteLine("Circle");
                return body.CreateCircle(Attributes.Hitbox["Radius"], 1, new Vector2(Attributes.Hitbox["xOffset"], Attributes.Hitbox["yOffset"]));
            }
            else if (shape == "Circle")
            {
                Console.WriteLine("CreateEllipse");
                return body.CreateEllipse(Attributes.Hitbox["x"], Attributes.Hitbox["y"], 20,1);
            }
            else
            {
                Console.WriteLine("Default");
                return body.CreateRectangle(1, 1, 1, new Vector2(0, 0));
            }
        }

    }
}
