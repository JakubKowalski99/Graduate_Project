using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player can interact with signs, chests, doors etc by pressing space
public class Interact : MonoBehaviour
{
    
    public Signal infoOn;
    public Signal infoOff;
    public bool signActive;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            infoOn.Raise();
            signActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            infoOff.Raise();
            signActive = false;
        }
    }
}
