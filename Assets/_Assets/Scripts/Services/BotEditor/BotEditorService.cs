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
        private readonly Dictionary<BotPart, PartData> _placedParts = new();

        private BotEditorService(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public void Spawn(Vector3 position, PartData.PartType type)
        {
            var part = _configProvider.PartsConfig.GetPart(type);
            var partInstance = _objectResolver.Instantiate(part.prefab, position, Quaternion.identity);
            AddPart(partInstance.GetComponent<BotPart>(), part.partData);
        }

        public void Destroy(BotPart botPart)
        {
            RemovePart(botPart);
            Object.Destroy(botPart.gameObject);
        }

        private void AddPart(BotPart botPart, PartData partData)
        {
            _placedParts.Add(botPart, partData);
        }

        private void RemovePart(BotPart botPart) => _placedParts.Remove(botPart);

        public void Save()
        {
            foreach (var partData in _placedParts.Values)
            {
                
            }
        }

        public void Load()
        {
            
        }
    }
}