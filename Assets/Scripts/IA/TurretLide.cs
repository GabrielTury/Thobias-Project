using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLide : MonoBehaviour
{
    [SerializeField] int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("PlayerShot"))
            life--;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
