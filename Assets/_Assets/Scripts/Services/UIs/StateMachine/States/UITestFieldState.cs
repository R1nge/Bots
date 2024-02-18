using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UITestFieldState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UITestFieldState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public void Enter() => _ui = _uiFactory.CreateTestFieldUI();

        public void Exit() => Object.Destroy(_ui);
    }
}