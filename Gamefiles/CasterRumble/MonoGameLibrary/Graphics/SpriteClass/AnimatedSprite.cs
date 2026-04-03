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
    /// <summary>
    /// refers to the accesable animations this animated sprite is able to use
    /// </summary>
    public Dictionary<string, Animation> AvaiableAnimations { get; set; }


    // VVV constructors VVV

    /// <summary>
    /// Creates a new animated sprite.
    /// </summary>
    public AnimatedSprite() { Subscribe_Update(GraphicsManager.Instance); }

    /// <summary>
    /// Creates a new animated sprite with the specified frames and delay.
    /// </summary>
    /// <param name="animation">The animation for this animated sprite.</param>
    public AnimatedSprite(AnimatedSpriteSet _animatedSpriteSet)
    {
        SpriteSet = _animatedSpriteSet;
        Animation = _animatedSpriteSet.ActiveAnimation;
        Subscribe_Update(GraphicsManager.Instance);
    }


    // VVV Methods VVV

    /// <summary>
    /// Updates this animated sprite.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
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
    }

    private void Subscribe_Update(GraphicsManager graphicsManager)
    {
        Subscribe(graphicsManager);
        graphicsManager.UpdateEvent += UpdateCall; // attach the listener
        Console.WriteLine($"[Logger] Now listening to 'graphicsManager.DrawEvent'.");
    }

    public void Unsubscribe_Update(GraphicsManager graphicsManager)
    {
        Unsubscribe(graphicsManager);
        graphicsManager.UpdateEvent -= UpdateCall; // detach cleanly
    }

    // This function runs when the event fires
    private void UpdateCall(object sender, UpdateEventArgs e)
    {
        Console.WriteLine("[Logger] Event received! clicked at {e._ClickedAt:HH:mm:ss}");
        Update(e.GameTime);

    }
}
