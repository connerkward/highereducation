using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
static class RandomExtensions {
    public static void Shuffle<T>(this System.Random rng, T[] array) {
        int n = array.Length;
        while (n > 1) {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
public class MusicController : MonoBehaviour
{
    //public AudioSource agitato;
    //public AudioSource smooth;
    //public AudioSource leggiero;
    //public AudioSource calm;
    [Range(0f, 1f)]
    public float volumeScale;
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
    public float fadeOutTime;
    public float fadeInTime;
    //private Arrangement currentArrangement;
    //private Arrangement oldArrangement;
    [System.Serializable]
    public struct Arrangement
    {
        public AudioSource[] melody;
        public AudioSource[] countermelody;
        public AudioSource[] accompaniment;
    }
    public Arrangement majorArrangement;
    public Arrangement minorArrangement;
    private Arrangement currentArrangement;
    //public Arrangement[] stage1Arrangements;
    //public Arrangement[] stage2Arrangements;
    //public Arrangement[] stage3Arrangements;
    //public Arrangement stage1Selection;
    //public Arrangement stage2Selection;
    //public Arrangement stage3Selection;
    //public Arrangement inactionSelection;
    //public Arrangement allSelection;
    public float inactionThreshhold;
    public bool isDream;
    public bool isGarden;
    private bool play;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        play = (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Garden 2") && isGarden && PlayerPrefs.GetInt("sceneNo") == 1) || (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("garden_dream") && isDream);
        majorMelodies = majorMelodyGO.GetComponentsInChildren<AudioSource>();
        majorCountermelodies = majorCountermelodyGO.GetComponentsInChildren<AudioSource>();
        majorAccompaniment = majorAccompanimentGO.GetComponentsInChildren<AudioSource>();
        new System.Random().Shuffle(majorMelodies);
        new System.Random().Shuffle(majorCountermelodies);
        new System.Random().Shuffle(majorAccompaniment);
        majorArrangement.melody = majorMelodies;
        majorArrangement.countermelody = majorCountermelodies;
        majorArrangement.accompaniment = majorAccompaniment;
        minorMelodies = minorMelodyGO.GetComponentsInChildren<AudioSource>();
        minorCountermelodies = minorCountermelodyGO.GetComponentsInChildren<AudioSource>();
        minorAccompaniment = minorAccompanimentGO.GetComponentsInChildren<AudioSource>();
        new System.Random().Shuffle(minorMelodies);
        new System.Random().Shuffle(minorCountermelodies);
        new System.Random().Shuffle(minorAccompaniment);
        minorArrangement.melody = minorMelodies;
        minorArrangement.countermelody = minorCountermelodies;
        minorArrangement.accompaniment = minorAccompaniment;
        currentArrangement = minorArrangement;
        //stage1Selection = stage1Arrangements[UnityEngine.Random.Range(0, stage1Arrangements.Length)];
        //stage2Selection = stage2Arrangements[UnityEngine.Random.Range(0, stage2Arrangements.Length)];
        //stage3Selection = stage3Arrangements[UnityEngine.Random.Range(0, stage3Arrangements.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        play = (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Garden 2") && isGarden) || (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("garden_dream") && isDream);
        if (ControllerInputHandler.instance.allowInput && play) {
            if (ControllerInputHandler.instance.speed > 0.5f + inactionThreshhold) {
                Debug.Log("Chaos Arrangement");
                PlayArrangement(minorArrangement);
                StopArrangement(majorArrangement);
                currentArrangement = minorArrangement;
            } else if (ControllerInputHandler.instance.speed < 0.5f - inactionThreshhold) {
                Debug.Log("Harmony Arrangement");
                PlayArrangement(majorArrangement);
                StopArrangement(minorArrangement);
                currentArrangement = majorArrangement;
            } else {
                // only play accompaniment
                //StopArrangement(majorArrangement);
                //StopArrangement(minorArrangement);
                PlayAccompaniment(currentArrangement);
            }
        } else {
            StopArrangement(minorArrangement);
            StopArrangement(majorArrangement);
        }

        //if (!oldArrangement.Equals(currentArrangement))
        //{
        //    StopArrangement(allSelection, 3);
        //    Debug.Log("Arrangement Changed");
        //}
        //oldArrangement = currentArrangement;
        //int a;
        //if (Mathf.Abs(ControllerInputHandler.instance.speed - 0.5f) < inactionThreshhold) {
        //    a = 0;
        //    inactionSelection.accompaniment = currentArrangement.accompaniment;
        //    currentArrangement = inactionSelection;
        //}
        //else if (Mathf.Abs(ControllerInputHandler.instance.speed - 0.5f) < 0.167f)
        //{
        //    currentArrangement = stage1Selection;
        //    a = 1;
        //}
        //else if (Mathf.Abs(ControllerInputHandler.instance.speed - 0.5f) < 0.333f)
        //{
        //    currentArrangement = stage2Selection;
        //    a = 2;
        //}
        //else
        //{
        //    currentArrangement = stage3Selection;
        //    a = 3;
        //}
        //PlayArrangement(currentArrangement, a);

    }

    IEnumerator FadeOut(AudioSource audioSource, float time)
    {
        float timer = 0;
        float initialVolume = audioSource.volume;
        while (timer < time)
        {
            timer += Time.deltaTime;
            audioSource.volume = volumeScale * (1 - timer / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        audioSource.volume = 0f;
    }
    IEnumerator FadeIn(AudioSource audioSource, float time)
    {
        float timer = 0;
        audioSource.volume = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            audioSource.volume = volumeScale * timer / time;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        audioSource.volume = volumeScale;
    }

    public void PlayAccompaniment(Arrangement arrangement) {
        AudioSource audioSource = arrangement.accompaniment[0];
        if(audioSource.volume == 0) {
            StartCoroutine(FadeIn(audioSource, fadeInTime));
        }
    }

    public void PlayArrangement(Arrangement arrangement) {
        foreach(AudioSource audioSource in arrangement.melody) {
            if(audioSource.volume != GetTrackVolume(audioSource) && (audioSource.volume == 0 || audioSource.volume == volumeScale)) {
                if(audioSource.volume == 0) {
                    StartCoroutine(FadeIn(audioSource, fadeInTime));
                } else {
                    StartCoroutine(FadeOut(audioSource, fadeInTime));
                }
            }
        }
        foreach (AudioSource audioSource in arrangement.countermelody) {
            if (audioSource.volume != GetTrackVolume(audioSource) && (audioSource.volume == 0 || audioSource.volume == volumeScale)) {
                if (audioSource.volume == 0) {
                    StartCoroutine(FadeIn(audioSource, fadeInTime));
                } else {
                    StartCoroutine(FadeOut(audioSource, fadeInTime));
                }
            }
        }
        foreach (AudioSource audioSource in arrangement.accompaniment) {
            if (audioSource.volume != GetTrackVolume(audioSource) && (audioSource.volume == 0 || audioSource.volume == volumeScale)) {
                if (audioSource.volume == 0) {
                    StartCoroutine(FadeIn(audioSource, fadeInTime));
                } else {
                    StartCoroutine(FadeOut(audioSource, fadeInTime));
                }
            }
        }
    }

    public void StopArrangement(Arrangement arrangement) {
        foreach (AudioSource audioSource in arrangement.melody) {
            audioSource.volume = 0;
        }
        foreach (AudioSource audioSource in arrangement.countermelody) {
            audioSource.volume = 0;
        }
        foreach (AudioSource audioSource in arrangement.accompaniment) {
            audioSource.volume = 0;
        }
    }

    //public void PlayArrangement(Arrangement arrangement, int a)
    //{
    //    UpdateArray(arrangement.melody, a);
    //    UpdateArray(arrangement.countermelody, a);
    //    UpdateArray(arrangement.accompaniment, a);
    //}
    //public void StopArrangement(Arrangement arrangement, int a)
    //{
    //    UpdateArray(arrangement.melody, a, true);
    //    UpdateArray(arrangement.countermelody, a, true);
    //    UpdateArray(arrangement.accompaniment, a, true);
    //}

    //void UpdateArray(AudioSource[] audioSources, int arrangement, bool stop = false)
    //{
    //    if (audioSources != null)
    //    {
    //        foreach (AudioSource audioSource in audioSources)
    //        {
    //            if (audioSource)
    //            {
    //                if (stop && audioSource.volume == 1 && BinaryTracks(audioSource, arrangement) == 0 && NotInCurrentArrangement(audioSource))
    //                {
    //                    Debug.Log("Lower Volume: " + audioSource.name);
    //                    StartCoroutine(FadeOut(audioSource, fadeOutTime));
    //                }
    //                if (stop && audioSource.volume == 1 && BinaryTracks(audioSource, arrangement) == 0
    //                    && (ControllerInputHandler.instance.speed > 0.5f + inactionThreshhold && (audioSource.name.Contains("major") || audioSource.name.Contains("acc")) ||
    //                    (ControllerInputHandler.instance.speed <= 0.5f - inactionThreshhold && (audioSource.name.Contains("minor") || audioSource.name.Contains("acc")))))
    //                {
    //                    Debug.Log("Stop Volume: " + audioSource.name);
    //                    audioSource.volume = 0;
    //                    //StartCoroutine(FadeOut(audioSource, fadeOutTime));
    //                }
    //                else if (!stop && audioSource.volume == 0 && BinaryTracks(audioSource, arrangement) == 1)
    //                {
    //                    Debug.Log("Raise Volume: " + audioSource.name);
    //                    StartCoroutine(FadeIn(audioSource, fadeInTime));
    //                }
    //                audioSource.priority = (int)Mathf.Clamp(18 / audioSource.volume - 21, 1, 256);
    //            }

    //        }
    //    }

    //}

    //private bool NotInCurrentArrangement(AudioSource audioSource)
    //{
    //    return (Array.Exists(oldArrangement.melody.Except(currentArrangement.melody).ToArray(), element => element == audioSource) ||
    //        Array.Exists(oldArrangement.countermelody.Except(currentArrangement.countermelody).ToArray(), element => element == audioSource) ||
    //        Array.Exists(oldArrangement.accompaniment.Except(currentArrangement.accompaniment).ToArray(), element => element == audioSource));
    //}
    //private bool InAllArrangements(AudioSource audioSource)
    //{
    //    return (Array.Exists(stage3Selection.melody, element => element == audioSource) ||
    //        Array.Exists(stage3Selection.countermelody.ToArray(), element => element == audioSource) ||
    //        Array.Exists(stage3Selection.accompaniment, element => element == audioSource));
    //}

    //float UpdateVolume(float mean)
    //{
    //    return (2 / Mathf.Sqrt(2 * Mathf.PI)) * Mathf.Pow((float)Math.E, -3 * (Mathf.Pow(ControllerInputHandler.instance.speed - mean, 2)));
    //}

    float GetTrackVolume(AudioSource audioSource) {
        if (audioSource.name.Contains("major")) {
            return ControllerInputHandler.instance.speed <= 0.5f - (Array.FindIndex(majorArrangement.melody, element => element == audioSource) + Array.FindIndex(majorArrangement.countermelody, element => element == audioSource) + Array.FindIndex(majorArrangement.accompaniment, element => element == audioSource) + 2) * 0.5f / (majorArrangement.melody.Length - 1) ? volumeScale : 0;

        } else if (audioSource.name.Contains("minor")) {
            return ControllerInputHandler.instance.speed >= 0.5f + (Array.FindIndex(minorArrangement.melody, element => element == audioSource) + Array.FindIndex(minorArrangement.countermelody, element => element == audioSource) + Array.FindIndex(minorArrangement.accompaniment, element => element == audioSource) + 2) * 0.5f / (minorArrangement.melody.Length - 1) ? volumeScale : 0;
        }
        return 0;
    }

    //int BinaryTracks(AudioSource audioSource, int arrangement)
    //{
    //    if (audioSource.name.Contains("major"))
    //    {
    //        if(arrangement == 3)
    //        {
    //            return ControllerInputHandler.instance.speed <= 0.167f ? 1 : 0;
    //        } else if (arrangement == 2)
    //        {
    //            return ControllerInputHandler.instance.speed <= 0.333 ? 1 : 0;
    //        } else if (arrangement == 1)
    //        {
    //            return ControllerInputHandler.instance.speed <= 0.5f - inactionThreshhold ? 1 : 0;
    //        } else {
    //            return (ControllerInputHandler.instance.speed <= 0.5f + inactionThreshhold && audioSource.name.Contains("acc") && audioSource.volume == 1) ? 1 : 0;
    //        }
            
    //    }
    //    else
    //    {
    //        if (arrangement == 3)
    //        {
    //            return ControllerInputHandler.instance.speed >= 0.833f ? 1 : 0;
    //        }
    //        else if (arrangement == 2)
    //        {
    //            return ControllerInputHandler.instance.speed >= 0.667 ? 1 : 0;
    //        }
    //        else if (arrangement == 1)
    //        {
    //            return ControllerInputHandler.instance.speed >= 0.5f + inactionThreshhold ? 1 : 0;
    //        } else {
    //            return (ControllerInputHandler.instance.speed >= 0.5f - inactionThreshhold && audioSource.name.Contains("acc") && audioSource.volume == 1) ? 1 : 0;
    //        }
    //    }
    //}


}
