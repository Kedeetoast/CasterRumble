using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.General.Utility; 
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace MonoGameLibrary.nodes
{
    public class Entity : Node2D

    {
        public string ID { get; set; }
        public override Vector2 Position
        {
            get => body.Position;
            set
            {
                body.Position = value;
            }
        }


        public override float Rotation => body.Rotation;

        



        public float Mass 
        { 
            get
            {
                return body.Mass;
            }
            
            set
            {
                body.Mass = value;
            }
           
        }



        public Vector2 Velocity
        {
            get => body.LinearVelocity;
            protected set => body.LinearVelocity = value;
        }


        public Body body;

        public World world;


        public Sprite sprite;

        public EntityData Attributes;

        public Vector2 Gravity
        {
            get
            {
                if (IsOnGround())
                    return Vector2.Zero;

                if (Velocity.Y <= 0)
                {
                    // Rising — apply reduced gravity for floatier jump arc
                    return new Vector2(0, GameManager.Instance.Gravity.Y * 0.6f);
                }
                else
                {
                    // Falling — apply stronger gravity for snappy landing
                    return new Vector2(0, GameManager.Instance.Gravity.Y * 2.5f);
                }
            }
        }




        public Entity(ref World _world, string _ID, Vector2 _position, float _rotation = 0, SpriteType _spriteType = SpriteType.Static) : base()
        {
            ID = _ID;

            Attributes = GameManager.Instance.EntityList.entityList[ID];

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

            body = _world.CreateBody(_position, _rotation, X);
            body.Tag = this;
            LoadSprite(_spriteType);


            string shape = Attributes.HitboxShape;

            //TextureAtlas atlas = TextureAtlas.FromFile(base.Content, "images/atlas_definition/atlas-definition.xml")

            //sprite = TextureAtlas.CreateSprite(entityList.entityList[ID].Sprite);



            Fixture fixture = HitboxShape(ref body, Attributes, shape);
        }

        public override void Update(GameTime gametime)
        {
            body.ApplyLinearImpulse(Gravity * Mass);
        }


        private Fixture HitboxShape(ref Body body, EntityData Attributes, string shape)
        {
            switch (shape.ToLowerInvariant())
            {
                case "rectangle":
                    System.Diagnostics.Debug.WriteLine("Rectangle");
                    return body.CreateRectangle(Attributes.Hitbox["x"] * Scale.X, Attributes.Hitbox["y"] * Scale.Y, 1, new Vector2(Attributes.Hitbox["xOffset"], Attributes.Hitbox["yOffset"]));
                case "circle":
                    System.Diagnostics.Debug.WriteLine("Circle");
                    return body.CreateCircle(Attributes.Hitbox["Radius"] * Scale.X, 1, new Vector2(Attributes.Hitbox["xOffset"], Attributes.Hitbox["yOffset"]));
                case "ellipse":
                    System.Diagnostics.Debug.WriteLine("Ellipse");
                    return body.CreateEllipse(Attributes.Hitbox["x"] * Scale.X, Attributes.Hitbox["y"] * Scale.Y, 20, 1);
                default:
                    System.Diagnostics.Debug.WriteLine($"Warning: Unknown hitbox shape \"{shape}\", defaulting to 1x1 rectangle.");
                    return body.CreateRectangle(1, 1, 1, Vector2.Zero);
            }
        }

        private void LoadSprite(SpriteType _spriteType = SpriteType.Static)
        {
            
            TextureAtlas atlas = TextureAtlas.FromFile(GameManager.Instance.Scene_Content, GameManager.Instance.EntityList.entityList[ID].SpriteAtlasPath);
            switch (_spriteType)
            {
                case SpriteType.Animated:
                    sprite = atlas.CreateAnimatedSprite(GameManager.Instance.EntityList.entityList[ID].Sprite);
                    break;
                case SpriteType.Static:
                    sprite = atlas.CreateSprite(GameManager.Instance.EntityList.entityList[ID].Sprite);
                    break;
                case SpriteType.Animated_SpriteSet:
                    sprite = atlas.CreateAnimatedSprite_spriteset(GameManager.Instance.EntityList.entityList[ID].SpriteList);
                    break;
                case SpriteType.Static_SpriteSet:
                    sprite = atlas.CreateSprite_spriteset(GameManager.Instance.EntityList.entityList[ID].SpriteList);
                    break;
                case SpriteType.Animated_FullSpriteSet:
                    sprite = atlas.CreateAnimatedSprite_spriteset();
                    break;
                case SpriteType.Static_FullSpriteSet:
                    sprite = atlas.CreateSprite_spriteset();
                    break;  
            }
            sprite.Parent = this;
            LoadSubSprites();
        }

        protected virtual void LoadSubSprites()
        {



        }

        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
                if (disposing)
                {
                   try { world?.Remove(body); }
                   catch (ArgumentException) { /* already removed — ignore or log */ }

                   sprite?.Dispose();
                }
        }

        public bool IsOnGround(float slopeThreshold = 0.7f)
        {
            var contactEdge = body.ContactList;

            while (contactEdge != null)
            {
                var contact = contactEdge.Contact;

                if (contact.IsTouching)
                {
                    contact.GetWorldManifold(out Vector2 normal, out FixedArray2<Vector2> _);

                    // The contact normal points from fixture A toward fixture B.
                    // If we are fixture B, the normal points toward us — flip it so it
                    // always points away from us (i.e. away from the ground, upward).
                    if (contact.FixtureB.Body == body)
                        normal = -normal;

                    // With Y+ up, a normal with a strong positive Y means the
                    // contact surface is below us — we're on the ground.
                    if (normal.Y >= slopeThreshold)
                        return true;
                }

                contactEdge = contactEdge.Next;
            }

            return false;
        }

    }
}