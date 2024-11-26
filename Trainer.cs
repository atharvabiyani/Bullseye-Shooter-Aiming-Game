using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class Trainer : MonoBehaviour
{
    public GameObject targetPrefab; // reference to the target prefab
    public static Trainer instance; // instance of the trainer
    public static bool gameOver; 
    public static int targetsHit, targetsMissed, accuracy; // trackers for hits, misses, and accuracy
    public Slider accuracySlider; // ui slider to display accuracy not working as of now
    public TextMeshProUGUI targetsHitLabel, targetsMissedLabel, accuracyLabel; // ui labels for hits, misses, and accuracy

    private void Start()
    {
        SpawnTargets(); // spawn the first target
        gameOver = false; // initialize gameOver as false since we just started
        instance = this; // set the instance to this script
    }

    private void Update()
    {
        int sum; // total targets seen in time frame
        if (targetsHit == 0 && targetsMissed == 0) { sum = 1; } // check for 0 division 
        else { sum = targetsHit + targetsMissed; } // calculate total attempts

        accuracySlider.value = targetsHit * 100 / sum; // update accuracy slider value, not working as of now

        targetsHitLabel.text = "Hit: " + targetsHit; // update ui label for hits
        targetsMissedLabel.text = "Miss: " + targetsMissed; // update ui label for misses
        accuracyLabel.text = "Accuracy: " + accuracySlider.value + "%"; // update ui label for accuracy

        if (gameOver == true) // check if the game is over
        {
            if (Input.GetKeyDown(KeyCode.R)) // if r key is pressed..
            {
                SceneManager.LoadScene(0); // reload the scene
            }
        }
    }

    public void SpawnTargets()
    {
        Vector3 randomSpawn = new Vector3(Random.Range(-6, 6), Random.Range(-3, 5), 0); // generate random spawn position
        Instantiate(targetPrefab, randomSpawn, Quaternion.identity); // spawn a new target
    }
}
