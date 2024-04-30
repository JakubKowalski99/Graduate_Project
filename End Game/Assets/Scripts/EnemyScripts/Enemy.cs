using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum telling us in which state the enemy is currently being in
public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
//Main class for enemies functionality
public class Enemy : MonoBehaviour
{
    
    public EnemyState currentState;
    public FloatValue maximumHP;
    public float health;
    public float speed;

    public GameObject deathEffect;
    public Signal roomSignal;
    public LootTable thisLoot;
    
    public string enemyName;
    public int baseAttack;

    private void Awake()
    {
        health = maximumHP.value;
    }

    private void OnEnable()
    {
        health = maximumHP.value;
        currentState = EnemyState.idle;
    }

    //Method using knobackroutine
    public void Knockback(Rigidbody2D myRigidbody2D, float knockTime, float dmg)
    {
        StartCoroutine(Knockroutine(myRigidbody2D, knockTime));
        DamageTaken(dmg);
    }
    //Interface that is responsible for knockback routine for enemy,
    //the direction that the enemy is being pushed in and the time he can be atacked again
    private IEnumerator Knockroutine(Rigidbody2D enemy, float knockbackTime)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            enemy.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            enemy.velocity = Vector2.zero;
        }
    }
    //Method responsible for enemy taking damage
    private void DamageTaken(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            DeathEffect();
            MakeLoot();
            roomSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }
    //Method responsible for activating death effect
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect,1f);
        }
    }
    //Method using lootTable for enemy and dropping loot
    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            Collectible current = thisLoot.LootCollectible();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}