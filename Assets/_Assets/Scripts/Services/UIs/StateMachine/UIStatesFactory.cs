﻿using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly UIFactory _uiFactory;
        
        private UIStatesFactory(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public IUIState CreateGameState(UIStateMachine uiStateMachine)
        {
            return new UIGameState(_uiFactory);
        }

        public IUIState CreateEditorState(UIStateMachine uiStateMachine)
        {
            return new UIEditorState(_uiFactory);
        }

        public IUIState CreateTestFieldState(UIStateMachine uiStateMachine)
        {
            return new UITestFieldState(_uiFactory);
        }
    }
}