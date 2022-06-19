using System;
using DefaultNamespace.UI;
using Pipes;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameModel : MonoBehaviour
    {
        [SerializeField] private Pirate _pirate;
        [SerializeField] private PipeGenerator _pipeGenerator;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;

        private void OnEnable()
        {
            _startScreen.PlayButtonCLick += OnPlayButtonClick;
            _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
            _pirate.GameOver += OnGameOver;
        }

        private void Start()
        {
            Time.timeScale = 0;
            _startScreen.Open();
        }

        private void OnDisable()
        {
            _startScreen.PlayButtonCLick -= OnPlayButtonClick;
            _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
            _pirate.GameOver -= OnGameOver;
        }

        private void OnPlayButtonClick()
        {
            _startScreen.Close();
            StartGame();
        }
        
        private void OnRestartButtonClick()
        {
            _gameOverScreen.Close();
            _pipeGenerator.ResetPool();
            StartGame();
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            _pirate.ResetPlayer();
        }

        public void OnGameOver()
        {
            Time.timeScale = 0;
            _gameOverScreen.Open();
        }
    }
}