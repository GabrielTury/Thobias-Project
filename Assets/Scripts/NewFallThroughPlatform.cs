
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFallThroughPlatform : MonoBehaviour
{

    private Collider2D _collider;
    private bool ignoreCollision = false;
    private Collision2D cdr;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cdr = collision;
            if (checkForInput == null)
            checkForInput = StartCoroutine(CheckForInput());
        }
        cdr = collision;

    }
    private Coroutine checkForInput;
    private IEnumerator CheckForInput()
    {
        //Invoke("Teste", 3f); //Isso significa que a função "Teste" vai ser executada após 3 segundos de ser chamada pelo Invoke.
        //Como essa função é meio pesada (se usa ela como um delay), é bom NÃO colocar no update.
        //O Invoke TEM que estar dentro do Coroutine/IEnumerator.
        while (!Input.GetKeyDown("s"))
        {
            yield return null;
        }
        Physics2D.IgnoreCollision(cdr.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(cdr.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        checkForInput = null;
    }

   /* private void Teste()
    {

    }*/
}
