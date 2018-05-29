using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public AudioSource source;
    public AudioClip[] scene1Audio;
    public AudioClip[] scene3Audio;
    public AudioClip[] scene4Audio;

    public float delay;
    public float waitTime;
    public Image overlayImage;
    // Use this for initialization

    private void Awake()
    {
        scene1Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene1");
        scene3Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene3");
        scene4Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene4");
    }
    IEnumerator Start () {
        Debug.Log("scenecontroller Garden 2");
        source = GetComponent<AudioSource>();
 
        if (PlayerPrefs.GetInt("sceneNo") == 0)
        {
            StartCoroutine(Scene1Events());
        }
        else if (PlayerPrefs.GetInt("sceneNo") == 1)
        {
            yield return StartCoroutine(Scene3Events());  
            yield return StartCoroutine(Scene4Events()); 
        }
        PlayerPrefs.DeleteAll();

    }

    IEnumerator Scene1Events()
    {
        foreach (AudioClip clip in scene1Audio)
        {
            yield return StartCoroutine(playDialogue(clip));
        }

        float timer = waitTime;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / waitTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene("garden_dream");
        yield return null;
    }

    IEnumerator Scene3Events()
    {
        Debug.Log("Scene 3 Events coroutine");
        //animation - amanda plants
        yield return StartCoroutine(playDialogue(scene3Audio[0]));
        //animation - flower grows
        yield return StartCoroutine(playDialogue(scene3Audio[1]));
        //animation - wind blows
        yield return StartCoroutine(playDialogue(scene3Audio[2]));
        yield return null;
    }

    IEnumerator Scene4Events()
    {
        Debug.Log("Scene 4 Events coroutine");
        //animation - amanda next to door, door opens
        yield return StartCoroutine(playDialogue(scene4Audio[0]));
        //animation - reaches out to touch player, rain shield
        yield return StartCoroutine(playDialogue(scene4Audio[1]));
        //pause
        yield return StartCoroutine(playDialogue(scene4Audio[2]));
        yield return null;
    }

    IEnumerator playDialogue(AudioClip clip)
    {
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length+1);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
