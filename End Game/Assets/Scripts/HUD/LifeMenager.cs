using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Manager of player hearts, class responsible for desplaying player health
public class LifeMenager : MonoBehaviour
{
    
    public Image[] health;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue maxHealth;
    public FloatValue currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }
    //Method initializing player health
    public void InitializeHealth()
    {
        for (int i = 0; i < maxHealth.runTime; i++)
        {
            if (i < health.Length)
            {
                health[i].gameObject.SetActive(true);
                health[i].sprite = fullHeart;
            }
        }
    }
    //Method updating player health on HUD
    public void UpdateHealth()
    {
        InitializeHealth();
        float tmpHealth = currentHealth.runTime / 2;
        for (int i = 0; i < maxHealth.runTime; i++)
        {
            //Full heart
            if (i <= tmpHealth-1)
            {
                health[i].sprite = fullHeart;
            }
            //Empty heart
            else if (i >= tmpHealth)
            {
                health[i].sprite = emptyHeart;
            }
            //Half heart
            else
            {
                health[i].sprite = halfHeart;
            }
        }
    }
}