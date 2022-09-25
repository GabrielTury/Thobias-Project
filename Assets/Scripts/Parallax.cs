using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    public float howMuchParallax;
    public GameObject gameCamera;


    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //vai trazer o tamanho dos sprites
    }

    void Update()
    {
        float temp = (gameCamera.transform.position.x * (1 - howMuchParallax)); //o quanto o player se mexeu comparando com a câmera

        float distanceFromStart = (gameCamera.transform.position.x * howMuchParallax); //o quanto o player se mexeu desde o Start Point

        transform.position = new Vector3(startPosition + distanceFromStart, 11f, transform.position.z); //mexe a câmera com o quanto nos movemos desde o Start Point

        if (temp > startPosition + length)
        {
            startPosition += length;
        }
        else if (temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
