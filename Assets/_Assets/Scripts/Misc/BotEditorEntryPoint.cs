using _Assets.Scripts.Services.StateMachines.BotEditorStateMachine;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Misc
{
    public class BotEditorEntryPoint : MonoBehaviour
    {
        [Inject] private BotEditorStatesFactory _botEditorStatesFactory;
        [Inject] private BotEditorStateMachine _botEditorStateMachine;

        private void Start() => _botEditorStateMachine.SwitchState(BotEditorStateType.BotEditor);
    }
}