using System;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs.BotEditor
{
    public class BotEditorView : MonoBehaviour
    {
        [SerializeField] private Button buyWheelButton;
        [SerializeField] private Button buyMediumBodyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private Button saveButton, loadButton;
        [SerializeField] private Button testButton;
        [SerializeField] private Button undo, redo;
        public Button SaveButton => saveButton;
        public Button TestButton => testButton;
        public Button UndoButton => undo;
        public Button RedoButton => redo;
        public event Action<PartData.PartType> OnBuy;
        public event Action OnSell;
        public event Action OnSave;
        public event Action OnLoad;
        public event Action OnTest;

        public event Action OnUndo;
        public event Action OnRedo;

        private void Start()
        {
            buyWheelButton.onClick.AddListener(BuySmallWheel);
            buyMediumBodyButton.onClick.AddListener(BuyMediumBody);
            sellButton.onClick.AddListener(Sell);
            saveButton.onClick.AddListener(Save);
            loadButton.onClick.AddListener(Load);
            testButton.onClick.AddListener(Test);

            undo.onClick.AddListener(Undo);
            redo.onClick.AddListener(Redo);
        }

        private void Undo() => OnUndo?.Invoke();

        private void Redo() => OnRedo?.Invoke();

        private void BuySmallWheel() => OnBuy?.Invoke(PartData.PartType.SmallWheel);
        private void BuyMediumBody() => OnBuy?.Invoke(PartData.PartType.MediumBody);
        private void Sell() => OnSell?.Invoke();
        private void Save() => OnSave?.Invoke();
        private void Load() => OnLoad?.Invoke();
        private void Test() => OnTest?.Invoke();

        private void OnDestroy()
        {
            buyWheelButton.onClick.RemoveAllListeners();
            buyMediumBodyButton.onClick.RemoveAllListeners();
            sellButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();
            testButton.onClick.RemoveAllListeners();
            
            undo.onClick.RemoveAllListeners();
            redo.onClick.RemoveAllListeners();
        }
    }
}