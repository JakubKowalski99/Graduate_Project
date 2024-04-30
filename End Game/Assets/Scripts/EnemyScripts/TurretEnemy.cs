using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for functionality of TurretEnemy shooting at player
public class TurretEnemy : TreeGoblin
{
    
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    
    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    //Overrides check distance from tree goblin so he shoots the player if he is in range
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
                transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Debug.Log("strzelac");
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                    ChangeEnemyState(EnemyState.walk);
                    animator.SetBool("wakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position,
            transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }
}