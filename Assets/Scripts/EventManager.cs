using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
	[SerializeField]
	private UnityEvent events;
	public static EventManager instance;
	// Use this for initialization
	void Awake () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			events.Invoke();
		}
	}

	public void AddEvent(UnityAction listener){
		events.AddListener (listener);
	}
}
