using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Move
{
    public interface IMovingPart
    {
        void Move(Vector3 direction);
    }
}