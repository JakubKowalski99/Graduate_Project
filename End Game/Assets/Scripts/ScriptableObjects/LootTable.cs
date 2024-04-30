using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Collectible thisloot;
    public int dropChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] allLoots;

    public Collectible LootCollectible()
    {
        int cummulitiveProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < allLoots.Length; i++)
        {
            cummulitiveProb += allLoots[i].dropChance;
            if (currentProb <= cummulitiveProb)
            {
                return allLoots[i].thisloot;
            }
        }
        return null;
    }
}
