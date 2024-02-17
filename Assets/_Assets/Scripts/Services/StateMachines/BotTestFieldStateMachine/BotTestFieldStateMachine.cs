using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine
{
    public class BotTestFieldStateMachine
    {
        private readonly Dictionary<BotTestFieldStateType, IBotTestFieldState> _states = new();
        private IBotTestFieldState _currentBotEditorState;
        private BotTestFieldStateType _currentBotTestFieldStateType;

        private BotTestFieldStateMachine(BotTestFieldStatesFactory botEditorStatesFactory)
        {
            AddState(BotTestFieldStateType.TestField, botEditorStatesFactory.CreateBotTestFieldState(this));
        }

        private void AddState(BotTestFieldStateType botTestFieldStateType, IBotTestFieldState botEditorState)
        {
            if (_states.TryAdd(botTestFieldStateType, botEditorState))
            {
                Debug.Log($"Added state {botTestFieldStateType}");
            }
            else
            {
                Debug.LogError($"State {botTestFieldStateType} already exists");
            }
        }

        public void SwitchState(BotTestFieldStateType botTestFieldStateType)
        {
            if (_currentBotTestFieldStateType == botTestFieldStateType)
            {
                Debug.LogError($"Trying to switch to the same state {botTestFieldStateType}");
                return;
            }
            
            _currentBotEditorState?.Exit();
            _currentBotEditorState = _states[botTestFieldStateType];
            _currentBotTestFieldStateType = botTestFieldStateType;
            _currentBotEditorState.Enter();
        }
    }
}