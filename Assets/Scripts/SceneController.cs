using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SceneController : MonoBehaviour {
    private AudioSource source;
    public GameObject rainShield;
    public AudioSource introMusic;
    private AudioClip[] scene1Audio;
    private AudioClip[] scene3Audio;
    private AudioClip[] scene4Audio;
    private AudioClip[] sfx;
    private AnimationObjects animObj;

    public float delay;
    public float waitTime;
    public Image overlayImage;
    public float transitionTime;
    private Weather_Controller weatherController;
    public ParticleSystem clouds;
    public ParticleSystem leaves;
    public bool skipToScene3;
    public static SceneController instance;
    // Use this for initialization

    private void Awake() {
        instance = this;
        rainShield.SetActive(false);
        QualitySettings.vSyncCount = 0;
        LoadAudioAssets();
    }
    public float GetDuration(int index) {
        return scene1Audio[index].length;
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
            if (skipToScene3) {
                ControllerInputHandler.instance.allowInput = true;
                PlayerPrefs.SetInt("sceneNo", 1);
            }
            animObj = FindObjectOfType<AnimationObjects>();
            weatherController = FindObjectOfType<Weather_Controller>();
            overlayImage = GameObject.Find("Canvas/Overlay").GetComponent<Image>();
            Debug.Log("scenecontroller Garden 2");
            StartCoroutine(IntroFade());
            if (PlayerPrefs.GetInt("sceneNo") == 0) {
                GameObject.Find("Canvas").SetActive(false);
                StartCoroutine(Scene1Events());
            } else if (PlayerPrefs.GetInt("sceneNo") == 1) {
                FindObjectOfType<SleepAnimation>().gameObject.SetActive(false);
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
        sfx = Resources.LoadAll<AudioClip>("Audio/sfx/garden");
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
        animObj.scene1Pd[0].gameObject.SetActive(true);
        animObj.scene1Pd[0].Play();
        //amanda dreaming animation
        animObj.scene1Pd[0].gameObject.SetActive(false);
        animObj.scene1Pd[1].gameObject.SetActive(true);
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
        //Amanda sleeping animation
        animObj.scene1Pd[0].gameObject.SetActive(true);
        animObj.scene1Pd[0].Play();
        yield return new WaitForSeconds(2);
        //amanda sits up 
        animObj.scene1Pd[0].gameObject.SetActive(false);
        animObj.scene3AmandaPd[0].gameObject.SetActive(true);
        animObj.scene3AmandaPd[0].Play();
        yield return new WaitForSeconds((float)animObj.scene3AmandaPd[0].duration);
        //animation - amanda plants
        animObj.scene3AmandaPd[0].gameObject.SetActive(false);
        animObj.scene3AmandaPd[1].gameObject.SetActive(true);
        animObj.scene3AmandaPd[1].Play();
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(PlayDialogue(scene3Audio[0]));
        yield return new WaitForSeconds(3);
        //animation - flower grows
        Debug.Log("bloom");
        animObj.scene3BloomPd[0].transform.parent.gameObject.SetActive(true);
        animObj.scene3BloomPd[0].Play(); //yellow flowers
        animObj.scene3BloomPd[1].Play(); //pink roses
        yield return StartCoroutine(PlayDialogue(sfx[0]));
        animObj.scene3AmandaPd[1].Pause();
        
        yield return new WaitForSeconds((float)animObj.scene3BloomPd[1].duration);
        if (ControllerInputHandler.instance.speed > 0.5) //if chaos wilt flowers
        {
            Debug.Log("Chaos Flowers");
            animObj.scene3BloomPd[0].transform.parent.gameObject.SetActive(false);
            animObj.scene3BloomPd[2].transform.parent.gameObject.SetActive(true);
            animObj.scene3BloomPd[2].Play(); //wilt yellow flowers
            animObj.scene3BloomPd[3].Play(); //wilt pink flowers
            yield return new WaitForSeconds((float)animObj.scene3BloomPd[2].duration);
        }
        animObj.scene3AmandaPd[1].gameObject.SetActive(false);
        animObj.scene3AmandaPd[2].gameObject.SetActive(true); //surprised Amanda
        animObj.scene3AmandaPd[2].Play();
        yield return StartCoroutine(PlayDialogue(scene3Audio[1]));
        yield return new WaitForSeconds(2);
        //animation - wind blows
        leaves.Play();


        animObj.scene3AmandaPd[2].gameObject.SetActive(false);
        animObj.scene3AmandaPd[3].gameObject.SetActive(true); //amanda stands up
        animObj.scene3AmandaPd[3].Play();
        yield return StartCoroutine(PlayDialogue(scene3Audio[2]));
        animObj.scene3AmandaPd[3].gameObject.SetActive(false);
        animObj.scene3AmandaPd[4].gameObject.SetActive(true); //amanda crosses arms
        animObj.scene3AmandaPd[4].Play();
        yield return new WaitForSeconds((float)animObj.scene3AmandaPd[4].duration);
        animObj.scene3AmandaPd[4].gameObject.SetActive(false);
        yield return StartCoroutine(Scene4Events());
    }

    IEnumerator Scene4Events() {
        Debug.Log("Scene 4 Events coroutine");
        clouds.Play();
        weatherController.ExitCurrentWeather((int)Weather_Controller.WeatherType.RAIN);
        //animation - amanda walks to door
        animObj.scene4AmandaPd[0].gameObject.SetActive(false);
        yield return StartCoroutine(AmandaWalk());
        //amanda tries to open door
        animObj.scene4AmandaPd[2].gameObject.SetActive(true);
        animObj.scene4AmandaPd[2].Play();
        yield return new WaitForSeconds((float)animObj.scene4AmandaPd[2].duration);
        

        // door opens
        yield return new WaitForSeconds(4);
        animObj.scene4DoorPd[2].gameObject.SetActive(false);
        if (ControllerInputHandler.instance.speed < 0.5) {
            Debug.Log("Harmony Door");
            animObj.scene4DoorPd[0].gameObject.SetActive(true);
            yield return StartCoroutine(PlayDialogue(sfx[1]));
            animObj.scene4DoorPd[0].Play();
            yield return new WaitForSeconds((float)animObj.scene4DoorPd[0].duration);
        } else {
            Debug.Log("Chaos Door");
            animObj.scene4DoorPd[0].gameObject.SetActive(false);
            animObj.scene4DoorPd[1].gameObject.SetActive(true);
            yield return StartCoroutine(PlayDialogue(sfx[2]));
            animObj.scene4DoorPd[1].Play();
            yield return new WaitForSeconds((float)animObj.scene4DoorPd[1].duration);
        }

        yield return StartCoroutine(PlayDialogue(scene4Audio[0]));
        //animation - reaches out to touch player
        animObj.scene4AmandaPd[2].gameObject.SetActive(false);
        animObj.scene4AmandaPd[4].gameObject.SetActive(true);
        animObj.scene4AmandaPd[4].Play();
        yield return new WaitForSeconds((float)animObj.scene4AmandaPd[4].duration);

        //rain shield appears
        rainShield.SetActive(true);
        animObj.scene4AmandaPd[4].gameObject.SetActive(false);
        animObj.scene4AmandaPd[5].gameObject.SetActive(true); //amanda smile
        animObj.scene4AmandaPd[5].Play();
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(PlayDialogue(scene4Audio[1]));
        yield return StartCoroutine(PlayDialogue(scene4Audio[2])); //thankyou dad
        rainShield.SetActive(false);
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(PlayDialogue(scene4Audio[3])); //archangel congrats
        yield return StartCoroutine(PlayDialogue(scene4Audio[4])); //archangel play in garden
    }

    IEnumerator PlayDialogue(AudioClip clip) {
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 1);
    }

    IEnumerator AmandaWalk() {
        Debug.Log("Amanda walk start");
        animObj.scene4AmandaPd[1].gameObject.SetActive(true);
        yield return new WaitForSeconds((float)animObj.scene4AmandaPd[1].duration);
        animObj.scene4AmandaPd[1].gameObject.SetActive(false);
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
