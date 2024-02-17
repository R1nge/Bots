using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Misc
{
    public class BotTestFieldEntryPoint : MonoBehaviour
    {
        [Inject] private GameStatesFactory _gameStatesFactory;
        [Inject] private GameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine.AddState(GameStateType.TestField, _gameStatesFactory.CreateBotTestFieldState(_gameStateMachine));
            _gameStateMachine.SwitchState(GameStateType.TestField);
        }
    }
}