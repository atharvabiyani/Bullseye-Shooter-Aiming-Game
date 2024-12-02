using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Target : MonoBehaviour
{
    private bool isTimedMode;
    private bool isLevelMode;
    private int spawned;
    public InputHandler inputHandler; // = InputHandler.gameObject.AddComponent<InputHandler>();
    private void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        isTimedMode = TimedTrainer.instance != null && TimedTrainer.instance.gameStarted;
        isLevelMode = LevelTrainer.instance != null && LevelTrainer.instance.gameStarted; 
        StartCoroutine(DestroyTarget());
    }

    IEnumerator DestroyTarget()
    {
        if (isTimedMode)
        {
            yield return new WaitForSeconds(5f); // Target stays for 5 seconds
            if (TimedTrainer.instance != null && !TimedTrainer.gameOver)
            {
                TimedTrainer.instance.RegisterMiss(); // Register a miss
                Destroy(gameObject); // Destroy the target
                TimedTrainer.instance.SpawnTargets(); // Spawn a new target
            }
        }

        else if(isLevelMode)
        {
            yield return new WaitForSeconds(LevelTrainer.instance.spawnTime);
            if (LevelTrainer.instance != null && !LevelTrainer.gameOver)
            {
                LevelTrainer.instance.RegisterMiss();
                Destroy(gameObject);
                LevelTrainer.instance.SpawnTarget();
                  
            }
        }
    }

    private void OnMouseDown()
    {
        if (inputHandler != null)
        {
            inputHandler.playHitAudio(); // Call the instance method
        }
        if (isTimedMode)
        {
            if (TimedTrainer.instance != null && TimedTrainer.instance.gameStarted)
            {
                //TimedTrainer.instance.RegisterClick(true); // Register the click
                TimedTrainer.instance.RegisterHit(); // Register a successful hit
                Destroy(gameObject); // Destroy the target
                TimedTrainer.instance.StartCoroutine(TimedTrainer.instance.SpawnTargetsWithDelay()); // Spawn a new target
               
            }
        }
        else if (isLevelMode)
        {
            if (LevelTrainer.instance != null && LevelTrainer.instance.gameStarted)
            {
                //LevelTrainer.instance.RegisterClick(true); // Register the click
                LevelTrainer.instance.RegisterHit(); // Register a successful hit
                Destroy(gameObject); // Destroy the target
                LevelTrainer.instance.SpawnTarget();
            }
        }
    }
}
