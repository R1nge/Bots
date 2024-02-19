using System.Collections.Generic;
using System.Linq;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Gameplay.Parts.Attack;
using _Assets.Scripts.Gameplay.Parts.Move;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    //TODO: separate it from the body
    //TODO: it should be the parent of the whole bot when in game
    public class BotController : InGameBotPart
    {
        private readonly List<IMovingPart> _moveParts = new();
        private readonly List<IAttackingPart> _attackParts = new();

        public void Init()
        {
            var moveParts = GetComponentsInChildren<MonoBehaviour>().OfType<IMovingPart>().ToArray();
            Debug.Log($"Length: {moveParts.Length}");  
            for (int i = 0; i < moveParts.Length; i++)
            {
                AddMovePart(moveParts[i]);
            }

            var attackParts = GetComponentsInChildren<MonoBehaviour>().OfType<IAttackingPart>().ToArray();
            for (int i = 0; i < attackParts.Length; i++)
            {
                AddAttackPart(attackParts[i]);
            }

            gameObject.AddComponent<Rigidbody>();
        }

        private void Update()
        {
            foreach (var moveBotPart in _moveParts)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    moveBotPart.Move(Vector3.forward);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    moveBotPart.Move(Vector3.left);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moveBotPart.Move(Vector3.back);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    moveBotPart.Move(Vector3.right);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                foreach (var attackPart in _attackParts)
                {
                    attackPart.Attack();
                }
            }
        }

        private void AddMovePart(IMovingPart movingPart) => _moveParts.Add(movingPart);

        public void RemoveMovePart(IMovingPart movingPart) => _moveParts.Remove(movingPart);

        private void AddAttackPart(IAttackingPart attackHammerPart) => _attackParts.Add(attackHammerPart);

        private void RemoveAttackPart(IAttackingPart attackHammerPart) => _attackParts.Remove(attackHammerPart);
    }
}