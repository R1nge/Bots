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
        public Button SaveButton => saveButton;
        public Button TestButton => testButton;
        public event Action<PartData.PartType> OnBuy;
        public event Action OnSell;
        public event Action OnSave;
        public event Action OnLoad;
        public event Action OnTest;

        private void Start()
        {
            buyWheelButton.onClick.AddListener(BuySmallWheel);
            buyMediumBodyButton.onClick.AddListener(BuyMediumBody);
            sellButton.onClick.AddListener(Sell);
            saveButton.onClick.AddListener(Save);
            loadButton.onClick.AddListener(Load);
            testButton.onClick.AddListener(Test);
        }

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
        }
    }
}