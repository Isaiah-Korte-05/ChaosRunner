using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int targetScore = 15;
    public int targetLives = 1;
    public int playerScore = 0;
    public int playerLives = 0;

    public bool isPlaying = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLives = targetLives;
    }

    //called when triggered to add point to score
    public void AddScore()
    {
        if (!isPlaying)
            return;

        playerScore++;

        Debug.Log("Score: " +  playerScore);

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

        playerLives--;

        Debug.Log("Lives: " + playerLives);

        if(playerLives <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        isPlaying = false;

        Debug.Log("VICTORY ACHIEVED");
    }

    void LoseGame()
    {
        isPlaying = true;

        Debug.Log("YOU DIED");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
