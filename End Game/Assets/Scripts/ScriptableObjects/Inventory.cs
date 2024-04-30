using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item foundItem;
    public List<Item> inventory = new List<Item>();
    public int keyCount;
    public int coinCount;
    public float maxMagic = 10;
    public float currentMagic;
    
    public void OnEnable()
    {
        currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public bool CheckForItem(Item item)
    { 
        if(inventory.Contains(item))
        {
            return true;
        }
        return false;
    }
    
    public void AddItem(Item addedItem)
    {
        if (addedItem.isKey)
        {
            keyCount++;
        }
        else
        {
            if (!inventory.Contains(addedItem))
            {
                inventory.Add(addedItem);
            }
        }
    }
}
