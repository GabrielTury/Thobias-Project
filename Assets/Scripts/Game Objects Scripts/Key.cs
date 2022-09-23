using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Door door;
    public GameObject thisDoor;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            door.keyOpenDoor = true;
        }
    }
}
