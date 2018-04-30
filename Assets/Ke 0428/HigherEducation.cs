using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherEducation : MonoBehaviour
{

    bool isFast1;
    bool isFast2;

    //public float slowSlowEndTime1 = 28.5f;
    //public float slowSlowEndTime2 = 73.5f; // slow-slow to slow-fast

    //public float fastSlowStartTime = 29.5f;
    //public float fastSlowEndTime = 78.5f;
    //public float fastFastStartTime = 78.5f;

    //public float slowFastStartTime = 73.5f;

    //public float beginningMusicTime = 30.0f;


    public GameObject DoorRotation;

    // Use this for initialization
    void Start()
    {

        isFast1 = false;


        //AudioController.PlayMusic("slow-slow");

        AudioController.PlayMusic("beginning");

        // StartCoroutine(Test());

        StartCoroutine(FirstPoint());

    }

    // Update is called once per frame
    void Update()
    {

        KeyboardControl();


    }


    IEnumerator FirstPoint()
    {
        while (AudioController.IsPlaying("beginning"))
        {
            yield return null;
        }

        if (isFast1)
        {

            AudioController.PlayMusic("first fast");


        }
        else
        {
            AudioController.PlayMusic("first slow");
        }



        yield return StartCoroutine(DoorOpen());
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(2.0f);
        DoorRotation.transform.Rotate(new Vector3(0, 1, 0) * 60f);

        yield return StartCoroutine(SecondPoint());

    }

    IEnumerator SecondPoint()
    {
        while (AudioController.IsPlaying("first slow") || AudioController.IsPlaying("first fast"))
        {
            yield return null;
        }


        if (isFast2)
        {

            AudioController.PlayMusic("second fast");


        }
        else
        {
            AudioController.PlayMusic("second slow");
        }

        yield return null;

    }

    //IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(slowSlowEndTime1);


    //    if (isFast1)
    //    {
    //        Debug.Log("switch1 start time: " + Time.time);

    //        AudioController.StopMusic();


    //        AudioController.PlayMusic("fast-slow", 1f, 0, fastSlowStartTime);

    //        Debug.Log("time after switch1: " + Time.time);

    //        DoorRotation.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);

    //        yield return new WaitForSeconds(fastSlowEndTime - fastSlowStartTime);

    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(slowSlowEndTime2 - slowSlowEndTime1);
    //    }

    //    if (isFast2)
    //    {
    //        Debug.Log("switch2 start time: " + Time.time);

    //        AudioController.StopMusic();

    //        AudioController.PlayMusic("fast-fast", 1f, 0, fastFastStartTime);

    //        Debug.Log("time after switch2: " + Time.time);                        

    //    }



    //    yield return null; 
    //}

    void KeyboardControl()
    {
        // change conditions later
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isFast1 = true;

            Debug.Log("isFast1 = true");

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isFast2 = true;

            Debug.Log("isFast2 = true");

        }



    }

}
