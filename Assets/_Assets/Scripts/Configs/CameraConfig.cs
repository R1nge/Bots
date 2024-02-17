using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Camera config", menuName = "Configs/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [SerializeField] private GameObject editorCamera;
        [SerializeField] private GameObject gameCamera;
        public GameObject EditorCamera => editorCamera;
        public GameObject GameCamera => gameCamera;
    }
}