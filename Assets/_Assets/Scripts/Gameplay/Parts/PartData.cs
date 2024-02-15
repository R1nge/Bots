using System;

namespace _Assets.Scripts.Gameplay.Parts
{
    [Serializable]
    public struct PartData
    {
        public float positionX, positionY, positionZ;
        public float rotationX, rotationY, rotationZ;
        public float scaleX, scaleY, scaleZ;
        public PartType partType;
        
        public enum PartType
        {
            SmallBody,
            MediumBody,
            LargeBody,
            SmallWheel,
            MediumWheel,
            LargeWheel
        }
    }
}