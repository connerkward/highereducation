using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    //public AudioSource agitato;
    //public AudioSource smooth;
    //public AudioSource leggiero;
    //public AudioSource calm;
    public GameObject majorMelodyGO;
    public GameObject majorCountermelodyGO;
    public GameObject majorAccompanimentGO;
    public GameObject minorMelodyGO;
    public GameObject minorCountermelodyGO;
    public GameObject minorAccompanimentGO;
    private AudioSource[] majorMelodies;
    private AudioSource[] majorCountermelodies;
    private AudioSource[] majorAccompaniment;
    private AudioSource[] minorMelodies;
    private AudioSource[] minorCountermelodies;
    private AudioSource[] minorAccompaniment;
    public Image overlayImage;
    public float transitionTime;
	// Use this for initialization
	void Start () {
        StartCoroutine(IntroFade());

        majorMelodies = majorMelodyGO.GetComponentsInChildren<AudioSource>();
        majorCountermelodies = majorCountermelodyGO.GetComponentsInChildren<AudioSource>();
        majorAccompaniment = majorAccompanimentGO.GetComponentsInChildren<AudioSource>();
        minorMelodies = minorMelodyGO.GetComponentsInChildren<AudioSource>();
        minorCountermelodies = minorCountermelodyGO.GetComponentsInChildren<AudioSource>();
        minorAccompaniment = minorAccompanimentGO.GetComponentsInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateVolumes();
	}
    void UpdateVolumes() {
        //calm.volume = UpdateVolume(0f);
        //leggiero.volume = UpdateVolume(0.33f);
        //smooth.volume = UpdateVolume(0.66f);
        //agitato.volume = UpdateVolume(1f);
        UpdateArray(majorMelodies, 0);
        UpdateArray(majorCountermelodies, 0);
        UpdateArray(majorAccompaniment, 0);
        UpdateArray(minorMelodies, 1);
        UpdateArray(minorCountermelodies, 1);
        UpdateArray(minorAccompaniment, 1);
    }

    void UpdateArray(AudioSource[] audioSources, float mean) {
        foreach (AudioSource audioSource in audioSources) {
            audioSource.volume = BinaryTracks(mean);//UpdateVolume(mean);
            audioSource.priority = (int)Mathf.Clamp(18 / audioSource.volume - 21, 1, 256);
        }
    }

    float UpdateVolume(float mean) {
        return (2 / Mathf.Sqrt(2 * Mathf.PI)) * Mathf.Pow((float)Math.E, -3 * (Mathf.Pow(ControllerInputHandler.instance.speed - mean, 2)));
    }

    int BinaryTracks(float mean)
    {
        if(mean == 0)
        {
            return ControllerInputHandler.instance.speed > 0.5f ? 0 : 1;
        } else
        {
            return ControllerInputHandler.instance.speed > 0.5f ? 1 : 0;
        }
        
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
