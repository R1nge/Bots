using System;
using System.Collections.Generic;
using System.Linq;
using _Assets.Scripts.Gameplay.Parts;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Assets.Scripts.Services
{
    public class BotDataService
    {
        //TODO: save in json instead
        private readonly Dictionary<BotPart, PartData> _placedParts = new();
        public IReadOnlyDictionary<BotPart, PartData> PlacedParts => _placedParts;
        public event Action<IReadOnlyList<PartData>> OnSaveLoaded; 

        public void AddPart(BotPart botPart, PartData partData) => _placedParts.Add(botPart, partData);

        public void RemovePart(BotPart botPart) => _placedParts.Remove(botPart);

        public bool CanSave()
        {
            if (_placedParts.Count == 0)
            {
                return false;
            }
            
            bool canSave = true;

            foreach (var pair in _placedParts)
            {
                var key = (EditorBotPart)pair.Key;
                if (!key.CanBePlaced)
                {
                    Debug.LogError($"Bot won't be saved; Part {pair.Value.partType} is not placed correctly");
                    canSave = false;
                }
            }

            return canSave;
        }
        
        public void Save()
        {
            if (!CanSave())
            {
                return;
            }

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

            OnSaveLoaded?.Invoke(data);
        } 
    }
}