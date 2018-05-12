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
    private float distanceLastFrame;
    public float upperThreshold;
    public float lowerThreshold;
    public float speed;
    public float mood;
    public static ControllerInputHandler instance;
    // Use this for initialization
    void Start() {
        instance = this;
        frames = 0;
        leftAverageSpeed = 0;
        leftTotalSpeed = 0;
        rightAverageSpeed = 0;
        rightTotalSpeed = 0;
        distanceLastFrame = 0;
        speed = 0.5f;
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

    private void UpdateIndicator() {
        indicator.anchoredPosition = new Vector2(730 * speed - 365, indicator.anchoredPosition.y);
    }

    private void ChangeSpeedBasedOnDistance()
    {
        frames++;
        float distance = Vector3.Distance(OVRInput.GetLocalControllerPosition(leftController), OVRInput.GetLocalControllerPosition(rightController));
        if(distance < distanceLastFrame)
        {
            Debug.Log("Max Distance: " + distance);
        }
        distanceLastFrame = distance;
    }
    private void AdjustSpeed() {
        float leftCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(leftController).magnitude);
        float rightCurrentSpeed = Mathf.Abs(OVRInput.GetLocalControllerVelocity(rightController).magnitude);
        float averageCurrentSpeed = Mathf.Clamp01((leftCurrentSpeed + rightAverageSpeed) / 2);
        float evaluation = Mathf.Pow(0.6f * (averageCurrentSpeed - 0.4f), 3);
        speed += evaluation;
        if (speed > 0.5f) { // current state is chaos
            if(averageCurrentSpeed < upperThreshold && averageCurrentSpeed > lowerThreshold) { // current movement is harmony
                // move towards harmony
            } else if (averageCurrentSpeed < lowerThreshold){ // current movement is inaction
                // move towards center
                //speed = Mathf.Clamp(speed, 0f, 0.5f);
            } else { // current movement is chaos
                // move towards chaos
            }
        } else { // current state is harmony
            if (averageCurrentSpeed < upperThreshold && averageCurrentSpeed > lowerThreshold) { // current movement is harmony
                // move towards harmony
            } else if (averageCurrentSpeed < lowerThreshold) { // current movement is inaction
                // move towards center
                //speed = Mathf.Clamp(speed, 0.5f, 1f);
            } else { // current movement is chaos
                // move towards chaos
            }
        }
        speed = Mathf.Clamp01(speed);
    }

    private void TestSpeed() {
        speed += 0.001f * (Input.GetKey(KeyCode.D) ? 1 : -1);
        speed = Mathf.Clamp01(speed);
    }

    private void CalculateMood() {
        float evaluation = Mathf.Pow(0.1f * (speed - 0.4f), 3);
        mood += evaluation;
        mood = Mathf.Clamp01(mood);
    }
}
