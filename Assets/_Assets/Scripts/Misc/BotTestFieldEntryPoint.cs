using _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Misc
{
    public class BotTestFieldEntryPoint : MonoBehaviour
    {
        [Inject] private BotTestFieldStateMachine _botTestFieldStateMachine;

        private void Start() => _botTestFieldStateMachine.SwitchState(BotTestFieldStateType.TestField);
    }
}