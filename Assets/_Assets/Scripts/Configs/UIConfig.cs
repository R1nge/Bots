using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        public GameObject gameUI;
        public GameObject editorUI;
        public GameObject GameUI => gameUI;
        public GameObject EditorUI => editorUI;
    }
}