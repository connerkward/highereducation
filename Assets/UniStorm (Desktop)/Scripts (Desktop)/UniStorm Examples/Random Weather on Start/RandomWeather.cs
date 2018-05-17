using UnityEngine;
using System.Collections;

public class RandomWeather : MonoBehaviour {

	private GameObject uniStormSystem;
	private UniStormWeatherSystem_C uniStormSystemScript;
	
	void Awake ()
	{
		//Find the UniStorm Weather System Editor, this must match the UniStorm Editor name
		uniStormSystem = GameObject.Find("UniStormSystemEditor");
		uniStormSystemScript = uniStormSystem.GetComponent<UniStormWeatherSystem_C>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			uniStormSystemScript.weatherForecaster = Random.Range(1,13);
		}
	}
}
