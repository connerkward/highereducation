using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;
    // Use this for initialization
    void Start() {
        transform.position = OVRInput.GetLocalControllerPosition(controller);
    }

    // Update is called once per frame
    void Update() {
		transform.position = OVRInput.GetLocalControllerPosition(controller);
    }
}
