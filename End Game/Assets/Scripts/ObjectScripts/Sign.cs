using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Class extending interact for signs
public class Sign : Interact
{
    public GameObject signBox;
    public Text signText;
    public string sign;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && signActive)
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