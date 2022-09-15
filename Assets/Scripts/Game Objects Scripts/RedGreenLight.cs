using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenLight : MonoBehaviour
{
    public int LightNumber;
    Animator animator;

    public static bool teste;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      /*  if (teste == false)
            Debug.Log("tá falso");
        if (teste)
            Debug.Log("tá verdadeiro");*/
    }
 /*   private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            teste = true;
        }
    }*/
}
