using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Move
{
    public class Wheel : MovePart
    {
        public override void Move(Vector3 direction)
        {
            rotatableTransform.Rotate(direction * speed);
        }
    }
}