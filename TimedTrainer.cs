using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class TimedTrainer : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Reference to the asteroid prefabs
    public GameObject crosshair; // Crosshair GameObject
    public GameObject gameOverPanel; // Game Over panel GameObject
    
    public static TimedTrainer instance;
    public static bool gameOver;
    public static int targetsHit, targetsMissed, score, totalClicks; // Added score variable
    public static int highScore = -1000; // High score persists throughout the game instance

    public TextMeshProUGUI targetsHitLabel, targetsMissedLabel, accuracyLabel, scoreLabel, countdownLabel; // Added scoreLabel
    public TextMeshProUGUI finalScoreLabel, finalHitLabel, finalMissLabel, finalAccuracyLabel, highScoreLabel; // Final stats labels
    
    public bool gameStarted; // Track whether the game has started
    public bool countdownDone;


    private void Start()
    {
        targetsHit = 0;
        targetsMissed = 0;
        score = 0;
        totalClicks = 0;
        gameOver = false;
        gameStarted = false;
        countdownDone = false;
        instance = this;
        score = 0; // Initialize score
        crosshair.SetActive(false); // Hide the crosshair initially
        gameOverPanel.SetActive(false); // Hide the Game Over panel initially
        StartCoroutine(StartGameWithCountdown()); // Start the game with a countdown
    }

    private void Update()
    {
        if (gameOver)
        {
            if (!gameOverPanel.activeSelf)
            {
                ShowGameOverPanel(); // Show the Game Over panel when the game ends
            }
            return;
        }

        //int totalAttempts = Mathf.Max(targetsHit + targetsMissed, 1); // Avoid division by zero
        if (totalClicks == 0) 
        { 
            accuracyLabel.text = "Accuracy: " + (targetsHit * 100 / 1) + "%";
        }
        else 
        { 
            accuracyLabel.text = "Accuracy: " + (targetsHit * 100 / totalClicks) + "%"; 
        }
        
        targetsHitLabel.text = "Hit: " + targetsHit;
        targetsMissedLabel.text = "Miss: " + targetsMissed;
        scoreLabel.text = "Score: " + score; // Update score UI

       // if (gameOver && Input.GetKeyDown(KeyCode.R))
       // {
        //    SceneManager.LoadScene(0); // Reload the scene
       // }
    }

    private IEnumerator StartGameWithCountdown()
    {
        countdownLabel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Ensure the cursor is visible
        Cursor.visible = true;

        for (int i = 3; i > 0; i--)
        {
            countdownLabel.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        countdownLabel.text = "GO!";
        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        crosshair.SetActive(true); // Show the crosshair after the countdown
        countdownLabel.gameObject.SetActive(false); // Hide the countdown label
        gameStarted = true; // Mark the game as started
        countdownDone = true;

        SpawnTargets(); // Spawn the first target
    }

    public IEnumerator SpawnTargetsWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Add a short delay before spawning a new asteroid
        SpawnTargets();
    }

    public void SpawnTargets()
    {
        if (gameOver || asteroidPrefabs.Length == 0)
        {
            Debug.LogError("Game over or no asteroid prefabs assigned!");
            return;
        }

        // Ensure only one target exists
        if (GameObject.FindGameObjectsWithTag("Target").Length > 0)
            return;

        Vector3 randomSpawn = new Vector3(Random.Range(-6f, 6f), Random.Range(-4f, 4f), 10f);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, 0f);
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);

        Instantiate(asteroidPrefabs[randomIndex], randomSpawn, randomRotation);
    }

    public void RegisterClick(bool isTarget)
    {
        if (!countdownDone) return;

        totalClicks++;

        if (isTarget)
        {
            // Do nothing; scoring for targets is handled in `Target.OnMouseDown()`.
        }
        else
        {
            if (gameStarted) // Only deduct score after the countdown
            {
                score -= 5; // Subtract 5 points for a random click
            }
        }
    }

    public void RegisterHit()
    {
        targetsHit++; 
        score += 15; // Add 10 points for hitting a target some error only adds 5 when I do 10
    }

    public void RegisterMiss()
    {
        targetsMissed++;
        score -= 10; // Subtract 10 points for missing a target
    }
    private void ShowGameOverPanel()
    {
        if (score > highScore)
        {
            highScore = score; // Update high score
        }
        // Display final statistics
        gameOver = true;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;

        finalScoreLabel.text = "Score: " + score;
        highScoreLabel.text = "High Score: " + highScore; 
        finalHitLabel.text = "Hit: " + targetsHit;
        finalMissLabel.text = "Miss: " + targetsMissed;
       // int totalAttempts = Mathf.Max(targetsHit + targetsMissed, 1);
        finalAccuracyLabel.text = "Accuracy: " + (targetsHit * 100 / totalClicks) + "%";

        // Hide other UI elements
       
        crosshair.SetActive(false);
        targetsHitLabel.gameObject.SetActive(false);
        targetsMissedLabel.gameObject.SetActive(false);
        accuracyLabel.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
    }
}

