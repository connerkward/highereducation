using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroTransition : MonoBehaviour {
    public float transitionTime;
    private Image overlayImage;
	// Use this for initialization
	void Start () {
        overlayImage = GameObject.Find("Canvas (1)/Overlay").GetComponent<Image>();
        StartCoroutine(IntroFade());
	}
    IEnumerator SceneTransition() {
        float timer = transitionTime;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer > 0) {
            timer -= Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / transitionTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene("Garden 2");
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
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(SceneTransition());
    }
}
