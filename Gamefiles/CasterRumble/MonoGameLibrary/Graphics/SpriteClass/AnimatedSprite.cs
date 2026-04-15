using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Managers;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Graphics.SpriteClass;

public class AnimatedSprite : Sprite
{

    // VVV Members VVV

    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

 
    /// <summary>
    /// Gets or Sets the animation for this animated sprite.
    /// </summary>
    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            SpriteSet.ActiveRegion = _animation.Frames[0];
        }
    }


    // VVV constructors VVV

    /// <summary>
    /// Creates a new animated sprite.
    /// </summary>
    public AnimatedSprite() {  }

    /// <summary>
    /// Creates a new animated sprite with the specified frames and delay.
    /// </summary>
    /// <param name="animation">The animation for this animated sprite.</param>
    public AnimatedSprite(AnimatedSpriteSet _animatedSpriteSet)
    {
        SpriteSet = _animatedSpriteSet;
        Animation = _animatedSpriteSet.ActiveAnimation;
        CenterOrigin();
        //Duel_Subscribe(GraphicsManager.Instance, GameManager.Instance);
    }


    // VVV Methods VVV

    public override void ChangeActive(string newSprite)
    {
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero; // also reset elapsed so the new animation starts cleanly
        SpriteSet.ChangeActive(newSprite);

        // Sync _animation with the newly active animation
        Animation = ((AnimatedSpriteSet)SpriteSet).ActiveAnimation;
    }

    /// <summary>
    /// Updates this animated sprite.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public override void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if (_currentFrame >= _animation.Frames.Count)
            {
                _currentFrame = 0;
            }

            SpriteSet.ActiveRegion = _animation.Frames[_currentFrame];
        }
        CenterOrigin();
    }

    

}
