using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Attack
{
    public abstract class AttackPart : BotPart
    {
        [SerializeField] protected int damage;
        [SerializeField] protected float cooldown;
        protected float currentCooldown;

        public abstract void Attack();
    }
}