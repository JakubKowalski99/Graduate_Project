using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Collectible
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            heartContainers.runTime += 1;
            playerHealth.runTime = heartContainers.runTime * 2;
            collectSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}