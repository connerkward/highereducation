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
    public static SceneController instance;
    // Use this for initialization

    private void Awake() {
        instance = this;
        LoadAudioAssets();
    }
    public float GetDuration(int index) {
        return scene1Audio[0].length;
    }
    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        source = GetComponent<AudioSource>();
        if (scene.name == "Garden 2") {
            animObj = FindObjectOfType<AnimationObjects>();
            weatherController = FindObjectOfType<Weather_Controller>();
            overlayImage = GameObject.Find("Canvas/Overlay").GetComponent<Image>();
            Debug.Log("scenecontroller Garden 2");
            StartCoroutine(IntroFade());
            if (PlayerPrefs.GetInt("sceneNo") == 0) {
                StartCoroutine(Scene1Events());
            } else if (PlayerPrefs.GetInt("sceneNo") == 1) {
                StartCoroutine(Scene3Events());
            }
        }
        if(scene.name == "Intro") {
            StartCoroutine(IntroDialogue());
        }
    }

    void LoadAudioAssets() {
        scene1Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene1");
        scene3Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene3");
        scene4Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene4");
    }

    IEnumerator IntroDialogue()
    {
        yield return StartCoroutine(PlayDialogue(scene1Audio[0]));
        yield return StartCoroutine(PlayDialogue(scene1Audio[1]));
    }
    IEnumerator Scene1Events() {
        ControllerInputHandler.instance.allowInput = false;
        introMusic.Play();
        //Amanda sleeping animation
        animObj.scene1Objects[0].SetActive(true);
        animObj.scene1Pd[0].Play();
        //amanda dreaming animation
        animObj.scene1Objects[0].SetActive(false);
        animObj.scene1Objects[1].SetActive(true);
        animObj.scene1Pd[1].Play();
        yield return new WaitForSeconds(4);
        yield return StartCoroutine(PlayDialogue(scene1Audio[2]));
        yield return StartCoroutine(PlayDialogue(scene1Audio[3]));

        introMusic.Stop();
        float timer = waitTime;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer > 0) {
            timer -= Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / waitTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene("garden_dream");
        ControllerInputHandler.instance.allowInput = true;
    }

    IEnumerator Scene3Events() {
        Debug.Log("Scene 3 Events coroutine");
        //animation - amanda plants
        animObj.scene4AmandaObj[0].SetActive(true); //Amanda idle
        yield return StartCoroutine(PlayDialogue(scene3Audio[0]));
        //animation - flower grows
        animObj.scene3BloomPd[0].gameObject.SetActive(true);
        animObj.scene3BloomPd[0].Play(); //yellow flowers
        animObj.scene3BloomPd[1].Play(); //pink roses
        yield return new WaitForSeconds((float)animObj.scene3BloomPd[1].duration);
        if (ControllerInputHandler.instance.speed > 0.5) //if chaos wilt flowers
        {
            Debug.Log("Chaos Flowers");
            animObj.scene3BloomPd[0].gameObject.SetActive(false);
            animObj.scene3BloomPd[1].gameObject.SetActive(true);
            animObj.scene3BloomPd[2].Play(); //wilt yellow flowers
            animObj.scene3BloomPd[3].Play(); //wilt pink flowers
            yield return new WaitForSeconds((float)animObj.scene3BloomPd[2].duration);
        }
        yield return StartCoroutine(PlayDialogue(scene3Audio[1]));
        //animation - wind blows
        yield return StartCoroutine(PlayDialogue(scene3Audio[2]));
        yield return StartCoroutine(Scene4Events());
    }

    IEnumerator Scene4Events() {
        Debug.Log("Scene 4 Events coroutine");
        clouds.Play();
        weatherController.ExitCurrentWeather((int)Weather_Controller.WeatherType.RAIN);
        //animation - amanda walks to door
        animObj.scene4AmandaObj[0].SetActive(false);
        yield return StartCoroutine(AmandaWalk());
        //set new idle position
        animObj.scene4AmandaObj[2].SetActive(true);
        //amanda tries to open door
        // door opens
        yield return new WaitForSeconds(4);
        animObj.scene4DoorObjects[2].SetActive(false);
        if (ControllerInputHandler.instance.speed < 0.5) {
            Debug.Log("Harmony Door");
            animObj.scene4DoorObjects[0].SetActive(true);
            animObj.scene4DoorPd[0].Play();
            yield return new WaitForSeconds((float)animObj.scene4DoorPd[0].duration);
        } else {
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
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(PlayDialogue(scene4Audio[3]));
    }

    IEnumerator PlayDialogue(AudioClip clip) {
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 1);
    }

    IEnumerator AmandaWalk() {
        Debug.Log("Amanda walk start");
        animObj.scene4AmandaObj[1].SetActive(true);
        yield return new WaitForSeconds((float)animObj.scene4AmandaPd[1].duration);
        animObj.scene4AmandaObj[1].SetActive(false);
    }

    private void OnApplicationQuit() {
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
