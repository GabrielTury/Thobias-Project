using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenLight : MonoBehaviour
{
    public int intLightNumber;
    static int lastLightNumber = 1; //o "static" usa o mesmo valor da variável para todos os scripts com esse nome (no caso, "RedGreenLight").
    //"public static" acaba fazendo essa variável acessível para todos os scripts, além de mudar o valor da variável para os scripts com o mesmo nome.
    Animator anim;

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
}

//Fazer no final de tudo a porta abrir (colocar as condições para cada porta)