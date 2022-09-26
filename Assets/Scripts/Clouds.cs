using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clouds : MonoBehaviour
{
    public float layerVelocity, startPosition;
    private float length;
    public GameObject gameCamera;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(layerVelocity, 0.0f, 0.0f);

        float temp = (gameObject.transform.position.x);

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
