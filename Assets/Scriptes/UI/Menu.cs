using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public void OpenPannel(GameObject panel)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void ClosePannel(GameObject panel)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}