using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs.BotTestField
{
    public class BotTestFieldView : MonoBehaviour
    {
        [SerializeField] private Button returnToBotEditorButton;
        public event Action OnReturnToBotEditor;

        private void Start() => returnToBotEditorButton.onClick.AddListener(ReturnToBotEditor);

        private void ReturnToBotEditor() => OnReturnToBotEditor?.Invoke();

        private void OnDestroy() => returnToBotEditorButton.onClick.RemoveAllListeners();
    }
}