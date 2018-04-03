using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;
    // Use this for initialization
    void Start() {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }

    // Update is called once per frame
    void Update() {

    }
}
