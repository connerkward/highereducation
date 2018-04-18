using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public AudioSource agitato;
    public AudioSource smooth;
    public AudioSource leggiero;
    public AudioSource calm;
    public Image overlayImage;
    public float transitionTime;
	// Use this for initialization
	void Start () {
        StartCoroutine(IntroFade());
	}
	
	// Update is called once per frame
	void Update () {
        UpdateVolumes();
	}
    void UpdateVolumes() {
        calm.volume = UpdateVolume(0f);
        leggiero.volume = UpdateVolume(0.33f);
        smooth.volume = UpdateVolume(0.66f);
        agitato.volume = UpdateVolume(1f);
    }

    float UpdateVolume(float mean) {
        return (2 / Mathf.Sqrt(2 * Mathf.PI)) * Mathf.Pow((float)Math.E, -64 * (Mathf.Pow(ControllerInputHandler.instance.speed - mean, 2)));
    }

    IEnumerator IntroFade() {
        float timer = 0;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer < transitionTime) {
            timer += Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / transitionTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
