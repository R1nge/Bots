using System.Collections.Generic;
using System.Linq;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.BotTestField
{
    public class BotTestFieldSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [Inject] private BotDataService _botDataService;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private IObjectResolver _objectResolver;

        public void Spawn()
        {
            int parentIndex = 0;

            KeyValuePair<BotPart, PartData> pair;
            for (int i = 0; i < _botDataService.PlacedParts.Count; i++)
            {
                pair = _botDataService.PlacedParts.ElementAt(i);
                if (pair.Value.partType == PartData.PartType.SmallBody || pair.Value.partType == PartData.PartType.MediumBody || pair.Value.partType == PartData.PartType.LargeBody)
                {
                    parentIndex = i;
                }
            }

            Debug.Log($"Spawn {_botDataService.PlacedParts.Count} parts");
            Debug.Log($"Parent {_botDataService.PlacedParts.ElementAt(parentIndex)}");
            
            pair = _botDataService.PlacedParts.ElementAt(parentIndex);
            var position = new Vector3(pair.Value.positionX + spawnPoint.position.x, pair.Value.positionY + spawnPoint.position.y, pair.Value.positionZ + pair.Value.positionZ);
            var rotation = Quaternion.Euler(new Vector3(pair.Value.rotationX, pair.Value.rotationY, pair.Value.rotationZ));
            var scale = new Vector3(pair.Value.scaleX, pair.Value.scaleY, pair.Value.scaleZ);
            var parent = SpawnNew(position, rotation, pair.Value.partType);
            parent.transform.localScale = scale;

            for (int i = 0; i < _botDataService.PlacedParts.Count; i++)
            {
                if (i == parentIndex)
                {
                    continue;
                }

                pair = _botDataService.PlacedParts.ElementAt(i);
                position = new Vector3(pair.Value.positionX + spawnPoint.position.x, pair.Value.positionY + spawnPoint.position.y, pair.Value.positionZ + pair.Value.positionZ);
                rotation = Quaternion.Euler(new Vector3(pair.Value.rotationX, pair.Value.rotationY, pair.Value.rotationZ));
                scale = new Vector3(pair.Value.scaleX, pair.Value.scaleY, pair.Value.scaleZ);
                var part = SpawnNew(position, rotation, pair.Value.partType);
                part.transform.parent = parent.transform;
                part.transform.localScale = scale;
            }
            
            parent.GetComponent<BotController>().Init();
        }

        public BotPart SpawnNew(Vector3 position, Quaternion rotation, PartData.PartType type)
        {
            var part = _configProvider.PartsConfig.GetPart(type);
            var partInstance = _objectResolver.Instantiate(part.inGamePrefab, position, rotation);
            return partInstance;
        }
    }
}