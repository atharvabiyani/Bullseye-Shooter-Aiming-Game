using TMPro;
using UnityEngine;
using System.Collections;

public class LevelTrainer : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Reference to asteroid prefabs
    public GameObject crosshair; // Crosshair GameObject
    public GameObject difficultyPanel; // Difficulty selection panel
    public GameObject gameOverPanel; // Game Over panel
    public GameCameraController gameCameraController; // Reference to the GameCameraController script

    public TextMeshProUGUI hitsLabel, remainingLabel, accuracyLabel, scoreLabel, countdownLabel; // In-game stats labels
    public TextMeshProUGUI gameOverLabel, finalScoreLabel, finalHitLabel, finalMissLabel, finalAccuracyLabel, highScoreLabel; // Game Over stats labels

    public static LevelTrainer instance;
    public static bool gameOver;

    private string selectedDifficulty = null; // Holds the selected difficulty
    private int targetsHit = 0, targetsMissed = 0, score = 0, totalClicks = 0;
    public float spawnTime = 2f; // Default spawn time (easy)
    private static int highScore =-1000; // High score persists for levels mode
    private int spawnedTargets = 0;

    public bool gameStarted = false; // Whether the game has started

    private void Start()
    {
        targetsHit = 0;
        targetsMissed = 0;
        score = 0;
        totalClicks = 0;
        spawnedTargets = 0;
        gameOver = false;
        gameStarted = false;
        instance = this;

        crosshair.SetActive(false); // Hide crosshair initially
        gameOverPanel.SetActive(false); // Hide game over panel initially
        difficultyPanel.SetActive(true); // Show difficulty selection panel
        gameCameraController.enabled = false; // Disable camera controller

        Cursor.lockState = CursorLockMode.None; // Ensure cursor is unlocked
        Cursor.visible = true;
    }

    private void Update()
    {
        if (gameOver)
            return;

        if (totalClicks == 0)
        {
            accuracyLabel.text = "Accuracy: 0%";
        }
        else
        {
            accuracyLabel.text = "Accuracy: " + (targetsHit * 100 / totalClicks) + "%";
        }
        hitsLabel.text = "Hits: " + targetsHit;
        remainingLabel.text = "Remaining: " + (20 - (targetsHit + targetsMissed));
        scoreLabel.text = "Score: " + score;
    }

    public void SetDifficulty(string difficulty)
    {
        // Store selected difficulty
        selectedDifficulty = difficulty.ToLower();
        Debug.Log("Difficulty set to: " + selectedDifficulty);
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(selectedDifficulty))
        {
            Debug.LogError("Difficulty not selected!");
            return;
        }

        // Set spawn time based on selected difficulty
        switch (selectedDifficulty)
        {
            case "easy":
                spawnTime = 2f;
                break;
            case "medium":
                spawnTime = 1.25f;
                break;
            case "hard":
                spawnTime = 0.75f;
                break;
        }

        difficultyPanel.SetActive(false); // Hide difficulty panel
        gameCameraController.enabled = true;
        StartCoroutine(StartGameWithCountdown()); // Start the game
    }

    private IEnumerator StartGameWithCountdown()
    {
        countdownLabel.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownLabel.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownLabel.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownLabel.gameObject.SetActive(false);
        crosshair.SetActive(true); // Show the crosshair

        if (gameCameraController != null)
        {
            gameCameraController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor

        gameStarted = true;
        SpawnTarget();
    }

    /*  private IEnumerator SpawnTargets()
      {
          for (int i = 0; i < 20; i++)
          {
              if (gameOver) yield break;

              GameObject target = SpawnTarget();
              yield return new WaitForSeconds(spawnTime);

              if (target != null)
              {
                  Destroy(target);
                  RegisterMiss();
              }
          }

          gameOver = true;
          ShowGameOverPanel();
      }*/

    public void SpawnTarget()
    {
        if (spawnedTargets >= 20 || gameOver) // Limit to 20 targets
        {
            gameOver = true;
            ShowGameOverPanel();
            return;
        }

        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            Debug.LogError("No asteroid prefabs assigned to the LevelTrainer script!");
            return;
        }

        Vector3 randomSpawn = new Vector3(Random.Range(-6f, 6f), Random.Range(-4f, 4f), 10f);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, 0f);
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);

        Instantiate(asteroidPrefabs[randomIndex], randomSpawn, randomRotation);
        spawnedTargets++; // Increment the spawn counter
    }

    public void RegisterClick(bool isTarget)
    {
        totalClicks++;

        if (isTarget)
        {
            // Nothing; handled in `Target.OnMouseDown()`
        }
        else
        {
            score -= 5; // Deduct points for misclick
        }
    }

    public void RegisterHit()
    {
        targetsHit++;
        score += 15; // Add points for hitting a target
        //SpawnTarget();
    }

    public void RegisterMiss()
    {
        targetsMissed++;
        score -= 10; // Deduct points for missing a target
        //SpawnTarget();
    }

    private void ShowGameOverPanel()
    {
        if (score > highScore)
        {
            highScore = score; // Update high score
        }
        gameOver = true;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;

        crosshair.gameObject.SetActive(false);
        accuracyLabel.gameObject.SetActive(false);
        hitsLabel.gameObject.SetActive(false);
        remainingLabel.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);

        gameOverLabel.text = "GAME OVER";
        finalScoreLabel.text = "Score: " + score;
        highScoreLabel.text = "High Score: " + highScore;
        finalHitLabel.text = "Hits: " + targetsHit;
        finalMissLabel.text = "Misses: " + targetsMissed;
        if (totalClicks == 0)
        {
            finalAccuracyLabel.text = "Accuracy: 0%";
        }
        else
        {
            finalAccuracyLabel.text = "Accuracy: " + (targetsHit * 100 / totalClicks) + "%";
        }

    }
}
