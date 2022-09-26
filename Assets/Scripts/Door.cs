using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform target;

    [NonSerialized] public bool lightOpenDoor, keyOpenDoor, buttonOpenDoor;

    public bool checkForLight;
    public bool checkForKey;
    public bool checkForButton;

    Button button;
    public GameObject whichButton;

    void Awake()
    {
        if(whichButton != null)
        button = whichButton.GetComponent<Button>();
    }
    void Update()
    {
        OpenDoor();
    }


    public void OpenDoor()
    {
        if (checkForLight && lightOpenDoor && checkForKey && keyOpenDoor)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
                StartCoroutine(OpenTheSesame());
        }

        if (checkForLight && lightOpenDoor && checkForKey == false)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
            {
                StartCoroutine(OpenTheSesame());
            }
        }

        if (checkForKey && keyOpenDoor && checkForLight == false)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
                StartCoroutine(OpenTheSesame());
        }

        if (checkForButton && checkForKey == false && checkForLight == false)
        {
            button.enableButtonCollision = true;
        }

        if (whichButton != null)
        {
            if (button.enableButtonCollision && button.GetComponent<Animator>().GetBool("ButtonDownAnimator"))
            buttonOpenDoor = true;
        }

        if (buttonOpenDoor == true)
            StartCoroutine(OpenTheSesame());

    }

    public IEnumerator OpenTheSesame()
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.01f);
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
