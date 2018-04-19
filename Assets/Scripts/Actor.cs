using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		EventManager.instance.AddEvent(Act);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Act(){
		Debug.Log ("Act");
	}

	public void ActTest(){
		Debug.Log ("ActEvent");
	}
}
