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

        private void Start()
        {
            botEditorView.OnCreate += Create;
            botEditorView.OnSave += Save;
            botEditorView.OnLoad += Load;
        }

        private void Create(PartData.PartType type)
        {
            _botEditorService.SpawnNew(Vector3.zero, type);
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
            botEditorView.OnCreate -= Create;
            botEditorView.OnSave -= Save;
            botEditorView.OnLoad -= Load;
        }
    }
}