using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Class responsible for showing coins in player HUD
public class CoinTextMenager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinNumber;
    //Method desplaying coins collected
    public void Update()
    {
        coinNumber.text = "" + playerInventory.coinCount;
    }

    public void UpdateCoinCount()
    {
        coinNumber.text = "" + playerInventory.coinCount;
    }
}
