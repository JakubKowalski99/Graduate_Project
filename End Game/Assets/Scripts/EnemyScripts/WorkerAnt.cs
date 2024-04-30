using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class exdending from enemy, responsible for patrolling enemy
public class WorkerAnt : Enemy
{
    
    public Transform[] path;
    public int currentPoint;
    public Transform currentMeta;
    public float roundingDistance;
    
    private Rigidbody2D treegoblinRigidbody;
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
        animator.SetBool("wakeUp",true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }
    //If player is in range of patrolling enemy he will follow him until player is out of distance
    void CheckDistance()
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
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                treegoblinRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeMeta();
            }
        }
    }
    //Method responsible for enemy going from point to point
    //after going throught all points he goes to the first one
    private void ChangeMeta()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentMeta = path[0];
        }
        else
        {
            currentPoint++;
            currentMeta = path[currentPoint];
        }
    }
    //Method responsible for vector that enemy is facing
    private void SetAnimatorFloat(Vector2 setVector)
    {
        animator.SetFloat("X", setVector.x);
        animator.SetFloat("Y", setVector.y);
    }
    //Method changing animations
    private void ChangeAnimation(Vector2 direction)
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
}