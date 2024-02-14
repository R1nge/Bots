using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class MoveBotPart : BotPart
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform rotatableTransform;

        public void Move(Vector3 direction)
        {
            rotatableTransform.Rotate(direction * (speed * Time.deltaTime));
        }
    }
}