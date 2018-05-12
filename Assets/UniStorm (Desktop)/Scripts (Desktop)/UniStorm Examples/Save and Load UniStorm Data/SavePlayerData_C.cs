//Black Horizon Studios
//UniStormSystem Save Example

using UnityEngine;
using System.Collections;

public class SavePlayerData_C : MonoBehaviour {

	Vector3 playerPosition;
	Vector3 playerRotation;
	UniStormWeatherSystem_C UniStormSystem;
	int currentHour;
	Vector3 rotationToSet;

	void Start () 
	{
		UniStormSystem = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();
	}
	

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.O))
		{
			PlayerPrefs.SetInt("Current Minute", UniStormSystem.minuteCounter);

			PlayerPrefs.SetInt("Current Hour", UniStormSystem.hourCounter);

			PlayerPrefs.SetInt("Current Weather", UniStormSystem.weatherForecaster);
			PlayerPrefs.SetInt("Current Day", UniStormSystem.dayCounter);
			PlayerPrefs.SetInt("Current Month", UniStormSystem.monthCounter);
			PlayerPrefs.SetInt("Current Year", UniStormSystem.yearCounter);
			PlayerPrefs.SetInt("Current Temperature", UniStormSystem.temperature);

			playerPosition = transform.position;
			playerRotation = transform.rotation.eulerAngles; 

			PlayerPrefs.SetFloat("Player Position X", playerPosition.x);
			PlayerPrefs.SetFloat("Player Position Y", playerPosition.y);
			PlayerPrefs.SetFloat("Player Position Z", playerPosition.z);

			PlayerPrefs.SetFloat("Player Rotation X", playerRotation.x);
			PlayerPrefs.SetFloat("Player Rotation Y", playerRotation.y);
			PlayerPrefs.SetFloat("Player Rotation Z", playerRotation.z);

			Debug.Log("Game Saved" + "\n" + " In-Game Time " + UniStormSystem.hourCounter + ":" + UniStormSystem.minuteCounter + " | "  + " In-Game Date " + UniStormSystem.monthCounter + "/" + UniStormSystem.dayCounter + "/" + UniStormSystem.yearCounter + " |  Current Weather " + UniStormSystem.weatherString + " | Current Temperature " + UniStormSystem.temperature);
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			UniStormSystem.SetDateAndTime(PlayerPrefs.GetInt("Current Hour"), PlayerPrefs.GetInt("Current Minute"), PlayerPrefs.GetInt("Current Month"), PlayerPrefs.GetInt("Current Day"), PlayerPrefs.GetInt("Current Year"));
			UniStormSystem.ChangeWeather(PlayerPrefs.GetInt("Current Weather"));

			playerPosition.x = PlayerPrefs.GetFloat("Player Position X");
			playerPosition.y = PlayerPrefs.GetFloat("Player Position Y");
			playerPosition.z = PlayerPrefs.GetFloat("Player Position Z");

			playerRotation.x = PlayerPrefs.GetFloat("Player Rotation X");
			playerRotation.y = PlayerPrefs.GetFloat("Player Rotation Y");
			playerRotation.z = PlayerPrefs.GetFloat("Player Rotation Z");

			UniStormSystem.InstantWeather();

			transform.position = new Vector3 (playerPosition.x, playerPosition.y, playerPosition.z);

			rotationToSet = new Vector3 (playerRotation.x, playerRotation.y, playerRotation.z);
			transform.eulerAngles = rotationToSet;

			Debug.Log("Game Loaded");

		}

		if(Input.GetKeyDown(KeyCode.I))
		{
			//UniStormSystem.startTimeMinute = System.DateTime.Now.Minute;
			//UniStormSystem.startTimeHour = System.DateTime.Now.Hour;

			//UniStormSystem.dayCounter = System.DateTime.Now.Day;
			//UniStormSystem.monthCounter = System.DateTime.Now.Month;
			//UniStormSystem.yearCounter = System.DateTime.Now.Year;
			//UniStormSystem.UniStormDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

			//UniStormSystem.LoadTime();

			//UniStormSystem.SetDateAndTime();

		}
	}
}
