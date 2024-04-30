using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    public List<SignalCatcher> catchers = new List<SignalCatcher>();
    
    public void Raise()
    {
        for (int i = catchers.Count - 1; i >= 0; i--)
        {
            catchers[i].SignalCatch();
        }
    }

    public void AddCathers(SignalCatcher catcher)
    {
        catchers.Add(catcher);
    }

    public void RemoveCatcher(SignalCatcher catcher)
    {
        catchers.Remove(catcher);
    }
}