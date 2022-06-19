using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace DefaultNamespace.UI
{
    public class StartScreen : Screen
    {
        public event Action PlayButtonCLick;
        
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
            PlayButtonCLick?.Invoke();
        }
    }
}