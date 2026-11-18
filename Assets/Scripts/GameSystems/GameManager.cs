using System.Security.Cryptography.X509Certificates;
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
    public TextMeshProUGUI CheckpointText;

    public int playerLives = 1;
    public int playerScore = 0;
    public int remainingLives = 0;
    public int checkpoint = 15;
    private int checkpointPassed = 0;

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

        if(CheckpointText != null)
            CheckpointText.text = "Next Checkpoint: " + checkpoint;

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

        Debug.Log("Obstacles avoided: " +  playerScore);
        minSpawnInterval = Mathf.Max(0.25f, minSpawnInterval - 0.05f);

        if(playerScore >= checkpoint)
        {
            checkpointPassed++; 
            checkpoint *= (int)Mathf.Pow(1.25f, checkpointPassed);
            UpdateUI();
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

        if(remainingLives <= 0) { LoseGame(); }
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

    public bool CanShoot()
    {
        return state == GameState.Playing && ammo > 0;
    }

    public void shotFired()
    {
        if (!CanShoot()) return;
        ammo--;
        projectilesInFlight++;
        UpdateUI();
    }

    public void ProjectileFinished()
    {
        projectilesInFlight = Mathf.Max(projectilesInFlight - 1, 0);
        if (state == GameState.Playing && ammo == 0 && projectilesInFlight == 0)
        {
            state = GameState.Lost;
            UpdateUI();
        }
    }
}
