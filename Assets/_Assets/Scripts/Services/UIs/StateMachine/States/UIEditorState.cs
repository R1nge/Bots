using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIEditorState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UIEditorState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public void Enter() => _ui = _uiFactory.CreateEditorUI();

        public void Exit() => Object.Destroy(_ui);
    }
}