using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachines.BotEditorStateMachine
{
    public class BotEditorStateMachine
    {
        private readonly Dictionary<BotEditorStateType, IBotEditorState> _states = new();
        private IBotEditorState _currentBotEditorState;
        private BotEditorStateType _currentBotTestFieldStateType;

        private BotEditorStateMachine(BotEditorStatesFactory botEditorStatesFactory)
        {
            AddState(BotEditorStateType.BotEditor, botEditorStatesFactory.CreateBotEditorState(this));
        }

        private void AddState(BotEditorStateType botTestFieldStateType, IBotEditorState botEditorState)
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

        public void SwitchState(BotEditorStateType botTestFieldStateType)
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