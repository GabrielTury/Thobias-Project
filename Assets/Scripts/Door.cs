using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform target;
    public GameObject Teste;

    private void OnParticleCollision(GameObject other)
    {
        StartCoroutine(OpenTheSesame());
    }

   /* private void OpenDoor()
    {
        if (gameObject.GetComponent<RedGreenLight>().lastLightNumber == 6) //quero pegar o int do outro script
        {

        }
    }*/

    IEnumerator OpenTheSesame()
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.01f);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
