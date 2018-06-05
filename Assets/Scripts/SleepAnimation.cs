using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAnimation : MonoBehaviour {
    public GameObject z1;
    public GameObject z2;
    public GameObject z3;
    public GameObject amanda;
    public float interval;
    private GameObject currentAnim;
    private GameObject[] zs;
    private int index;
	// Use this for initialization
	void Start () {
        index = 0;
        currentAnim = z1;
        z1.SetActive(true);
        z2.SetActive(false);
        z3.SetActive(false);
        zs = new GameObject[3];
        zs[0] = z1;
        zs[1] = z2;
        zs[2] = z3;
        StartCoroutine(SleepAnim());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SleepAnim() {
        while (PlayerPrefs.GetInt("sceneNo") == 0) {
            yield return new WaitForSeconds(interval / (index + 1));
            zs[index].SetActive(true);
            zs[(index + 1) % 3].SetActive(false);
            zs[(index + 2) % 3].SetActive(false);
            index++;
            index %= 3;
        }
    }
}
