using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Assets.Scripts.Services.UIs.BotTestField
{
    public class BotTestFieldController : MonoBehaviour
    {
        [SerializeField] private BotTestFieldView botTestFieldView;

        private void Start() => botTestFieldView.OnReturnToBotEditor += ReturnToBotEditor;

        private void ReturnToBotEditor() => SceneManager.LoadSceneAsync("_Assets/Scenes/BotEditor", LoadSceneMode.Single);

        private void OnDestroy() => botTestFieldView.OnReturnToBotEditor -= ReturnToBotEditor;
    }
}