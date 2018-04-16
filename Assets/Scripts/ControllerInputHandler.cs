using System.Collections;
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
    public float averageSpeed;
	public float multiplier;
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
	void Update () {
        averageSpeed = CalculateSpeed();
    }

    private float CalculateSpeed() {
        float leftCurrentSpeed = multiplier* Mathf.Abs(OVRInput.GetLocalControllerVelocity(leftController).magnitude);
        float rightCurrentSpeed = multiplier*Mathf.Abs(OVRInput.GetLocalControllerVelocity(rightController).magnitude);
		Debug.Log (leftCurrentSpeed);
		Debug.Log (rightCurrentSpeed);
        //float leftCurrentSpeed = Input.GetKey(KeyCode.A) ? 3 : 0;
        //float rightCurrentSpeed = Input.GetKey(KeyCode.D) ? 3 : 0;
        leftTotalSpeed += leftCurrentSpeed;
        rightTotalSpeed += rightCurrentSpeed;
        frames++;
        leftAverageSpeed = leftTotalSpeed / frames;
        rightAverageSpeed = rightTotalSpeed / frames;
        leftAverageSpeed = Mathf.Clamp01(leftAverageSpeed);
        rightAverageSpeed = Mathf.Clamp01(rightAverageSpeed);
        Debug.Log("Left Average Speed: " + leftAverageSpeed);
        Debug.Log("Right Average Speed: " + rightAverageSpeed);
        return (leftAverageSpeed + rightAverageSpeed) / 2;
    }
}
