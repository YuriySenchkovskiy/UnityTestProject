using Scriptes.Creatures.Hero;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    
    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _player.Died += OnDied;
        _restartButton.onClick.AddListener(OnRestartButtonCLick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }
    
    private void OnDisable()
    {
        _player.Died -= OnDied;
        _restartButton.onClick.RemoveListener(OnRestartButtonCLick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void OnDied()
    {
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;
    }

    private void OnRestartButtonCLick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}