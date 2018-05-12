using UnityEngine;
using System.Collections;

public class RandomWeatherOnStart : MonoBehaviour {

	UniStormWeatherSystem_C UniStormSystem;

	void Start () 
	{
		//Get a reference to UniStorm
		UniStormSystem = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();
		StartCoroutine(GenerateWeatherOnStart());
	}

	IEnumerator GenerateWeatherOnStart ()
	{
		//Do a quick delay to allow UniStorm to initialize
		yield return new WaitForSeconds(0.05f);

		//Random Weather
		UniStormSystem.weatherForecaster = Random.Range(1, 14);
		UniStormSystem.InstantWeather();
	}
}
