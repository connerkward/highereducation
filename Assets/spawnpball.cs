using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpball : MonoBehaviour {
    //public Animation pbanimation;
    public GameObject powerball;
    //public Transform playerpos;
	// Use this for initialization
	void Start () {
        Instantiate(powerball);
        //pbanimation.Play();
    }

    // Update is called once per frame
    void Update () {
        //powerball
        //Debug.Log("powerball");
        //powerball.SetActive(true); // energy ball flies to garden
        
    }
}
