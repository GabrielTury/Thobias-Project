using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallThrough : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;


    private void Start()
    {
            _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerOnPlatform && Input.GetAxisRaw("Vertical")<0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }


    /// <summary>
    /// quanto tempo demora para a colisão voltar
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D anyOtherCollision, bool value)
    {
        var player = anyOtherCollision.gameObject.GetComponent<Control>(); //pega o objeto que possui o script Control.cs (no caso, o player)
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D anyOtherCollision)
    {
        SetPlayerOnPlatform(anyOtherCollision, true);
    }

    private void OnCollisionExit2D(Collision2D anyOtherCollision)
    {
        SetPlayerOnPlatform(anyOtherCollision, true);
    }

}


//falta: consertar pulo com spacebar clicado (fazer ele considerar 1 vez enquanto deixar apertado)
//mudar código para que essa colisão só afete o player, e nada mais. Talvez usar:
//https://docs.unity3d.com/ScriptReference/Physics2D.IgnoreCollision.html