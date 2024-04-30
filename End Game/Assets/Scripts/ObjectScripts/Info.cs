using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for info appearing while reading something
public class Info : MonoBehaviour
{
    public GameObject infoWindow;

    public void Enable()
    {
        infoWindow.SetActive(true);
    }
    
    public void Disable()
    {
        infoWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
