using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public AudioSource source;
    public AudioSource introMusic;
    public AudioClip[] scene1Audio;
    public AudioClip[] scene3Audio;
    public AudioClip[] scene4Audio;

    public float delay;
    public float waitTime;
    public Image overlayImage;
    public float transitionTime;
    private Weather_Controller weatherController;
    public ParticleSystem clouds;
    // Use this for initialization

    private void Awake()
    {
        scene1Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene1");
        scene3Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene3");
        scene4Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene4");
    }
    void Start () {
        weatherController = FindObjectOfType<Weather_Controller>();
        overlayImage = GameObject.Find("Canvas/Overlay").GetComponent<Image>();
        StartCoroutine(IntroFade());

        Debug.Log("scenecontroller Garden 2");
        source = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("sceneNo") == 0) {
            StartCoroutine(Scene1Events());
        } else if (PlayerPrefs.GetInt("sceneNo") == 1) {
            StartCoroutine(Scene3Events());
        }
        //StartCoroutine(Scene3Events());
    }

    IEnumerator Scene1Events()
    {
        ControllerInputHandler.instance.allowInput = false;
        introMusic.Play();
        foreach (AudioClip clip in scene1Audio)
        {
            yield return StartCoroutine(PlayDialogue(clip));
        }
        introMusic.Stop();
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
        ControllerInputHandler.instance.allowInput = true;
    }

    IEnumerator Scene3Events()
    {
        Debug.Log("Scene 3 Events coroutine");
        //animation - amanda plants
        yield return StartCoroutine(PlayDialogue(scene3Audio[0]));
        //animation - flower grows
        yield return StartCoroutine(PlayDialogue(scene3Audio[1]));
        //animation - wind blows
        yield return StartCoroutine(PlayDialogue(scene3Audio[2]));
        yield return StartCoroutine(Scene4Events());
    }

    IEnumerator Scene4Events()
    {
        clouds.Play();
        weatherController.ExitCurrentWeather((int)Weather_Controller.WeatherType.RAIN);
        Debug.Log("Scene 4 Events coroutine");
        //animation - amanda next to door, door opens
        yield return StartCoroutine(PlayDialogue(scene4Audio[0]));
        //animation - reaches out to touch player, rain shield
        yield return StartCoroutine(PlayDialogue(scene4Audio[1]));
        //pause
        yield return StartCoroutine(PlayDialogue(scene4Audio[2]));
        yield return null;
    }

    IEnumerator PlayDialogue(AudioClip clip)
    {
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length+1);
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void OnApplicationQuit()
    {
        Debug.Log("player pref reset");
        PlayerPrefs.DeleteAll();
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
