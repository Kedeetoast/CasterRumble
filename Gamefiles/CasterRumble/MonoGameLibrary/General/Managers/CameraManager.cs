using MonoGameLibrary.Graphics;
using System;

namespace MonoGameLibrary.General.Managers
{

    /// <summary>
    /// Manages the single active camera. Prevents duplicate registrations
    /// and provides a null-safe accessor for Core to consume.
    /// </summary>
    public static class CameraManager
    {
        /// <summary>
        /// The currently active camera, or null if none has been registered.
        /// Core.Draw() checks this before applying a transform.
        /// </summary>
        public static Camera ActiveCamera { get; private set; }

        /// <summary>
        /// Returns true if a camera is currently registered and active.
        /// </summary>
        public static bool HasCamera => ActiveCamera != null;

        /// <summary>
        /// Registers a camera as the active camera.
        /// Throws InvalidOperationException if a camera is already registered.
        /// </summary>
        internal static void Register(Camera camera)
        {
            if (ActiveCamera != null)
            {
                throw new InvalidOperationException(
                    $"A Camera is already registered. Call Destroy() on the existing camera " +
                    $"before creating a new one, or use CameraManager.ActiveCamera to access it.");
            }

            ActiveCamera = camera;
        }

        /// <summary>
        /// Unregisters the given camera. Silently ignores if it is not the active one.
        /// </summary>
        internal static void Unregister(Camera camera)
        {
            if (ActiveCamera == camera)
            {
                ActiveCamera = null;
            }
        }

        /// <summary>
        /// Force-clears the active camera without needing a reference.
        /// Useful during scene transitions to guarantee a clean slate.
        /// </summary>
        public static void Clear()
        {
            ActiveCamera = null;
        }
    }

}
