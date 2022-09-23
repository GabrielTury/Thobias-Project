using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenLight : MonoBehaviour
{
    public int intLightNumber;
    public static int lastLightNumber = 1; //o "static" usa o mesmo valor da variável para todos os scripts com esse nome (no caso, "RedGreenLight").
    //"public static" acaba fazendo essa variável acessível para todos os scripts, além de mudar o valor da variável para os scripts com o mesmo nome.
    Animator anim;

    Door door;
    public GameObject thisDoor;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
    }

        void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        LightNumberFunction();
        LightDoorOpen();
    }

    public void ItWasDashed()
    {
        if (intLightNumber == lastLightNumber)
        {
            anim.SetBool("GreenLight", true);
            lastLightNumber++;
        }
    }

    private void LightNumberFunction()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (intLightNumber == i)
            {
                anim.SetInteger("LightNumberAnimator", i);
            }
        }
    }

    public void LightDoorOpen()
    {
        if (lastLightNumber == 6)
        {
            lastLightNumber = 1;
            door.lightOpenDoor = true;
        }
    }
}
