using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : TreeGoblin
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position,
                    speed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                treegoblinRigidbody.MovePosition(temp);
                ChangeEnemyState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position,
                     transform.position) <= chaseRadius
                 && Vector3.Distance(target.position,
                     transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }

    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        animator.SetBool("attack", false);
    }
}
