using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AracnoControl : MonoBehaviour {
    public HingeJoint2D[] joints;
    JointMotor2D motor;
	// Use this for initialization
	void Start () {
        motor.maxMotorTorque = 10000;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        motor.motorSpeed = Input.GetAxis("Horizontal")*100;

        foreach (HingeJoint2D joint in joints)
        {
            joint.motor = motor;
        }
	}
}
