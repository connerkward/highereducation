using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetUniStormComponents_C_Mobile : MonoBehaviour
{
	#region fields
	public GameObject unistorm;
	public GameObject unistormCamera;
	public ParticleSystem rain;
	public ParticleSystem snow;
	public ParticleSystem lightningBugs;
	public ParticleSystem rainMist;
	public ParticleSystem snowDust;
	public ParticleSystem windyLeaves;
	public GameObject lightningBolt1;
	public ParticleSystem rainSplash;
	public Transform lightningPosition;

	#endregion
	
	void Awake ()	
	{
		unistorm = GameObject.FindGameObjectWithTag("UniStorm");

		if (!ReferenceEquals (unistorm,null))	
		{	
			UniStormMobileWeatherSystem_C[] weathers = unistorm.GetComponentsInChildren<UniStormMobileWeatherSystem_C>();	
			UniStormMobileWeatherSystem_C weather = weathers[0];
			
			if (!ReferenceEquals (weather,null))	
			{	
				weather.rain = rain;	
				weather.snow = snow;	
				weather.butterflies = lightningBugs;		
				weather.rainMist = rainMist;
				weather.rainSplashes = rainSplash;
				weather.lightningSpawn = lightningPosition;	
				weather.snowMistFog = snowDust;			
				weather.windyLeaves = windyLeaves;		
				weather.lightningBolt1 = lightningBolt1;	
				weather.cameraObject = unistormCamera;	
			}
		}	
	}
}