using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for chest insides and interaction with player

public class Chest : Interact
{
    public Item insides;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    public Signal itemFound;
    public GameObject dialogWindow;
    public Text dialogText;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        isOpen = storedOpen.runTime;
        if (isOpen)
        {
            _anim.SetBool("isOpened",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && signActive)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestIsOpened();
            }
        }
    }

    public void OpenChest()
    {
        dialogWindow.SetActive(true);
        dialogText.text = insides.itemDesc;
        playerInventory.AddItem(insides);
        playerInventory.foundItem = insides;
        itemFound.Raise();
        infoOff.Raise();
        isOpen = true;
        _anim.SetBool("isOpened", true);
        storedOpen.runTime = isOpen;
    }

    public void ChestIsOpened()
    {
        dialogWindow.SetActive(false);
        itemFound.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            infoOn.Raise();
            signActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            infoOff.Raise();
            signActive = false;
        }
    }
}