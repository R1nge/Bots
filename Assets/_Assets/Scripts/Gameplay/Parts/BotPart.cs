using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts
{
    public abstract class BotPart : MonoBehaviour
    {
        [SerializeField] protected PartData.PartType partType;
        public PartData.PartType PartType => partType;

        public virtual void Awake(){}
    }
}