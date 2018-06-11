using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkScript2 : MonoBehaviour {

    private float startTime, journeyLength;
    public GameObject keys;
    public GameObject handout;
    public float speed = 1.0F;
    public Vector3 start, end;
    public float destX = 303.4123f;
    public float destZ = 328.4f;
    // Use this for initialization
    void Start()
    {
        //start = new Vector3(310.4809f, 2.996787f, 333.907f);
        start = new Vector3(7.049987f, 8.279999f, -1.069999f);
        //end = new Vector3(303.4123f, 2.955074f, 328.4f);
        end = new Vector3(5.62f, 12.16f, -2.84f);
        //handout = GameObject.Find("Amanda_handout");
        startTime = Time.time;
        journeyLength = Vector3.Distance(start, end);
    }

    // Update is called once per frame
    void Update()
    {
        //float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
        //transform.position = Vector3.Lerp(start, end, fracJourney);
        if (transform.position.x > destX )
        {
            transform.Translate(Time.deltaTime, 0, 0);
            //transform.Translate(0, Time.deltaTime, 0, Space.World);
        }
        if (transform.position.z > destZ)
        {
            transform.Translate(0, 0, Time.deltaTime);
        }
    }
}
