using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Graphics.SpriteClass;
using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MonoGameLibrary.General.Utility
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
        [JsonPropertyName("_Id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("_SpriteAtlasPath")]
        public string SpriteAtlasPath { get; init; } = string.Empty;

        [JsonPropertyName("_Sprite")]
        public string Sprite { get; init; } = string.Empty;

        [JsonPropertyName("_BodyType")]
        public string BodyType { get; init; } = string.Empty;

        [JsonPropertyName("_HitboxShape")]
        public string HitboxShape { get; init; } = string.Empty;

        [JsonPropertyName("_Scale")]
        public float Scale { get; init; } = 1f;

        [JsonPropertyName("_DespawnTime")]
        public float DespawnTime { get; init; }

        [JsonPropertyName("_Hitbox")]
        public Dictionary<string, float> Hitbox { get; init; } = new();

        [JsonPropertyName("_SpriteList")]
        public List<string> SpriteList { get; init; } = new();


    }


    public class EntityListData
    {
        public List<EntityData> Entities { get; set; }
    }
}
