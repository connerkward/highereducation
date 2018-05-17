using UnityEngine;
using System.Collections;

public class PlantGrowthSystem : MonoBehaviour {

	UniStormWeatherSystem_C uniStormSystem;
	GameObject uniStormObject;

	public GameObject plantObject;

	public int UpdateTime;

	public float growthAmountX = 0.1f;
	public float growthAmountY = 0.1f;
	public float growthAmountZ = 0.1f;

	public float maxGrowthAmountX = 2;
	public float maxGrowthAmountY = 2;
	public float maxGrowthAmountZ = 2;

	public bool isFullyGrown = false;

	public bool canGrowUsingPrec = false;
	public bool isWaitingUsingPrec = false;

	public bool canGrowUsingSun = false;
	public bool isWaitingUsingSun = false;

	public bool canGrowUsingTemp = false;
	public bool isWaitingUsingTemp = false;

	public int usePrecipitation;
	public int rainNeededToGrow = 100;

	public int useTemperature;
	public int temperatureNeededToGrow = 70;

	public int useSunLight;
	public float sunLightNeededToGrow = 0.5f;

	public int tempRoll;
	public int precRoll;
	public int sunRoll;

	public int seasonInhibitor;
	public bool isSeasonallyInhibited = false;

	public bool isTemperatureInhibited = false;
	public int temperatureInhibitor;
	public int minTemperature;
	public int currentTemperature;

	void Start () 
	{
		uniStormObject = GameObject.Find("UniStormSystemEditor");
		uniStormSystem = uniStormObject.GetComponent<UniStormWeatherSystem_C>(); 

		transform.rotation = Quaternion.Euler (0, Random.Range(5,350), 0);

		maxGrowthAmountX = transform.localScale.x + maxGrowthAmountX;
		maxGrowthAmountY = transform.localScale.y + maxGrowthAmountY;
		maxGrowthAmountZ = transform.localScale.z + maxGrowthAmountZ;
	}

	void Update () 
	{
		if (!isSeasonallyInhibited && !isFullyGrown && !isTemperatureInhibited)
		{
			if (usePrecipitation != 11)
			{
				UsePrecipitation();
			}

			if (useSunLight != 11)
			{
				UseSunLight();
			}

			if (useTemperature != 11)
			{
				UseTemperature();
			}
		}
	}

	void UseSunLight () 
	{
		if (!isFullyGrown)
		{
			if (!isWaitingUsingSun && !canGrowUsingSun)
			{
				if (uniStormSystem.sun.intensity >= sunLightNeededToGrow)
				{
					canGrowUsingSun = true;
				}
			}

				if (canGrowUsingSun && !isWaitingUsingSun && uniStormSystem.minuteCounter >= 59)
				{
					CheckSize();

					sunRoll = Random.Range(useSunLight,11);

					if (sunRoll == useSunLight)
					{
						transform.localScale = new Vector3(transform.localScale.x + growthAmountX, transform.localScale.y + growthAmountY, transform.localScale.z + growthAmountZ);
						isWaitingUsingSun = true;
						canGrowUsingSun = false;
						sunRoll = 0;
					}
				}
				
				if (uniStormSystem.minuteCounter <= 1 && isWaitingUsingSun)
				{
					isWaitingUsingSun = false;
				}
				
				if (uniStormSystem.minuteCounter >= 59 && canGrowUsingSun)
				{
					canGrowUsingSun = false;
				}
			
		}
	}

	void UseTemperature () 
	{
		if (!isFullyGrown)
		{
			if (!isWaitingUsingTemp && !canGrowUsingTemp)
			{
				if (uniStormSystem.temperature >= temperatureNeededToGrow)
				{
					canGrowUsingTemp = true;
				}
			}
			
			if (canGrowUsingTemp && !isWaitingUsingTemp && uniStormSystem.minuteCounter >= 59)
			{
				CheckSize();

				tempRoll = Random.Range(useTemperature,11);

				if (tempRoll == useTemperature)
				{
					transform.localScale = new Vector3(transform.localScale.x + growthAmountX, transform.localScale.y + growthAmountY, transform.localScale.z + growthAmountZ);
					isWaitingUsingTemp = true;
					canGrowUsingTemp = false;
					tempRoll = 0;
				}
			}
			
			if (uniStormSystem.minuteCounter <= 1 && isWaitingUsingTemp)
			{
				isWaitingUsingTemp = false;
			}
			
			if (uniStormSystem.minuteCounter >= 59 && canGrowUsingTemp)
			{
				canGrowUsingTemp = false;
			}
			
		}
	}


	void UsePrecipitation () 
	{
		if (!isFullyGrown)
		{
			if (!isWaitingUsingPrec && !canGrowUsingPrec)
			{
				if (uniStormSystem.currentRainIntensity >= rainNeededToGrow)
				{
					canGrowUsingPrec = true;
				}
			}

			if (canGrowUsingPrec && !isWaitingUsingPrec && uniStormSystem.minuteCounter >= 59)
			{
				CheckSize();

				precRoll = Random.Range(usePrecipitation,11);

				if (precRoll == usePrecipitation)
				{
					transform.localScale = new Vector3(transform.localScale.x + growthAmountX, transform.localScale.y + growthAmountY, transform.localScale.z + growthAmountZ);
					isWaitingUsingPrec = true;
					canGrowUsingPrec = false;
					precRoll = 0;
				}
			}

			if (uniStormSystem.minuteCounter <= 1 && isWaitingUsingPrec)
			{
				isWaitingUsingPrec = false;
			}

			if (uniStormSystem.minuteCounter >= 59 && canGrowUsingPrec)
			{
				canGrowUsingPrec = false;
			}

		}
	}

	void CheckUniStorm ()
	{
		if (temperatureInhibitor == 0)
		{
			currentTemperature = uniStormSystem.temperature;

			if (currentTemperature < minTemperature)
			{
				isTemperatureInhibited = true;
			}

			if (currentTemperature >= minTemperature)
			{
				isTemperatureInhibited = false;
			}
		}

		if (seasonInhibitor == 0)
		{
			isSeasonallyInhibited = uniStormSystem.isSpring;

			if (!uniStormSystem.isSpring)
			{
				isSeasonallyInhibited = false;
			}
		}

		if (seasonInhibitor == 1)
		{
			isSeasonallyInhibited = uniStormSystem.isSummer;

			if (!uniStormSystem.isSummer)
			{
				isSeasonallyInhibited = false;
			}
		}

		if (seasonInhibitor == 2)
		{
			isSeasonallyInhibited = uniStormSystem.isFall;

			if (!uniStormSystem.isFall)
			{
				isSeasonallyInhibited = false;
			}
		}

		if (seasonInhibitor == 3)
		{
			isSeasonallyInhibited = uniStormSystem.isWinter;

			if (!uniStormSystem.isWinter)
			{
				isSeasonallyInhibited = false;
			}
		}
	}

	void CheckSize ()
	{
		CheckUniStorm();

		if (transform.localScale.x >= maxGrowthAmountX)
		{
			transform.localScale = new Vector3(maxGrowthAmountX, transform.localScale.y, transform.localScale.z);
		}

		if (transform.localScale.y >= maxGrowthAmountY)
		{
			transform.localScale = new Vector3(transform.localScale.x, maxGrowthAmountY, transform.localScale.z);
		}

		if (transform.localScale.z >= maxGrowthAmountZ)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, maxGrowthAmountZ);
		}

		if (!isFullyGrown)
		{
			if (transform.localScale.x >= maxGrowthAmountX && transform.localScale.y >= maxGrowthAmountY && transform.localScale.z >= maxGrowthAmountZ)
			{
				isFullyGrown = true;
			}
		}
	}
}
