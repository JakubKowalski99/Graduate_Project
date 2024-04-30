using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldLock : Interact
{
    public GameObject signBox;
    public Text signText;
    public string sign;
    
    public Inventory playerInventory;
    
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && signActive)
        {
            if (playerInventory.coinCount > 100)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                if (signBox.activeInHierarchy)
                {
                    signBox.SetActive(false);
                }
                else
                {
                    signBox.SetActive(true);
                    signText.text = sign;
                }
            }
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            infoOff.Raise();
            signActive = false;
            signBox.SetActive(false);
        }
    }
}
