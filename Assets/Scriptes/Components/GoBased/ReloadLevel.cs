using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scriptes.Components.GoBased
{
    public class ReloadLevel : MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }
}