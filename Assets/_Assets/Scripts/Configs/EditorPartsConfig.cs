using System;
using System.Collections.Generic;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Services.BotEditor;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EditorPartConfig", menuName = "Configs/EditorPartConfig")]
    public class EditorPartsConfig : ScriptableObject
    {
        [SerializeField] private List<EditorPartsData> editorParts;

        public EditorPartsData GetPart(PartData.PartType type)
        {
            foreach (var partsData in editorParts)
            {
                if (partsData.partData.partType == type)
                {
                    return partsData;
                }
            }
            
            throw new Exception($"Part with type {type} not found");
        }
        
        [Serializable]
        public struct EditorPartsData
        {
            public PartData partData;
            public BotPart prefab;
            public PartPreview preview;
            public string name;
            public int cost;
        }
    }
}