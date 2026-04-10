using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.nodes;
using System;
using System.Collections.Generic;


namespace MonoGameLibrary.Graphics.SpriteClass;

public class Sprite : Canvas
{
    // VVV atributes VVV

    /// <summary>
    /// Gets or Sets the active source texture region represented by this sprite.
    /// </summary>
    public TextureRegion Region => SpriteSet.ActiveRegion;

    /// <summary>
    /// refers to the accesable source texture regions this sprite is able to use
    /// </summary>
    public Dictionary<string,TextureRegion> AvaiableRegions { get; set; }

    public SpriteSet SpriteSet { get; set; }

    public float Width => Region.Width * Scale.X;

    /// <summary>
    /// Gets the height, in pixels, of this sprite.
    /// </summary>
    /// <remarks>
    /// Height is calculated by multiplying the height of the source texture region by the y-axis scale factor.
    /// </remarks>
    public float Height => Region.Height * Scale.Y;

    // VVV constuctors VVV

    /// <summary>
    /// Creates a new sprite.
    /// </summary>
    public Sprite() 
    {
        //Subscribe_Draw(GraphicsManager.Instance); 
    }

    /// <summary>
    /// Creates a new sprite using the specified source texture region and no dictionary of avaiable regions.
    /// </summary>
    /// <param name="region">The texture region to use as the source texture region for this sprite.</param>
    //public Sprite(TextureRegion region)
    //{
    //    Region = region;
    //    AvaiableRegions.Add("default", Region );
    //    Subscribe(GraphicsManager.Instance);
    //}

    //public Sprite(TextureRegion region, Entity _entity)
    //{
    //    Region = region;
    //    AvaiableRegions.Add("default", Region);
    //    Subscribe(GraphicsManager.Instance);
    //}


    /// <summary>
    /// Creates a new sprite using the specified active source texture region and a dictionary of avaiable regions.
    /// </summary>
    /// <param name="region">The texture region to use as the source texture region for this sprite.</param>
    public Sprite(SpriteSet _SpriteSet)
    {
        SpriteSet = _SpriteSet;


        //Subscribe_Draw(GraphicsManager.Instance);
    }


    //VVV methods VVV


    /// <summary>
    /// Sets the origin of this sprite to the center.
    /// </summary>
    public void CenterOrigin()
    {
        Origin = new Vector2(Region.Width, Region.Height) * 0.5f;
    }

    /// <summary>
    /// Submit this sprite for drawing to the current batch.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch instance used for batching draw calls.</param>
    /// <param name="position">The xy-coordinate position to render this sprite at.</param>
    public override void Draw(GameTime gameTime)
    {
        Region.Draw(spriteBatch, GlobalPosition, Color, GlobalRotation, Origin, GlobalScale, Effects, LayerDepth);
    }


    
}

public enum SpriteType
{
    Animated,
    Static,
    Animated_SpriteSet,
    Static_SpriteSet,
    Animated_FullSpriteSet,
    Static_FullSpriteSet,
}