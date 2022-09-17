using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewControls : MonoBehaviour
{

    Rigidbody2D rig;
    #region Move Variables
    private float moveInput;
    [HideInInspector] public float moveForce;
    public float maxSpeed;
    public float initialMoveForce;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

    }
    private void FixedUpdate()
    {
        if(moveInput != 0)
        Run();       
        else if(Mathf.Abs(rig.velocity.x) !=0)
            rig.AddForce(moveForce * Vector2.right * Mathf.Sign(-rig.velocity.x), ForceMode2D.Force);
    }
    void Run()
    {
        print(rig.velocity.x);
        rig.AddForce(moveForce * Vector2.right * Mathf.Sign(moveInput), ForceMode2D.Force);
        if (Mathf.Abs(rig.velocity.x) > maxSpeed)
        {
            moveForce = 0;
        }
        else
        {
            moveForce = initialMoveForce;
        }            
    }
    void CheckDirection()
    {
        
    }
}
