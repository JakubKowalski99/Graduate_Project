using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum holding door states
public enum DoorType
{
    key,
    kill,
    click
}

//Class responsible for doors in dungeons
public class Door : Interact
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D doorOpened;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (signActive && thisDoorType == DoorType.key)
            {
                if (playerInventory.keyCount > 0)
                {
                    playerInventory.keyCount--;
                    Open();
                }   
            }
        }
    }
    //Opening doors
    public void Open()
    {
        animator.SetBool("isOpen",true);
        open = true;
        physicsCollider.enabled = false;
        doorOpened.enabled = false;
    }
    //Closing doors
    public void Close()
    {
        animator.SetBool("isOpen",false);
        open = false;
        physicsCollider.enabled = true;
        doorOpened.enabled = true;
    }
}
