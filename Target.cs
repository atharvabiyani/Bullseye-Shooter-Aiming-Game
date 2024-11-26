using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyTarget());
    }
    IEnumerator DestroyTarget()
    {
        yield return new WaitForSeconds(2); // how long target stays on screen before new spawn
        Trainer.targetsMissed = Trainer.targetsMissed + 1; // increments if you miss
        if (Trainer.gameOver == false) // check if game is over
        {
            Trainer.instance.SpawnTargets(); // spawn a new target
        }
        Destroy(gameObject); // destroys the current target since you didn't click in the 2s wait period
    }

    private void OnMouseDown()
    {
        Trainer.targetsHit = Trainer.targetsHit + 1; // increments if you hit
        Destroy(gameObject); // destroy the current target
        if (Trainer.gameOver == false) // until gameOver is true new targets will spawn
        {
            Trainer.instance.SpawnTargets(); // spawn a new target
        }
    }
}
