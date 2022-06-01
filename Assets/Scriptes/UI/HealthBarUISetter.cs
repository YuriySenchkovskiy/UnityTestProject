using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBarUISetter : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        
        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}