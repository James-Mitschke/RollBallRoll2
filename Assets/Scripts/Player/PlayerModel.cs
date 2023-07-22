using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public PlayerModel(float movementSpeed, float jumpSpeed)
        {
            MovementSpeed = movementSpeed;
            JumpSpeed = jumpSpeed;
        }

        public float MovementSpeed { get; set; } = 250f;

        public float JumpSpeed { get; set; } = 10000f;
    }
}
