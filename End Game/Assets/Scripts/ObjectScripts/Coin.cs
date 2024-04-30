using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class responsible for coin collectible
public class Coin : Collectible
{
    public Inventory playerInventory;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coinCount += 1;
            collectSignal.Raise();
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
