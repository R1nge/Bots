using System;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Services.BotEditor;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs.BotEditor
{
    public class BotEditorController : MonoBehaviour
    {
        [SerializeField] private BotEditorView botEditorView;
        [Inject] private BotEditorService _botEditorService;
        [Inject] private PartSelectionService _partSelectionService;
        private BotPart _botPart;

        private void Start()
        {
            botEditorView.OnBuy += Buy;
            botEditorView.OnSell += Sell;
            botEditorView.OnSave += Save;
            botEditorView.OnLoad += Load;
        }

        private void Update()
        {
            botEditorView.SaveButton.interactable = _botEditorService.CanSave();
        }

        private void Sell()
        {
            if (_partSelectionService.SelectedPart != null)
            {
                _botEditorService.Destroy(_partSelectionService.SelectedPart);
            }
        }

        private void Buy(PartData.PartType type)
        {
            //Spawn preview
            //Which checks if can be placed
            //LMB to place it
            _botPart = _botEditorService.SpawnNew(Vector3.zero, type);
            _partSelectionService.Select(_botPart);
        }

        private void Save()
        {
            _botEditorService.Save();
        }

        private void Load()
        {
            _botEditorService.Load();
        }

        private void OnDestroy()
        {
            botEditorView.OnBuy -= Buy;
            botEditorView.OnSell -= Sell;
            botEditorView.OnSave -= Save;
            botEditorView.OnLoad -= Load;
        }
    }
}