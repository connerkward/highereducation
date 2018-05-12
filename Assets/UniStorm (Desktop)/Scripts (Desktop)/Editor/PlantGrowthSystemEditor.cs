using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(PlantGrowthSystem))] 
public class PlantGrowthSystemEditor : Editor 
{
	enum UsePrecipitation
	{
		_10 = 1,
		_20 = 2,
		_30 = 3,
		_40 = 4,
		_50 = 5,
		_60 = 6,
		_70 = 7,
		_80 = 8,
		_90 = 9,
		Always = 10,
		Never = 11
	}

	enum UseSunlight
	{
		_10 = 1,
		_20 = 2,
		_30 = 3,
		_40 = 4,
		_50 = 5,
		_60 = 6,
		_70 = 7,
		_80 = 8,
		_90 = 9,
		Always = 10,
		Never = 11
	}

	enum UseTemperature
	{
		_10 = 1,
		_20 = 2,
		_30 = 3,
		_40 = 4,
		_50 = 5,
		_60 = 6,
		_70 = 7,
		_80 = 8,
		_90 = 9,
		Always = 10,
		Never = 11
	}

	/*
	enum UpdateTime
	{
		Hourly = 0,
		Daily = 1
	}
	*/

	enum SeasonInhibitor
	{
		Spring = 0,
		Summer = 1,
		Fall = 2,
		Winter = 3,
		None = 4
	}

	enum TemperatureInhibitor
	{
		Yes = 0,
		No = 1
	}

	UsePrecipitation editorUsePrecipitation = UsePrecipitation._30;
	UseSunlight editorUseSunlight = UseSunlight._30;
	UseTemperature editorUseTemperature = UseTemperature._30;

	//UpdateTime editorUpdateTime = UpdateTime.Hourly;

	SeasonInhibitor editorSeasonInhibitor = SeasonInhibitor.Winter;
	TemperatureInhibitor editorTemperatureInhibitor = TemperatureInhibitor.Yes;

	public override void OnInspectorGUI () 
	{

		PlantGrowthSystem self = (PlantGrowthSystem)target;

		EditorGUILayout.LabelField("Plant Growth System", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("By: Black Horizon Studios", EditorStyles.label);
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		//EditorGUILayout.LabelField("Growth Options", EditorStyles.boldLabel);

		//EditorGUILayout.HelpBox("Here you can control how your plants will grow according to UniStorm's conditions. There are 3 options to choose from. If desired, all 3 can be used simultaneously.", MessageType.None, true);

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		/*
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Update Time controls how often this plant will grow according to UniStorm's time. Hourly, will allow the plant to grow every Hour, if the proper conditions are met. Daily, will allow the plant to grow once every Day, if the proper conditions are met.", MessageType.None, true);

		editorUpdateTime = (UpdateTime)self.UpdateTime;
		editorUpdateTime = (UpdateTime)EditorGUILayout.EnumPopup("Update Time", editorUpdateTime);
		self.UpdateTime = (int)editorUpdateTime;

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		*/

		EditorGUILayout.LabelField("Grow From Precipitation", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("Grow From Precipitation will allow your plants to grow once the Rain Needed to Grow (UniStorm's current Rain Intensity) has been met. The system will then roll to see whether or not your plant will grow. You can control the odds below from a percent, to never, or to always. This process will only happen once per UniStorm Hour.", MessageType.None, true);

		editorUsePrecipitation = (UsePrecipitation)self.usePrecipitation;
		editorUsePrecipitation = (UsePrecipitation)EditorGUILayout.EnumPopup("Growth Chance", editorUsePrecipitation);
		self.usePrecipitation = (int)editorUsePrecipitation;

		if (self.usePrecipitation != 11)
		{
			EditorGUILayout.HelpBox("You plant has a " + self.usePrecipitation + "0% chance of growing when this condition is met.", MessageType.None, true);
		}

		if (self.usePrecipitation == 11)
		{
			EditorGUILayout.HelpBox("You plant will not use this condition and it is disabled.", MessageType.None, true);
		}

		EditorGUILayout.Space();

		if (self.usePrecipitation != 11)
		{
			self.rainNeededToGrow = EditorGUILayout.IntField ("Rain Needed to Grow", self.rainNeededToGrow);
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();


		EditorGUILayout.LabelField("Grow From Temperature", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("Grow From Temperature will allow your plants to grow once the Temperature Needed to Grow (UniStorm's current Temperature) has been met. The system will then roll to see whether or not your plant will grow. You can control the odds below from a percent, to never, or to always. This process will only happen once per UniStorm Hour.", MessageType.None, true);

		editorUseTemperature = (UseTemperature)self.useTemperature;
		editorUseTemperature = (UseTemperature)EditorGUILayout.EnumPopup("Growth Chance", editorUseTemperature);
		self.useTemperature = (int)editorUseTemperature;

		if (self.useTemperature != 11)
		{
			EditorGUILayout.HelpBox("You plant has a " + self.useTemperature + "0% chance of growing when this condition is met.", MessageType.None, true);
		}
		
		if (self.useTemperature == 11)
		{
			EditorGUILayout.HelpBox("You plant will not use this condition and it is disabled.", MessageType.None, true);
		}
		
		EditorGUILayout.Space();
		
		if (self.useTemperature != 11)
		{
			self.temperatureNeededToGrow = EditorGUILayout.IntField ("Temp Needed to Grow", self.temperatureNeededToGrow);
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Grow From Sunlight Intensity", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("Grow From Temperature will allow your plants to grow once the Sunlight Intensity Needed to Grow (UniStorm's current Sunlight Intensity) has been met. The system will then roll to see whether or not your plant will grow. You can control the odds below from a percent, to never, or to always. This process will only happen once per UniStorm Hour.", MessageType.None, true);

		editorUseSunlight = (UseSunlight)self.useSunLight;
		editorUseSunlight = (UseSunlight)EditorGUILayout.EnumPopup("Growth Chance", editorUseSunlight);
		self.useSunLight = (int)editorUseSunlight;

		if (self.useSunLight != 11)
		{
			EditorGUILayout.HelpBox("You plant has a " + self.useSunLight + "0% chance of growing when this condition is met.", MessageType.None, true);
		}
		
		if (self.useSunLight == 11)
		{
			EditorGUILayout.HelpBox("You plant will not use this condition and it is disabled.", MessageType.None, true);
		}
		
		EditorGUILayout.Space();
		
		if (self.useSunLight != 11)
		{
			self.sunLightNeededToGrow = EditorGUILayout.Slider ("Sunlight Needed to Grow", self.sunLightNeededToGrow, 0.1f, 1.0f);
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();



		EditorGUILayout.LabelField("Growth Amounts", EditorStyles.boldLabel);

		EditorGUILayout.HelpBox("Here you can choose how much your plant will grow for each axis. Once the Max Growth Amount has been reached, your plant will no longer grow in that axis.", MessageType.None, true);

		EditorGUILayout.Space();

		Vector3 temp = EditorGUILayout.Vector3Field ("Growth Amount", new Vector3 (self.growthAmountX, self.growthAmountY, self.growthAmountZ));
		self.growthAmountX = temp.x;
		self.growthAmountY = temp.y;
		self.growthAmountZ = temp.z;

		EditorGUILayout.Space();

		Vector3 temp2 = EditorGUILayout.Vector3Field ("Max Growth Amount", new Vector3 (self.maxGrowthAmountX, self.maxGrowthAmountY, self.maxGrowthAmountZ));
		self.maxGrowthAmountX = temp2.x;
		self.maxGrowthAmountY = temp2.y;
		self.maxGrowthAmountZ = temp2.z;

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Inhibiting Options", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("Inhibiting Options controls what conditions stop your plant from growing such as a certain season or a below a certain temperature. Inhibiting Options will affect all Growth Options for each Inhibitor that is enabled stoppping the plant from growing until that condition is passed. For example: If the season is Winter, and the Inhibitor for Winter is enabled, the plant will no longer grow even though the Sunlight Intensity has been reached. Once Winter is over, the plant will continue to grow as it should when the Sunlight condition has bee met.", MessageType.None, true);

		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Season Inhibitor will stop this plant from growing when UniStorm reaches the assigned Season.", MessageType.None, true);

		editorSeasonInhibitor = (SeasonInhibitor)self.seasonInhibitor;
		editorSeasonInhibitor = (SeasonInhibitor)EditorGUILayout.EnumPopup("Season Inhibitor", editorSeasonInhibitor);
		self.seasonInhibitor = (int)editorSeasonInhibitor;

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Temperature Inhibitor will stop this plant from growing if the temperature falls below the Min Temperature.", MessageType.None, true);

		EditorGUILayout.Space();

		editorTemperatureInhibitor = (TemperatureInhibitor)self.temperatureInhibitor;
		editorTemperatureInhibitor = (TemperatureInhibitor)EditorGUILayout.EnumPopup("Temperature Inhibited", editorTemperatureInhibitor);
		self.temperatureInhibitor = (int)editorTemperatureInhibitor;

		if (self.temperatureInhibitor == 0)
		{
			self.minTemperature = EditorGUILayout.IntField ("Min Temperature", self.minTemperature);
		}

	}

}
