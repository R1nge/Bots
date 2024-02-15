using System;

namespace _Assets.Scripts.Gameplay.Parts
{
    [Serializable]
    public struct PartData
    {
        public float positionX, positionY, positionZ;
        public float rotationX, rotationY, rotationZ;
        public float scale;
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