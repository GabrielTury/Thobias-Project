using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iEnemyScript : MonoBehaviour
{
    [Range(0f, 1f)]
    public float touchDamageValue = 0.2f;

    [Range(0f, 1f)]
    public float atackDamageValue;

    public static iEnemyScript instance;

    [SerializeField]
    private float iEnemyLife = 20; //talvez deixar em 7
    [SerializeField]
    float rangeDistanceOne, rangeHeightOne, rangeDistanceTwo, rangeHeightTwo, iEnemyVelocity;
    //deixar iEnemyVelocity em 1.8 e RangeDistanceTwo em 2.34
    [SerializeField]
    LayerMask layerMask;

    Rigidbody2D rigidbody;
    Animator animator;

    bool stillIdle = false;
    bool rangeOneOn = true;
    bool forAttackDamage = false;
    public bool invincible = false;

    //Start
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("STILL", true);
    }

    private void Awake()
    {
        instance = this;
    }

    //Update
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("iEnemy taking damage"))
            StartCoroutine(TakingDamageDelay());
    }

    private void FixedUpdate()
    {
        if (stillIdle == true)
        {
            animator.SetBool("STILL", false);
            animator.SetBool("IDLE", true);
        }


        if (rangeOneOn == true)
        {
            RangeOne();
        }

        RangeTwo();
    }

    private void RangeOne()
    {
        Vector3 heightAdjustment = new Vector3(rangeDistanceOne / 2, rangeHeightOne, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + heightAdjustment, Vector2.left, rangeDistanceOne, layerMask);

        if (hit.collider != null && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("iEnemy attack"))
        {

            if (transform.position.x > hit.point.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            rigidbody.velocity = new Vector2(transform.localScale.x * iEnemyVelocity, 0);
            animator.SetBool("WALKING", true);
            stillIdle = true;
        }
        if (hit.collider == null)
        {
            animator.SetBool("WALKING", false);
        }

    }

    private void RangeTwo()
    {
        Vector3 heightAdjustment = new Vector3(rangeDistanceTwo / 2, rangeHeightTwo, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + heightAdjustment, Vector2.left, rangeDistanceTwo, layerMask);

        if (hit.collider != null)
        {
            rangeOneOn = false;
            animator.SetBool("ATTACKING", true);
        }
        else if (hit.collider == null) //precisa desse else?
        {
            animator.SetBool("ATTACKING", false);
            rangeOneOn = true;
        }


        if (hit.collider != null && animator.GetBool("ATTACKING"))
        {
            forAttackDamage = true;
        }
    }

    private void OnSpecificFrame()
    {
        if (forAttackDamage == true && !animator.GetBool("DYING"))
        {
            LevelManager.instance.AttackDamage();
            forAttackDamage = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 startingPositionOne = transform.position + new Vector3(rangeDistanceOne / 2, rangeHeightOne, 0);
        Vector3 finalPositionOne = transform.position + new Vector3(-rangeDistanceOne / 2, rangeHeightOne, 0);
        Gizmos.DrawLine(startingPositionOne, finalPositionOne);

        Gizmos.color = Color.cyan;
        Vector3 startingPositionTwo = transform.position + new Vector3(rangeDistanceTwo / 2, rangeHeightTwo, 0);
        Vector3 finalPositionTwo = transform.position + new Vector3(-rangeDistanceTwo / 2, rangeHeightTwo, 0);
        Gizmos.DrawLine(startingPositionTwo, finalPositionTwo);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("iEnemy attack") && invincible == false && other.gameObject.CompareTag("PlayerShot") || animator.GetBool("STILL") && invincible == false && other.gameObject.CompareTag("PlayerShot"))
        {
            animator.SetBool("TAKINGDAMAGE", true);
            iEnemyLife--;
            if (iEnemyLife <= 0)
            {
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DYING", true);
            }
            stillIdle = true;
        }
        else
        {
            animator.SetTrigger("DEFENDING");
        }
    }

    private void ToEndTakingDamage()
    {
        animator.SetBool("TAKINGDAMAGE", false);
    }

    private void FrameToDie()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            LevelManager.instance.TouchDamage();
        }
    }

    IEnumerator TakingDamageDelay()
    {
        invincible = true;

        yield return new WaitForSeconds(2);

        invincible = false;
    }
}

/*


Códigos:

Isso é para checar se uma animação está acontecendo:
 if (animator.GetBool("STILL"))
 {
     animator.SetBool("STILL", false);
     animator.SetBool("IDLE", true);
 }

Debug.Log("string");


        if ( && this.animator.GetCurrentAnimatorStateInfo(0).IsName("iEnemy attack"))
        {
            LevelManager.instance.AttackDamage();
        }

*/