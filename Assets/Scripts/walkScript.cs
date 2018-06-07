using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class walkScript : MonoBehaviour {
    private float destX = 315.0f;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < destX)
        {
            transform.Translate(2*Time.deltaTime, 0, 0);
            //transform.Translate(0, Time.deltaTime, 0, Space.World);
        }
    }
}
