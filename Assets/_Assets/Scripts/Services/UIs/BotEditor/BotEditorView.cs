using System;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs.BotEditor
{
    public class BotEditorView : MonoBehaviour
    {
        [SerializeField] private Button createWheelButton;
        [SerializeField] private Button creteMediumBodyButton;
        [SerializeField] private Button saveButton, loadButton;
        public event Action<PartData.PartType> OnCreate;
        public event Action OnSave;
        public event Action OnLoad;

        private void Start()
        {
            createWheelButton.onClick.AddListener(CreateSmallWheel);
            creteMediumBodyButton.onClick.AddListener(CreateMediumBody);
            saveButton.onClick.AddListener(Save);
            loadButton.onClick.AddListener(Load);
        }

        private void CreateSmallWheel() => OnCreate?.Invoke(PartData.PartType.SmallWheel);
        private void CreateMediumBody() => OnCreate?.Invoke(PartData.PartType.MediumBody);
        private void Save() => OnSave?.Invoke();
        private void Load() => OnLoad?.Invoke();

        private void OnDestroy()
        {
            createWheelButton.onClick.RemoveAllListeners();
            creteMediumBodyButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();
        }
    }
}