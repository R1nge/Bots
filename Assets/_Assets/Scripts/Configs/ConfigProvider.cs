using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private EditorPartsConfig partsConfig;
        [SerializeField] private CameraConfig cameraConfig;
        public UIConfig UIConfig => uiConfig;
        public EditorPartsConfig PartsConfig => partsConfig;
        public CameraConfig CameraConfig => cameraConfig;
    }
}