using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : SingletonMonobehaviour<GameplayUI>
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button pauseButton;
    
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private TMP_Text highScoreText;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AddListenersToButtons();
    }

    private void AddListenersToButtons()
    {
        restartButton.onClick.AddListener(GameManager.Instance.GameRestart);
        continueButton.onClick.AddListener(GameManager.Instance.GameContinue);
        quitButton.onClick.AddListener(GameManager.Instance.GoToMainMenu);
        pauseButton.onClick.AddListener(GameManager.Instance.GamePause);
    }
    
    public void UpdateUICounters(int score, int diamondCount, int highScore)
    {
        scoreText.text = score.ToString();
        diamondText.text = diamondCount.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void GameContinueUI()
    {
        continuePanel.SetActive(false);
        endPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void GameplayPauseUI()
    {
        continuePanel.SetActive(true);
        endPanel.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void GameOverUI()
    {
        continuePanel.SetActive(false);
        endPanel.SetActive(true);
        pausePanel.SetActive(true);
    }
}
