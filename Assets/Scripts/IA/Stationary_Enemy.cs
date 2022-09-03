using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Stationary_Enemy : MonoBehaviour
{
    public static Stationary_Enemy instance;
    public ParticleSystem shoot;
    [SerializeField] GameObject player;
    public float rangeRadius;
    [SerializeField] LayerMask layerMask;
    bool stopped = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        RotatingEnemy();
        if (stopped)
        {
            StartCoroutine(Shoot());
        }

    }
    private void LateUpdate()
    {
        GetPlayerTransform();
    }
    void RotatingEnemy()
    {
        Vector3 difference = (player.transform.position + new Vector3(0, 0.5f, 0)) - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void GetPlayerTransform()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Transform>();
    }

    IEnumerator Shoot()
    {
        stopped = false;
        Collider2D inRange = Physics2D.OverlapCircle(transform.position, rangeRadius, layerMask);
        if(inRange != null)
        {
            shoot.Emit(1);
            yield return new WaitForSeconds(1);
        }
        stopped = true;
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

}
