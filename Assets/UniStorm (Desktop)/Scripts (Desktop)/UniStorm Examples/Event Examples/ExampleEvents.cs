using UnityEngine;
using System.Collections;

//UniStorm Events can be called by simply creating a public function
//and calling it with UniStorm's Event system.
public class ExampleEvents : MonoBehaviour {

	public void SpawnObject(GameObject ObjectToSpawn){
		if (GameObject.Find("Event 1 Spawn Location") != null){
			Instantiate(ObjectToSpawn, GameObject.Find("Event 1 Spawn Location").transform.position, Quaternion.Euler(180,0,0));
		}
	}

	public void LightsControl(bool LightControl){
		GameObject AllLights = GameObject.Find("Light Controller");

		foreach (Transform T in AllLights.transform){
			T.gameObject.SetActive(LightControl);
		}
	}
}
