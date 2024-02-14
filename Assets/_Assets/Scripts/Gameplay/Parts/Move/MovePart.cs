using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Move
{
    public abstract class MovePart : BotPart
    {
        [SerializeField] protected float speed;
        [SerializeField] protected Transform rotatableTransform;

        public abstract void Move(Vector3 direction);
    }
}