using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehavior
{
    private void start()
    {
        StartCoroutine(DestroyTarget());
    }
    IEnumerator DestroyTarget()
    {
        yield return new waitForSeconds(2); // how long target stays on screen before new spawn
        Trainer.targetMissed = Trainer.targetMissed + 1;
        if (Trainer.gameOver == false)
        {
            Trainer.instance.SpawnTargets();
        }
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Trainer.targetsHit = trainer.targetsHit + 1;
        Destroy(gameObject);
        if (Trainer.gameOver == false)
        {
            Trainer.instance.SpawnTargets();
        }
    }
}
