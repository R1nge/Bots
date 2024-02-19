using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.BotEditor.Commands
{
    public class BuyCommand : IEditorCommand
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly BotDataService _botDataService;
        private readonly PartData.PartType _partType;
        private readonly Vector3 _position;
        private BotPart _partInstance;

        public BuyCommand(ConfigProvider configProvider, IObjectResolver objectResolver, BotDataService botDataService, PartData.PartType partType, Vector3 position)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _botDataService = botDataService;
            _partType = partType;
            _position = position;
        }

        public BotPart PartInstance => _partInstance;

        public void Execute()
        {
            var part = _configProvider.PartsConfig.GetPart(_partType);
            if (_partInstance != null)
            {
                _partInstance.gameObject.SetActive(true);
            }
            else
            {
                _partInstance = _objectResolver.Instantiate(part.editorPrefab, _position, Quaternion.identity);
            }

            _botDataService.AddPart(_partInstance.GetComponent<BotPart>(), part.partData);
        }

        public void Undo()
        {
            _botDataService.RemovePart(_partInstance);
            _partInstance.gameObject.SetActive(false);
        }
    }
}