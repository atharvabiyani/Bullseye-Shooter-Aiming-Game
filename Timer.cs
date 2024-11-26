using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : Monobehavior
{
    public TextMeshProGUI timerLabel;
    public float timer = 10f;
    private void Update()
    {
        if (timer > 0)
        {
            timer = Timer.deltaTime;
            DisplayTime(timer);
        }
        else
        {
            Trainer.gameOver = true;
            timerLabel.text = "GAME OVER";
        }
    }
    private void DisplayTime(float displayTime)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timerLabel.text = $"{minutes}:{seconds}";
    }
}