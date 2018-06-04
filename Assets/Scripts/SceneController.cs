using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SceneController : MonoBehaviour {
    public AudioSource source;
    public AudioSource introMusic;
    private AudioClip[] scene1Audio;
    private AudioClip[] scene3Audio;
    private AudioClip[] scene4Audio;
    private AnimationObjects animObj;

    public float delay;
    public float waitTime;
    public Image overlayImage;
    public float transitionTime;
    private Weather_Controller weatherController;
    public ParticleSystem clouds;
    // Use this for initialization

    private void Awake()
    {
        LoadAudioAssets();
        animObj = GetComponent<AnimationObjects>();
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
    }

    void LoadAudioAssets()
    {
        scene1Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene1");
        scene3Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene3");
        scene4Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene4");
    }

IEnumerator Scene1Events()
    {
        ControllerInputHandler.instance.allowInput = false;
        introMusic.Play();
        //Amanda sleeping animation
        animObj.scene1Objects[0].SetActive(true);
        animObj.scene1Pd[0].Play();
        //play dialogues
        yield return StartCoroutine(PlayDialogue(scene1Audio[0]));
        yield return StartCoroutine(PlayDialogue(scene1Audio[1]));
        //amanda dreaming animation
        animObj.scene1Objects[0].SetActive(false);
        animObj.scene1Objects[1].SetActive(true);
        animObj.scene1Pd[1].Play();
        yield return StartCoroutine(PlayDialogue(scene1Audio[2]));
        yield return StartCoroutine(PlayDialogue(scene1Audio[3]));

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
        animObj.scene3BloomObjects[0].SetActive(true);
        animObj.scene3BloomPd[0].Play(); //yellow flowers
        animObj.scene3BloomPd[1].Play(); //pink roses
        yield return new WaitForSeconds((float)animObj.scene3BloomPd[1].duration);
        if (ControllerInputHandler.instance.speed > 0.5) //if chaos wilt flowers
        {
            Debug.Log("Chaos Flowers");
            animObj.scene3BloomObjects[0].SetActive(false);
            animObj.scene3BloomObjects[1].SetActive(true);
            animObj.scene3BloomPd[2].Play(); //wilt yellow flowers
            animObj.scene3BloomPd[3].Play(); //wilt pink flowers
            yield return new WaitForSeconds((float)animObj.scene3BloomPd[2].duration);
        }
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
        animObj.scene4DoorObjects[2].SetActive(false);
        if (ControllerInputHandler.instance.speed < 0.5)
        {
            Debug.Log("Harmony Door");
            animObj.scene4DoorObjects[0].SetActive(true);
            animObj.scene4DoorPd[0].Play();
            yield return new WaitForSeconds((float)animObj.scene4DoorPd[0].duration);
        }
        else
        {
            Debug.Log("Chaos Door");
            animObj.scene4DoorObjects[0].SetActive(false);
            animObj.scene4DoorObjects[1].SetActive(true);
            animObj.scene4DoorPd[1].Play();
            yield return new WaitForSeconds((float)animObj.scene4DoorPd[1].duration);
        }
        
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
