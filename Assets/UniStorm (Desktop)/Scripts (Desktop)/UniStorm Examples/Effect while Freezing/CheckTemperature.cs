//Black Horizon Studios

using UnityEngine;
using System.Collections;

public class CheckTemperature : MonoBehaviour 
{
	UniStormWeatherSystem_C uniStormSystem;		//Our UniStorm Script

	public float coldDamageAmount; 		//Doesn't just have to be from cold. This can be for anything UniStorm related.

	public float checkTemperature = 10;		//How often the script checks the temperature. This also is how often the player is damaged when the temp is 32 or below
	public float checkTemperatureTimer = 0;

	//Get our components and store them so we don't have to use GetComponent in the update function
	void Start () 
	{
		uniStormSystem = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();	//Our reference to UniStorm		
	}

	void Update ()
	{
		//Used to check temperature only when checkTemperature's time is reached to avoid checking it every frame.
		//This is also used to check how often the player is damaged from cold weather
		checkTemperatureTimer += Time.deltaTime;

		//It's above freezing
		if (checkTemperatureTimer >= checkTemperature && uniStormSystem.temperature >= 33)
		{
			checkTemperatureTimer = 0;		//Reset our timer and check when the time has been met
		}

		//It's below freezing
		if (checkTemperatureTimer >= checkTemperature && uniStormSystem.temperature <= 32)
		{
			checkTemperatureTimer = 0;		//Reset our timer and check when the time has been met
		}

		if (uniStormSystem.weatherForecaster == 33 && uniStormSystem.temperature >= 33)
		{
			//Start rain puddle effect
		}
	}
}

