using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DreamSceneController : MonoBehaviour {
    public AudioSource source;
    public AudioClip[] scene2p1Audio;
    public AudioClip[] scene2p2Audio;
    public AudioClip[] scene2p3Audio;
    public AudioClip[] scene2p4Audio;

    private List<GameObject> dadObj;

    public float delay;
    public float waitTime;
    public float transitionTime;
    public Image overlayImage;
    // Use this for initialization

    private void Awake()
    {
        scene2p1Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene2p1");
        scene2p2Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene2p2");
        scene2p3Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene2p3");
        scene2p4Audio = Resources.LoadAll<AudioClip>("Audio/Recordings/scene2p4");

    }

    void Start () {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "garden_dream") {
            overlayImage = GameObject.Find("Canvas/Overlay").GetComponent<Image>();
            StartCoroutine(LoadAnimations(scene));
            Debug.Log("Loaded Garden Dream");
            source = GetComponent<AudioSource>();
            StartCoroutine(IntroFade());
            if (PlayerPrefs.GetInt("sceneNo") == 0) {
                PlayerPrefs.SetInt("sceneNo", 1);
                StartCoroutine(Scene2p1Events());
            }
        }
    }

    IEnumerator Scene2p1Events()
    {
        Debug.Log("scene2p1 Events");
        foreach (AudioClip clip in scene2p1Audio)
        {
            yield return StartCoroutine(PlayDialogue(clip));
        }
        //yield return new WaitUntil(()=> ControllerInputHandler.instance.speed < 0.01f);
        yield return StartCoroutine(Scene2p2Events()); ;
    }

    IEnumerator Scene2p2Events()
    {
        Debug.Log("scene2p2 Events");
        foreach (AudioClip clip in scene2p2Audio)
        {
            yield return StartCoroutine(PlayDialogue(clip));
        }
        //yield return new WaitUntil(() => ControllerInputHandler.instance.speed > 0.99f);
        yield return StartCoroutine(Scene2p3Events());
    }

    IEnumerator Scene2p3Events()
    {
        Debug.Log("scene2p3 Events");
        //amanda and father watching stars
        dadObj[0].SetActive(true);
        foreach (AudioClip clip in scene2p3Audio)
        {
            yield return StartCoroutine(PlayDialogue(clip));
        }
        yield return StartCoroutine(Scene2p4Events());
    }

    IEnumerator Scene2p4Events()
    {
        dadObj[0].SetActive(false);
        dadObj[1].SetActive(true);
        Debug.Log("scene2p4 Events");
        yield return StartCoroutine(PlayDialogue(scene2p4Audio[0]));
        if (ControllerInputHandler.instance.speed<0.5)
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[1]));  
        }
        else
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[4]));  
        }
        if (ControllerInputHandler.instance.speed < 0.5)
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[2]));
        }
        else
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[5]));
        }
        if (ControllerInputHandler.instance.speed < 0.5)
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[3]));
        }
        else
        {
            yield return StartCoroutine(PlayDialogue(scene2p4Audio[6]));
        }
        yield return SceneTransition();
    }

    IEnumerator PlayDialogue(AudioClip clip)
    {
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length+1);
    }

    IEnumerator SceneTransition()
    {
        float timer = waitTime;
        Color c = overlayImage.color;
        overlayImage.enabled = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            overlayImage.color = new Color(c.r, c.b, c.g, 1f - timer / waitTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene("Garden 2");
    }

    IEnumerator LoadAnimations(Scene scene)
    {
        yield return new WaitForEndOfFrame();
        dadObj = new List<GameObject>();
        dadObj.Add(GameObject.Find("Dad"));
        dadObj.Add(GameObject.Find("Dadread"));
        dadObj[0].SetActive(false);
        dadObj[1].SetActive(false);
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
