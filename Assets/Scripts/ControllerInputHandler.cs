using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputHandler : MonoBehaviour {
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    public RectTransform indicator;
    public bool keyboardDebugMode;
    private int frames;
    private float leftAverageSpeed;
    private float leftTotalSpeed;
    private float rightAverageSpeed;
    private float rightTotalSpeed;
    public float speed;
	public float mood;
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

        if (keyboardDebugMode)
        {
            TestSpeed();
        } else
        {
            AdjustSpeed ();
        }

        CalculateMood ();
        UpdateIndicator();
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

    private void UpdateIndicator() {
        indicator.anchoredPosition = new Vector2(730 * speed - 365, indicator.anchoredPosition.y);
    }
	private void AdjustSpeed(){
		float leftCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(leftController).magnitude);
		float rightCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(rightController).magnitude );
		float averageCurrentSpeed = Mathf.Clamp01((leftCurrentSpeed + rightAverageSpeed) / 2);
		float evaluation = Mathf.Pow (0.6f*(averageCurrentSpeed - 0.4f), 3);
		speed += evaluation;
		speed = Mathf.Clamp01 (speed);
        Debug.Log(Vector3.Distance(OVRInput.GetLocalControllerPosition(leftController), OVRInput.GetLocalControllerPosition(rightController)));
	}

    private void TestSpeed() {
        speed += 0.001f * (Input.GetKey(KeyCode.D) ? 1 : -1);
        speed = Mathf.Clamp01(speed);
    }

	private void CalculateMood(){
		float evaluation = Mathf.Pow (0.1f*(speed - 0.4f), 3);
		mood += evaluation;
		mood = Mathf.Clamp01 (mood);
	}
}
