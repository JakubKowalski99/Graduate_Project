using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingGoblin : TreeGoblin
{
    public Collider2D boundary;
    
    //Changed check distance method from TreeGoblin so if the player is
    //in his area he follows him but if he leaves it he goes back to the area
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
                transform.position) > attackRadius
            && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                treegoblinRigidbody.MovePosition(temp);
                ChangeEnemyState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position,
                     transform.position) > chaseRadius
                 || !boundary.bounds.Contains(target.transform.position))
        {
            animator.SetBool("wakeUp", false);
        }
    }
}