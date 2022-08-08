using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabPistol : MonoBehaviour {
    public enum CrabState { WalkL,WalkR,Attack,Berserk,Dead};
    public CrabState crabState;
    Animator anim;
    Rigidbody2D rdb;
    float counter;
    float counterlimit;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rdb = GetComponent<Rigidbody2D>();
        counterlimit = UnityEngine.Random.Range(2.1f, 4);
    }
	
	// Update is called once per frame
	void Update () {
        switch (crabState)
        {
            case CrabState.Attack:
                Attack();
                break;
            case CrabState.Berserk:
                Berserk();
                break;
            case CrabState.Dead:
                break;
            case CrabState.WalkL:
                WalkL();
                break;
            case CrabState.WalkR:
                WalkR();
                break;
        }
	}

    private void WalkR()
    {
        rdb.MovePosition(transform.position + Vector3.right * 0.1f);
        if (Physics2D.Raycast(transform.position + Vector3.up * 0.5f, Vector2.right, 1))
        {
            crabState = CrabState.WalkL;
        }
        if (!Physics2D.Raycast(transform.position + Vector3.up * 0.5f, new Vector2(1,-1), 1))
        {
            crabState = CrabState.WalkL;
        }
        CounterAttack();
    }

    private void CounterAttack()
    {
        counter += Time.deltaTime;
        if (counter > counterlimit)
        {
            counter = 0;
            counterlimit = UnityEngine.Random.Range(2.1f, 4);
            crabState = CrabState.Attack;
            anim.SetBool("Attack", true);
        }
    }

    private void WalkL()
    {
        rdb.MovePosition(transform.position - Vector3.right * 0.1f);
        if (Physics2D.Raycast(transform.position+Vector3.up*0.5f, -Vector2.right, 1))
        {
            crabState = CrabState.WalkR;
        }
        if (!Physics2D.Raycast(transform.position + Vector3.up * 0.5f, new Vector2(-1, -1), 1))
        {
            crabState = CrabState.WalkR;
        }
        CounterAttack();
    }

    private void Berserk()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        counter += Time.deltaTime;
        if (counter > counterlimit)
        {
            counter = 0;
            anim.SetBool("Attack", false);
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                crabState = CrabState.WalkR;
            }
            else
            {
                crabState = CrabState.WalkL;
            }

        }
    }
}
