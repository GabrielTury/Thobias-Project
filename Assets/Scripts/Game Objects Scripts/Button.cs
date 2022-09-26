using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] bool checkForProjectileOnly;//se estiver falso, vai checar se o Player passou por cima
    [NonSerialized] public bool enableButtonCollision;
    Animator anima;
    Door door;
    public GameObject thisDoor;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
    }

    void Start()
    {
        anima = GetComponent<Animator>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (enableButtonCollision && checkForProjectileOnly)
        {
            anima.SetBool("ButtonDownAnimator", true);
            door.buttonOpenDoor = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.CompareTag("Player") && (checkForProjectileOnly == false))
       {
         anima.SetBool("ButtonDownAnimator", true);
            GetComponent<Collider2D>().enabled = false;
       }
    }
}
