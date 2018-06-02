using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class testAnimationScript : MonoBehaviour {

    public List<PlayableDirector> playableDirectors;
    public bool harmony;
    public List<GameObject> gObjs;

    private void Start()
    {
        StartCoroutine(SceneAnim());
    }
    public IEnumerator SceneAnim()
    {
        //if (ControllerInputHandler.instance.speed > 0.5)
        if (harmony)
        {
            gObjs[0].SetActive(true);
            Debug.Log("harmony - flower rose bloom");
            playableDirectors[0].Play();
        }
        else
        {
            Debug.Log("chaos - yellow flower wilt");
            gObjs[1].SetActive(true);
            playableDirectors[1].Play();
            yield return new WaitForSeconds((float)playableDirectors[1].duration);
            gObjs[1].SetActive(false);
            gObjs[2].SetActive(true);
            playableDirectors[2].Play();
        }
    }
}
