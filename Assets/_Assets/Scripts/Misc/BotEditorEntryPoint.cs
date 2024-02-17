using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Misc
{
    public class BotEditorEntryPoint : MonoBehaviour
    {
        [Inject] private GameStateMachine _gameStateMachine;

        private void Start() => _gameStateMachine.SwitchState(GameStateType.BotEditor);
    }
}