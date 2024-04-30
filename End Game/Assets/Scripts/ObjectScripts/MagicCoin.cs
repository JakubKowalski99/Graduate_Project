using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCoin : Collectible
{
    public Inventory playerInventory;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coinCount += 1000;
            collectSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
