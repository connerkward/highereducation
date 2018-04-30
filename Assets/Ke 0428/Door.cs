using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public float openAngularSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        transform.Rotate(Vector3.up * Time.deltaTime * 10f);

    }


	public void DoorOpen(float w)
	{

        transform.Rotate(Vector3.up * Time.deltaTime * w);

        // stop


    }

		
		
		
}
