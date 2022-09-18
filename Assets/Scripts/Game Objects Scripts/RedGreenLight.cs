using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenLight : MonoBehaviour
{
    public int intLightNumber;
    int lastLightNumber = 1;
    Animator anim;

    public static bool activateItWasDashedRGLFunction = false;

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

        if (activateItWasDashedRGLFunction)
        {
            ItWasDashed();
        }
    }

    public void ItWasDashed()
    {
        /* if (intLightNumber == lastLightNumber)
         {
             anim.SetBool("GreenLight", true);
             lastLightNumber++;
        print(lastLightNumber);
         }*/

        //gameObject.GetComponent<Animator>().SetBool("GreenLight", true);
        anim.SetBool("GreenLight", true);

        //activateItWasDashedRGLFunction = false; //isso tava bugando quando ficava verde, por isso tirei
        //Debug.Log("ItWasDashed");
    }

    private void LightNumberFunction()
    {

        /*for (int i = 1; i <= 5; i++)
        {
            if (intLightNumber == i)
            {
                anim.SetInteger("LightNumberAnimator", i);
            }
        }*/

    }
}

//Fazer no final de tudo a porta abrir (colocar as condições para cada porta)