using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load the Game Modes scene
    public void LoadGameModesScene()
    {
        SceneManager.LoadScene("Game Modes Scene");
    }

    // Load the Timed Trainer scene
    public void LoadTimedTrainerScene()
    {
        SceneManager.LoadScene("Timed Scene");
    }

    // Load the Levels Trainer scene
    public void LoadLevelsTrainerScene()
    {
        SceneManager.LoadScene("Levels Scene");
    }

    // Load the Main Menu scene
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Load the current scene without resetting variables or creating a new instance 
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    // Quit game
    public void Quit()
    {
        Application.Quit();
        //Debug.Log("Player has quit the game :(");
    }

}
