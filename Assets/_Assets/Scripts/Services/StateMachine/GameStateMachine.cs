﻿using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<GameStateType, IGameState> _states;
        private IGameState _currentGameState;
        private GameStateType _currentGameStateType;

        public GameStateMachine(GameStatesFactory gameStatesFactory)
        {
            _states = new Dictionary<GameStateType, IGameState>
            {
                { GameStateType.BotEditor, gameStatesFactory.CreateBotEditorState(this) }
            };
        }

        public void SwitchState(GameStateType gameStateType)
        {
            if (_currentGameStateType == gameStateType)
            {
                Debug.LogError($"Trying to switch to the same state {gameStateType}");
                return;
            }
            
            _currentGameState?.Exit();
            _currentGameState = _states[gameStateType];
            _currentGameStateType = gameStateType;
            _currentGameState.Enter();
        }
    }
}