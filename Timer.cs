using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerLabel; // text label to display the timer
    public float timer = 30f; // start the timer at 30 seconds

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // decrement the timer
            DisplayTime(timer);
        }
        else
        {
            Trainer.gameOver = true; // set gameOver to true
            timerLabel.text = "GAME OVER"; // display game-over message
        }
    }

    private void DisplayTime(float displayTime)
    {
        // so that the time doesn't go negative
        displayTime = Mathf.Max(displayTime, 0);

        // convert total seconds into minutes and seconds
        int minutes = Mathf.FloorToInt(displayTime / 60);
        int seconds = Mathf.FloorToInt(displayTime % 60);

        // format minutes and seconds with leading zeros
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        // display the formatted time
        timerLabel.text = formattedTime;
    }
}
