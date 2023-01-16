using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Text bestScoreText;

    private int bestScoreCounter;
    
    private void Start()
    {
        bestScoreCounter = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        bestScoreText.text = bestScoreCounter.ToString();
        
        AddListenersToButtons();
    }
    
    private void AddListenersToButtons()
    {
        startButton.onClick.AddListener(GameManager.Instance.GameRestart);
        quitButton.onClick.AddListener(GameManager.Instance.GameQuit);
    }
}
