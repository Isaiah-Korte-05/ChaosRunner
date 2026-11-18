using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI AmmoText;

    public int playerLives = 1;
    public int playerScore = 0;
    public int remainingLives = 0;

    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 2f;
    private float spawnTimer;
    private float nextSpawnTime;

    public bool isPlaying = true;

    public int startingAmmo = 10;
    private int ammo, projectilesInFlight;

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
    public GameState state = GameState.Playing;
    public static GameManager Instance { get; private set; }

    void Start()
    {
        ammo = startingAmmo;
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
            ScoreText.text = "Obstacles Avoided: " + playerScore;

        if (LivesText != null)
            LivesText.text = "Lives Left: " + remainingLives + " / " + playerLives;

        if (AmmoText != null)
            AmmoText.text = "Ammo: " + ammo;
    }

    //called when triggered to add point to score
    public void AddScore()
    {
        if (!isPlaying)
            return;

        playerScore++;
        UpdateUI();

        minSpawnInterval = Mathf.Max(0.25f, minSpawnInterval - 0.05f);

    }

    public void LoseAttempt()
    {
        if (!isPlaying)
        {
            return;
        }

        remainingLives--;
        UpdateUI();

        if(remainingLives <= 0) { LoseGame(); }
    }

    void LoseGame()
    {
        isPlaying = false;
        state = GameState.Lost;
        Time.timeScale = 0f;

        if (StatusText != null)
        {
            StatusText.gameObject.SetActive(true);
            StatusText.text = "GAME OVER!";
        }

    }

    public bool CanShoot()
    {
        return state == GameState.Playing && ammo > 0;
    }

    public void shotFired()
    {
        if (!CanShoot()) return;
        ammo--;
        UpdateUI();
    }
}
