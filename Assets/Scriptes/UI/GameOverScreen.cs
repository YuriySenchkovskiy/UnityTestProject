using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class GameOverScreen : Screen
    {
        public event Action RestartButtonClick;
        
        public override void Open()
        {
            CanvasGroup.alpha = 1;
            Button.interactable = true;
        }

        public override void Close()
        {
            CanvasGroup.alpha = 0;
            Button.interactable = false;
        }

        protected override void OnButtonClick()
        {
            RestartButtonClick?.Invoke();
        }
    }
}