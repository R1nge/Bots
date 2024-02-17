using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Assets.Scripts.Misc
{
    public class Boot : MonoBehaviour
    {
        private void Start() => SceneManager.LoadSceneAsync("_Assets/Scenes/BotEditor", LoadSceneMode.Single);
    }
}