using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Attack
{
    public abstract class AttackHammerPart : BotPart, IAttackingPart
    {
        [SerializeField] protected int damage;
        [SerializeField] protected float cooldown;
        protected float currentCooldown;

        public void Attack()
        {
            
        }
    }
}