using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI StatusText;

    public int targetScore = 15;
    public int playerLives = 1;
    public int playerScore = 0;
    public int remainingLives = 0;

    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 2.125f;
    private float spawnTimer;
    private float nextSpawnTime;

    public bool isPlaying = true;

    void Start()
    {
        remainingLives = playerLives;
        UpdateUI();
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        if (StatusText != null) { 
            StatusText.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (!isPlaying) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= nextSpawnTime)
        {
            spawnTimer = 0f;
            nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);

            int spawnCount = Random.Range(1, 3);
            for (int i = 0; i < spawnCount; i++)
            {
                spawnManager.SpawnObject();
            }
        }
    }

    void UpdateUI()
    {
        if (ScoreText != null)
            ScoreText.text = "Obstacles Avoided: " + playerScore + " / " + targetScore;

        if (LivesText != null)
            LivesText.text = "Lives Left: " + remainingLives + " / " + playerLives;
    }

    //called when triggered to add point to score
    public void AddScore()
    {
        if (!isPlaying)
            return;

        playerScore++;
        UpdateUI();

        Debug.Log("Obstacles avoided: " +  playerScore);

        if(playerScore >= targetScore)
        {
            WinGame();
        }
    }

    public void LoseAttempt()
    {
        if (!isPlaying)
        {
            return;
        }

        remainingLives--;
        UpdateUI();

        Debug.Log("Remaining lives: " + remainingLives);

        if(remainingLives <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        isPlaying = false;
        Time.timeScale = 0f;

        if (StatusText != null)
        {
            StatusText.gameObject.SetActive(true);
            StatusText.text = "YOU WIN!";
        }

        Debug.Log("VICTORY ACHIEVED");
    }

    void LoseGame()
    {
        isPlaying = false;
        Time.timeScale = 0f;

        if (StatusText != null)
        {
            StatusText.gameObject.SetActive(true);
            StatusText.text = "GAME OVER!";
        }

        Debug.Log("YOU DIED");
    }

}
