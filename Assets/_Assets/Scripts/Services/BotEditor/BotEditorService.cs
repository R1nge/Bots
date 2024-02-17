using System.Collections.Generic;
using System.Linq;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Parts;
using Newtonsoft.Json;
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

        private void AddPart(BotPart botPart, PartData partData) => _placedParts.Add(botPart, partData);

        private void RemovePart(BotPart botPart) => _placedParts.Remove(botPart);

        //TODO: save in json instead
        public void Save()
        {
            foreach (var pair in _placedParts.Keys.ToList())
            {
                var part = _placedParts[pair];

                part.positionX = pair.transform.position.x;
                part.positionY = pair.transform.position.y;
                part.positionZ = pair.transform.position.z;

                part.rotationX = pair.transform.rotation.eulerAngles.x;
                part.rotationY = pair.transform.rotation.eulerAngles.y;
                part.rotationZ = pair.transform.rotation.eulerAngles.z;

                part.scaleX = pair.transform.localScale.x;
                part.scaleY = pair.transform.localScale.y;
                part.scaleZ = pair.transform.localScale.z;

                _placedParts[pair] = part;
            }

            var json = JsonConvert.SerializeObject(_placedParts.Values);

            Debug.Log(json);

            PlayerPrefs.SetString("RobotData", json);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            var list = _placedParts.ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Object.Destroy(list[i].Key.gameObject);
            }

            _placedParts.Clear();

            var json = PlayerPrefs.GetString("RobotData");
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("No robot data found");
                return;
            }

            Debug.Log(json);

            var data = JsonConvert.DeserializeObject<List<PartData>>(json);

            for (int i = 0; i < data.Count; i++)
            {
                Spawn(data[i]);
            }
        }
    }
}