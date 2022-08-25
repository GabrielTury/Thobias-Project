using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] float circleRadius,speed;
    [SerializeField] LayerMask layerMask;
    public GameObject target;
    Rigidbody2D rdb;
    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
    }
    void CheckRange()
    {
        Collider2D inRange = Physics2D.OverlapCircle(transform.position, circleRadius, layerMask);
        if(inRange != null)
        {
            target = inRange.gameObject;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position+new Vector3(0,(float)0.5,0),speed);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckRange();
        /*if (target)
        {
            Vector3 dif = target.transform.position - transform.position;
            rdb.AddForce(dif);
        }*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = null;
        }
    }*/
}
