using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Move
{
    public class MoveWheelPart : InGameBotPart, IMovingPart
    {
        [SerializeField] protected float speed;
        [SerializeField] protected Transform rotatableTransform;

        public void Move(Vector3 direction)
        {
            rotatableTransform.Rotate(direction * speed);
        }
    }
}