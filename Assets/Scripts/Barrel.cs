using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {
    public int lives = 5;
    public ParticleSystem smoke;
    public ParticleSystem explosion;
    Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        lives--;
        if (lives < 3)
        {
            smoke.Play();
        }

            if (lives < 1)
        {
            explosion.Play();
            rend.enabled = false;
            Destroy(gameObject,1f);
        }
    }
}
