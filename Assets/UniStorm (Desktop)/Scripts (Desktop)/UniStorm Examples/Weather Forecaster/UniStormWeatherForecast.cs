using UnityEngine;
using System.Collections;
using System;

public class UniStormWeatherForecast : MonoBehaviour {

	UniStormWeatherSystem_C uniStormSystem;
	GameObject uniStormObject;


	public int Monday = 4;
	public int Tuesday = 4;
	public int Wednesday = 2;
	public int Thursday = 8;
	public int Friday = 8;
	public int Saturday = 11;
	public int Sunday = 8;
	public bool generateForecastOnStart = false;

	public int clearOdds = 70;
	public int generatedOddsClear;
	public int precipitationOdds;

	public int temp = 7;

	public int[] PrecipitationWeatherTypes = {1, 2, 3, 4, 12};
	public int[] ClearWeatherTypes = {4, 7, 8, 11};

	void Start () 
	{
		uniStormObject = GameObject.Find("UniStormSystemEditor");
		uniStormSystem = uniStormObject.GetComponent<UniStormWeatherSystem_C>();

		uniStormSystem.staticWeather = true;

		//uniStormSystem.UniStormDate = new DateTime(uniStormSystem.yearCounter, uniStormSystem.monthCounter, uniStormSystem.dayCounter);

		if (generateForecastOnStart)
		{
			GenerateForecast ();
		}

		UpdateWeather ();
		StartCoroutine(UpdateDelay());
	}

	IEnumerator UpdateDelay()
	{
		yield return new WaitForSeconds(0.0f);
		uniStormSystem.InstantWeather();
	}

	public void GenerateForecast ()
	{
		/*
		Monday = UnityEngine.Random.Range(1,14);
		Tuesday = UnityEngine.Random.Range(1,14);
		Wednesday = UnityEngine.Random.Range(1,14);
		Thursday = UnityEngine.Random.Range(1,14);
		Friday = UnityEngine.Random.Range(1,14);
		Saturday = UnityEngine.Random.Range(1,14);
		Sunday = UnityEngine.Random.Range(1,14);
		*/

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Monday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Monday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Tuesday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Tuesday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Wednesday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Wednesday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Thursday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Thursday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Friday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Friday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Saturday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Saturday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}

		generatedOddsClear = UnityEngine.Random.Range(1, 101);

		if (generatedOddsClear <= clearOdds)
		{
			Sunday = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}

		if (generatedOddsClear > clearOdds)
		{
			Sunday = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
		}
	}

	public void UpdateWeather ()
	{
		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Monday")
		{
			uniStormSystem.weatherForecaster = Monday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Tuesday")
		{
			uniStormSystem.weatherForecaster = Tuesday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Wednesday")
		{
			uniStormSystem.weatherForecaster = Wednesday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Thursday")
		{
			uniStormSystem.weatherForecaster = Thursday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Friday")
		{
			uniStormSystem.weatherForecaster = Friday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Saturday")
		{
			uniStormSystem.weatherForecaster = Saturday;
		}

		if (uniStormSystem.UniStormDate.DayOfWeek.ToString() == "Sunday")
		{
			uniStormSystem.weatherForecaster = Sunday;
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			GenerateForecast ();
		}

		UpdateWeather ();
	}
}
