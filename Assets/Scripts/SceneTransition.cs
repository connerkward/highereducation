using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SceneTransition : MonoBehaviour {
    public AudioClip intro;
    public AudioClip transition;
    public float delay;
    public float waitTime;
    public Image overlayImage;
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        StartCoroutine(Transition());
        overlayImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Transition() {
        source.PlayOneShot(intro);
        yield return new WaitForSeconds(intro.length + delay);
        source.PlayOneShot(transition);
        yield return new WaitForSeconds(transition.length);
        float timer = waitTime;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer > 0) {
            timer -= Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / waitTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SceneManager.LoadScene("Music");
    }

}
