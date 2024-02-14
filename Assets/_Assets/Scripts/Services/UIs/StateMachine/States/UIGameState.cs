using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIGameState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UIGameState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public void Enter() => _ui = _uiFactory.CreateGameUI();

        public void Exit() => Object.Destroy(_ui);
    }
}