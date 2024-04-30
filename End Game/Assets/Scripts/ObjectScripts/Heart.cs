using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for heeart containers collectibles
public class Heart : Collectible
{
    public FloatValue playerHealth;
    public float howMuchHP;
    public FloatValue maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.runTime += howMuchHP;
            if (playerHealth.value > maxHealth.runTime * 2)
            {
                playerHealth.value = maxHealth.runTime * 2;
            }
            collectSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
