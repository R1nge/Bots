using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Services.BotEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Assets.Scripts.Services.UIs.BotEditor
{
    public class BotEditorController : MonoBehaviour
    {
        [SerializeField] private BotEditorView botEditorView;
        [Inject] private BotDataService _botDataService;
        [Inject] private BotEditorService _botEditorService;
        [Inject] private PartSelectionService _partSelectionService;
        [Inject] private BotEditorCommandBufferService _botEditorCommandBufferService;
        private BotPart _botPart;

        private void Start()
        {
            botEditorView.OnBuy += Buy;
            botEditorView.OnSell += Sell;
            botEditorView.OnSave += Save;
            botEditorView.OnLoad += Load;
            botEditorView.OnTest += Test;

            botEditorView.OnRedo += Redo;
            botEditorView.OnUndo += Undo;
        }

        private void Redo()
        {
            _botEditorCommandBufferService.Redo();
        }

        private void Undo()
        {
            _botEditorCommandBufferService.Undo();
        }

        private void Test()
        {
            SceneManager.LoadSceneAsync("_Assets/Scenes/BotTestField", LoadSceneMode.Single);
        }

        private void Update()
        {
            botEditorView.SaveButton.interactable = _botDataService.CanSave();
            botEditorView.TestButton.interactable = _botDataService.CanSave();
            botEditorView.UndoButton.interactable = _botEditorCommandBufferService.HasCommands();
            botEditorView.RedoButton.interactable = _botEditorCommandBufferService.HasUndoCommands();
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
            _botPart = _botEditorService.SpawnNew(Vector3.zero, type);
            _partSelectionService.Select(_botPart);
        }

        private void Save() => _botDataService.Save();

        private void Load() => _botDataService.Load();

        private void OnDestroy()
        {
            botEditorView.OnBuy -= Buy;
            botEditorView.OnSell -= Sell;
            botEditorView.OnSave -= Save;
            botEditorView.OnLoad -= Load;
            botEditorView.OnTest -= Test;

            botEditorView.OnRedo -= Redo;
            botEditorView.OnUndo -= Undo;
        }
    }
}