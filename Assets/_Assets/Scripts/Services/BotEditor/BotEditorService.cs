using System.Collections.Generic;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Services.BotEditor.Commands;
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
        private readonly BotEditorCommandBufferService _botEditorCommandBufferService;

        private BotEditorService(IObjectResolver objectResolver, ConfigProvider configProvider, BotDataService botDataService, BotEditorCommandBufferService botEditorCommandBufferService)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _botDataService = botDataService;
            _botEditorCommandBufferService = botEditorCommandBufferService;
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
            var command = new BuyCommand(_configProvider, _objectResolver, _botDataService, type, position);
            _botEditorCommandBufferService.Execute(command);
            return command.PartInstance;
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

        public void Sell(BotPart botPart)
        {
            _botEditorCommandBufferService.Execute(new SellCommand(_configProvider, _objectResolver, _botDataService, botPart.PartType, botPart.transform.position, botPart));
        }

        private void AddPart(BotPart botPart, PartData partData) => _botDataService.AddPart(botPart, partData);

        public void Dispose() => _botDataService.OnSaveLoaded -= OnSaveLoaded;
    }
}