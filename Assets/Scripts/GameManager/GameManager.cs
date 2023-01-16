using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject blockPrefab;
    public Player player;
    public GameplayUI gameplayUI;

    public bool isGamePlaying;
    
    private int score;
    private int diamondCount;
    private int highScore;

    private const float constOffSetZ = -43.5f;
    private float newOffSetZ;
    private int incrementOfOffSetZ;
    private float timePassed;

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1f;
        isGamePlaying = false;
        
        newOffSetZ = -43.5f;
        incrementOfOffSetZ = 0;
        score = 0;
        timePassed = 0f;
        
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        diamondCount = PlayerPrefs.HasKey("Diamonds") ? PlayerPrefs.GetInt("Diamonds") : 0;
    }

    private void Update()
    {
        if (isGamePlaying)
        {
            Vector3 playerPosition = Player.Instance.GetPlayerPosition();
            
            if (playerPosition.z <= ((constOffSetZ / 3) + (incrementOfOffSetZ * 2 * constOffSetZ / 3))) // x/3 + n * 2x/3
            {
                incrementOfOffSetZ++;
                SpawnBlock();
            }
            
            // Score Update each second
            timePassed += Time.deltaTime;
            
            if (timePassed >= 1f)
            {
                UpdateScore();
                timePassed -= 1f;
            }
        }
    }

    private void SpawnBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab);
        newBlock.transform.position = new Vector3(0f, 0f, newOffSetZ);

        newOffSetZ += 2 * constOffSetZ / 3;
    }

    public void UpdateDiamond()
    {
        diamondCount++;
        PlayerPrefs.SetInt("Diamonds", diamondCount);
        
        gameplayUI.UpdateUICounters(score, diamondCount, highScore);
    }

    private void UpdateScore()
    {
        score++;
        GameplayUI.Instance.UpdateUICounters(score, diamondCount, highScore);
    }

    private void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    
    public void GamePause()
    {
        Time.timeScale = 0f;
        isGamePlaying = false;

        GameplayUI.Instance.GameplayPauseUI();
        CheckHighScore();
        GameplayUI.Instance.UpdateUICounters(score, diamondCount, highScore);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isGamePlaying = false;
        
        GameplayUI.Instance.GameOverUI();
        CheckHighScore();
        GameplayUI.Instance.UpdateUICounters(score, diamondCount, highScore);
    }

    public void GameContinue()
    {
        GameplayUI.Instance.GameContinueUI();
        
        Time.timeScale = 1f;
        isGamePlaying = true;
    }

    public void GameRestart()
    {
        var op = SceneManager.LoadSceneAsync(1);
        op.completed += (x) =>
        {
            Debug.Log("Loaded");
            GameplayUI.Instance.GameContinueUI();
            GameplayUI.Instance.UpdateUICounters(score, diamondCount, highScore);
            GameManager.Instance.isGamePlaying = true;
        };
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
