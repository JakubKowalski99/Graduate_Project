using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class extend from Enemy and give functionality for Tree Goblin
public class TreeGoblin : Enemy
{
    public Rigidbody2D treegoblinRigidbody;
    public Transform target;
    
    public float chaseRadius;
    public float attackRadius;
    
    public Transform homePosition;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        treegoblinRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }
    
    //Method responsible for checking if player is in the distance and awaking enemy
    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle ||
                currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                treegoblinRigidbody.MovePosition(temp);
                ChangeEnemyState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }
    //Method responsible for vector that enemy is facing
    private void SetAnimatorFloat(Vector2 setVector)
    {
        animator.SetFloat("X", setVector.x);
        animator.SetFloat("Y", setVector.y);
    }
    
    public void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimatorFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimatorFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloat(Vector2.down);
            }
        }
    }
    //Method changing states of the enemy
    public void ChangeEnemyState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}