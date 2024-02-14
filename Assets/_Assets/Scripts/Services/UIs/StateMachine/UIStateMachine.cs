using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStateMachine
    {
        private readonly Dictionary<UIStateType, IUIState> _states;
        private IUIState _currentGameState;
        private UIStateType _currentUIStateType;

        public UIStateMachine(UIStatesFactory uiStatesFactory)
        {
            _states = new Dictionary<UIStateType, IUIState>
            {
                { UIStateType.Game, uiStatesFactory.CreateGameState(this) }
            };
        }

        public void SwitchState(UIStateType uiStateType)
        {
            if (_currentUIStateType == uiStateType)
            {
                Debug.LogError($"Trying to switch to the same state {uiStateType}");
                return;
            }
            
            _currentGameState?.Exit();
            _currentGameState = _states[uiStateType];
            _currentUIStateType = uiStateType;
            _currentGameState.Enter();
        }
    }
}