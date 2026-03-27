using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.General.Managers;
using MonoGameLibrary.nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameLibrary.Graphics.SpriteClass;

public class Sprite
{
    // VVV atributes VVV

    /// <summary>
    /// Gets or Sets the active source texture region represented by this sprite.
    /// </summary>
    public TextureRegion Region => SpriteSet.ActiveSprite;

    /// <summary>
    /// refers to the accesable source texture regions this sprite is able to use
    /// </summary>
    public Dictionary<string,TextureRegion> AvaiableRegions { get; set; }

    public SpriteSet SpriteSet { get; set; }

    /// <summary>
    /// Gets or Sets the color mask to apply when rendering this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is Color.White
    /// </remarks>
    public Color Color { get; set; } = Color.White;

    /// <summary>
    /// Gets or Sets the amount of rotation, in radians, to apply when rendering this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is 0.0f
    /// </remarks>
    public float Rotation { get; set; } = 0.0f;

    /// <summary>
    /// Gets or Sets the scale factor to apply to the x- and y-axes when rendering this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is Vector2.One
    /// </remarks>
    public Vector2 Scale { get; set; } = Vector2.One;


    /// <summary>
    /// Gets or Sets the scale factor to apply to the x- and y-axes when rendering this sprite.
    /// </summary>


    /// <remarks>
    /// Default value is Vector2.One
    /// </remarks>
    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    /// Gets or Sets the xy-coordinate origin point, relative to the top-left corner, of this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is Vector2.Zero
    /// </remarks>
    public Vector2 Origin { get; set; } = Vector2.Zero;

    /// <summary>
    /// Gets or Sets the sprite effects to apply when rendering this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is SpriteEffects.None
    /// </remarks>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// Gets or Sets the layer depth to apply when rendering this sprite.
    /// </summary>
    /// <remarks>
    /// Default value is 0.0f
    /// </remarks>
    public float LayerDepth { get; set; } = 0.0f;

    /// <summary>
    /// Gets the width, in pixels, of this sprite. 
    /// </summary>
    /// <remarks>
    /// Width is calculated by multiplying the width of the source texture region by the x-axis scale factor.
    /// </remarks>
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
        Subscribe(GraphicsManager.Instance); 
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


        Subscribe(GraphicsManager.Instance);
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
    public void Draw(SpriteBatch spriteBatch)
    {
        Region.Draw(spriteBatch, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }

    public void ChangeActiveRegion(string _Active)
    {
        SpriteSet.ChangeActive(_Active);
    }

    private void Subscribe(GraphicsManager graphicsManager)
    {
        graphicsManager.DrawEvent += DrawCall; // attach the listener
        Console.WriteLine($"[Logger] Now listening to 'graphicsManager.DrawEvent'.");
    }

    private void Unsubscribe(GraphicsManager graphicsManager)
    {
        graphicsManager.DrawEvent -= DrawCall; // detach cleanly
    }

    // This function runs when the event fires
    private void DrawCall(object sender, DrawEventArgs e)
    {
        Console.WriteLine("[Logger] Event received! clicked at {e._ClickedAt:HH:mm:ss}");
        Draw(e.spriteBatch);
    }
}
