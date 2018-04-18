﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputHandler : MonoBehaviour {
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    private int frames;
    private float leftAverageSpeed;
    private float leftTotalSpeed;
    private float rightAverageSpeed;
    private float rightTotalSpeed;
    public float speed;
    public static ControllerInputHandler instance;
	// Use this for initialization
	void Start () {
        instance = this;
        frames = 0;
        leftAverageSpeed = 0;
        leftTotalSpeed = 0;
        rightAverageSpeed = 0;
        rightTotalSpeed = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		AdjustSpeed ();
    }
//
//    private float CalculateSpeed() {
//		float leftCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(leftController).magnitude);
//		float rightCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(rightController).magnitude);
//		float averageCurrentSpeed = Mathf.Clamp01((leftCurrentSpeed + rightAverageSpeed) / 2);
//		Debug.Log (rightCurrentSpeed);
//        //float leftCurrentSpeed = Input.GetKey(KeyCode.A) ? 3 : 0;
//        //float rightCurrentSpeed = Input.GetKey(KeyCode.D) ? 3 : 0;
//        leftTotalSpeed += leftCurrentSpeed;
//        rightTotalSpeed += rightCurrentSpeed;
//        frames++;
//        leftAverageSpeed = leftTotalSpeed / frames;
//        rightAverageSpeed = rightTotalSpeed / frames;
//        leftAverageSpeed = Mathf.Clamp01(leftAverageSpeed);
//        rightAverageSpeed = Mathf.Clamp01(rightAverageSpeed);
//		float averageSpeed = (leftAverageSpeed + rightAverageSpeed) / 2;
//        //Debug.Log("Left Average Speed: " + leftAverageSpeed);
//        //Debug.Log("Right Average Speed: " + rightAverageSpeed);
//		return instantaneousWeight*averageCurrentSpeed + averageWeight*averageSpeed;
//    }
	private void AdjustSpeed(){
		float leftCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(leftController).magnitude);
		float rightCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(rightController).magnitude );
		float averageCurrentSpeed = Mathf.Clamp01((leftCurrentSpeed + rightAverageSpeed) / 2);
		float evaluation = Mathf.Pow (0.5f*(averageCurrentSpeed - 0.4f), 3);
		speed += evaluation;
		speed = Mathf.Clamp01 (speed);
	}
}
