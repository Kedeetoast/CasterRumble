using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.General;
using MonoGameLibrary.nodes;
using MonoGameLibrary.General.Managers;
using System;

namespace MonoGameLibrary.Graphics
{
    public class Camera : Node2D
    {
        public float Zoom { get; set; }
        public Rectangle Bounds => Core.GraphicsDevice.Viewport.Bounds;
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        public Camera()
        {
            Zoom = 1f;
            Position = Vector2.Zero;

            // Self-register with the manager. Throws if a camera already exists.
            CameraManager.Register(this);
        }

        // Call this (or let GC/scene cleanup call it) to deregister safely.
        protected override void Dispose(Boolean dispose)
        {
            CameraManager.Unregister(this);
        }

        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-GlobalPosition.X, -GlobalPosition.Y, 0)) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Position += movePosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom = MathHelper.Clamp(Zoom + zoomAmount, 0.35f, 2f);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateMatrix();
        }
    }
}