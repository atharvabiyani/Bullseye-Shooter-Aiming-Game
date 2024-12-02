using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public  AudioSource hitAudio;
    public  AudioSource missAudio;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if ((TimedTrainer.instance != null && TimedTrainer.instance.gameStarted && !TimedTrainer.gameOver && TimedTrainer.instance.countdownDone) ||
   (LevelTrainer.instance != null && LevelTrainer.instance.gameStarted && !LevelTrainer.gameOver))// Check if the game is active
            {
               // Play the gunshot sound
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (!hit.collider.CompareTag("Target")) // Non-target click
                    {
                        playMissAudio();
                        HandleMisclick();
                    }
                  
                }
                else
                {
                    playMissAudio();
                    HandleMisclick(); // Clicked on empty space
                }
            }
        }
    }
    public void playHitAudio()
    {       
           hitAudio.Play(); // Play the shooting sound
        
    }
    private void playMissAudio()
    {
        missAudio.Play(); // Play the shooting sound

    }
    private void HandleMisclick()
    {
        if (TimedTrainer.instance != null && TimedTrainer.instance.gameStarted)
        {
            TimedTrainer.instance.RegisterClick(false);
        }
        else if (LevelTrainer.instance != null && LevelTrainer.instance.gameStarted)
        {
            LevelTrainer.instance.RegisterClick(false);
        }
    }
}
