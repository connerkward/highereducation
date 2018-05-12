using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UniStorm_Clock_C : MonoBehaviour {

	UniStormWeatherSystem_C uniStormSystem;
	GameObject uniStormObject;

	public bool use12hourclock = false;

	public Text ClockText;
	public Text DateText;
	//public Text WeatherText;
	public Text TempText;
	public Image WeatherImage;
	public Image SeasonImage;
	public Image MoonImage;

	float WeatherUpdateTimer;
	public float WeatherUpdateSeconds = 30;
	float UpdateTimer;

	public Sprite ClearWeatherIcon;
	public Sprite MostlyClearWeatherIcon;
	public Sprite PartlyCloudyWeatherIcon;
	public Sprite MostlyCloudyWeatherIcon;
	public Sprite LightRainWeatherIcon;
	public Sprite HeavyRainWeatherIcon;
	public Sprite ThunderStormsWeatherIcon;
	public Sprite LightSnowWeatherIcon;
	public Sprite HeavySnowWeatherIcon;
	public Sprite FoggyWeatherIcon;
	public Sprite WindyWeatherIcon;

	public Sprite SpringIcon;
	public Sprite SummerIcon;
	public Sprite FallIcon;
	public Sprite WinterIcon;
	
	public Sprite MoonPhase1Icon;
	public Sprite MoonPhase2Icon;
	public Sprite MoonPhase3Icon;
	public Sprite MoonPhase4Icon;
	public Sprite MoonPhase5Icon;
	public Sprite MoonPhase6Icon;
	public Sprite MoonPhase7Icon;
	public Sprite MoonPhase8Icon;

	// Use this for initialization
	void Start () 
	{
		uniStormObject = GameObject.Find("UniStormSystemEditor");
		uniStormSystem = uniStormObject.GetComponent<UniStormWeatherSystem_C>(); 

		WeatherUpdateTimer = WeatherUpdateSeconds - 0.25f;
	}


	void Update () 
	{
		UpdateTimer += Time.deltaTime;

		if (!use12hourclock && UpdateTimer > 0.25f)
		{
			ClockText.text = uniStormSystem.hourCounter + ":" + uniStormSystem.minuteCounter.ToString("00");
			DateText.text = uniStormSystem.monthCounter + "/" + uniStormSystem.dayCounter + "/" + uniStormSystem.yearCounter;
			TempText.text = uniStormSystem.temperature + "°";
			UpdateTimer = 0;
		}

		if (use12hourclock && UpdateTimer > 0.25f)
		{
			if (uniStormSystem.hourCounter <= 11)
			{
				ClockText.text = uniStormSystem.hourCounter + ":" + uniStormSystem.minuteCounter.ToString("00") + " AM";
			}

			if (uniStormSystem.hourCounter == 0)
			{
				ClockText.text = (uniStormSystem.hourCounter + 12) + ":" + uniStormSystem.minuteCounter.ToString("00") + " AM";
			}

			if (uniStormSystem.hourCounter == 12)
			{
				ClockText.text = uniStormSystem.hourCounter + ":" + uniStormSystem.minuteCounter.ToString("00") + " PM";
			}

			if (uniStormSystem.hourCounter >= 13)
			{
				ClockText.text = (uniStormSystem.hourCounter - 12) + ":" + uniStormSystem.minuteCounter.ToString("00") + " PM";
			}

			DateText.text = uniStormSystem.monthCounter + "/" + uniStormSystem.dayCounter + "/" + uniStormSystem.yearCounter;
			TempText.text = uniStormSystem.temperature + "°";

			UpdateTimer = 0;
		}

		WeatherUpdateTimer += Time.deltaTime;

		if (WeatherUpdateTimer >= WeatherUpdateSeconds)
		{
			UpdateIcons();
			WeatherUpdateTimer = 0;
		}
	}

	public void UpdateIcons ()
	{
		//Weather Icons
		if (uniStormSystem.weatherString == "Foggy")
		{
			WeatherImage.sprite = FoggyWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Light Rain")
		{
			WeatherImage.sprite = LightRainWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Heavy Rain & Thunder Storm")
		{
			WeatherImage.sprite = ThunderStormsWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Heavy Rain (No Thunder)")
		{
			if (uniStormSystem.temperature > 32)
			{
				WeatherImage.sprite = HeavyRainWeatherIcon;
			}

			if (uniStormSystem.temperature <= 32)
			{
				WeatherImage.sprite = HeavySnowWeatherIcon;
			}
		}
		
		if (uniStormSystem.weatherString == "Light Snow")
		{
			WeatherImage.sprite = LightSnowWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Heavy Snow")
		{
			WeatherImage.sprite = HeavySnowWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Clear")
		{
			WeatherImage.sprite = ClearWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Mostly Clear")
		{
			WeatherImage.sprite = MostlyClearWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Partly Cloudy")
		{
			WeatherImage.sprite = PartlyCloudyWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Mostly Cloudy")
		{
			WeatherImage.sprite = MostlyCloudyWeatherIcon;
		}
		
		if (uniStormSystem.weatherString == "Falling Fall Leaves")
		{
			WeatherImage.sprite = WindyWeatherIcon;
		}

		//Season Icons
		if (uniStormSystem.isSpring)
		{
			SeasonImage.sprite = SpringIcon;
		}

		if (uniStormSystem.isSummer)
		{
			SeasonImage.sprite = SummerIcon;
		}

		if (uniStormSystem.isFall)
		{
			SeasonImage.sprite = FallIcon;
		}

		if (uniStormSystem.isWinter)
		{
			SeasonImage.sprite = WinterIcon;
		}

		//Moon Icons
		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[0])
		{
			MoonImage.sprite = MoonPhase1Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[1])
		{
			MoonImage.sprite = MoonPhase2Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[2])
		{
			MoonImage.sprite = MoonPhase3Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[3])
		{
			MoonImage.sprite = MoonPhase4Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[4])
		{
			MoonImage.sprite = MoonPhase5Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[5])
		{
			MoonImage.sprite = MoonPhase6Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[6])
		{
			MoonImage.sprite = MoonPhase7Icon;
		}

		if (uniStormSystem.moonObjectComponent.sharedMaterial == uniStormSystem.MoonPhases[7])
		{
			MoonImage.sprite = MoonPhase8Icon;
		}
	}
}
