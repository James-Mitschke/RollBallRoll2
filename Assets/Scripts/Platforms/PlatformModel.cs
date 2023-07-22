using UnityEngine;

namespace Assets.Scripts.Platforms
{
    /// <summary>
    /// The model for storing positional data of a platform.
    /// </summary>
    public class PlatformModel
    {
        /// <summary>
        /// Initializes a new <see cref="PlatformModel"/> using the current position and the old position of the <see cref="GameObject"/>.
        /// </summary>
        /// <param name="currentPosition">The current position of the <see cref="GameObject"/>.</param>
        /// <param name="oldPosition">The position of the <see cref="GameObject"/> at the end of the last frame.</param>
        public PlatformModel(Vector3 currentPosition, Vector3 oldPosition)
        {
            CurrentPosition = currentPosition;
            OldPosition = oldPosition;
        }

        /// <summary>
        /// Initializes a new <see cref="PlatformModel"/> using the initial position of the <see cref="GameObject"/>>.
        /// </summary>
        /// <param name="initialPosition">The initial position of the <see cref="GameObject"/>.</param>
        public PlatformModel(Vector3 initialPosition)
        {
            CurrentPosition = initialPosition;
            OldPosition = initialPosition;
        }

        /// <summary>
        /// Gets or sets the current position of the gameobject.
        /// </summary>
        public Vector3 CurrentPosition { get; set; }

        /// <summary>
        /// Gets or sets the position of the gameobject from the last frame.
        /// </summary>
        public Vector3 OldPosition { get; set; }
    }
}
