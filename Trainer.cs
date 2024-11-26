using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using System;
public class trainer : MonoBehavior
{
    public GameObject targetPrefab;
    public static trainer instance;
    public static bool gameOver;
    public static int targetsHit = 1, targetsMissed = 1, accuracy;
    public Slider accuracySlider;
    public TextMeshProGUI targetsHitLabel, targetsMissedLabel, accuracyLabel;

    private void Start()
    {
        SpawnTargets();
        gameOver = false;
        instance = this;
    }

    private void Update()
    {
        int sum = targetsHit + targetsMissed;
        accuracySlider.value = targetsHit * 100 / sum;

        targetsHitLabel.text = "Target Hit: " + targetsHit;
        targetsMissedLabel.text = "Target Missed: " + targetsMissed;
        accuracyLabel.text = "Accuracy: " + accuracySlider.value + "%";

        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void SpawnTargets()
    {
        Vector3 randomSpawn = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        Initiate(targetPrefab, randomSpawn, Quaternion.identity);
    }
}