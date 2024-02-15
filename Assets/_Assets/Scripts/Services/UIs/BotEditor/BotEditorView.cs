using System;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs.BotEditor
{
    public class BotEditorView : MonoBehaviour
    {
        [SerializeField] private Button createWheel;
        public event Action<PartData.PartType> OnCreate;

        private void Start() => createWheel.onClick.AddListener(CreateSmallWheel);

        private void CreateSmallWheel() => OnCreate?.Invoke(PartData.PartType.SmallWheel);
    }
}