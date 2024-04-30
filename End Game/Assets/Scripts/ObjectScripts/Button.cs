using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for button function in dungeon,
//nin some rooms the button must be pressed to get throught the door
public class Button : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.runTime;
        if(active)
        {
            ActivateButton();
        }
    }

    public void ActivateButton()
    {
        active = true;
        storedValue.runTime = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateButton();
        }
    }
}
