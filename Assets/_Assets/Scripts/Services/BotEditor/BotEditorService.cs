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

        private BotEditorService(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public void Spawn(Vector3 position, PartData.PartType type)
        {
            _objectResolver.Instantiate(_configProvider.PartsConfig.GetPart(type).prefab);
        }
    }
}