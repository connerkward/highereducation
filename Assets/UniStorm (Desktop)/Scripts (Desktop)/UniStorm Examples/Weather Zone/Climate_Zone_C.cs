using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Climate_Zone_C : MonoBehaviour 
{
	public string ClimateName = "Enter Name in Editor";
	public GameObject uniStormSystem;
	public UniStormWeatherSystem_C uniStormsScript;
	public int zoneWeather = 1;

	public string playerTag = "Player";

	public GameObject PlayerObject;
	public float updateInterval = 0.1f;
	public int climateHeight = 350;
	float updateIntervalTimer = 0.1f;
	public int ifGreaterOrLessThan = 0;

	public int DetectionType = 0;
	public int TemperatureType = 0;

	public int minSpringTemp;
	public int maxSpringTemp;
	public int minSummerTemp;
	public int maxSummerTemp;
	public int minFallTemp;
	public int maxFallTemp;
	public int minWinterTemp;
	public int maxWinterTemp;
	public int startingSpringTemp;
	public int startingSummerTemp;
	public int startingFallTemp;
	public int startingWinterTemp;

	public int precipitationOdds = 70;
	public int precipitationOddsSpring = 30;
	public int precipitationOddsSummer = 80;
	public int precipitationOddsFall = 50;
	public int precipitationOddsWinter = 30;

	public AnimationCurve PrecipitationGraph;


	void Start () 
	{
		uniStormSystem = GameObject.Find("UniStormSystemEditor");
		uniStormsScript = uniStormSystem.GetComponent<UniStormWeatherSystem_C>();
	}

	void Update ()
	{
		if (DetectionType == 1)
		{
			updateIntervalTimer += Time.deltaTime;

			if (updateIntervalTimer >= updateInterval)
			{
				if (PlayerObject.transform.position.y > climateHeight && ifGreaterOrLessThan == 0)
				{
					UpdateUniStorm ();
				}

				if (PlayerObject.transform.position.y < climateHeight && ifGreaterOrLessThan == 1)
				{
					UpdateUniStorm ();
				}

				updateIntervalTimer= 0;
			}
		}
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == playerTag && DetectionType == 0)
		{
			UpdateUniStorm ();
		}
	}

	public void UpdateUniStorm ()
	{
		uniStormsScript.minSpringTemp = minSpringTemp;
		uniStormsScript.maxSpringTemp = maxSpringTemp;
		uniStormsScript.minSummerTemp = minSummerTemp;
		uniStormsScript.maxSummerTemp = maxSummerTemp;
		uniStormsScript.minFallTemp = minFallTemp;
		uniStormsScript.maxFallTemp = maxFallTemp;
		uniStormsScript.minWinterTemp = minWinterTemp;
		uniStormsScript.maxWinterTemp = maxWinterTemp;
		
		uniStormsScript.startingSpringTemp = startingSpringTemp;
		uniStormsScript.startingSummerTemp = startingSummerTemp;
		uniStormsScript.startingFallTemp = startingFallTemp;
		uniStormsScript.startingWinterTemp = startingWinterTemp;
		
		uniStormsScript.precipitationOddsSpring = precipitationOddsSpring;
		uniStormsScript.precipitationOddsSummer = precipitationOddsSummer;
		uniStormsScript.precipitationOddsFall = precipitationOddsFall;
		uniStormsScript.precipitationOddsWinter = precipitationOddsWinter;


		if (uniStormsScript.windSoundComponent.volume >= 0.8f)
		{
			uniStormsScript.windSnowSoundComponent.volume = 0.8f;
		}

		if (uniStormsScript.windSnowSoundComponent.volume >= 0.8f)
		{
			uniStormsScript.windSoundComponent.volume = 0.8f;
		}

		if (uniStormsScript.currentRainIntensity >= 500 && uniStormsScript.temperature <= 32)
		{
			uniStormsScript.currentSnowIntensity = 1000;
		}

		if (uniStormsScript.currentSnowIntensity >= 500 && uniStormsScript.temperature > 32)
		{
			uniStormsScript.currentRainIntensity = 1000;
		}


	}
}
