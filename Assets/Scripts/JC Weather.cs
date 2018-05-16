using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public static float 

public class JCWeather : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float s = ControllerInputHandler.instance.speed;
        //if (s >= 0.16f && s < 0.5f && en_CurrWeather != WeatherType.CLOUDY) {
        //    ExitCurrentWeather((int)WeatherType.CLOUDY);
        //} else if (s >= 0.50f && s < 0.83f && en_CurrWeather != WeatherType.RAIN) {
        //    ExitCurrentWeather((int)WeatherType.RAIN);
        //} else if (s >= 0.83f && s < 1f && en_CurrWeather != WeatherType.THUNDERSTORM) {
        //    ExitCurrentWeather((int)WeatherType.THUNDERSTORM);
        //} else if (s >= 0f && s < 0.16f && en_CurrWeather != WeatherType.SUN) {
        //    ExitCurrentWeather((int)WeatherType.SUN);
        //}
        //if (_bChangeWeather == true)
        //    PickRandomWeather();

        //if (_bStartWeatherChange == true)
        //    ExitCurrentWeather(_iNewWeather);

        GameObject lightGameObject = new GameObject("The Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.color = Color.blue;
        lightGameObject.transform.position = new Vector3(0, 5, 0);
        lightComp.intensity = -s;
    }
}
