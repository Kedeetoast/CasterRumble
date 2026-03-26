using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using MonoGameLibrary.Graphics.SpriteClass;

namespace MonoGameLibrary.nodes
{
    /// <summary>
    /// holds a dictionary detailing the attributes for entities based of ID
    /// </summary>
    public class EntityList
    {

        public EntityList(ContentManager content, string fileName)
        {
            entityList = Jsonlookup(content, fileName);
        }

        public Dictionary<string, EntityData> entityList;

        /// <summary>
        /// reads the given json file and creates a dictionary of EntityData objects, where the key is the Id of the entity
        /// </summary>
        public static Dictionary<string, EntityData> Jsonlookup(ContentManager content, string fileName)
        {
            string filePath = Path.Combine(content.RootDirectory, fileName);

            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<EntityListData>(json);

            var entityList = data.Entities.ToDictionary(e => e.Id);

            return entityList;
        }



    }

    /// <summary>
    /// read-only class that represents data of an entity object, such as sprite and hitbox info
    /// </summary>
    public class EntityData
    {
        public EntityData(string _id, string _SpriteAtlas, string _sprite, string _bodyType, string _hitboxShape,float _DespawnTime, Dictionary<string, float> _hitbox)
        {
            Id = _id;
            SpriteAtlasPath = _SpriteAtlas;
            Sprite = _sprite;
            BodyType = _bodyType;
            HitboxShape = _hitboxShape;
            Hitbox = _hitbox;
            DespawnTime = _DespawnTime;
        }

        public readonly string Id;
        public readonly string SpriteAtlasPath;
        public readonly string Sprite;
        public readonly string BodyType;
        public readonly string HitboxShape;
        public readonly float DespawnTime;
        public readonly Dictionary<string, float> Hitbox;

     

    }


    public class EntityListData
    {
        public List<EntityData> Entities { get; set; }
    }
}
