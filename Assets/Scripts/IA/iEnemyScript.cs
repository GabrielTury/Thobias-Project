using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iEnemyScript : MonoBehaviour
{
    [SerializeField]
    [Range (0f, 1f)]
    public float touchDamageValue;

    [SerializeField]
    private float iEnemyLife = 20;
    [SerializeField]
    float rangeDistance, rangeHeight, iEnemyVelocity;
    [SerializeField]
    LayerMask layerMask;

    Rigidbody2D rigidbody;
    Animator animator;

    //Start
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Update
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 heightAdjustment = new Vector3(rangeDistance / 2, rangeHeight, 0);
        RaycastHit2D hit = Physics2D.Raycast (transform.position + heightAdjustment, Vector2.left, rangeDistance, layerMask);
        if (hit.collider != null)
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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 startingPosition = transform.position + new Vector3 (rangeDistance / 2, rangeHeight, 0);
        Vector3 finalPosition = transform.position + new Vector3(-rangeDistance / 2, rangeHeight, 0);
        Gizmos.DrawLine(startingPosition, finalPosition);
    }

    private void OnParticleCollision()
    {
        iEnemyLife--;
        if (iEnemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            LevelManager.instance.TouchDamage();
        }
    }
    //ver pq no script LevelManager o touchDamageValue não está funcionando (só funciona com números definidos em float)
}
