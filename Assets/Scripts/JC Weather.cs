using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public static float 

public class JCWeather : MonoBehaviour {
    float speedLowBound = 0;
    float speedUpBound = 1;

    // 3 tiers of weather, can be increased to 4, 5, or 6 steps
    float WeatherMin = 0; // can be reduced later with in line math
    float WeatherStep1 = 40;
    float WeatherStep2 = 80;
    float WeatherMax = 120;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float _speed = ControllerInputHandler.instance.speed;
        float speedcoef = ((_speed - speedLowBound) / (speedUpBound - speedLowBound)); //speed coeficient
        float curWeatherState = speedcoef * (WeatherMin - (WeatherMax)) + WeatherMax;
        //case statement and done


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
    }
}
