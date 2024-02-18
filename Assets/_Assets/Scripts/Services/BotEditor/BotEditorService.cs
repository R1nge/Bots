using System.Collections.Generic;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorService
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;
        private readonly BotDataService _botDataService;

        private BotEditorService(IObjectResolver objectResolver, ConfigProvider configProvider, BotDataService botDataService)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _botDataService = botDataService;
        }
        
        public void Init() => _botDataService.OnSaveLoaded += OnSaveLoaded;

        private void OnSaveLoaded(IReadOnlyList<PartData> dataList)
        {
            foreach (var data in dataList)
            {
                Spawn(data);
            }
        }

        public BotPart SpawnNew(Vector3 position, PartData.PartType type)
        {
            var part = _configProvider.PartsConfig.GetPart(type);
            var partInstance = _objectResolver.Instantiate(part.prefab, position, Quaternion.identity);
            AddPart(partInstance.GetComponent<BotPart>(), part.partData);
            return partInstance;
        }

        private void Spawn(PartData partData)
        {
            var part = _configProvider.PartsConfig.GetPart(partData.partType);
            var position = new Vector3(partData.positionX, partData.positionY, partData.positionZ);
            var scale = new Vector3(partData.scaleX, partData.scaleY, partData.scaleZ);
            var rotation = new Vector3(partData.rotationX, partData.rotationY, partData.rotationZ);
            var partInstance = _objectResolver.Instantiate(part.prefab, position, Quaternion.Euler(rotation));
            partInstance.transform.localScale = scale;
            AddPart(partInstance, partData);
        }

        public void Destroy(BotPart botPart)
        {
            RemovePart(botPart);
            Object.Destroy(botPart.gameObject);
        }

        private void AddPart(BotPart botPart, PartData partData) => _botDataService.AddPart(botPart, partData);

        private void RemovePart(BotPart botPart) => _botDataService.RemovePart(botPart);

        public void Dispose() => _botDataService.OnSaveLoaded -= OnSaveLoaded;
    }
}