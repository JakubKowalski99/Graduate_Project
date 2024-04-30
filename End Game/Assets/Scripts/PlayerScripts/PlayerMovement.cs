using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Player states
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
//Class responsible for player controller, taking damage, animations and hitting enemies
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Vector3 change;
    private Rigidbody2D playerRigidbody;
    public VectorValue playerStartingPosition;
    
    private Animator animator;
    
    public FloatValue currentHealth;
    public Signal playerHpSignal;
    public Signal playerHit;
    
    
    public Inventory playerInventory;
    public SpriteRenderer itemSprite;

    public GameObject projectile;
    public Item bow;
    
    public Signal reduceMagic;
    
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
        transform.position = playerStartingPosition.positionValue;
    }

    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(Attackroutine());
        }
        else if (Input.GetButtonDown("second attack") && currentState != PlayerState.attack
                                                      && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondAttackRoutine());
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            MoveProcess();
        }
    }
    //Interface responsible for attacking routine
    private IEnumerator Attackroutine()
    {
        animator.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    
    private IEnumerator SecondAttackRoutine()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    
    //Making arrow
    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("X"), animator.GetFloat("Y"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();
        }
    }
    //Arrow direction
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("Y"), animator.GetFloat("X"))* Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    
    //Picking up an item
    public void RaiseItem()
    {
        if (playerInventory.foundItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("itemFound", true);
                currentState = PlayerState.interact;
                itemSprite.sprite = playerInventory.foundItem.itemImg;
            }
            else
            {
                animator.SetBool("itemFound", false);
                currentState = PlayerState.idle;
                itemSprite.sprite = null;
                playerInventory.foundItem = null;
            }
        }
    }
    //Movement of the Player, whole process
    void MoveProcess()
    {
        if (change != Vector3.zero)
        {
            Movement();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("X", change.x);
            animator.SetFloat("Y", change.y);
            animator.SetBool("inMove", true);
        }
        else
        {
            animator.SetBool("inMove", false);
        }
    }
    //Player changing position and speed
    void Movement()
    {
        change.Normalize();
        playerRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }
    //Player getting dmg and being knocked
    public void Knockback(float knockTime, float dmg)
    {
        currentHealth.runTime -= dmg;
        playerHpSignal.Raise();
        if (currentHealth.runTime > 0)
        {
            StartCoroutine(Knockroutine(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("WastedScene", LoadSceneMode.Single);
        }
    }
    //Player interface for being knocked
    private IEnumerator Knockroutine(float knockbackTime)
    {
        playerHit.Raise();
        if (playerRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockbackTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidbody.velocity = Vector2.zero;
        }
    }
    
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}