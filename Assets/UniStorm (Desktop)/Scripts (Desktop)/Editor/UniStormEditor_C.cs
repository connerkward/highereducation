//UniStorm Weather System Editor C# Version 2.4.1 @ Copyright
//Black Horizon Studios

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEditor.SceneManagement;

[System.Serializable]
[CustomEditor(typeof(UniStormWeatherSystem_C))] 
public class UniStormEditor_C : Editor {
	enum AmbientSource{
		Skybox = 1, Gradient = 2, Color = 3
	}

	enum PrecipitationType{
		VisualBarsAndSliders = 1, LineGraph = 2
	}
	
	enum MonthDropDown{
		January = 1,February = 2,March = 3,April = 4,May = 5,June = 6,July = 7,August = 8,September = 9,October = 10,November = 11,December = 12
	}
	
	enum WeatherTypeDropDown{
		Foggy = 1, LightRainOrLightSnowWinterOnly = 2, ThunderStormOrSnowStormWinterOnly = 3, PartlyCloudy = 4, MostlyClear = 7,Sunny = 8,  LightningBugsSummerOnly = 10,MostlyCloudy = 11, HeavyRainNoThunder = 12,  FallingLeavesFallOnly = 13
	}
	
	enum MoonPhaseDropDown{
		NewMoon = 1,WaxingCresent = 2,FirstQuarter = 3,WaxingGibbous = 4,FullMoon = 5,WaningGibbous = 6,ThirdQuater = 7,WaningCresent = 8
	}
	
	enum FogModeDropDown{
		linear = 1,exponential = 2,exp2 = 3
	}
	
	enum DayShadowTypeDropDown{
		Hard = 1,Soft = 2
	}
	
	enum NightShadowTypeDropDown{
		Hard = 1,Soft = 2
	}
	
	enum LightningShadowTypeDropDown{
		Hard = 1,Soft = 2
	}
	
	enum TemperatureDropDown{
		Fahrenheit = 1,Celsius = 2
	}

	enum TemperatureControlDropDown{
		Simple = 1,Advanced = 2
	}

	enum CalendarDropDown{
		Standard = 1,Custom = 2
	}

	enum GenerateDateAndTime{
		Yes = 1,No = 2
	}

	enum DayHourDropDown{
		_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
	}

	enum NightHourDropDown{
		_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
	}

	enum NightMDropDown{
		_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
	}

	enum DayMDropDown{
		_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
	}

	enum StartTimeNew{
		_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
	}

	enum AmbientIntensityDropDown{
		Automatically = 1,
		Manually = 2
	}

	enum ProceduralLightningControl{
		Enabled = 1, Disabled = 2
	}
	
	bool showAdvancedOptions = true;
	bool confirmationToGenerate = false;

	WeatherTypeDropDown editorWeatherType = WeatherTypeDropDown.PartlyCloudy;
	MonthDropDown editorMonth = MonthDropDown.January;
	MoonPhaseDropDown editorMoonPhase = MoonPhaseDropDown.FullMoon;
	FogModeDropDown editorFogMode = FogModeDropDown.linear;
	DayShadowTypeDropDown editorDayShadowType = DayShadowTypeDropDown.Hard;
	NightShadowTypeDropDown editorNightShadowType = NightShadowTypeDropDown.Hard;
	LightningShadowTypeDropDown editorLightningShadowType = LightningShadowTypeDropDown.Hard;
	TemperatureDropDown editorTemperature = TemperatureDropDown.Fahrenheit;
	TemperatureControlDropDown editorTemperatureControl = TemperatureControlDropDown.Simple;
	CalendarDropDown editorCalendarType = CalendarDropDown.Standard;
	DayHourDropDown editorDayHour = DayHourDropDown._6;
	NightHourDropDown editorNightHour = NightHourDropDown._18;
	StartTimeNew editorStartTimeNew = StartTimeNew._12;
	GenerateDateAndTime editorGenerateDateAndTime = GenerateDateAndTime.Yes;
	AmbientIntensityDropDown editorAmbientIntensity = AmbientIntensityDropDown.Manually;
	PrecipitationType editorPrecipitationType = PrecipitationType.VisualBarsAndSliders;
	AmbientSource editorAmbientSource = AmbientSource.Color;
	ProceduralLightningControl editorProceduralLightningControl = ProceduralLightningControl.Enabled;

	Dictionary<string, string> SaveUniStormColorSettings = new Dictionary<string, string>();
	Dictionary<string, string> LoadedUniStormColorSettings = new Dictionary<string, string>();

	SerializedProperty TabNumberProp;
	SerializedProperty DocumentationProp;
	SerializedProperty EventTabNumberProp;
	SerializedObject GetTarget;
	SerializedProperty t;
	UnityEvent NewEvent;
	System.DateTime NewDate;

	public string[] EventTabName = new string[] {"Time Event", "Weather Event (Coming Soon)"};
	public string[] TabName = new string[] {"Climate Options", "Time Options", "Weather Options", "Wind Options", "Atmosphere Options", "Fog Options", "Lightning Options", "Temperature Options", "Sun Options", "Moon Options", "Precipitation Options", "Cloud Options", "Sound Manager Options", "Lighting Options", "Object Options", "Import/Export Settings", "UniStorm Events (BETA)", "Documentation/Support"};

	SerializedProperty StarBrightness;

	void OnEnable () {
		TabNumberProp = serializedObject.FindProperty ("TabNumber");
		EventTabNumberProp = serializedObject.FindProperty ("EventTabNumber");
		StarBrightness = serializedObject.FindProperty("StarColorGradient");
	}

	public override void OnInspectorGUI () {
		UniStormWeatherSystem_C self = (UniStormWeatherSystem_C)target;

		serializedObject.Update ();

		SerializedProperty _UniStormEvents = serializedObject.FindProperty("TimeEvent");

		//Time Number Variables
		EditorGUILayout.LabelField("UniStorm Weather System", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("By: Black Horizon Studios", EditorStyles.label);
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Current Time: " + self.hourCounter + ":" + self.minuteCounter.ToString("00"), MessageType.None, true);
		EditorGUILayout.HelpBox("Date: " + self.monthCounter + "/" + self.dayCounter + "/" + self.yearCounter, MessageType.None, true);
		
		if (self.calendarType == 1){
			EditorGUILayout.HelpBox("Day of the Week: " + self.UniStormDate.DayOfWeek, MessageType.None, true);
		}
		
		EditorGUILayout.HelpBox("Current Weather: " + self.weatherString, MessageType.None, true);
		
		if (self.temperatureType == 1){
			EditorGUILayout.HelpBox("Current Temperature: " + self.temperature + " °F", MessageType.None, true);
		}
		
		if (self.temperatureType == 2){
			EditorGUILayout.HelpBox("Current Temperature: " + self.temperature + " °C", MessageType.None, true);
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		
		TabNumberProp.intValue = GUILayout.SelectionGrid (TabNumberProp.intValue, TabName, 2);

		EditorGUILayout.Space();
		EditorGUILayout.Space();


		if (TabNumberProp.intValue == 0 || confirmationToGenerate && TabNumberProp.intValue == 15){
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Generate Climate will randomize several UniStorm settings to generate a climate for you. This includeds Weather Odds, Min and Max Temperautes (as well as calculating your seasonal averages), Starting Time, Date, Starting Weather, Moon Phases, and more. This can be useful for testing randomized settings or even generating a climate for your games. The Presets all use real world data (excluding the Random Preset) to give you a well rounded generated climate of that type.", MessageType.None, true);
			}

			EditorGUILayout.HelpBox("Generating a new climate will change your current settings. This process cannot be undone. However, you can always reset to the default settings we used with our demos.", MessageType.Warning, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			editorGenerateDateAndTime = (GenerateDateAndTime)self.generateDateAndTime;
			editorGenerateDateAndTime = (GenerateDateAndTime)EditorGUILayout.EnumPopup("Generate Additional Factors?", editorGenerateDateAndTime);
			self.generateDateAndTime = (int)editorGenerateDateAndTime;

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Generate Additional Factors will randomly generate your Starting Time, Date, Weather, and Moon Phase. This is useful to test out various factors with UniStorm without have to set everything manually.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if(GUILayout.Button("Random")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = Random.Range(1, 14);
					self.moonPhaseCalculator = Random.Range(0, 9);
				}

				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(35, 45);
					self.maxSpringTemp = Random.Range(46, 60);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(70, 80);
					self.maxSummerTemp = Random.Range(81, 115);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(35, 45);
					self.maxFallTemp = Random.Range(46, 60);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(-25, 0);
					self.maxWinterTemp = Random.Range(1, 40);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(35, 45)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(46, 60)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(70, 80)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(81, 115)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(35, 45)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(46, 60)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(-25, 0)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(1, 40)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}

			EditorGUILayout.HelpBox("A Random Climate will generate a random climate with no real world data. Everything is completely randomized, while still being realistic. This can give you very unique results which you can then alter how you'd like.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if(GUILayout.Button("Rainforest")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = Random.Range(1, 14);
					self.moonPhaseCalculator = Random.Range(0, 9);
				}

				if (self.PrecipitationType == 1){
					self.precipitationOddsSpring = 80;
					self.precipitationOddsSummer = 80;
					self.precipitationOddsWinter = 80;
					self.precipitationOddsFall = 80;
				}

				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(75, 80);
					self.maxSpringTemp = Random.Range(80, 85);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(80, 85);
					self.maxSummerTemp = Random.Range(85, 93);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(75, 80);
					self.maxFallTemp = Random.Range(80, 85);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(68, 70);
					self.maxWinterTemp = Random.Range(70, 75);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(75, 80)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(80, 85)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(80, 85)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(85, 93)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(75, 80)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(80, 85)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(68, 70)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(70, 75)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}

			EditorGUILayout.HelpBox("The Rainforest Preset will generate a random Rainforest like Climate according to real world data.\n\nThe Rainforest climate consists of high odds of precipitation evenly distributed throughout the year. The yearly average temperature is relatively warm. It ralely exceeds 90° during the summer months and rarely falls below 68° during the winter.\n\nAfter your climate has been generated, you can tweak the settings to your liking.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if(GUILayout.Button("Desert")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = 7;
					self.moonPhaseCalculator = Random.Range(0, 9);
				}

				if (self.PrecipitationType == 1){
					self.precipitationOddsSpring = 10;
					self.precipitationOddsSummer = 5;
					self.precipitationOddsWinter = 10;
					self.precipitationOddsFall = 10;
				}

				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(70, 85);
					self.maxSpringTemp = Random.Range(85, 90);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(90, 95);
					self.maxSummerTemp = Random.Range(100, 120);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(70, 85);
					self.maxFallTemp = Random.Range(85, 90);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(0, 50);
					self.maxWinterTemp = Random.Range(50, 60);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(70, 85)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(90, 95)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(100, 120)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(70, 85)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(0, 50)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(50, 60)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}

			EditorGUILayout.HelpBox("The Desert Preset will generate a random Desert like Climate according to real world data.\n\nThe Desert climate consists of very low odds of precipitation throughout the year. The average temperature is very hot duirng the Summer, but can be very cold during the Winter. Temperatures can often exceed 100° during the summer months and fall as cold as 0° during the winter.\n\nAfter your climate has been generated, you can tweak the settings to your liking.", MessageType.None, true);

			EditorGUILayout.Space();

			if(GUILayout.Button("Mountainous")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = Random.Range(1, 14);
					self.moonPhaseCalculator = Random.Range(0, 9);
				}

				if (self.PrecipitationType == 1){
					self.precipitationOddsSpring = 60;
					self.precipitationOddsSummer = 60;
					self.precipitationOddsWinter = 60;
					self.precipitationOddsFall = 60;
				}

				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(45, 55);
					self.maxSpringTemp = Random.Range(55, 70);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(70, 90);
					self.maxSummerTemp = Random.Range(90, 96);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(40, 50);
					self.maxFallTemp = Random.Range(50, 65);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(-30, 10);
					self.maxWinterTemp = Random.Range(10, 30);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(45, 55)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(55, 70)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(70, 90)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(90, 96)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(40, 50)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(50, 65)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(-30, 10)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(10, 30)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}

			EditorGUILayout.HelpBox("The Mountainous Preset will generate a random Mountainous like Climate according to real world data. \n\nThe Mountainous climate consists of medium to high odds of precipitation throughout the year. The average temperature is relatively mild during the Summer and very cold during the Winter. Temperatures can rarely exceed 86° during the summer months and fall as cold as -22° during the winter.\n\nAfter your climate has been generated, you can tweak the settings to your liking.", MessageType.None, true);

			EditorGUILayout.Space();

			if(GUILayout.Button("Grassland")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = Random.Range(1, 14);
					self.moonPhaseCalculator = Random.Range(0, 9);
				}
				
				if (self.PrecipitationType == 1){
					self.precipitationOddsSpring = 60;
					self.precipitationOddsSummer = 60;
					self.precipitationOddsWinter = 20;
					self.precipitationOddsFall = 20;
				}
				
				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(50, 85);
					self.maxSpringTemp = Random.Range(85, 90);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(90, 95);
					self.maxSummerTemp = Random.Range(95, 115);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(50, 85);
					self.maxFallTemp = Random.Range(85, 90);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(30, 40);
					self.maxWinterTemp = Random.Range(40, 50);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(50, 85)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(90, 95)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(95, 115)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(50, 85)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(30, 40)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(40, 50)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}
			
			EditorGUILayout.HelpBox("The Grassland Preset will generate a random Grassland like Climate according to real world data. \n\nThe Grassland climate consists of medium odds of precipitation mainly in the Spring and Summer months. The average temperature is hot during the Summer and cold during the Winter. Temperatures can exceed 100° during the summer months and fall as cold as 30° during the winter.\n\nAfter your climate has been generated, you can tweak the settings to your liking.", MessageType.None, true);
			
			EditorGUILayout.Space();


			if(GUILayout.Button("Alien Climate")){
				if (self.generateDateAndTime == 1){
					self.startTimeHour = Random.Range(0, 24);
					self.startTimeMinute = Random.Range(0, 60);
					self.dayCounter = Random.Range(1, 30);
					self.monthCounter = Random.Range(1, 13);
					self.yearCounter = Random.Range(1, 3000);
					self.weatherForecaster = Random.Range(1, 14);
					self.moonPhaseCalculator = Random.Range(0, 9);
				}
				
				if (self.PrecipitationType == 1){
					self.precipitationOddsSpring = Random.Range(1,101);
					self.precipitationOddsSummer = Random.Range(1,101);
					self.precipitationOddsWinter = Random.Range(1,101);
					self.precipitationOddsFall = Random.Range(1,101);
				}

				self.lightningColor = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f);

				self.moonColor = new Color((Random.Range(100.0f, 150.0f))/255.0f, (Random.Range(100.0f, 150.0f))/255.0f, (Random.Range(100.0f, 150.0f))/255.0f);

				self.stormCloudColor1 = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				self.stormCloudColor2 = new Color((Random.Range(25.0f, 150.0f))/255.0f, (Random.Range(25.0f, 150.0f))/255.0f, (Random.Range(25.0f, 150.0f))/255.0f);

				self.cloudColorMorning = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, 1);
				self.cloudColorDay = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, 1);
				self.cloudColorEvening = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, 1);
				self.cloudColorNight = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, 1);

				if (self.AmbientSource == 1){
					self.MorningAmbientLight = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.MiddayAmbientLight = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.DuskAmbientLight = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.NightAmbientLight = new Color((Random.Range(0, 80.0f))/255.0f, (Random.Range(0, 80.0f))/255.0f, (Random.Range(0, 80.0f))/255.0f);
					self.TwilightAmbientLight = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				}

				if (self.AmbientSource == 2){
					self.SkyTwilightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
					self.SkyMorningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.SkyDayColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.SkyEveningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.SkyNightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);

					self.EquatorTwilightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
					self.EquatorMorningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.EquatorDayColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.EquatorEveningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.EquatorNightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);

					self.GroundTwilightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
					self.GroundMorningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.GroundDayColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.GroundEveningColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
					self.GroundNightColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				}

				self.SunMorning = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f);
				self.SunDay = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f);
				self.SunDusk = new Color((Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f, (Random.Range(200.0f, 255.0f))/255.0f);
				self.SunNight = Color.black;

				self.fogMorningColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				self.fogDayColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				self.fogDuskColor = new Color((Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f, (Random.Range(50.0f, 150.0f))/255.0f);
				self.fogNightColor = new Color((Random.Range(10.0f, 25.0f))/255.0f, (Random.Range(10.0f, 25.0f))/255.0f, (Random.Range(10.0f, 25.0f))/255.0f);

				self.stormyFogColorMorning = new Color((Random.Range(50.0f, 100.0f))/255.0f, (Random.Range(50.0f, 100.0f))/255.0f, (Random.Range(50.0f, 100.0f))/255.0f);
				self.stormyFogColorDay = new Color((Random.Range(50.0f, 125.0f))/255.0f, (Random.Range(50.0f, 125.0f))/255.0f, (Random.Range(50.0f, 125.0f))/255.0f);
				self.stormyFogColorEvening = new Color((Random.Range(50.0f, 100.0f))/255.0f, (Random.Range(50.0f, 100.0f))/255.0f, (Random.Range(50.0f, 100.0f))/255.0f);
				self.stormyFogColorNight = new Color((Random.Range(10.0f, 25.0f))/255.0f, (Random.Range(10.0f, 25.0f))/255.0f, (Random.Range(10.0f, 25.0f))/255.0f);

				self.MorningAtmosphericLight = new Color((Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f);
				self.MiddayAtmosphericLight = new Color((Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f);
				self.DuskAtmosphericLight = new Color((Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f, (Random.Range(100.0f, 255.0f))/255.0f);

				self.skyColorMorning = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
				self.skyColorDay = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
				self.skyColorEvening = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
				self.nightTintColor = new Color((Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f, (Random.Range(0, 255.0f))/255.0f);
				
				if (self.temperatureType == 1){
					self.minSpringTemp = Random.Range(-500, 100);
					self.maxSpringTemp = Random.Range(100, 500);
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = Random.Range(-500, 100);
					self.maxSummerTemp = Random.Range(100, 500);
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = Random.Range(-500, 100);
					self.maxFallTemp = Random.Range(100, 500);
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = Random.Range(-500, 100);
					self.maxWinterTemp = Random.Range(100, 500);
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((Random.Range(50, 85)) - 32) * 5/9;
					self.maxSpringTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingSpringTemp = (self.minSpringTemp + self.maxSpringTemp) / 2;
					
					self.minSummerTemp = ((Random.Range(90, 95)) - 32) * 5/9;
					self.maxSummerTemp = ((Random.Range(95, 115)) - 32) * 5/9;
					self.startingSummerTemp = (self.minSummerTemp + self.maxSummerTemp) / 2;
					
					self.minFallTemp = ((Random.Range(50, 85)) - 32) * 5/9;
					self.maxFallTemp = ((Random.Range(85, 90)) - 32) * 5/9;
					self.startingFallTemp = (self.minFallTemp + self.maxFallTemp) / 2;
					
					self.minWinterTemp = ((Random.Range(30, 40)) - 32) * 5/9;
					self.maxWinterTemp = ((Random.Range(40, 50)) - 32) * 5/9;
					self.startingWinterTemp = (self.minWinterTemp + self.maxWinterTemp) / 2;
				}
			}
			
			EditorGUILayout.HelpBox("The Alien Climate Preset is different than the other climate generator presets. \n\nThe Alien Climate Setting will generate a randomized alien-like climate with extreme temperatures, randomized color values, and other settings that may only be found on alien planets. Expect the settings to be a bit wild. We have ensured that certain colors have been eliminated such as black for truly unique and wild planets. We also have a default color setting that will reset the colors generated back to UniStorm's default colors if needed. \n\nAfter your climate has been generated, you can tweak the settings to your liking.", MessageType.None, true);

			EditorGUILayout.Space();

			if(GUILayout.Button("Reset Colors and Settings to Default")){
				
				self.lightningColor = new Color((164)/255.0f, (198)/255.0f, (223)/255.0f);
				
				self.moonColor = new Color((110)/255.0f, (128)/255.0f, (139)/255.0f);
				
				self.stormCloudColor1 = new Color((81)/255.0f, (81)/255.0f, (81)/255.0f);
				self.stormCloudColor2 = new Color((49)/255.0f, (49)/255.0f, (49)/255.0f);
				
				self.cloudColorMorning = new Color((171)/255.0f, (162)/255.0f, (152)/255.0f, 1);
				self.cloudColorDay = new Color((255)/255.0f, (255)/255.0f, (255)/255.0f, 1);
				self.cloudColorEvening = new Color((148)/255.0f, (141)/255.0f, (133)/255.0f, 1);
				self.cloudColorNight = new Color((212)/255.0f, (212)/255.0f, (212)/255.0f, 1);
				
				self.MorningAmbientLight = new Color((206)/255.0f, (187)/255.0f, (144)/255.0f);
				self.MiddayAmbientLight = new Color((151)/255.0f, (152)/255.0f, (143)/255.0f);
				self.DuskAmbientLight = new Color((234)/255.0f, (205)/255.0f, (172)/255.0f);
				self.NightAmbientLight = new Color((33)/255.0f, (38)/255.0f, (38)/255.0f);
				self.TwilightAmbientLight = new Color((127)/255.0f, (143)/255.0f, (150)/255.0f);
				
				self.SunMorning = new Color((235)/255.0f, (184)/255.0f, (0)/255.0f);
				self.SunDay = new Color((232)/255.0f, (229)/255.0f, (219)/255.0f);
				self.SunDusk = new Color((255)/255.0f, (184)/255.0f, (0)/255.0f);
				self.SunNight = Color.black;
				
				self.fogMorningColor = new Color((68)/255.0f, (54)/255.0f, (40)/255.0f);
				self.fogDayColor = new Color((162)/255.0f, (170)/255.0f, (176)/255.0f);
				self.fogDuskColor = new Color((73)/255.0f, (60)/255.0f, (45)/255.0f);
				self.fogNightColor = new Color((33)/255.0f, (33)/255.0f, (33)/255.0f);
				
				self.stormyFogColorMorning = new Color((82)/255.0f, (82)/255.0f, (82)/255.0f);
				self.stormyFogColorDay = new Color((120)/255.0f, (120)/255.0f, (120)/255.0f);
				self.stormyFogColorEvening = new Color((75)/255.0f, (75)/255.0f, (75)/255.0f);
				self.stormyFogColorNight = new Color((36)/255.0f, (36)/255.0f, (36)/255.0f);
				
				self.MorningAtmosphericLight = new Color((201)/255.0f, (136)/255.0f, (0)/255.0f);
				self.MiddayAtmosphericLight = new Color((231)/255.0f, (190)/255.0f, (102)/255.0f);
				self.DuskAtmosphericLight = new Color((191)/255.0f, (130)/255.0f, (0)/255.0f);
				
				self.skyColorMorning = new Color((255)/255.0f, (255)/255.0f, (255)/255.0f);
				self.skyColorDay = new Color((195)/255.0f, (169)/255.0f, (141)/255.0f);
				self.skyColorEvening = new Color((231)/255.0f, (234)/255.0f, (234)/255.0f);
				self.nightTintColor = new Color((25)/255.0f, (60)/255.0f, (38)/255.0f);
				
				self.startTimeHour = 11;
				self.startTimeMinute = 0;
				self.dayCounter = 15;
				self.monthCounter = 6;
				self.yearCounter = 2015;
				self.weatherForecaster = 4;
				self.moonPhaseCalculator = 3;
				self.weatherChanceSpring = 60;
				self.weatherChanceSummer = 20;
				self.weatherChanceFall = 40;
				self.weatherChanceWinter = 80;
				
				if (self.temperatureType == 1){
					self.minSpringTemp = 45;
					self.maxSpringTemp = 65;
					self.minSummerTemp = 70;;
					self.maxSummerTemp = 100;
					self.minFallTemp = 35;
					self.maxFallTemp = 55;
					self.minWinterTemp = 0;
					self.maxWinterTemp = 40;
					
					self.startingSpringTemp = 55;
					self.startingSummerTemp = 85;
					self.startingFallTemp = 45;
					self.startingWinterTemp = 30;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((45) - 32) * 5/9;
					self.maxSpringTemp = ((65) - 32) * 5/9;
					self.minSummerTemp = ((70) - 32) * 5/9;
					self.maxSummerTemp = ((100) - 32) * 5/9;
					self.minFallTemp = ((35) - 32) * 5/9;
					self.maxFallTemp = ((55) - 32) * 5/9;
					self.minWinterTemp = ((0) - 32) * 5/9;
					self.maxWinterTemp = ((40) - 32) * 5/9;
					
					self.startingSpringTemp = ((55) - 32) * 5/9;
					self.startingSummerTemp = ((85) - 32) * 5/9;
					self.startingFallTemp = ((45) - 32) * 5/9;
					self.startingWinterTemp = ((30) - 32) * 5/9;
				}
			}


			EditorGUILayout.Space();

			if(GUILayout.Button("Reset Climate to Default settings")){
				self.startTimeHour = 11;
				self.startTimeMinute = 0;
				self.dayCounter = 15;
				self.monthCounter = 6;
				self.yearCounter = 2015;
				self.weatherForecaster = 4;
				self.moonPhaseCalculator = 3;
				self.precipitationOddsSpring = 60;
				self.precipitationOddsSummer = 20;
				self.precipitationOddsFall = 40;
				self.precipitationOddsWinter = 80;

				if (self.temperatureType == 1){
					self.minSpringTemp = 45;
					self.maxSpringTemp = 65;
					self.minSummerTemp = 70;;
					self.maxSummerTemp = 100;
					self.minFallTemp = 35;
					self.maxFallTemp = 55;
					self.minWinterTemp = 0;
					self.maxWinterTemp = 40;
					
					self.startingSpringTemp = 55;
					self.startingSummerTemp = 85;
					self.startingFallTemp = 45;
					self.startingWinterTemp = 30;
				}
				
				if (self.temperatureType == 2){
					self.minSpringTemp = ((45) - 32) * 5/9;
					self.maxSpringTemp = ((65) - 32) * 5/9;
					self.minSummerTemp = ((70) - 32) * 5/9;
					self.maxSummerTemp = ((100) - 32) * 5/9;
					self.minFallTemp = ((35) - 32) * 5/9;
					self.maxFallTemp = ((55) - 32) * 5/9;
					self.minWinterTemp = ((0) - 32) * 5/9;
					self.maxWinterTemp = ((40) - 32) * 5/9;
					
					self.startingSpringTemp = ((55) - 32) * 5/9;
					self.startingSummerTemp = ((85) - 32) * 5/9;
					self.startingFallTemp = ((45) - 32) * 5/9;
					self.startingWinterTemp = ((30) - 32) * 5/9;
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (self.weatherForecaster == 5 || self.weatherForecaster == 6 || self.weatherForecaster == 9){
			self.weatherForecaster = Random.Range(1, 14);
		}

		if (TabNumberProp.intValue == 1){
			EditorGUILayout.LabelField("Time Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The current UniStorm time is displayed with these variables. Setting the Starting Time will start UniStorm at that specific time of day according to the Hour and Minute. Time variables can be used to create events, quests, and effects at specific times.", MessageType.None, true);
			}
			
			editorStartTimeNew = (StartTimeNew)self.startTimeHour;
			editorStartTimeNew = (StartTimeNew)EditorGUILayout.EnumPopup("Start Time Hour", editorStartTimeNew);
			self.startTimeHour = (int)editorStartTimeNew;

			if (self.startTimeMinute <= 9){
				EditorGUILayout.LabelField("Your day will start at " + self.startTimeHour + ":0" + self.startTimeMinute, EditorStyles.miniButton); //objectFieldThumb
			}

			if (self.startTimeMinute >= 10){
				EditorGUILayout.LabelField("Your day will start at " + self.startTimeHour + ":" + self.startTimeMinute, EditorStyles.miniButton); //objectFieldThumb
			}

			self.startTimeMinute = EditorGUILayout.IntSlider ("Start Time Minute", self.startTimeMinute, 0, 59);

			
			EditorGUILayout.Space();

			self.minuteCounter = EditorGUILayout.IntField ("Minutes", self.minuteCounter);
			
			self.hourCounter = EditorGUILayout.IntField ("Hours", self.hourCounter);
			
			self.dayCounter = EditorGUILayout.IntField ("Days", self.dayCounter);
			
			if (self.calendarType == 1){	
				editorMonth = (MonthDropDown)self.monthCounter;
				editorMonth = (MonthDropDown)EditorGUILayout.EnumPopup("Month", editorMonth);
				self.monthCounter = (int)editorMonth;
			}

			if (self.calendarType == 2){
				EditorGUILayout.Space();	
				EditorGUILayout.HelpBox("While Custom Calendar is enabled, UniStorm will display numbers for months.", MessageType.Warning, true);
				
				self.monthCounter = EditorGUILayout.IntField ("Months", self.monthCounter);
				
				EditorGUILayout.Space();
			}
			
			self.yearCounter = EditorGUILayout.IntField ("Years", self.yearCounter);
			
			EditorGUILayout.Space();

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Day Legnth Hour determins what time UniStorm will start using the Day Length time. This allows you to make your days longer or short than nights, if desired.", MessageType.None, true);
			}

			editorDayHour = (DayHourDropDown)self.dayLengthHour;
			editorDayHour = (DayHourDropDown)EditorGUILayout.EnumPopup("Day Length Hour", editorDayHour);
			self.dayLengthHour = (int)editorDayHour;

			if (self.dayLengthHour > self.nightLengthHour || self.dayLengthHour == self.nightLengthHour){
				EditorGUILayout.HelpBox("Your Starting Day Hour can't be higher than, or equal to, your Starting Night Hour.", MessageType.Warning, true);
			}

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Your in-game Day will start at " + self.dayLengthHour + ":00", EditorStyles.miniButton); //objectFieldThumb

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Day Length is calculated by how many real-time minutes pass until UniStorm switches to night, based on the hour you've set for Starting Night Hour. A value of 60 would give you 1 hour long days. This can be changed to any value that's desired.", MessageType.None, true);
			}

			self.dayLength = EditorGUILayout.FloatField ("Day Length", self.dayLength); 

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Night Length Hour determins what time UniStorm will start using the Night Length time. This allows you to make your nights longer or short than days, if desired.", MessageType.None, true);
			}

			editorNightHour = (NightHourDropDown)self.nightLengthHour;
			editorNightHour = (NightHourDropDown)EditorGUILayout.EnumPopup("Night Legnth Hour", editorNightHour);
			self.nightLengthHour = (int)editorNightHour;

			if (self.nightLengthHour < self.dayLengthHour){
				EditorGUILayout.HelpBox("Your Starting Night Hour can't be lower than, or equal to, your Starting Day Hour.", MessageType.Warning, true);
			}

			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Your in-game Night will start at " + self.nightLengthHour + ":00", EditorStyles.miniButton); //objectFieldThumb
			
			EditorGUILayout.Space();

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Night Length is calculated by how many real-time minutes pass until UniStorm switches to day, based on the hour you've set for Starting Day Hour. A value of 60 would give you 1 hour long nights. This can be changed to any value that's desired.", MessageType.None, true);
			}

			self.nightLength = EditorGUILayout.FloatField ("Night Length", self.nightLength); 

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Time Stopped will stop UniStorm's time and sun from moving, but will allow the clouds to keep animating.", MessageType.None, true);
			}
			
			self.timeStopped = EditorGUILayout.Toggle ("Time Stopped",self.timeStopped);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			editorCalendarType = (CalendarDropDown)self.calendarType;
			editorCalendarType = (CalendarDropDown)EditorGUILayout.EnumPopup("Calendar Type", editorCalendarType);
			self.calendarType = (int)editorCalendarType;
			
			if (self.calendarType == 1){
				if (self.helpOptions == true){	
					EditorGUILayout.Space();
					
					EditorGUILayout.HelpBox("While the Calendar Type is set to Standard, UniStorm will have standard calendar calculations. This includes the automatic calculation of Leap Year.", MessageType.None, true);
				}
			}
			
			if (self.calendarType == 2){	
				EditorGUILayout.Space();
				
				self.numberOfDaysInMonth = EditorGUILayout.IntField ("Days In Month", self.numberOfDaysInMonth);
				self.numberOfMonthsInYear = EditorGUILayout.IntField ("Months In Year", self.numberOfMonthsInYear);
				
				if (self.helpOptions == true){	
					EditorGUILayout.Space();
					
					EditorGUILayout.HelpBox("While the Calendar Type is set to Custom, UniStorm will choose the values you set within the Editor to calculate Days, Months, and Years. The Month will be changed and listed as a number value.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}


		if (TabNumberProp.intValue == 2){
			EditorGUILayout.LabelField("Weather Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Weather Options allow you to control weather. The Weather Type allows you to manually change UniStorm's weather to any weather that's listed in the drop down menu. This can be used for starting weather or be changed while testing out your scene.", MessageType.None, true);
			}

			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();


			
			editorWeatherType = (WeatherTypeDropDown)self.weatherForecaster;
			editorWeatherType = (WeatherTypeDropDown)EditorGUILayout.EnumPopup("Weather Type", editorWeatherType);
			self.weatherForecaster = (int)editorWeatherType;

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Static Weather will stop the weather from ever changing making it static. However, you can still change it manually.", MessageType.None, true);
			}

			self.staticWeather = EditorGUILayout.Toggle ("Static Weather",self.staticWeather);

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("If Instant Starting Weather is enabled, weather will be instantly faded in on start bypassing the transitioning of weather. This function can also be called to bypass weather transitions for instance, loading a player's game or an event.", MessageType.None, true);
			}

			self.useInstantStartingWeather = EditorGUILayout.Toggle ("Instant Starting Weather", self.useInstantStartingWeather);

			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Precipitation Odds", EditorStyles.label);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("There are 2 options for adjusting your Precipitation Odds using the Generation Type. The Visual Bars and Sliders option allows you to easily see and adjust the odds for each season. The Line Graph option allows you to use a line graph to choose the precise percentage for each month throughout the year. The line graph allows you to smoothly trantion between months and seasons giving you the most reaslitic results. The Line Graph setting is intented for more advanced users.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			editorPrecipitationType = (PrecipitationType)self.PrecipitationType;
			editorPrecipitationType = (PrecipitationType)EditorGUILayout.EnumPopup("Generation Type", editorPrecipitationType);
			self.PrecipitationType = (int)editorPrecipitationType;

			if (self.PrecipitationType == 2){
				if (self.helpOptions == true){
					EditorGUILayout.HelpBox("Setting weather odds via the line graph gives you precise control over each month. The line graph consists of a Y value of 0 through 100. This Y value represents the percentage of precipitation chance. If you have a Y value of 10, there would be a 10% chance precipitation at this point. The X value represents each month from 1-12. This graph will seamlessly transition throughout the year. This advanced control allows weather odds to fade in between each month and allows you to make fronts or even follow real-world data from other graphs. This setting is meant for advanced users.", MessageType.None, true);
				}

				self.PrecipitationGraph = EditorGUILayout.CurveField("Percentage Curve", self.PrecipitationGraph);

				EditorGUILayout.HelpBox("Note: Climate Generation doesn't generate random precipitation odds while using the Percentage Curve.", MessageType.Info, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.PrecipitationType == 1){
				if (self.helpOptions == true){
					EditorGUILayout.HelpBox("You can adjust the Precipitation Odds for each season using the sliders below. Weather will be generated based on the values for each season. The sliders determine the odds (in percent) for both precipitation and clear weather. The higher the Precipitation Odds, the more chance of precipitation. The lower the Precipitation Odds, the lower chance of precipitation. The bars below visually represent the odds for each season. UniStorm will generate its weather based off the percentages below.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("This slider adjusts the odds of having precipitation for Spring.", MessageType.None, true);
				self.precipitationOddsSpring = EditorGUILayout.IntSlider ("Spring Precipitation Odds", self.precipitationOddsSpring, 0, 100);

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Chance for precipitation weather types during Spring", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(170,200,210,125);
				ProgressBar (self.precipitationOddsSpring / 100.0f, self.precipitationOddsSpring + "%");
				GUI.backgroundColor = Color.white;

				EditorGUILayout.LabelField("Chance of clear weather types during Spring", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(255,255,90,200);
				ProgressBar ((100.0f - self.precipitationOddsSpring) / 100.0f, 100.0f - self.precipitationOddsSpring + "%");
				GUI.backgroundColor = Color.white;
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("This slider adjusts the odds of having precipitation for Summer.", MessageType.None, true);
				self.precipitationOddsSummer = EditorGUILayout.IntSlider ("Summer Precipitation Odds", self.precipitationOddsSummer, 0, 100);

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Chance for precipitation weather types during Summer", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(170,200,210,125);
				ProgressBar (self.precipitationOddsSummer / 100.0f, self.precipitationOddsSummer + "%");
				GUI.backgroundColor = Color.white;

				EditorGUILayout.LabelField("Chance for clear weather types during Summer", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(255,255,90,200);
				ProgressBar ((100.0f - self.precipitationOddsSummer) / 100.0f, 100.0f - self.precipitationOddsSummer + "%");
				GUI.backgroundColor = Color.white;

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("This slider adjusts the odds of having precipitation for Fall.", MessageType.None, true);
				self.precipitationOddsFall = EditorGUILayout.IntSlider ("Fall Precipitation Odds", self.precipitationOddsFall, 0, 100);

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Chance for precipitation weather types during Fall", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(170,200,210,125);
				ProgressBar (self.precipitationOddsFall / 100.0f, self.precipitationOddsFall + "%");
				GUI.backgroundColor = Color.white;

				EditorGUILayout.LabelField("Chance for clear weather types during Fall", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(255,255,90,200);
				ProgressBar ((100.0f - self.precipitationOddsFall) / 100.0f, 100.0f - self.precipitationOddsFall + "%");
				GUI.backgroundColor = Color.white;

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("This slider adjusts the odds of having precipitation for Winter.", MessageType.None, true);
				self.precipitationOddsWinter = EditorGUILayout.IntSlider ("Winter Precipitation Odds", self.precipitationOddsWinter, 0, 100);

				EditorGUILayout.Space();
				
				EditorGUILayout.LabelField("Chance for precipitation weather types during Winter", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(170,200,210,125);
				ProgressBar (self.precipitationOddsWinter / 100.0f, self.precipitationOddsWinter + "%");
				GUI.backgroundColor = Color.white;
				
				EditorGUILayout.LabelField("Chance for clear weather types during Winter", EditorStyles.miniButton);
				GUI.backgroundColor = new Color32(255,255,90,200);
				ProgressBar ((100.0f - self.precipitationOddsWinter) / 100.0f, 100.0f - self.precipitationOddsWinter + "%");
				GUI.backgroundColor = Color.white;
			}

			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Mist Particle Color", EditorStyles.label);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Mist Particle Color controls the color of UniStorm's mist for rain and snow for each time of day.", MessageType.None, true);
			}

			self.particleMistColorTwilight = EditorGUILayout.ColorField("Mist Color Twilight", self.particleMistColorTwilight);
			self.particleMistColorMorning = EditorGUILayout.ColorField("Mist Color Morning", self.particleMistColorMorning);
			self.particleMistColorDay = EditorGUILayout.ColorField("Mist Color Day", self.particleMistColorDay);
			self.particleMistColorEvening = EditorGUILayout.ColorField("Mist Color Evening", self.particleMistColorEvening);
			self.particleMistColorNight = EditorGUILayout.ColorField("Mist Color Night", self.particleMistColorNight);

			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Rain Particle Color", EditorStyles.label);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Rain Particle Color controls the color of UniStorm's rain and rain splashes for each time of day.", MessageType.None, true);
			}
			
			self.rainColorTwilight = EditorGUILayout.ColorField("Rain Color Twilight", self.rainColorTwilight);
			self.rainColorMorning = EditorGUILayout.ColorField("Rain Color Morning", self.rainColorMorning);
			self.rainColorDay = EditorGUILayout.ColorField("Rain Color Day", self.rainColorDay);
			self.rainColorEvening = EditorGUILayout.ColorField("Rain Color Evening", self.rainColorEvening);
			self.rainColorNight = EditorGUILayout.ColorField("Rain Color Night", self.rainColorNight);
			
			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Snow Particle Color", EditorStyles.label);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Rain Particle Color controls the color of UniStorm's rain and rain splashes for each time of day.", MessageType.None, true);
			}
			
			self.snowColorTwilight = EditorGUILayout.ColorField("Snow Color Twilight", self.snowColorTwilight);
			self.snowColorMorning = EditorGUILayout.ColorField("Snow Color Morning", self.snowColorMorning);
			self.snowColorDay = EditorGUILayout.ColorField("Snow Color Day", self.snowColorDay);
			self.snowColorEvening = EditorGUILayout.ColorField("Snow Color Evening", self.snowColorEvening);
			self.snowColorNight = EditorGUILayout.ColorField("Snow Color Night", self.snowColorNight);
			
			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Weather Fade Multiplier", EditorStyles.label);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Below you can adjust the speed for fading in and out particle effects, sound effects, fog, and more. This affects the transition from clear weather types to precipitation weather types and vise versa.", MessageType.None, true);
			}

			self.RegularCloudFadeInMultiplier = EditorGUILayout.Slider ("Regular Cloud Fade In", self.RegularCloudFadeInMultiplier, 0.1f, 25.0f);
			self.RegularCloudFadeOutMultiplier = EditorGUILayout.Slider ("Regular Cloud Fade Out", self.RegularCloudFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();

			self.CloudFadeInMultiplier = EditorGUILayout.Slider ("Storm Cloud Fade In", self.CloudFadeInMultiplier, 0.1f, 25.0f);
			self.CloudFadeOutMultiplier = EditorGUILayout.Slider ("Storm Cloud Fade Out", self.CloudFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			
			self.ParticleEffectsFadeInMultiplier = EditorGUILayout.Slider ("Particle Effects Fade In", self.ParticleEffectsFadeInMultiplier, 0.1f, 25.0f);
			self.ParticleEffectsFadeOutMultiplier = EditorGUILayout.Slider ("Particle Effects Fade Out", self.ParticleEffectsFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();

			self.SoundFadeInMultiplier = EditorGUILayout.Slider ("Sound Fade In", self.SoundFadeInMultiplier, 0.1f, 25.0f);
			self.SoundFadeOutMultiplier = EditorGUILayout.Slider ("Sound Fade Out", self.SoundFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			
			self.FogDistanceFadeInMultiplier = EditorGUILayout.Slider ("Fog Distance Fade In", self.FogDistanceFadeInMultiplier, 0.1f, 25.0f);
			self.FogDistanceFadeOutMultiplier = EditorGUILayout.Slider ("Fog Distance Fade Out", self.FogDistanceFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			
			self.FogColorFadeOutMultiplier = EditorGUILayout.Slider ("Fog Color Fade In", self.FogColorFadeOutMultiplier, 0.1f, 25.0f);
			self.FogColorFadeInMultiplier = EditorGUILayout.Slider ("Fog Color Fade Out", self.FogColorFadeInMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			
			self.EffectsFadeInMultiplier = EditorGUILayout.Slider ("Light Fade In", self.EffectsFadeInMultiplier, 0.1f, 25.0f);
			self.EffectsFadeOutMultiplier = EditorGUILayout.Slider ("Light Fade Out", self.EffectsFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			
			self.WindFadeInMultiplier = EditorGUILayout.Slider ("Wind Fade In", self.WindFadeInMultiplier, 0.1f, 25.0f);
			self.WindFadeOutMultiplier = EditorGUILayout.Slider ("Wind Fade Out", self.WindFadeOutMultiplier, 0.1f, 25.0f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			#if CTS_PRESENT
			EditorGUILayout.LabelField("CTS Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("Complete Terrain Shader (CTS) has been detected and is automatically enabled with UniStorm. If you'd like to disable this feature, you can set 'Use CTS' to false.", MessageType.None, true);
			EditorGUILayout.HelpBox("CTS allows UniStorm to have all sorts of cool weather and season related features. This includes dynamic snow build-up, rain wetness, seasonal terrain tinting, and more. If you plan on using the dynamic snow feature, ensure that you have a CTS Profile with snow setup similar to CTS's Landscape Demo CTS Profile.", MessageType.None, true);

			self.UseCTS = EditorGUILayout.Toggle ("Use CTS?",self.UseCTS);

			if (self.UseCTS){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Rain", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("The Max Rain Wetness controls the max wetness shading CTS will use. The higher the value, the more reflective and shiny the terrain will become while raining. Note: This option only works with rainy weather types.", MessageType.None, true);
			self.MaxWetness = EditorGUILayout.Slider ("Max Rain Wetness", self.MaxWetness, 0.01f, 1.0f);

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("The Rain Buildup Speed controls how fast the rain shading will buildup after it starts raining.", MessageType.None, true);
			self.RainBuildUpSpeed = EditorGUILayout.IntSlider ("Rain Buildup Speed", self.RainBuildUpSpeed, 1, 100);

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("The Rain Dry Speed controls how fast the rain shading will dissipate after it has stopped raining.", MessageType.None, true);
			self.RainDrySpeed = EditorGUILayout.IntSlider ("Rain Dry Speed", self.RainDrySpeed, 1, 100);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Snow", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("The Max Snow Amount controls the max snow shading CTS will use. The higher the value, the more dense the snow shading will become on the terrain while it's snowing. Note: This option only works with snowy weather types. Snow will start to melt after the Snow Melting Temperature has been met, unless it's disabled.", MessageType.None, true);
			self.MaxSnowAmount = EditorGUILayout.Slider ("Max Snow Amount", self.MaxSnowAmount, 0.01f, 1.0f);

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("The Snow Buildup Speed controls how fast the snow shading will buildup after it starts snowing.", MessageType.None, true);
			self.SnowBuildUpSpeed = EditorGUILayout.IntSlider ("Snow Buildup Speed", self.SnowBuildUpSpeed, 1, 100);

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("The Snow Melt Speed controls how fast the snow shading will dissipate after it has stopped snowing and the melting temperature has been met, if it's enabled.", MessageType.None, true);
			self.SnowMeltSpeed = EditorGUILayout.IntSlider ("Snow Melt Speed", self.SnowMeltSpeed, 1, 100);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("The Snow Melting Temperature controls the temperature that the CTS snow will melt. This allows users to have snow all winter long until it melts in the spring or when there's a warmer period during the wintery months. If you don't want this feature enabled, you can use a snow melting temperature of -1000.", MessageType.None, true);
			self.SnowMeltingTemperature = EditorGUILayout.IntField ("Snow Melting Temperature", self.SnowMeltingTemperature);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Tint Color", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("The color fields below control CTS terrain tint color for each season (Spring, Summer, Fall, and Winter). This allows the possibility for having spring greener than summer and fall while winter can be more grey. Season calculation is handled automatically and uses UniStorm's current date. It will be updated each day throughout the year.", MessageType.None, true);
			self.SpringTint = EditorGUILayout.ColorField("Spring Tint", self.SpringTint);
			self.SummerTint = EditorGUILayout.ColorField("Summer Tint", self.SummerTint);
			self.FallTint = EditorGUILayout.ColorField("Fall Tint", self.FallTint);
			self.WinterTint = EditorGUILayout.ColorField("Winter Tint", self.WinterTint);
			}
			#endif

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}
		
		if (TabNumberProp.intValue == 3){
			EditorGUILayout.LabelField("Wind Options", EditorStyles.boldLabel);
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Here you can adjust the wind settings for the terrain's grass. UniStorm will use the normal wind settings during none precipitation weather types and will slowly transition into stormy wind during precipitation weather types.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			
			self.normalGrassWavingAmount = EditorGUILayout.Slider ("Normal Grass Wind Speed", self.normalGrassWavingAmount, 0.1f, 1.0f);
			self.stormGrassWavingAmount = EditorGUILayout.Slider ("Stormy Grass Wind Speed", self.stormGrassWavingAmount, 0.1f, 1.0f);
			
			EditorGUILayout.Space();
			
			
			self.normalGrassWavingSpeed = EditorGUILayout.Slider ("Normal Grass Wind Size", self.normalGrassWavingSpeed, 0.1f, 1.0f);
			self.stormGrassWavingSpeed = EditorGUILayout.Slider ("Stormy Grass Wind Size", self.stormGrassWavingSpeed, 0.1f, 1.0f);
			
			EditorGUILayout.Space();
			
			self.normalGrassWavingStrength = EditorGUILayout.Slider ("Normal Grass Wind Bending", self.normalGrassWavingStrength, 0.1f, 1.0f);
			self.stormGrassWavingStrength = EditorGUILayout.Slider ("Stormy Grass Wind Bending", self.stormGrassWavingStrength, 0.1f, 1.0f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("With Using Multiple Terrains enabled, UniStorm will switch to the nexly activated terrain for controlling the wind. One is used at a time for performance reasons.", MessageType.None, true);
			}
			
			self.usingMultipleTerrains = EditorGUILayout.Toggle ("Using Multiple Terrains?",self.usingMultipleTerrains);
			
			EditorGUILayout.Space();

			EditorGUILayout.Space();
			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 4){
			EditorGUILayout.LabelField("Atmosphere Options", EditorStyles.boldLabel);			
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("UniStorm now uses a Physically Based Skybox shader. This shader allows you to adjust factors of the atmosphere that affect the color of the sky which changes according to the angle of the Sun.", MessageType.None, true);
			}

			self.skyColorMorning = EditorGUILayout.ColorField("Sky Tint Color Morning", self.skyColorMorning);
			self.skyColorDay = EditorGUILayout.ColorField("Sky Tint Color Day", self.skyColorDay);
			self.skyColorEvening = EditorGUILayout.ColorField("Sky Tint Color Evening", self.skyColorEvening);

			EditorGUILayout.Space();

			self.nightTintColor = EditorGUILayout.ColorField("Sky Tint Color Night", self.nightTintColor);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Sky Tint Color Night allows you to adjust the color of the sky when it's night. Note: This color option also affects the overall tint of the Procedural Skybox. Darker colors tend to work best.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			
			self.groundColor = EditorGUILayout.ColorField("Ground Color", self.groundColor);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Here you can adjust the Skybox Tint and Ground colors. The procedural skybox shader will accurately shade according to the time of day and angle of the sun.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			self.atmosphereThickness = EditorGUILayout.Slider ("Atmosphere Thickness", self.atmosphereThickness, 0.0f, 5.0f);
			
			EditorGUILayout.Space();
			
			self.exposure = EditorGUILayout.Slider ("Exposure", self.exposure, 0.0f, 8.0f);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Here you can adjust the Atmosphere Thickness and Exposure. These values allow you to control how thick the atosphere is and how much light is scattered.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.PropertyField(StarBrightness);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Star Color Gradient allows you to adjust the color and how bright your stars will shine. The alpha amount controls when your stars will fade in and out. By default, they fade out right before the sun rises and start fading in right after the sun sets.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			self.starSpeed = EditorGUILayout.IntSlider ("Star Scroll Speed", self.starSpeed, 0, 10);

			self.starRotationSpeed = EditorGUILayout.Slider ("Star Rotation Speed", self.starRotationSpeed, 0f, 10f);

			EditorGUILayout.HelpBox("The Star Speeds both control how fast the stars move in the sky. Scroll Speed controls how fast the stars scroll in the sky where Rotation Speed controls how fast they rotate.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}
		
		
		if (TabNumberProp.intValue == 5){
			EditorGUILayout.LabelField("Fog Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Fog Options allow you to control all densities of Unity's fog. Unity has 3 fog modes; Linear, Exponential, and Exp2. UniStorm will adjust the options according to the fog mode you've selected. Auto Enable Fog will enable Unity's Fog automatically if the check box is checked.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			
			self.useUnityFog = EditorGUILayout.Toggle ("Auto Enable Fog?",self.useUnityFog);
			
			EditorGUILayout.Space();
			
			editorFogMode = (FogModeDropDown)self.fogMode;
			editorFogMode = (FogModeDropDown)EditorGUILayout.EnumPopup("Fog Mode", editorFogMode);
			self.fogMode = (int)editorFogMode; 
			
			EditorGUILayout.Space();
			
			if (self.fogMode == 1){
				self.stormyFogDistanceStart = EditorGUILayout.IntSlider ("Stormy Fog Start Distance", self.stormyFogDistanceStart, -400, 1000);
				self.stormyFogDistance = EditorGUILayout.IntSlider ("Stormy Fog End Distance", self.stormyFogDistance, 200, 2500);
				self.fogStartDistance = EditorGUILayout.IntSlider ("Regular Fog Start Distance", self.fogStartDistance, -400, 1000);
				self.fogEndDistance = EditorGUILayout.IntSlider ("Regular Fog End Distance", self.fogEndDistance, 200, 5000);
			}
			
			if (self.fogMode == 2 || self.fogMode == 3){
				self.fogDensity = EditorGUILayout.FloatField ("Fog Density", self.fogDensity);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("These fog colors allow you to control the fog color for non-precipitation fog color for each time of day.", MessageType.None, true);
			}

			//Normal Fog Color
			self.fogTwilightColor = EditorGUILayout.ColorField("Fog Twilight", self.fogTwilightColor);
			self.fogMorningColor = EditorGUILayout.ColorField("Fog Morning", self.fogMorningColor);
			self.fogDayColor = EditorGUILayout.ColorField("Fog Day", self.fogDayColor);
			self.fogDuskColor = EditorGUILayout.ColorField("Fog Evening", self.fogDuskColor);
			self.fogNightColor = EditorGUILayout.ColorField("Fog Night", self.fogNightColor);

			//Storm Fog Color
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("These fog colors allow you to control the fog color for precipitation (stormy) fog color for each time of day.", MessageType.None, true);
			}

			self.stormyFogColorTwilight = EditorGUILayout.ColorField("Stormy Fog Twilight", self.stormyFogColorTwilight);
			self.stormyFogColorMorning = EditorGUILayout.ColorField("Stormy Fog Morning", self.stormyFogColorMorning);
			self.stormyFogColorDay = EditorGUILayout.ColorField("Stormy Fog Day", self.stormyFogColorDay);
			self.stormyFogColorEvening = EditorGUILayout.ColorField("Stormy Fog Evening", self.stormyFogColorEvening);
			self.stormyFogColorNight = EditorGUILayout.ColorField("Stormy Fog Night", self.stormyFogColorNight);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 6){
			EditorGUILayout.LabelField("Lightning Options", EditorStyles.boldLabel);
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("These settings allow you to adjust any lightning and thunder related options. These features will only happen during the Thunder Storm weather type.", MessageType.None, true);
			}
			
			self.shadowsLightning = EditorGUILayout.Toggle ("Shadows Enabled?",self.shadowsLightning);
			
			if (self.shadowsLightning){
				EditorGUILayout.Space();
				
				editorLightningShadowType = (LightningShadowTypeDropDown)self.lightningShadowType;
				editorLightningShadowType = (LightningShadowTypeDropDown)EditorGUILayout.EnumPopup("Shadow Type", editorLightningShadowType);
				self.lightningShadowType = (int)editorLightningShadowType;
				
				EditorGUILayout.Space();
				
				self.lightningShadowIntensity = EditorGUILayout.Slider ("Shadow Intensity", self.lightningShadowIntensity, 0, 1.0f);
				
				EditorGUILayout.Space();
			}
			
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The lighting intesity settings control the possible minimum and maximum light intensity of the lightning.", MessageType.None, true);
			}
			
			self.lightningColor = EditorGUILayout.ColorField("Lightning Color", self.lightningColor);
			
			EditorGUILayout.Space();
			
			self.minIntensity = EditorGUILayout.Slider ("Min Lightning Intensity", (float)self.minIntensity, 0.5f, 1.5f);
			self.maxIntensity = EditorGUILayout.Slider ("Max Lightning Intensity", self.maxIntensity, 0.5f, 1.5f);
			
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The minimum and maximum wait controls the seconds between each lightning strike.", MessageType.None, true);
			}
			
			self.lightningMinChance = EditorGUILayout.IntSlider ("Min Wait", (int)self.lightningMinChance, 1, 20);
			self.lightningMaxChance = EditorGUILayout.IntSlider ("Max Wait", (int)self.lightningMaxChance, 5, 40);
			
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Lightning Flash Length controls how long the lightning flash will last before fading out.", MessageType.None, true);
			}
			
			self.lightningFlashLength = EditorGUILayout.Slider ("Lightning Flash Length", self.lightningFlashLength, 0.1f, 1.2f);

			EditorGUILayout.Space();
			
			EditorGUILayout.HelpBox("The Lighnting Fade Speed controls how fast the lightning flash will fade out.", MessageType.None, true);
			
			self.lightningFadeSpeed = EditorGUILayout.IntSlider ("Lighnting Fade Speed", self.lightningFadeSpeed, 1, 10);
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			editorProceduralLightningControl = (ProceduralLightningControl)self.UseProceduralLightning;
			editorProceduralLightningControl = (ProceduralLightningControl)EditorGUILayout.EnumPopup("Procedural Lighting", editorProceduralLightningControl);
			self.UseProceduralLightning = (int)editorProceduralLightningControl;

			if (self.UseProceduralLightning == 1){
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Procedural Lightning Settings", EditorStyles.label);

				EditorGUILayout.HelpBox("Controls how intense the procedural lightning bolts will generated.", MessageType.None, true);

				self.ProceduralLightningMinIntensity = EditorGUILayout.IntSlider ("Min Intensity", self.ProceduralLightningMinIntensity, 5, 50);
				self.ProceduralLightningMaxIntensity = EditorGUILayout.IntSlider ("Max Intensity", self.ProceduralLightningMaxIntensity, 5, 200);
				EditorGUILayout.Space();

				#if UNITY_5_5_OR_NEWER
				EditorGUILayout.HelpBox("Controls how wide the procedural lightning bolts will be generated.", MessageType.None, true);
				
				self.ProceduralLightningMinWidth = EditorGUILayout.IntSlider ("Min Width", self.ProceduralLightningMinWidth, 5, 40);
				self.ProceduralLightningMaxWidth = EditorGUILayout.IntSlider ("Max Width", self.ProceduralLightningMaxWidth, 15, 60);
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("Controls the overall thickness that the procedural lightning bolts will generated.", MessageType.None, true);
				
				self.ProceduralLightningWidthMultiplier = EditorGUILayout.IntSlider ("Width Multiplier", self.ProceduralLightningWidthMultiplier, 1, 50);
				EditorGUILayout.Space();
				#endif

				EditorGUILayout.HelpBox("Controls the distance between each generated point of the procedural lightning bolts.", MessageType.None, true);
				
				self.ProceduralLightningMinSpaceBetweenPoints = EditorGUILayout.IntSlider ("Min Line Distance", self.ProceduralLightningMinSpaceBetweenPoints, 20, 100);
				self.ProceduralLightningMaxSpaceBetweenPoints = EditorGUILayout.IntSlider ("Max Line Distance", self.ProceduralLightningMaxSpaceBetweenPoints, 40, 200);
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("Controls the time (in milliseconds) how long it takes a procedural lightning bolt to reach its end point.", MessageType.None, true);
				
				self.ProceduralLightningMillisecondsBetweenLines = EditorGUILayout.IntSlider ("Time to Reach End Point", self.ProceduralLightningMillisecondsBetweenLines, 1, 50);
				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("Controls the strating and ending color of the procedural lightning bolts", MessageType.None, true);
				self.ProceduralLightningStartingLightningColor = EditorGUILayout.ColorField("Starting Color", self.ProceduralLightningStartingLightningColor);
				self.ProceduralLightningEndingLightningColor = EditorGUILayout.ColorField("Ending Color", self.ProceduralLightningEndingLightningColor);

				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("Controls the strike radius that the procedural lightning bolts will generated.", MessageType.None, true);
				
				self.ProceduralLightningStrikeRadius = EditorGUILayout.IntSlider ("Stike Radius", self.ProceduralLightningStrikeRadius, 500, 6000);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Thunder Sounds", EditorStyles.label);
			EditorGUILayout.HelpBox("Here you can add as many thunder sounds as needed. UniStorm will play them randomly with each lightning strike.", MessageType.None, true);

			EditorGUILayout.Space();

			//Thunder Sounds
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Add Thunder Sound")){
				self.ThunderSounds.Add (new AudioClip());
				
				EditorUtility.SetDirty(self);
				serializedObject.Update ();
			}
			
			if (self.ThunderSounds.Count > 0){
				if (GUILayout.Button("Remove Last Thunder Sound")){
					self.ThunderSounds.RemoveAt(self.ThunderSounds.Count-1);
					
					EditorUtility.SetDirty(self);
					serializedObject.Update ();
				}
			}
			GUILayout.EndHorizontal();
			
			EditorGUILayout.Space();
			if (self.ThunderSounds.Count == 0){
				EditorGUILayout.HelpBox("You currently have no Thunder Sounds. If you'd like to create one, press the Add Thunder Sound button.", MessageType.None, true);
			}
			EditorGUILayout.Space();
			
			if (self.ThunderSounds != null){
				for(int i = 0; i < self.ThunderSounds.Count; i++){
					self.ThunderSounds[i] = (AudioClip)EditorGUILayout.ObjectField("Thunder Sound " + i + ":" , self.ThunderSounds[i], typeof(AudioClip), true );
					GUILayout.Space(5);
				}
			}
			
			EditorGUILayout.Space();
			GUILayout.Space(5);


			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}
		
		
		if (TabNumberProp.intValue == 7){
			//Temperature Options
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Temperature Options", EditorStyles.boldLabel); 
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("With the Temperature Options you can see the current temperature and adjust many temperature related settings.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();

			self.disableTemperatureGeneration = EditorGUILayout.Toggle ("Disable Temperature",self.disableTemperatureGeneration);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Disable Temperature will disable the temperature from being generated. However, weather is still affected by temperature requiring it to be at or below freezing to snow.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (self.disableTemperatureGeneration){
				EditorGUILayout.HelpBox("Temperatures have been disabled.", MessageType.Warning, true);
			}

			if (!self.disableTemperatureGeneration){
				editorTemperature = (TemperatureDropDown)self.temperatureType;
				editorTemperature = (TemperatureDropDown)EditorGUILayout.EnumPopup("Temperature Unit", editorTemperature);
				self.temperatureType = (int)editorTemperature;
				
				if (self.temperatureType == 1){
					self.temperature = EditorGUILayout.IntField ("Current Temperature", self.temperature);

					EditorGUILayout.HelpBox("While using the Fahrenheit temperature type, UniStorm will snow at a temperature of 32 degrees or below.", MessageType.Info, true);
				}
				
				if (self.temperatureType == 2){
					self.temperature = EditorGUILayout.IntField ("Current Temperature", self.temperature);

					EditorGUILayout.HelpBox("While using the Celsuis temperature type, UniStorm will snow at a temperature of 0 degrees or below.", MessageType.Info, true);
				}

				EditorGUILayout.Space();

				editorTemperatureControl = (TemperatureControlDropDown)self.temperatureControlType;
				editorTemperatureControl = (TemperatureControlDropDown)EditorGUILayout.EnumPopup("Temperature Control Type", editorTemperatureControl);
				self.temperatureControlType = (int)editorTemperatureControl;

				EditorGUILayout.Space();

				if (self.temperatureControlType == 1){
					if (self.helpOptions == true){
						EditorGUILayout.HelpBox("Here you can adjust the minimum and maximum temperature for each month. UniStorm will generate realistic temperature fluctuations according to your minimum and maximums. This is done both hourly and daily.", MessageType.None, true);
					}
					
					self.startingSpringTemp = EditorGUILayout.IntField ("Starting Spring Temp", self.startingSpringTemp);
					self.minSpringTemp = EditorGUILayout.IntField ("Min Spring", self.minSpringTemp);
					self.maxSpringTemp = EditorGUILayout.IntField ("Max Spring", self.maxSpringTemp);
					EditorGUILayout.Space();
					
					self.startingSummerTemp = EditorGUILayout.IntField ("Starting Summer Temp", self.startingSummerTemp);
					self.minSummerTemp = EditorGUILayout.IntField ("Min Summer", self.minSummerTemp);
					self.maxSummerTemp = EditorGUILayout.IntField ("Max Summer", self.maxSummerTemp);
					EditorGUILayout.Space();
					
					self.startingFallTemp = EditorGUILayout.IntField ("Starting Fall Temp", self.startingFallTemp);
					self.minFallTemp = EditorGUILayout.IntField ("Min Fall", self.minFallTemp);
					self.maxFallTemp = EditorGUILayout.IntField ("Max Fall", self.maxFallTemp);
					EditorGUILayout.Space();
					
					self.startingWinterTemp = EditorGUILayout.IntField ("Starting Winter Temp", self.startingWinterTemp);
					self.minWinterTemp = EditorGUILayout.IntField ("Min Winter", self.minWinterTemp);
					self.maxWinterTemp = EditorGUILayout.IntField ("Max Winter", self.maxWinterTemp);
					

					EditorGUILayout.Space();
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}

				if (self.temperatureControlType == 2){
					if (self.helpOptions == true){
						EditorGUILayout.HelpBox("The Average Temperature Graph allows you to pick the average temperature each each month. The graph allows a smooth transition between each month and season.", MessageType.None, true);
					}

					self.TemperatureCurve = EditorGUILayout.CurveField("Average Temperature", self.TemperatureCurve);

					EditorGUILayout.Space();

					if (self.helpOptions == true){
						EditorGUILayout.HelpBox("Temperature Fluctuation Graph allows you to adjust the minimum and maximum temperature fluctuation for each UniStorm hour. The graph allows a smooth transition between each hour.", MessageType.None, true);
					}

					self.TemperatureFluctuationn = EditorGUILayout.CurveField("Temperature Fluctuation", self.TemperatureFluctuationn);

					EditorGUILayout.Space();
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
			}
		}

		if (TabNumberProp.intValue == 8){
			//Sun Intensity
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Sun Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("With the Sun Options you can control things like Sun Revolution, Sun Light and Sun Shadow Intensity, and whether or not you would like to enable or disable shadows for the Sun. Adjusting the Sun Revolution rotates the Sun's rising and setting posistions. You can rotate the Sun 360 degrees to perfectly suit your needs. The Sun Max Intensity is the max intesity the Sun will reach for the day. The Enable Shadows check box controls whether or not the Sun will use shadows.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();		
			EditorGUILayout.Space();

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sun Intensity Curve allows users to set the sun intensity via a line graph. This graph controls the brightness for each hour. This allows users to set the exact time of sunsets and sunrises, if needed.", MessageType.None, true);
			}

			self.sunIntensityCurve = EditorGUILayout.CurveField("Sun Intensity Curve", self.sunIntensityCurve);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			self.SunIntensityMultiplier = EditorGUILayout.Slider ("Sun Intensity", self.SunIntensityMultiplier, 0.1f, 3.0f);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sun Intensity allows users to control the sun intensity without having to modify the above graph.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			self.sunSize = EditorGUILayout.Slider ("Sun Size", self.sunSize, 0, 10f);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sun Size controls the size of UniStorm's sun.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			self.StormySunIntensity = EditorGUILayout.IntSlider ("Sun Intensity (Cloudy)", self.StormySunIntensity, 0, 100);

			EditorGUILayout.LabelField("Sun Intensity (Cloudy)", EditorStyles.miniButton);
			GUI.backgroundColor = new Color32(255,255,90,200);
			ProgressBar ((self.StormySunIntensity) / 100.0f, self.StormySunIntensity + "%");
			GUI.backgroundColor = Color.white;

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Sun Intensity (Cloudy) controls how much precentage of sunlight is allowed during precipitation weather types, including Foggy and Cloudy. This allows the sunlight to still be shinning when it's raining, snowing, cloudy, or foggy. Keeping the sunlight on, during precipitation weather types, can improve the shading of objects and the terrain.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			
			self.shadowsDuringDay = EditorGUILayout.Toggle ("Shadows Enabled?",self.shadowsDuringDay);
			
			if (self.shadowsDuringDay){
				EditorGUILayout.Space();
				
				editorDayShadowType = (DayShadowTypeDropDown)self.dayShadowType;
				editorDayShadowType = (DayShadowTypeDropDown)EditorGUILayout.EnumPopup("Shadow Type", editorDayShadowType);
				self.dayShadowType = (int)editorDayShadowType;
				
				EditorGUILayout.Space();
				
				self.dayShadowIntensity = EditorGUILayout.Slider ("Shadow Intensity", self.dayShadowIntensity, 0, 1.0f);
			}

			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sun Revolution controls the direction the sun will rise and set. For accurate sunrises and sunsets, you can set the Sun Revolution to -90.", MessageType.None, true);
			}

			self.sunRevolution = EditorGUILayout.IntSlider ("Sun Revolution", (int)self.sunRevolution, -180, 180);

			//Sun Colors
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sun colors allow you to adjust the color of UniStorm's sun for each time of day.", MessageType.None, true);
			}

			self.SunMorning = EditorGUILayout.ColorField("Sun Morning", self.SunMorning);
			self.SunDay = EditorGUILayout.ColorField("Sun Day", self.SunDay);
			self.SunDusk = EditorGUILayout.ColorField("Sun Evening", self.SunDusk);
			self.SunNight = EditorGUILayout.ColorField("Sun Night", self.SunNight);

			//Atmospheric Light Color
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			self.useSunShafts = EditorGUILayout.Toggle ("Use Sun Light Shafts?",self.useSunShafts);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Use Sun Light Shafts controls whether or not UniStorm will use light shafts with UniStorm's sun.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.useSunShafts){
				if (self.helpOptions == true){
					EditorGUILayout.HelpBox("The Sunshaft colors allow you to adjust the sunshaft colors for each time of day.", MessageType.None, true);
				}

				self.MorningAtmosphericLight = EditorGUILayout.ColorField("Sunshafts Morning", self.MorningAtmosphericLight);
				self.MiddayAtmosphericLight = EditorGUILayout.ColorField("Sunshafts Day", self.MiddayAtmosphericLight);
				self.DuskAtmosphericLight = EditorGUILayout.ColorField("Sunshafts Evening", self.DuskAtmosphericLight);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 9){
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Moon Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Moon Options allow you to choose the starting moon phase. There are a total of 8 moon phases that are updated each day. The moon phase will continue to cycle and starts with the moon phase you choose. You can change the materials of the moon pahases and UniStorm will cycle throught them accordingly.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			self.moonIntensityCurve = EditorGUILayout.CurveField("Moon Intensity Curve", self.moonIntensityCurve);

			EditorGUILayout.Space();

			self.StormyMoonLightIntensity = EditorGUILayout.IntSlider ("Moon Intensity (Cloudy)", self.StormyMoonLightIntensity, 0, 100);

			EditorGUILayout.LabelField("Moon Intensity (Cloudy)", EditorStyles.miniButton);
			GUI.backgroundColor = new Color32(255,255,90,200);
			ProgressBar ((self.StormyMoonLightIntensity) / 100.0f, self.StormyMoonLightIntensity + "%");
			GUI.backgroundColor = Color.white;

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Moon Intensity (Cloudy) controls how much precentage of moonlight is allowed during precipitation weather types, including Foggy and Cloudy. This allows the moonlight to still be shinning when it's raining, snowing, cloudy, or foggy. Keeping the moonlight on, during precipitation weather types, can improve the shading of objects and the terrain.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			self.moonColor = EditorGUILayout.ColorField("Moon Color", self.moonColor);

			EditorGUILayout.Space();

			self.shadowsDuringNight = EditorGUILayout.Toggle ("Shadows Enabled?",self.shadowsDuringNight);
			
			if (self.shadowsDuringNight){
				EditorGUILayout.Space();
				
				editorNightShadowType = (NightShadowTypeDropDown)self.nightShadowType;
				editorNightShadowType = (NightShadowTypeDropDown)EditorGUILayout.EnumPopup("Shadow Type", editorNightShadowType);
				self.nightShadowType = (int)editorNightShadowType;
				
				EditorGUILayout.Space();
				
				self.nightShadowIntensity = EditorGUILayout.Slider ("Shadow Intensity", self.nightShadowIntensity, 0, 1.0f);
			}
			
			EditorGUILayout.Space();

			self.useMoonLightShafts = EditorGUILayout.Toggle ("Use Moon Light Shafts?",self.useMoonLightShafts);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Use Moon Light Shafts controls whether or not UniStorm will use light shafts with UniStorm's moon.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			
			self.customMoonSize = EditorGUILayout.Toggle ("Customize Moon Size?",self.customMoonSize);
			
			EditorGUILayout.Space();
			
			if (self.customMoonSize){
				self.moonSize = EditorGUILayout.Slider ("Moon Size", self.moonSize, 1.0f, 15.0f);
				
				EditorGUILayout.Space();
				
				EditorGUILayout.HelpBox("The Moon's size can be adjust on a scale of 1 to 15.", MessageType.Info, true);
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
			}

			/*
			self.customMoonRotation = EditorGUILayout.Toggle ("Customize Moon Rotation?",self.customMoonRotation);
			
			if (self.customMoonRotation){
				self.moonRotationY = EditorGUILayout.Slider ("Moon Rotation", self.moonRotationY, 0, 360);
				
				EditorGUILayout.Space();
				
				EditorGUILayout.HelpBox("The Moon's rotation, on the Z Axis, can be adjust on a scale of 0 to 360. This will change the default setting rotation of 0 to whatever value you use on with slider. The Z Axis adjusts which direction the bright side of the moon faces. ", MessageType.Info, true);
				
				EditorGUILayout.Space();
			}
			*/
			
			EditorGUILayout.Space();
			
			editorMoonPhase = (MoonPhaseDropDown)self.moonPhaseCalculator;
			editorMoonPhase = (MoonPhaseDropDown)EditorGUILayout.EnumPopup("Moon Phase", editorMoonPhase);
			self.moonPhaseCalculator = (int)editorMoonPhase;

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}
		
		if (TabNumberProp.intValue == 10){
			//Weather Particle Slider Adjustments Rain
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Precipitation Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Precipitation Options allow you to set a max number for weather that uses particles. This is useful for keeping draw calls low and keeping the frame rate high. Each game is different so these options are completely customizable.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			
			self.randomizedPrecipitation = EditorGUILayout.Toggle ("Randomize Precipitation?",self.randomizedPrecipitation);
			
			EditorGUILayout.Space();
			
			if (self.randomizedPrecipitation){
				EditorGUILayout.HelpBox("Selecting Randomize Precipitation generates new precipitation maxes for every storm. While Randomize Precipitation is selected the maxes below are the caps of the max possible precipitation generation for that weather type.", MessageType.Info, true);
			}

			EditorGUILayout.Space();
			
			self.UseRainMist = EditorGUILayout.Toggle ("Use Mist?",self.UseRainMist);
			
			if (self.UseRainMist){
				EditorGUILayout.HelpBox("While Use Mist is enabled UniStorm will use the rain mist and snow dust particle effects to simulate windy rain and snow during the heavy precipitation weather types.", MessageType.Info, true);
			}
			
			EditorGUILayout.Space();
			
			self.UseRainSplashes = EditorGUILayout.Toggle ("Use Rain Splashes?",self.UseRainSplashes);
			
			if (self.UseRainSplashes){
				EditorGUILayout.HelpBox("When using Rain Splashes UniStorm will have splashes spawn where the rain collisions hit. This allows rain splashes to collide with objects and create splash effects.", MessageType.Info, true);

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				self.rainSplashSize = EditorGUILayout.FloatField ("Rain Splash Size", self.rainSplashSize);
				EditorGUILayout.HelpBox("The Rain Splash Size controls the size of the rain splashes when it rains.", MessageType.None, true);
			}
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("When using Precipitation Control, UniStorm will change the weather after the set amount of consecutive stormy days has been reached. This is helpful to help control (in rare cases) it raining or snowing for too long.", MessageType.Info, true);

			self.stormControl = EditorGUILayout.Toggle ("Use Precipitation Control?",self.stormControl);
			
			EditorGUILayout.Space();

			if (self.stormControl){
				EditorGUILayout.HelpBox("The number of consecutive days of precipitation that need to pass before UniStorm generates a new weather type.", MessageType.None, true);
				self.forceWeatherChange = EditorGUILayout.IntSlider ("Change Weather Days", (int)self.forceWeatherChange, 1, 7);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			self.rainNeededForLightning = EditorGUILayout.IntSlider ("Rain Needed for Lightning", self.rainNeededForLightning, 1, self.maxStormRainIntensity);
			EditorGUILayout.HelpBox("Rain Needed for Lightning controls the amount of rain that must be reached or exceeded for lightning to occur. For example, a setting of 100 would allow lightning to happen after the cuurent rain intensity has reach or exceeded 100.", MessageType.None, true);
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			self.maxLightRainIntensity = EditorGUILayout.IntSlider ("Light Rain Intensity", (int)self.maxLightRainIntensity, 1, 1000);
			self.maxLightRainMistCloudsIntensity = EditorGUILayout.IntSlider ("Light Rain Mist Intensity", (int)self.maxLightRainMistCloudsIntensity, 0, 6);
			self.maxStormRainIntensity = EditorGUILayout.IntSlider ("Heavy Rain Intensity", (int)self.maxStormRainIntensity, 1, 5000);
			self.maxHeavyRainMistIntensity = EditorGUILayout.IntSlider ("Heavy Rain Mist Intensity", (int)self.maxHeavyRainMistIntensity, 0, 50);
			
			//Weather Particle Slider Adjustments Snow
			self.maxLightSnowIntensity = EditorGUILayout.IntSlider ("Light Snow Intensity", (int)self.maxLightSnowIntensity, 1, 1000);
			self.maxLightSnowDustIntensity = EditorGUILayout.IntSlider ("Light Snow Dust Intensity", (int)self.maxLightSnowDustIntensity, 0, 20);
			self.maxSnowStormIntensity = EditorGUILayout.IntSlider ("Heavy Snow Intensity", (int)self.maxSnowStormIntensity, 1, 3000);
			self.maxHeavySnowDustIntensity = EditorGUILayout.IntSlider ("Heavy Snow Dust Intensity", (int)self.maxHeavySnowDustIntensity, 0, 50);
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			self.useCustomPrecipitationSounds = EditorGUILayout.Toggle ("Use Custom Sounds?",self.useCustomPrecipitationSounds);
			
			if (self.useCustomPrecipitationSounds){
				EditorGUILayout.Space();
				
				EditorGUILayout.HelpBox("While Use Custom Sounds is enabled UniStorm will use these sounds for the precipitation noises instead of UniStorm's default sounds. If the audio slots below are empty no sounds will play during precipitation weather types.", MessageType.Info, true);
				
				bool customRainSound = !EditorUtility.IsPersistent (self);
				self.customRainSound = (AudioClip)EditorGUILayout.ObjectField ("Rain Sound", self.customRainSound, typeof(AudioClip), customRainSound);
				
				bool customRainWindSound = !EditorUtility.IsPersistent (self);
				self.customRainWindSound = (AudioClip)EditorGUILayout.ObjectField ("Rain Wind Sound", self.customRainWindSound, typeof(AudioClip), customRainWindSound);
				
				bool customSnowWindSound = !EditorUtility.IsPersistent (self);
				self.customSnowWindSound = (AudioClip)EditorGUILayout.ObjectField ("Snow Wind Sound", self.customSnowWindSound, typeof(AudioClip), customSnowWindSound);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 11){
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Cloud Options", EditorStyles.boldLabel);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Cloud Options allow you to adjust all cloud related options, colors, and settings.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			self.CloudDensity = EditorGUILayout.IntSlider ("Cloud Density", self.CloudDensity, 0, 100);

			EditorGUILayout.LabelField("Cloud Desnsity", EditorStyles.miniButton);
			GUI.backgroundColor = new Color32(255,255,90,200);
			ProgressBar ((self.CloudDensity) / 100.0f, self.CloudDensity + "%");
			GUI.backgroundColor = Color.white;

			EditorGUILayout.HelpBox("The Cloud Desnsity controls the denisty of all non-precipitation clouds. The slider controls the percentage of density where 100% is full density and 0% is completely faded.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			self.CloudHeight = EditorGUILayout.Slider ("Cloud Height", self.CloudHeight, 3.0f, 10000.0f);

			EditorGUILayout.HelpBox("The Cloud Height controls the the height the clouds will render. The lower the value, the lower the clouds will render. If you are having issues seeing your clouds, adjust these settings to make sure they're the proper height for your scene.", MessageType.None, true);

			EditorGUILayout.Space();

			self.CloudFade = EditorGUILayout.Slider ("Cloud Height Fade", self.CloudFade, 1.0f, self.CloudHeight-1);

			EditorGUILayout.HelpBox("The Cloud Height Fade controls how gradual the Cloud Height will fade.", MessageType.None, true);

			EditorGUILayout.Space();

			self.cloudSpeed = EditorGUILayout.IntSlider ("Regular Cloud Speed", self.cloudSpeed, 0, 50);

			EditorGUILayout.HelpBox("The Regular Cloud Speed control how fast the clouds move in the sky non-precipitation clouds.", MessageType.None, true);

			EditorGUILayout.Space();

			self.heavyCloudSpeed = EditorGUILayout.IntSlider ("Storm Cloud Speed", self.heavyCloudSpeed, 0, 50);

			EditorGUILayout.HelpBox("The Storm Cloud Speed control how fast the clouds move in the sky for stormy clouds.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Cloud Color allows you to control the color of the regular non-precipitation clouds for each time of day.", MessageType.None, true);
			}

			self.cloudColorTwilight = EditorGUILayout.ColorField("Clouds Twilight", self.cloudColorTwilight);
			self.cloudColorMorning = EditorGUILayout.ColorField("Clouds Morning", self.cloudColorMorning);
			self.cloudColorDay = EditorGUILayout.ColorField("Clouds Day", self.cloudColorDay);
			self.cloudColorEvening = EditorGUILayout.ColorField("Clouds Evening", self.cloudColorEvening);
			self.cloudColorNight = EditorGUILayout.ColorField("Clouds Night", self.cloudColorNight);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Storm Cloud Color allows you to control the color of the stormy clouds that are used for precipitation weather types for each time of day. There are 2 layers of clouds.", MessageType.None, true);
			}

			EditorGUILayout.LabelField("Layer 1", EditorStyles.label);

			self.stormClouds1ColorTwilight = EditorGUILayout.ColorField("Storm Clouds 1 Twilight", self.stormClouds1ColorTwilight);
			self.stormClouds1ColorMorning = EditorGUILayout.ColorField("Storm Clouds 1 Morning", self.stormClouds1ColorMorning);
			self.stormClouds1ColorDay = EditorGUILayout.ColorField("Storm Clouds 1 Day", self.stormClouds1ColorDay);
			self.stormClouds1ColorEvening = EditorGUILayout.ColorField("Storm Clouds 1 Evening", self.stormClouds1ColorEvening);
			self.stormClouds1ColorNight = EditorGUILayout.ColorField("Storm Clouds 1 Night", self.stormClouds1ColorNight);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Layer 2", EditorStyles.label);

			self.stormClouds2ColorTwilight = EditorGUILayout.ColorField("Storm Clouds 2 Twilight", self.stormClouds2ColorTwilight);
			self.stormClouds2ColorMorning = EditorGUILayout.ColorField("Storm Clouds 2 Morning", self.stormClouds2ColorMorning);
			self.stormClouds2ColorDay = EditorGUILayout.ColorField("Storm Clouds 2 Day", self.stormClouds2ColorDay);
			self.stormClouds2ColorEvening = EditorGUILayout.ColorField("Storm Clouds 2 Evening", self.stormClouds2ColorEvening);
			self.stormClouds2ColorNight = EditorGUILayout.ColorField("Storm Clouds 2 Night", self.stormClouds2ColorNight);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		
		if (TabNumberProp.intValue == 12){
			//Sound Manager Options
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Sound Manager Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Sound Manager allows you to set an array of sounds that will play dynamically for each time of each day according to the min and max seconds set within the editor (One for morning, day, evening, and night) An example for this could be birds in the morning and evening, wind during the day, and crickets at night. UniStorm will pick from a selection of up to 20 sounds (for each time of day) that will play throughout the day and night. You can choose to enable or disable sounds for each time of day using the checkboxes below.", MessageType.None, true);
			}
			
			self.timeToWaitMin = EditorGUILayout.IntField ("Min Wait Time", self.timeToWaitMin);
			self.timeToWaitMax = EditorGUILayout.IntField ("Max Wait Time", self.timeToWaitMax);
			
			EditorGUILayout.Space();
			
			self.useMorningSounds = EditorGUILayout.Toggle ("Use Morning Sounds?",self.useMorningSounds);
			self.useDaySounds = EditorGUILayout.Toggle ("Use Day Sounds?",self.useDaySounds);
			self.useEveningSounds = EditorGUILayout.Toggle ("Use Evening Sounds?",self.useEveningSounds);
			self.useNightSounds = EditorGUILayout.Toggle ("Use Night Sounds?",self.useNightSounds);
			
			EditorGUILayout.Space();
			
			//Sound Manager Lists
			//Morning
			if (self.useMorningSounds){
				GUILayout.Space(10);
				self.MorningSoundsFoldOut = EditorGUILayout.Foldout(self.MorningSoundsFoldOut, "Morning Sounds");

				if (self.MorningSoundsFoldOut){
					GUILayout.Space(10);
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Morning Sound")){
						self.ambientSoundsMorning.Add (new AudioClip());
						
						EditorUtility.SetDirty(self);
						serializedObject.Update ();
						
					}

					if (self.ambientSoundsMorning.Count > 0){
						if (GUILayout.Button("Remove Last Morning Sound")){
							self.ambientSoundsMorning.RemoveAt(self.ambientSoundsMorning.Count-1);
							
							EditorUtility.SetDirty(self);
							serializedObject.Update ();
							
						}
					}
					GUILayout.EndHorizontal();
					
					EditorGUILayout.Space();
					if (self.ambientSoundsMorning.Count == 0){
						EditorGUILayout.HelpBox("You currently have no Morning Sounds. If you'd like to create one, press the Add Morning Sound button.", MessageType.None, true);
					}
					EditorGUILayout.Space();

					if (self.ambientSoundsMorning != null){
						for(int i = 0; i < self.ambientSoundsMorning.Count; i++){
							self.ambientSoundsMorning[i] = (AudioClip)EditorGUILayout.ObjectField("Morning Sound " + i + ":" , self.ambientSoundsMorning[i], typeof(AudioClip), true );
							GUILayout.Space(5);
						}
					}

					EditorGUILayout.Space();
					GUILayout.Space(5);
				}
			}

			GUILayout.Space(5);

			//Day
			if (self.useDaySounds){
				self.DaySoundsFoldOut = EditorGUILayout.Foldout(self.DaySoundsFoldOut, "Day Sounds");
				
				if (self.DaySoundsFoldOut){
					GUILayout.Space(10);
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Day Sound")){
						self.ambientSoundsDay.Add (new AudioClip());
						
						EditorUtility.SetDirty(self);
						serializedObject.Update ();
						
					}

					if (self.ambientSoundsDay.Count > 0){
						if (GUILayout.Button("Remove Last Day Sound")){
							self.ambientSoundsDay.RemoveAt(self.ambientSoundsDay.Count-1);
							
							EditorUtility.SetDirty(self);
							serializedObject.Update ();
							
						}
					}
					GUILayout.EndHorizontal();
					
					EditorGUILayout.Space();
					if (self.ambientSoundsDay.Count == 0){
						EditorGUILayout.HelpBox("You currently have no Day Sounds. If you'd like to create one, press the Add Day Sound button.", MessageType.None, true);
					}
					EditorGUILayout.Space();
					
					if (self.ambientSoundsDay != null){
						for(int i = 0; i < self.ambientSoundsDay.Count; i++){
							self.ambientSoundsDay[i] = (AudioClip)EditorGUILayout.ObjectField("Day Sound " + i + ":" , self.ambientSoundsDay[i], typeof(AudioClip), true );
							GUILayout.Space(5);
						}
					}
					
					EditorGUILayout.Space();
					GUILayout.Space(5);
				}
			}
			
			GUILayout.Space(5);
			
			//Evening
			if (self.useEveningSounds){
				self.EveningSoundsFoldOut = EditorGUILayout.Foldout(self.EveningSoundsFoldOut, "Evening Sounds");
				
				if (self.EveningSoundsFoldOut){
					GUILayout.Space(10);
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Evening Sound")){
						self.ambientSoundsEvening.Add (new AudioClip());
						
						EditorUtility.SetDirty(self);
						serializedObject.Update ();
						
					}

					if (self.ambientSoundsEvening.Count > 0){
						if (GUILayout.Button("Remove Last Evening Sound")){
							self.ambientSoundsEvening.RemoveAt(self.ambientSoundsEvening.Count-1);
							
							EditorUtility.SetDirty(self);
							serializedObject.Update ();
						}
					}
					GUILayout.EndHorizontal();
					
					EditorGUILayout.Space();
					if (self.ambientSoundsEvening.Count == 0){
						EditorGUILayout.HelpBox("You currently have no Evening Sounds. If you'd like to create one, press the Add Evening Sound button.", MessageType.None, true);
					}
					EditorGUILayout.Space();
					
					if (self.ambientSoundsEvening != null){
						for(int i = 0; i < self.ambientSoundsEvening.Count; i++){
							self.ambientSoundsEvening[i] = (AudioClip)EditorGUILayout.ObjectField("Evening Sound " + i + ":" , self.ambientSoundsEvening[i], typeof(AudioClip), true );
							GUILayout.Space(5);
						}
					}
					
					EditorGUILayout.Space();
					GUILayout.Space(5);
				}
			}

			GUILayout.Space(5);

			//Night
			if (self.useNightSounds){
				self.NightSoundsFoldOut = EditorGUILayout.Foldout(self.NightSoundsFoldOut, "Night Sounds");
				
				if (self.NightSoundsFoldOut){
					GUILayout.Space(10);
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Night Sound")){
						self.ambientSoundsNight.Add (new AudioClip());
						
						EditorUtility.SetDirty(self);
						serializedObject.Update ();
						
					}

					if (self.ambientSoundsNight.Count > 0){
						if (GUILayout.Button("Remove Last Night Sound")){
							self.ambientSoundsNight.RemoveAt(self.ambientSoundsNight.Count-1);
							
							EditorUtility.SetDirty(self);
							serializedObject.Update ();
						}
					}
					GUILayout.EndHorizontal();
					
					EditorGUILayout.Space();
					if (self.ambientSoundsNight.Count == 0){
						EditorGUILayout.HelpBox("You currently have no Night Sounds. If you'd like to create one, press the Add Night Sound button.", MessageType.None, true);
					}
					EditorGUILayout.Space();
					
					if (self.ambientSoundsNight != null){
						for(int i = 0; i < self.ambientSoundsNight.Count; i++){
							self.ambientSoundsNight[i] = (AudioClip)EditorGUILayout.ObjectField("Night Sound " + i + ":" , self.ambientSoundsNight[i], typeof(AudioClip), true );
							GUILayout.Space(5);
						}
					}
					
					EditorGUILayout.Space();
					GUILayout.Space(10);
				}
			}
		}

		if (TabNumberProp.intValue == 13){
			EditorGUILayout.LabelField("Lighting Options", EditorStyles.boldLabel);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Here you can adjust all Environment Lighting related settings such as intensities, colors, and more. Note: These light settings are different than the sun settings. The sun settings are found under Sun Options.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Ambient Source lets you pick the ambient source that the Environment Lighting will use.", MessageType.None, true);
			}

			editorAmbientSource = (AmbientSource)self.AmbientSource;
			editorAmbientSource = (AmbientSource)EditorGUILayout.EnumPopup("Ambient Source", editorAmbientSource);
			self.AmbientSource = (int)editorAmbientSource;

			if (self.AmbientSource == 3){
				EditorGUILayout.HelpBox("The Color Ambient Source simply uses a flat color for all ambient light in the scene. UniStorm will blend bewteen these colors for each time of day.", MessageType.None, true);
			}

			if (self.AmbientSource == 2){
				EditorGUILayout.HelpBox("The Gradient Ambient Source lets you choose colors separately for ambient light from the sky, horizon and ground and blends smoothly between them. UniStorm will blend bewteen the gradient colors for each time of day.", MessageType.None, true);
			}

			if (self.AmbientSource == 1){
				EditorGUILayout.HelpBox("The Skybox Ambient Source uses the colors of the skybox to determine the ambient light coming from different angles.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (self.AmbientSource == 2){
				self.SkyTwilightColor = EditorGUILayout.ColorField("Sky Twilight Color", self.SkyTwilightColor);
				self.SkyMorningColor = EditorGUILayout.ColorField("Sky Morning Color", self.SkyMorningColor);
				self.SkyDayColor = EditorGUILayout.ColorField("Sky Day Color", self.SkyDayColor);
				self.SkyEveningColor = EditorGUILayout.ColorField("Sky Evening Color", self.SkyEveningColor);
				self.SkyNightColor = EditorGUILayout.ColorField("Sky Night Color", self.SkyNightColor);

				EditorGUILayout.Space();

				self.EquatorTwilightColor = EditorGUILayout.ColorField("Equator Twilight Color", self.EquatorTwilightColor);
				self.EquatorMorningColor = EditorGUILayout.ColorField("Equator Morning Color", self.EquatorMorningColor);
				self.EquatorDayColor = EditorGUILayout.ColorField("Equator Day Color", self.EquatorDayColor);
				self.EquatorEveningColor = EditorGUILayout.ColorField("Equator Evening Color", self.EquatorEveningColor);
				self.EquatorNightColor = EditorGUILayout.ColorField("Equator Night Color", self.EquatorNightColor);

				EditorGUILayout.Space();

				self.GroundTwilightColor = EditorGUILayout.ColorField("Ground Twilight Color", self.GroundTwilightColor);
				self.GroundMorningColor = EditorGUILayout.ColorField("Ground Morning Color", self.GroundMorningColor);
				self.GroundDayColor = EditorGUILayout.ColorField("Ground Day Color", self.GroundDayColor);
				self.GroundEveningColor = EditorGUILayout.ColorField("Ground Evening Color", self.GroundEveningColor);
				self.GroundNightColor = EditorGUILayout.ColorField("Ground Night Color", self.GroundNightColor);
			}

			if (self.AmbientSource == 3) {
				self.TwilightAmbientLight = EditorGUILayout.ColorField ("Ambient Twilight", self.TwilightAmbientLight);
				self.MorningAmbientLight = EditorGUILayout.ColorField ("Ambient Morning", self.MorningAmbientLight);
				self.MiddayAmbientLight = EditorGUILayout.ColorField ("Ambient Day", self.MiddayAmbientLight);
				self.DuskAmbientLight = EditorGUILayout.ColorField ("Ambient Evening", self.DuskAmbientLight);
				self.NightAmbientLight = EditorGUILayout.ColorField ("Ambient Night", self.NightAmbientLight);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Reflection Intensity allows you to control the Relfection Intensity for each time of day.", MessageType.None, true);
			}

			self.ReflectionIntensityTwilight = EditorGUILayout.Slider ("Reflection Intensity Twilight", self.ReflectionIntensityTwilight, 0f, 1f);
			self.ReflectionIntensityMorning = EditorGUILayout.Slider ("Reflection Intensity Morning", self.ReflectionIntensityMorning, 0f, 1f);
			self.ReflectionIntensityDay = EditorGUILayout.Slider ("Reflection Intensity Day", self.ReflectionIntensityDay, 0f, 1f);
			self.ReflectionIntensityEvening = EditorGUILayout.Slider ("Reflection Intensity Evening", self.ReflectionIntensityEvening, 0f, 1f);
			self.ReflectionIntensityNight = EditorGUILayout.Slider ("Reflection Intensity Night", self.ReflectionIntensityNight, 0f, 1f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Below you can choose whether or not the Ambient Intensity is Automatically calculated or is adjusted manually. If adjusted manually, you will be able to choose the Ambient Intensity for each time of day.", MessageType.None, true);
			}

			editorAmbientIntensity = (AmbientIntensityDropDown)self.AutoCalculateAmbientIntensity;
			editorAmbientIntensity = (AmbientIntensityDropDown)EditorGUILayout.EnumPopup("Ambient Intensity", editorAmbientIntensity);
			self.AutoCalculateAmbientIntensity = (int)editorAmbientIntensity;

			EditorGUILayout.Space();

			if (self.AutoCalculateAmbientIntensity == 1){
				if (self.helpOptions == true){
					EditorGUILayout.HelpBox("Below you can adjust the a value between 0.1 and 1.0. This allows you to adjust the threshold of the automatically calculated Ambient Intensity.", MessageType.None, true);
				}

				self.ambientLightMultiplier = EditorGUILayout.Slider ("Ambient Intensity Multiplier", self.ambientLightMultiplier, 0.1f, 1.0f);
			}

			if (self.AutoCalculateAmbientIntensity == 2){
				if (self.helpOptions == true){
					EditorGUILayout.HelpBox("Below you can adjust the Ambient Intensity for each time of day. Ambient Intensity can help reduce the ambient lighting looking too flat or washed out.", MessageType.None, true);
				}

				self.TwilightAmbientIntensity = EditorGUILayout.Slider ("Twilight Ambient Intensity", self.TwilightAmbientIntensity, 0.1f, 1.0f);
				self.MorningAmbientIntensity = EditorGUILayout.Slider ("Morning Ambient Intensity", self.MorningAmbientIntensity, 0.1f, 1.0f);
				self.DayAmbientIntensity = EditorGUILayout.Slider ("Day Ambient Intensity", self.DayAmbientIntensity, 0.1f, 1.0f);
				self.EveningAmbientIntensity = EditorGUILayout.Slider ("Evening Ambient Intensity", self.EveningAmbientIntensity, 0.1f, 1.0f);
				self.NightAmbientIntensity = EditorGUILayout.Slider ("Night Ambient Intensity", self.NightAmbientIntensity, 0.1f, 1.0f);
			}

			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 14){
			EditorGUILayout.LabelField("Object Fields", EditorStyles.boldLabel);

			if (showAdvancedOptions == false){
				EditorGUILayout.HelpBox("The viewing of the Object Fields have been disabled. You can enabled them in the Editor Options of the UniStorm Editor.", MessageType.None, true);
			}
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("Here is where all object related UniStorm objects are kept. All components are already applied. If you are missing a component you will be notified with Error Log that will tell you how to fix it. If you are using custom objects refer to UniStorm's Documentation.", MessageType.None, true);
			}
			
			//Sun Objects
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Light Components", EditorStyles.boldLabel);
			bool sunObject = !EditorUtility.IsPersistent (self);
			self.sun = (Light)EditorGUILayout.ObjectField ("Sun Light", self.sun, typeof(Light), sunObject);
			
			bool moonLight = !EditorUtility.IsPersistent (self);
			self.moon = (Light)EditorGUILayout.ObjectField ("Moon Light", self.moon, typeof(Light), moonLight);
			
			bool lightningLight = !EditorUtility.IsPersistent (self);
			self.lightningLight = (Light)EditorGUILayout.ObjectField ("Lightning Light", self.lightningLight, typeof(Light), lightningLight);
			
			//Weather Particle Effects
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Particle Systems", EditorStyles.boldLabel);
			
			bool rainObject = !EditorUtility.IsPersistent (self);
			self.rain = (ParticleSystem)EditorGUILayout.ObjectField ("Rain Particle System", self.rain, typeof(ParticleSystem), rainObject);
			
			bool splashObject = !EditorUtility.IsPersistent (self);
			self.rainSplashes = (ParticleSystem)EditorGUILayout.ObjectField ("Rain Splash System", self.rainSplashes, typeof(ParticleSystem), splashObject);
			
			bool windyRainObject = !EditorUtility.IsPersistent (self);
			self.rainMist = (ParticleSystem)EditorGUILayout.ObjectField ("Rain Mist Particle System", self.rainMist, typeof(ParticleSystem), windyRainObject);
			
			bool snowObject = !EditorUtility.IsPersistent (self);
			self.snow = (ParticleSystem)EditorGUILayout.ObjectField ("Snow Particle System", self.snow, typeof(ParticleSystem), snowObject);
			
			bool snowMistFogObject = !EditorUtility.IsPersistent (self);
			self.snowMistFog = (ParticleSystem)EditorGUILayout.ObjectField ("Snow Dust Particle System", self.snowMistFog, typeof(ParticleSystem), snowMistFogObject);
			
			bool butterflieObject = !EditorUtility.IsPersistent (self);
			self.butterflies = (ParticleSystem)EditorGUILayout.ObjectField ("Lightning Bugs Particle System", self.butterflies, typeof(ParticleSystem), butterflieObject);
			
			bool windyLeavesObject = !EditorUtility.IsPersistent (self);
			self.windyLeaves = (ParticleSystem)EditorGUILayout.ObjectField ("Windy Leaves Particle System", self.windyLeaves, typeof(ParticleSystem), windyLeavesObject);
			
			//Sound Objects
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Sound Components", EditorStyles.boldLabel);
			
			bool rainSoundObject = !EditorUtility.IsPersistent (self);
			self.rainSound = (GameObject)EditorGUILayout.ObjectField ("Rain Sound Object", self.rainSound, typeof(GameObject), rainSoundObject);
			
			bool windSoundObject = !EditorUtility.IsPersistent (self);
			self.windSound = (GameObject)EditorGUILayout.ObjectField ("Wind Rain Sound Object", self.windSound, typeof(GameObject), windSoundObject);
			
			bool windSnowSoundObject = !EditorUtility.IsPersistent (self);
			self.windSnowSound = (GameObject)EditorGUILayout.ObjectField ("Wind Snow Sound Object", self.windSnowSound, typeof(GameObject), windSnowSoundObject);
			
			//Sky Objects
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Sky Components", EditorStyles.boldLabel);
			
			bool starObject = !EditorUtility.IsPersistent (self);
			self.starSphere = (GameObject)EditorGUILayout.ObjectField ("Star Sphere", self.starSphere, typeof(GameObject), starObject);
			
			bool moon = !EditorUtility.IsPersistent (self);
			self.moonObject = (GameObject)EditorGUILayout.ObjectField ("Moon Object", self.moonObject, typeof(GameObject), moon);
			
			//Cloud Objects
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Cloud Objects", EditorStyles.boldLabel);
			
			bool cloud1  = !EditorUtility.IsPersistent (self);
			self.mostlyClearClouds1 = (GameObject)EditorGUILayout.ObjectField ("Dynamic Light Clouds", self.mostlyClearClouds1, typeof(GameObject), cloud1);

			bool partlyCloudy1  = !EditorUtility.IsPersistent (self);
			self.partlyCloudyClouds1 = (GameObject)EditorGUILayout.ObjectField ("Dynamic Partly Cloudy Clouds", self.partlyCloudyClouds1, typeof(GameObject), partlyCloudy1);

			bool mostlyCloudy1  = !EditorUtility.IsPersistent (self);
			self.mostlyCloudyClouds1 = (GameObject)EditorGUILayout.ObjectField ("Dynamic Mostly Cloudy Clouds", self.mostlyCloudyClouds1, typeof(GameObject), mostlyCloudy1);
			
			//Heavy Cloud Objects
			bool storm1  = !EditorUtility.IsPersistent (self);
			self.heavyClouds = (GameObject)EditorGUILayout.ObjectField ("Base Storm Clouds", self.heavyClouds, typeof(GameObject), storm1);
			
			bool storm3  = !EditorUtility.IsPersistent (self);
			self.heavyCloudsLayerLight = (GameObject)EditorGUILayout.ObjectField ("Dynamic Storm Clouds", self.heavyCloudsLayerLight, typeof(GameObject), storm3);
			
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Unity Components", EditorStyles.boldLabel);
			
			//Camera Object
			bool cameraObjectObject = !EditorUtility.IsPersistent (self);
			self.cameraObject = (GameObject)EditorGUILayout.ObjectField ("Camera Object", self.cameraObject, typeof(GameObject), cameraObjectObject);

			self.PlayerObject = (GameObject)EditorGUILayout.ObjectField ("Player Object", self.PlayerObject, typeof(GameObject), true);
			
			bool windZoneObject = !EditorUtility.IsPersistent (self);
			self.windZone = (GameObject)EditorGUILayout.ObjectField ("Wind Zone", self.windZone, typeof(GameObject), windZoneObject);	
			
			//Skybox Materials
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Skybox Material", EditorStyles.boldLabel);
			
			bool SkyBoxMaterial  = !EditorUtility.IsPersistent (self);
			self.SkyBoxMaterial = (Material)EditorGUILayout.ObjectField ("Skybox Material", self.SkyBoxMaterial, typeof(Material), SkyBoxMaterial);
			
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Moon Phase Materials", EditorStyles.boldLabel);

			if (self.MoonPhases.Count == 0){
				for (int i = 0; i < 8; i++){
					self.MoonPhases.Add(null);
				}
			}

			if (self.MoonPhases.Count > 0){
				self.MoonPhases[0] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 1", self.MoonPhases[0], typeof(Material), false);
				self.MoonPhases[1] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 2", self.MoonPhases[1], typeof(Material), false);
				self.MoonPhases[2] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 3", self.MoonPhases[2], typeof(Material), false);
				self.MoonPhases[3] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 4", self.MoonPhases[3], typeof(Material), false);
				self.MoonPhases[4] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 5", self.MoonPhases[4], typeof(Material), false);
				self.MoonPhases[5] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 6", self.MoonPhases[5], typeof(Material), false);
				self.MoonPhases[6] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 7", self.MoonPhases[6], typeof(Material), false);
				self.MoonPhases[7] = (Material)EditorGUILayout.ObjectField ("Moon Phase Material 8", self.MoonPhases[7], typeof(Material), false);
			}

			EditorGUILayout.Space();

			//GUI Options
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("GUI Options", EditorStyles.boldLabel);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("GUI Options are useful for development and can be enabled and disabled in-game by pressing F12, or for mobile devices pressing 2 fingers on the screen and 3 for disabling it. The checkboxes below control what is turned on when the GUI Options are enabled. If you don't want either on unckeck both checkboxes.", MessageType.None, true);
			}

			self.timeScrollBarUseable = EditorGUILayout.Toggle ("Time Scroll Bar",self.timeScrollBarUseable);
			self.weatherCommandPromptUseable = EditorGUILayout.Toggle ("WCPS Enabled",self.weatherCommandPromptUseable);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 15){
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Import/Export Settings options allow you to either import or export your desired UniStorm settings. You can choose what to import and export using the options below. All data is written to a txt file so they can share and use settings from other users.", MessageType.None, true);
			}

			EditorGUILayout.LabelField("Import Options", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("The Import button below will allow you to import all visual settings and colors from a txt file. This makes it easy to recreate your UniStorm settings as needed as well as import settings from others.", MessageType.None, true);

			EditorGUILayout.Space();

			self.CustomUniStormTxtFile = (TextAsset)EditorGUILayout.ObjectField ("UniStorm Settings file", self.CustomUniStormTxtFile, typeof(TextAsset), false);
			EditorGUILayout.HelpBox("The txt file that UniStorm will use to import its new settings. Note: This process cannot be undone.", MessageType.None, true);

			EditorGUILayout.Space();

			self.ImportColors = EditorGUILayout.Toggle ("Import Colors", self.ImportColors);
			EditorGUILayout.HelpBox("Imports all UniStorm colors from the above txt file.", MessageType.None, true);

			EditorGUILayout.Space();

			self.ImportSettings = EditorGUILayout.Toggle ("Import Settings", self.ImportSettings);
			EditorGUILayout.HelpBox("Imports all UniStorm settings from the above txt file.", MessageType.None, true);

			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("This process cannot be undone. It is recommended that you create a backup of your current settings before importing new ones.", MessageType.Info, true);
			if (GUILayout.Button("Import")){
				if (EditorUtility.DisplayDialog("", "Are you sure you want to import new settings and apply them to UniStorm? Importing new settings cannot be undone.", "Yes", "No")){
					LoadedUniStormColorSettings.Clear();
					ImportUniStormSettings();
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Export Options", EditorStyles.boldLabel);
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("The Export button below will allow you to export all selected settings and colors to a txt file. This txt file can then be used to reimport settings in other scenes or can be shared with others.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (GUILayout.Button("Export")){
				if (self.fileName.Contains(".txt")){
					self.fileName = self.fileName.Replace(".txt", "");
				}

				self.filePath = EditorUtility.SaveFilePanelInProject("Save as txt", "UniStorm Settings file", "txt", "Please enter a file name to save the file to");

				SaveUniStormColorSettings.Clear();
				ExportUniStormSettings();
			}
		}

		if (TabNumberProp.intValue == 16){
			EditorGUILayout.LabelField("UniStorm Events (BETA)", EditorStyles.boldLabel);
			EditorGUILayout.Space();
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("UniStorm Events use Unity's event system to trigger customizable events. This brings you the power to trigger any public function according to many UniStorm releated variables such as time, weather, date, and more. To open a category, open and close the foldout menu by pressing the little triangle.", MessageType.None, true);
			}

			EventTabNumberProp.intValue = GUILayout.SelectionGrid (EventTabNumberProp.intValue, EventTabName, 2);
			
			if (EventTabNumberProp.intValue == 0){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Time Event", EditorStyles.boldLabel);
				EditorGUILayout.HelpBox("Time Events allow you to specify the exact hour and/or date you'd like an event to be triggered. This system uses Unity's UnityEvent system that allows you to call any public function.", MessageType.None, true);
				
				GUILayout.BeginHorizontal();
				GUILayout.Space(75);
				
				if (GUILayout.Button("Add Time Event")){
					self.TotalUniStormTimeEvents.Add(new UniStormWeatherSystem_C.UniStormEvents());
					self.TimeEvent.Add (null);
					self.TimeEventName.Add ("Event " + (self.TotalUniStormTimeEvents.Count));
					self.TimeEventFoldOut.Add (true);
					
					EditorUtility.SetDirty(self);
					serializedObject.Update ();
					
				}
				GUILayout.Space(75);
				GUILayout.EndHorizontal();
				
				EditorGUILayout.Space();
				if (self.TotalUniStormTimeEvents.Count == 0){
					EditorGUILayout.HelpBox("You currently have no Time Events. If you'd like to create one, press the Add Time Event button.", MessageType.None, true);
				}
				EditorGUILayout.Space();
			
				
				if (self.TotalUniStormTimeEvents != null){
					for(int i = 0; i < self.TotalUniStormTimeEvents.Count; i++){
						UniStormWeatherSystem_C.UniStormEvents _UniStormEventClass = self.TotalUniStormTimeEvents[i];

						GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
						myFoldoutStyle.fontStyle = FontStyle.Bold;
						myFoldoutStyle.fontSize = 14;
						Color myStyleColor = Color.black;
						myFoldoutStyle.normal.textColor = myStyleColor;
						myFoldoutStyle.onNormal.textColor = myStyleColor;
						myFoldoutStyle.hover.textColor = myStyleColor;
						myFoldoutStyle.onHover.textColor = myStyleColor;
						myFoldoutStyle.focused.textColor = myStyleColor;
						myFoldoutStyle.onFocused.textColor = myStyleColor;
						myFoldoutStyle.active.textColor = myStyleColor;
						myFoldoutStyle.onActive.textColor = myStyleColor;

						self.TimeEventFoldOut[i] = EditorGUILayout.Foldout(self.TimeEventFoldOut[i], self.TimeEventName[i]);
						
						if (self.TimeEventFoldOut[i]){
							EditorGUILayout.Space();
							
							self.TimeEventName[i] = EditorGUILayout.TextField("Event Name ", self.TimeEventName[i]);
							
							EditorGUILayout.Space();
							EditorGUILayout.Space();

							UniStormWeatherSystem_C.UniStormEvents.UniStormEventType UniStormEventType = UniStormWeatherSystem_C.UniStormEvents.UniStormEventType.Daily;
							
							EditorGUILayout.HelpBox("Controls the type of UniStorm Event which can happen Hourly, Daily, Weekly, Monthly, or Yearly.", MessageType.None, true);
							
							UniStormEventType = (UniStormWeatherSystem_C.UniStormEvents.UniStormEventType)_UniStormEventClass.EventTypeNumber;
							UniStormEventType = (UniStormWeatherSystem_C.UniStormEvents.UniStormEventType)EditorGUILayout.EnumPopup("Event Type", UniStormEventType);
							_UniStormEventClass.EventTypeNumber = (int)UniStormEventType;

							EditorGUILayout.Space();

							//GUILayout.ExpandWidth(false)

							/*

							if (_UniStormEventClass.EventTypeNumber == 5){
								EditorGUILayout.Space();

								EditorGUILayout.HelpBox("A Random Event allows UniStorm to generate a random time this event will happen based on the minumum and maximum amount of days.", MessageType.None, true);
							}
							*/
							
							//Yearly
							if (_UniStormEventClass.EventTypeNumber == 4){
								EditorGUILayout.Space();
								
								EditorGUILayout.HelpBox("The Month of Event is the month that needs to be reached to trigger this event.", MessageType.None, true);
								
								UniStormWeatherSystem_C.UniStormEvents.UniStormMonthEvent UniStormMonthEvent = UniStormWeatherSystem_C.UniStormEvents.UniStormMonthEvent.January;
								
								UniStormMonthEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormMonthEvent)_UniStormEventClass.MonthOfEvent;
								UniStormMonthEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormMonthEvent)EditorGUILayout.EnumPopup("Month of Event", UniStormMonthEvent);
								_UniStormEventClass.MonthOfEvent = (int)UniStormMonthEvent;
								
								EditorGUILayout.Space();
							}
							
							EditorGUILayout.Space();

							//Weekly
							if (_UniStormEventClass.EventTypeNumber == 2){
								EditorGUILayout.Space();
								
								EditorGUILayout.HelpBox("The Day of Week Event is the day that needs to be reached to trigger this event.", MessageType.None, true);

								UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent UniStormDayOfWeekEvent = UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent.Monday;

								UniStormDayOfWeekEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent)_UniStormEventClass.DayOfWeekEvent;
								UniStormDayOfWeekEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent)EditorGUILayout.EnumPopup("Day of Week Event", UniStormDayOfWeekEvent);
								_UniStormEventClass.DayOfWeekEvent = (int)UniStormDayOfWeekEvent;

								EditorGUILayout.Space();
							}
							
							//Daily/Monthly
							if (_UniStormEventClass.EventTypeNumber == 4 || _UniStormEventClass.EventTypeNumber == 3){
								EditorGUILayout.Space();
								
								EditorGUILayout.HelpBox("The Day of Event is the day that needs to be reached to trigger this event.", MessageType.None, true);
								
								UniStormWeatherSystem_C.UniStormEvents.UniStormDayEvent UniStormDayEvent = UniStormWeatherSystem_C.UniStormEvents.UniStormDayEvent._1;
								
								UniStormDayEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormDayEvent)_UniStormEventClass.DayOfEvent;
								UniStormDayEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormDayEvent)EditorGUILayout.EnumPopup("Day of Event", UniStormDayEvent);
								_UniStormEventClass.DayOfEvent = (int)UniStormDayEvent;
								
								EditorGUILayout.Space();
							}
							
							EditorGUILayout.Space();
							
							//Hourly
							if (_UniStormEventClass.EventTypeNumber != 0 && _UniStormEventClass.EventTypeNumber != 5){
								EditorGUILayout.HelpBox("The Hour of Event is the hour that needs to be reached to trigger an event. An event will only happen once for each day that its date is reached.", MessageType.None, true);
								
								UniStormWeatherSystem_C.UniStormEvents.UniStormHourEvent UniStormHourEvent = UniStormWeatherSystem_C.UniStormEvents.UniStormHourEvent._0;
								
								UniStormHourEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormHourEvent)_UniStormEventClass.HourOfEvent;
								UniStormHourEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormHourEvent)EditorGUILayout.EnumPopup("Hour of Event", UniStormHourEvent);
								_UniStormEventClass.HourOfEvent = (int)UniStormHourEvent;

								if (_UniStormEventClass.EventTypeNumber == 1){
									EditorGUILayout.Space();
									EditorGUILayout.Space();
									EditorGUILayout.HelpBox("Calculate on Start is useful for events that need to be calculated throughout the day such as enabling an object during the day and disabling it at night.", MessageType.None, true);

									_UniStormEventClass.CalculateOnStart = EditorGUILayout.Toggle ("Calculate on Start?", _UniStormEventClass.CalculateOnStart);
								}

								if (_UniStormEventClass.CalculateOnStart){
									EditorGUILayout.Space();

									EditorGUILayout.HelpBox("Below, you can set how this event is triggered on Start by its hour.", MessageType.None, true);

									UniStormWeatherSystem_C.UniStormEvents.TimeCalculationType TimeCalculationType = UniStormWeatherSystem_C.UniStormEvents.TimeCalculationType.LessThan;

									TimeCalculationType = (UniStormWeatherSystem_C.UniStormEvents.TimeCalculationType)_UniStormEventClass.TimeCalculationTypeNumber;
									TimeCalculationType = (UniStormWeatherSystem_C.UniStormEvents.TimeCalculationType)EditorGUILayout.EnumPopup("Start Calculation Type", TimeCalculationType);
									_UniStormEventClass.TimeCalculationTypeNumber = (int)TimeCalculationType;
								}
								
								EditorGUILayout.Space();	
								EditorGUILayout.Space();
							}
							
							EditorGUILayout.LabelField("Event Time", EditorStyles.label);
							if (_UniStormEventClass.EventTypeNumber == 0){
								EditorGUILayout.HelpBox("Your event will happen every hour regardless of the date.", MessageType.None, true);
							}
							
							if (_UniStormEventClass.EventTypeNumber == 1){
								EditorGUILayout.HelpBox("Your event will happen daily at " + _UniStormEventClass.HourOfEvent + ":00", MessageType.None, true);
							}
							
							if (_UniStormEventClass.EventTypeNumber == 2){
									UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent UniStormDayOfWeekEvent = (UniStormWeatherSystem_C.UniStormEvents.UniStormDayOfWeekEvent)_UniStormEventClass.DayOfWeekEvent;
									EditorGUILayout.HelpBox("Your event will happen weekly on " + UniStormDayOfWeekEvent.ToString() + " at " + _UniStormEventClass.HourOfEvent + ":00", MessageType.None, true);
							}
							
							if (_UniStormEventClass.EventTypeNumber == 3){
								EditorGUILayout.HelpBox("Your event will happen once a month on day " + _UniStormEventClass.DayOfEvent + " at " + _UniStormEventClass.HourOfEvent + ":00", MessageType.None, true);
							}
							
							if (_UniStormEventClass.EventTypeNumber == 4){
								EditorGUILayout.HelpBox("Your event will happen once a year on "+_UniStormEventClass.MonthOfEvent + "/" + _UniStormEventClass.DayOfEvent + " at " + _UniStormEventClass.HourOfEvent + ":00", MessageType.None, true);
							}
							
							if (_UniStormEventClass.EventTypeNumber == 5){
								EditorGUILayout.HelpBox("Your event will happen every hour regardless of the date.", MessageType.None, true);
							}
							
							EditorGUILayout.Space();
							EditorGUILayout.Space();
							
							EditorGUILayout.HelpBox("Below you can create an event using Unity's event system. UniStorm will trigger the event according to the time and/or date you've set above.", MessageType.None, true);
							EditorGUILayout.HelpBox("To create an event, press the + button. You will then be able to assign an object and call any of its public functions when UniStorm triggers the Time Event. An example of a Timed Event could be enabling a certain quest or resetting loot.", MessageType.None, true);
							
							EditorGUILayout.PropertyField(_UniStormEvents.GetArrayElementAtIndex(i));
							
							EditorGUILayout.Space();
							EditorGUILayout.Space();
						}
						GUILayout.Space(3f);
					}
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.TotalUniStormTimeEvents.Count > 0){
					if (GUILayout.Button("Remove Last Time Event")){
						self.TotalUniStormTimeEvents.RemoveAt(self.TotalUniStormTimeEvents.Count-1);
						self.TimeEvent.RemoveAt(self.TimeEvent.Count-1);
						self.TimeEventName.RemoveAt(self.TimeEventName.Count-1);
						self.TimeEventFoldOut.RemoveAt(self.TimeEventFoldOut.Count-1);
						
						EditorUtility.SetDirty(self);
						serializedObject.Update ();
					}
				}
			}
			
			if (EventTabNumberProp.intValue == 1){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Weather Event", EditorStyles.boldLabel);
				EditorGUILayout.HelpBox("Weather Events are coming soon with UniStorm's next update. If you have any suggestions for UniStorm Events, you can post them on UniStorm's thread using the button below.", MessageType.None, true);
				EditorGUILayout.Space();
				
				if (GUILayout.Button("Make Suggestions")){
					Application.OpenURL ("https://forum.unity3d.com/threads/released-unistorm-dynamic-day-night-weather-system-now-includes-unistorm-mobile-free.121021/");
				}
			}
			
			/*
			//Weather Event
			if (EventTabNumberProp.intValue == 1){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Weather Event", EditorStyles.boldLabel);
				EditorGUILayout.HelpBox("Weather Events allow you to have an event be triggered by a certain weather type. This system uses Unity's UnityEvent system that allows you to call any public function.", MessageType.None, true);
				
				GUILayout.BeginHorizontal();
				GUILayout.Space(75);
				
				if (GUILayout.Button("Add Weather Event")){
					self.TotalUniStormWeatherEvents.Add(new UniStormWeatherSystem_C.UniStormEvents());
					self.TimeEvent.Add (NewEvent);
					self.WeatherEventName.Add ("Event " + (self.TotalUniStormWeatherEvents.Count));
					self.WeatherEventTemperature.Add(35);
					
					EditorUtility.SetDirty(self);
					serializedObject.Update ();
					
				}
				GUILayout.Space(75);
				GUILayout.EndHorizontal();
				
				EditorGUILayout.Space();
				if (self.TotalUniStormWeatherEvents.Count == 0){
					EditorGUILayout.HelpBox("You currently have no Weather Events. If you'd like to create one, press the Add Weather Event button.", MessageType.None, true);
				}
				EditorGUILayout.Space();
				
				if (self.TotalUniStormWeatherEvents != null){
					for(int i = 0; i < self.TotalUniStormWeatherEvents.Count; i++){
						UniStormWeatherSystem_C.UniStormEvents _UniStormEventClass = self.TotalUniStormWeatherEvents[i];
						
						EditorGUILayout.LabelField(self.WeatherEventName[i], EditorStyles.boldLabel);
						
						EditorGUILayout.Space();
						
						self.WeatherEventName[i] = EditorGUILayout.TextField("Event Name ", self.WeatherEventName[i]);
						
						EditorGUILayout.Space();
						EditorGUILayout.Space();
						
						UniStormWeatherSystem_C.UniStormEvents.UniStormEventWeatherType UniStormEventWeatherType = UniStormWeatherSystem_C.UniStormEvents.UniStormEventWeatherType.LightRainOrLightSnowWinterOnly;
						
						EditorGUILayout.HelpBox("The Event Weather Type is the weather type that this event will use to trigger a Weather Event. If you don't want a weather type to affect this event, you can choose None.", MessageType.None, true);
						
						UniStormEventWeatherType = (UniStormWeatherSystem_C.UniStormEvents.UniStormEventWeatherType)_UniStormEventClass.EventTypeNumber;
						UniStormEventWeatherType = (UniStormWeatherSystem_C.UniStormEvents.UniStormEventWeatherType)EditorGUILayout.EnumPopup("Event Weather Type", UniStormEventWeatherType);
						_UniStormEventClass.EventTypeNumber = (int)UniStormEventWeatherType;
						
						EditorGUILayout.Space();
						EditorGUILayout.Space();

						EditorGUILayout.HelpBox("Controls whether or not this event is affected by temperature.", MessageType.None, true);

						_UniStormEventClass.UseTemperature = EditorGUILayout.Toggle ("Use Temperature?", _UniStormEventClass.UseTemperature);

						if (_UniStormEventClass.UseTemperature){
							EditorGUILayout.Space();

							UniStormWeatherSystem_C.UniStormEvents.TemperatureCalculationType TemperatureCalculationType = UniStormWeatherSystem_C.UniStormEvents.TemperatureCalculationType.LessThan;

							EditorGUILayout.Space();

							EditorGUILayout.HelpBox("The temperature that affects this event.", MessageType.None, true);

							self.WeatherEventTemperature[i] = EditorGUILayout.IntField ("Event Temperature", self.WeatherEventTemperature[i]);

							EditorGUILayout.Space();

							EditorGUILayout.HelpBox("The Calculation Type controls how this event will calculate the temperature. For example, if your event damages your player's health when below freezing temperatures, you would set the temperature to 32° F or 0° C and set the Calculation Type to Less Than.", MessageType.None, true);
							
							TemperatureCalculationType = (UniStormWeatherSystem_C.UniStormEvents.TemperatureCalculationType)_UniStormEventClass.TemperatureCalculationTypeNumber;
							TemperatureCalculationType = (UniStormWeatherSystem_C.UniStormEvents.TemperatureCalculationType)EditorGUILayout.EnumPopup("Calculation Type", TemperatureCalculationType);
							_UniStormEventClass.TemperatureCalculationTypeNumber = (int)TemperatureCalculationType;

							EditorGUILayout.Space();
						}
						EditorGUILayout.Space();
						
						EditorGUILayout.HelpBox("Below you can create an event using Unity's event system. UniStorm will trigger the event according to the weather type you've set above.", MessageType.None, true);
						EditorGUILayout.HelpBox("To create an event, press the + button. You will then be able to assign an object and call any of its public functions when UniStorm triggers the Weather Event. An example of a Weather Event could be lowering an AI's visibility during foggy or rainy weather. Another example could be having certain AI only come out during certain weather types.", MessageType.None, true);
						
						EditorGUILayout.PropertyField(_UniStormEvents.GetArrayElementAtIndex(i));
						
						EditorGUILayout.Space();
						EditorGUILayout.Space();
					}
				}
				
				if (GUILayout.Button("Remove Last Weather Event")){
					self.TotalUniStormWeatherEvents.RemoveAt(self.TotalUniStormWeatherEvents.Count-1);
					self.TimeEvent.RemoveAt(self.TimeEvent.Count-1);
					self.WeatherEventName.RemoveAt(self.WeatherEventName.Count-1);
					self.WeatherEventTemperature.RemoveAt(self.WeatherEventTemperature.Count-1);
					
					EditorUtility.SetDirty(self);
					serializedObject.Update ();
					
				}
			}
			*/
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();		
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		if (TabNumberProp.intValue == 17){
			EditorGUILayout.LabelField("Documentation/Support", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Customer Support", EditorStyles.label);

			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("If you need any assistance with UniStorm, we would be more than happy to help out. Email or forum contact is the fastest and best way to receive support.", MessageType.None, true);
				EditorGUILayout.HelpBox("You can email us at: Support@BlackHorizonStudios.com", MessageType.None, true);
			}

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Documentation", EditorStyles.label);
			
			if (self.helpOptions == true){
				EditorGUILayout.HelpBox("UniStorm is well documented. We provide detailed documentation and Solutions to Possible Issues as well as a Wiki Site. Below, each button will open up the appropriate webpage.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (GUILayout.Button("Documentation")){
				Application.OpenURL ("http://unistorm-weather-system.wikia.com/wiki/Documentation");
			}

			if (GUILayout.Button("Wiki")){
				Application.OpenURL ("http://unistorm-weather-system.wikia.com/wiki/UniStorm_Weather_System_Wiki");
			}

			if (GUILayout.Button("Solutions to Possible Issues")){
				Application.OpenURL ("http://unistorm-weather-system.wikia.com/wiki/Solutions_to_Possible_Issues");
			}

			if (GUILayout.Button("Forums")){
				Application.OpenURL ("https://forum.unity3d.com/threads/released-unistorm-dynamic-day-night-weather-system-now-includes-unistorm-mobile-free.121021/");
			}

			if (GUILayout.Button("Tutorial Videos")){
				Application.OpenURL ("https://www.youtube.com/playlist?list=PLlyiPBj7FznYmPW9NR6U0WKudaeFuAgKL");
			}
		}

		if (GUI.changed && !EditorApplication.isPlaying){
			EditorUtility.SetDirty(self); 
		}

		serializedObject.ApplyModifiedProperties ();
	}

	void ExportUniStormSettings (){
		UniStormWeatherSystem_C self = (UniStormWeatherSystem_C)target;

		SaveUniStormColorSettings.Add("rainColorTwilight", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.rainColorTwilight));
		SaveUniStormColorSettings.Add("rainColorMorning", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.rainColorMorning));
		SaveUniStormColorSettings.Add("rainColorDay", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.rainColorDay));
		SaveUniStormColorSettings.Add("rainColorEvening", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.rainColorEvening));
		SaveUniStormColorSettings.Add("rainColorNight", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.rainColorNight));
		
		SaveUniStormColorSettings.Add("particleMistColorTwilight", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.particleMistColorTwilight));
		SaveUniStormColorSettings.Add("particleMistColorMorning", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.particleMistColorMorning));
		SaveUniStormColorSettings.Add("particleMistColorDay", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.particleMistColorDay));
		SaveUniStormColorSettings.Add("particleMistColorEvening", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.particleMistColorEvening));
		SaveUniStormColorSettings.Add("particleMistColorNight", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.particleMistColorNight));
		
		SaveUniStormColorSettings.Add("cloudColorTwilight", "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.cloudColorTwilight));
		SaveUniStormColorSettings.Add("cloudColorMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.cloudColorMorning));
		SaveUniStormColorSettings.Add("cloudColorDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.cloudColorDay));
		SaveUniStormColorSettings.Add("cloudColorEvening",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.cloudColorEvening));
		SaveUniStormColorSettings.Add("cloudColorNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.cloudColorNight));
		
		SaveUniStormColorSettings.Add("stormClouds1ColorTwilight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds1ColorTwilight));
		SaveUniStormColorSettings.Add("stormClouds1ColorMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds1ColorMorning));
		SaveUniStormColorSettings.Add("stormClouds1ColorDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds1ColorDay));
		SaveUniStormColorSettings.Add("stormClouds1ColorEvening",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds1ColorEvening));
		SaveUniStormColorSettings.Add("stormClouds1ColorNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds1ColorNight));
		
		SaveUniStormColorSettings.Add("stormClouds2ColorTwilight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds2ColorTwilight));
		SaveUniStormColorSettings.Add("stormClouds2ColorMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds2ColorMorning));
		SaveUniStormColorSettings.Add("stormClouds2ColorDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds2ColorDay));
		SaveUniStormColorSettings.Add("stormClouds2ColorEvening",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds2ColorEvening));
		SaveUniStormColorSettings.Add("stormClouds2ColorNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormClouds2ColorNight));
		
		SaveUniStormColorSettings.Add("skyColorTwilight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SkyTwilightColor));
		SaveUniStormColorSettings.Add("skyColorMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SkyMorningColor));
		SaveUniStormColorSettings.Add("skyColorDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SkyDayColor));
		SaveUniStormColorSettings.Add("skyColorEvening",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SkyEveningColor));
		SaveUniStormColorSettings.Add("skyColorNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SkyNightColor));
		
		SaveUniStormColorSettings.Add("EquatorTwilightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.EquatorTwilightColor));
		SaveUniStormColorSettings.Add("EquatorMorningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.EquatorMorningColor));
		SaveUniStormColorSettings.Add("EquatorDayColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.EquatorDayColor));
		SaveUniStormColorSettings.Add("EquatorEveningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.EquatorEveningColor));
		SaveUniStormColorSettings.Add("EquatorNightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.EquatorNightColor));
		
		SaveUniStormColorSettings.Add("GroundTwilightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.GroundTwilightColor));
		SaveUniStormColorSettings.Add("GroundMorningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.GroundMorningColor));
		SaveUniStormColorSettings.Add("GroundDayColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.GroundDayColor));
		SaveUniStormColorSettings.Add("GroundEveningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.GroundEveningColor));
		SaveUniStormColorSettings.Add("GroundNightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.GroundNightColor));
		
		SaveUniStormColorSettings.Add("fogTwilightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.fogTwilightColor));
		SaveUniStormColorSettings.Add("fogMorningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.fogMorningColor));
		SaveUniStormColorSettings.Add("fogDayColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.fogDayColor));
		SaveUniStormColorSettings.Add("fogDuskColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.fogDuskColor));
		SaveUniStormColorSettings.Add("fogNightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.fogNightColor));
		
		SaveUniStormColorSettings.Add("stormyFogColorTwilight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormyFogColorTwilight));
		SaveUniStormColorSettings.Add("stormyFogColorMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormyFogColorMorning));
		SaveUniStormColorSettings.Add("stormyFogColorDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormyFogColorDay));
		SaveUniStormColorSettings.Add("stormyFogColorEvening",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormyFogColorEvening));
		SaveUniStormColorSettings.Add("stormyFogColorNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.stormyFogColorNight));
		
		SaveUniStormColorSettings.Add("TwilightAmbientLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.TwilightAmbientLight));
		SaveUniStormColorSettings.Add("MorningAmbientLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.MorningAmbientLight));
		SaveUniStormColorSettings.Add("DayAmbientLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.MiddayAmbientLight));
		SaveUniStormColorSettings.Add("EveningAmbientLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.DuskAmbientLight));
		SaveUniStormColorSettings.Add("NightAmbientLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.NightAmbientLight));
		
		SaveUniStormColorSettings.Add("SunMorning",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SunMorning));
		SaveUniStormColorSettings.Add("SunDay",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SunDay));
		SaveUniStormColorSettings.Add("SunDusk",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SunDusk));
		SaveUniStormColorSettings.Add("SunNight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.SunNight));
		
		SaveUniStormColorSettings.Add("MorningAtmosphericLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.MorningAtmosphericLight));
		SaveUniStormColorSettings.Add("MiddayAtmosphericLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.MiddayAtmosphericLight));
		SaveUniStormColorSettings.Add("DuskAtmosphericLight",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.DuskAtmosphericLight));
		
		SaveUniStormColorSettings.Add("SkyBoxMorningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.skyColorMorning));
		SaveUniStormColorSettings.Add("SkyBoxDayColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.skyColorDay));
		SaveUniStormColorSettings.Add("SkyBoxEveningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.skyColorEvening));
		SaveUniStormColorSettings.Add("SkyBoxNightColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.skyColorNight));
		SaveUniStormColorSettings.Add("nightTintColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.nightTintColor));
		
		SaveUniStormColorSettings.Add("lightningColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.lightningColor));
		SaveUniStormColorSettings.Add("moonColor",  "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(self.moonColor));
		
		//Floats
		SaveUniStormColorSettings.Add("ReflectionIntensityTwilight", self.ReflectionIntensityTwilight.ToString());
		SaveUniStormColorSettings.Add("ReflectionIntensityMorning", self.ReflectionIntensityMorning.ToString());
		SaveUniStormColorSettings.Add("ReflectionIntensityDay", self.ReflectionIntensityDay.ToString());
		SaveUniStormColorSettings.Add("ReflectionIntensityEvening", self.ReflectionIntensityEvening.ToString());
		SaveUniStormColorSettings.Add("ReflectionIntensityNight", self.ReflectionIntensityNight.ToString());
		
		SaveUniStormColorSettings.Add("TwilightAmbientIntensity", self.TwilightAmbientIntensity.ToString());
		SaveUniStormColorSettings.Add("MorningAmbientIntensity", self.MorningAmbientIntensity.ToString());
		SaveUniStormColorSettings.Add("DayAmbientIntensity", self.DayAmbientIntensity.ToString());
		SaveUniStormColorSettings.Add("EveningAmbientIntensity", self.EveningAmbientIntensity.ToString());
		SaveUniStormColorSettings.Add("NightAmbientIntensity", self.NightAmbientIntensity.ToString());
		
		SaveUniStormColorSettings.Add("RegularCloudFadeInMultiplier", self.RegularCloudFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("RegularCloudFadeOutMultiplier", self.RegularCloudFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("CloudFadeInMultiplier", self.CloudFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("CloudFadeOutMultiplier", self.CloudFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("SoundFadeInMultiplier", self.SoundFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("SoundFadeOutMultiplier", self.SoundFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("EffectsFadeInMultiplier", self.EffectsFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("EffectsFadeOutMultiplier", self.EffectsFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("FogDistanceFadeInMultiplier", self.FogDistanceFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("FogDistanceFadeOutMultiplier", self.FogDistanceFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("FogColorFadeInMultiplier", self.FogColorFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("FogColorFadeOutMultiplier", self.FogColorFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("ParticleEffectsFadeInMultiplier", self.ParticleEffectsFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("ParticleEffectsFadeOutMultiplier", self.ParticleEffectsFadeOutMultiplier.ToString());
		SaveUniStormColorSettings.Add("WindFadeInMultiplier", self.WindFadeInMultiplier.ToString());
		SaveUniStormColorSettings.Add("WindFadeOutMultiplier", self.WindFadeOutMultiplier.ToString());
		
		SaveUniStormColorSettings.Add("dayShadowIntensity", self.dayShadowIntensity.ToString());
		SaveUniStormColorSettings.Add("nightShadowIntensity", self.nightShadowIntensity.ToString());
		SaveUniStormColorSettings.Add("lightningShadowIntensity", self.lightningShadowIntensity.ToString());
		
		SaveUniStormColorSettings.Add("moonLightIntensity", self.moonLightIntensity.ToString());
		SaveUniStormColorSettings.Add("SunIntensityMultiplier", self.SunIntensityMultiplier.ToString());
		
		//Ints
		SaveUniStormColorSettings.Add("AmbientSource", self.AmbientSource.ToString());
		SaveUniStormColorSettings.Add("StormyMoonLightIntensity", self.StormyMoonLightIntensity.ToString());
		SaveUniStormColorSettings.Add("StormySunIntensity", self.StormySunIntensity.ToString());
		SaveUniStormColorSettings.Add("CloudDensity", self.CloudDensity.ToString());
		SaveUniStormColorSettings.Add("AutoCalculateAmbientIntensity", self.AutoCalculateAmbientIntensity.ToString());
		SaveUniStormColorSettings.Add("fogMode", self.fogMode.ToString());
		SaveUniStormColorSettings.Add("cloudSpeed", self.cloudSpeed.ToString());
		SaveUniStormColorSettings.Add("stormyCloudSpeed", self.heavyCloudSpeed.ToString());
		SaveUniStormColorSettings.Add("starSpeed", self.starSpeed.ToString());
		
		SaveUniStormColorSettings.Add("stormyFogDistanceStart", self.stormyFogDistanceStart.ToString());
		SaveUniStormColorSettings.Add("stormyFogDistanceEnd", self.stormyFogDistance.ToString());
		SaveUniStormColorSettings.Add("fogStartDistance", self.fogStartDistance.ToString());
		SaveUniStormColorSettings.Add("fogEndDistance", self.fogEndDistance.ToString());
		
		using (System.IO.FileStream fs = new System.IO.FileStream(self.filePath, System.IO.FileMode.OpenOrCreate)){
			using (System.IO.TextWriter tw = new System.IO.StreamWriter(fs))

				foreach (KeyValuePair<string, string> kvp in SaveUniStormColorSettings){
					string TempString = kvp.Value;

					if (TempString[0] == '#'){
						TempString = TempString.Replace("0","x");
						TempString = TempString.Replace("1","y");
						TempString = TempString.Replace("2","w");
						TempString = TempString.Replace("3","u");
						TempString = TempString.Replace("4","z");
						TempString = TempString.Replace("F","q");
					}

					tw.WriteLine(string.Format("{0}:{1}", kvp.Key, TempString));
				}
		}
			
		AssetDatabase.Refresh();
	}

	void ImportUniStormSettings (){
		UniStormWeatherSystem_C self = (UniStormWeatherSystem_C)target;

		string[] textLines =  self.CustomUniStormTxtFile.text.Split ('\n');

		for (int i = 0 ; i < textLines.Length - 1; i++) {
			string valueLine = textLines[i];
			string[] SeparatedString = valueLine.Split(new char[] { ':' });

			string TempString = SeparatedString[1];

			if (TempString[0] == '#'){
				TempString = TempString.Replace("x","0");
				TempString = TempString.Replace("y","1");
				TempString = TempString.Replace("w","2");
				TempString = TempString.Replace("u","3");
				TempString = TempString.Replace("z","4");
				TempString = TempString.Replace("q","F");
			}

			TempString = TempString.Substring(0, TempString.Length - 1);

			LoadedUniStormColorSettings.Add(SeparatedString[0], TempString);
		}

		if (self.ImportColors){
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["cloudColorTwilight"], out self.cloudColorTwilight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["cloudColorMorning"], out self.cloudColorMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["cloudColorDay"], out self.cloudColorDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["cloudColorEvening"], out self.cloudColorEvening);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["cloudColorNight"], out self.cloudColorNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds1ColorTwilight"], out self.stormClouds1ColorTwilight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds1ColorMorning"], out self.stormClouds1ColorMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds1ColorDay"], out self.stormClouds1ColorDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds1ColorEvening"], out self.stormClouds1ColorEvening);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds1ColorNight"], out self.stormClouds1ColorNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds2ColorTwilight"], out self.stormClouds2ColorTwilight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds2ColorMorning"], out self.stormClouds2ColorMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds2ColorDay"], out self.stormClouds2ColorDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds2ColorEvening"], out self.stormClouds2ColorEvening);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormClouds2ColorNight"], out self.stormClouds2ColorNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["skyColorTwilight"], out self.SkyTwilightColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["skyColorMorning"], out self.SkyMorningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["skyColorDay"], out self.SkyDayColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["skyColorEvening"], out self.SkyEveningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["skyColorNight"], out self.SkyNightColor);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EquatorTwilightColor"], out self.EquatorTwilightColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EquatorMorningColor"], out self.EquatorMorningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EquatorDayColor"], out self.EquatorDayColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EquatorEveningColor"], out self.EquatorEveningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EquatorNightColor"], out self.EquatorNightColor);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["GroundTwilightColor"], out self.GroundTwilightColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["GroundMorningColor"], out self.GroundMorningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["GroundDayColor"], out self.GroundDayColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["GroundEveningColor"], out self.GroundEveningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["GroundNightColor"], out self.GroundNightColor);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["fogTwilightColor"], out self.fogTwilightColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["fogMorningColor"], out self.fogMorningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["fogDayColor"], out self.fogDayColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["fogDuskColor"], out self.fogDuskColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["fogNightColor"], out self.fogNightColor);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormyFogColorTwilight"], out self.stormyFogColorTwilight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormyFogColorMorning"], out self.stormyFogColorMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormyFogColorDay"], out self.stormyFogColorDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormyFogColorEvening"], out self.stormyFogColorEvening);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["stormyFogColorNight"], out self.stormyFogColorNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["TwilightAmbientLight"], out self.TwilightAmbientLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["MorningAmbientLight"], out self.MorningAmbientLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["DayAmbientLight"], out self.MiddayAmbientLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["EveningAmbientLight"], out self.DuskAmbientLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["NightAmbientLight"], out self.NightAmbientLight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SunMorning"], out self.SunMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SunDay"], out self.SunDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SunDusk"], out self.SunDusk);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SunNight"], out self.SunNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["MorningAtmosphericLight"], out self.MorningAtmosphericLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["MiddayAtmosphericLight"], out self.MiddayAtmosphericLight);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["DuskAtmosphericLight"], out self.DuskAtmosphericLight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SkyBoxMorningColor"], out self.skyColorMorning);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SkyBoxDayColor"], out self.skyColorDay);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SkyBoxEveningColor"], out self.skyColorEvening);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["SkyBoxNightColor"], out self.skyColorNight);
			
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["lightningColor"], out self.lightningColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["moonColor"], out self.moonColor);
			UnityEngine.ColorUtility.TryParseHtmlString(LoadedUniStormColorSettings["nightTintColor"], out self.nightTintColor);
		}
		
		if (self.ImportSettings){
			//Floats
			self.ReflectionIntensityTwilight = float.Parse(LoadedUniStormColorSettings["ReflectionIntensityTwilight"]);
			self.ReflectionIntensityMorning = float.Parse(LoadedUniStormColorSettings["ReflectionIntensityMorning"]);
			self.ReflectionIntensityDay = float.Parse(LoadedUniStormColorSettings["ReflectionIntensityDay"]);
			self.ReflectionIntensityEvening = float.Parse(LoadedUniStormColorSettings["ReflectionIntensityEvening"]);
			self.ReflectionIntensityNight = float.Parse(LoadedUniStormColorSettings["ReflectionIntensityNight"]);
			
			self.TwilightAmbientIntensity = float.Parse(LoadedUniStormColorSettings["TwilightAmbientIntensity"]);
			self.MorningAmbientIntensity = float.Parse(LoadedUniStormColorSettings["MorningAmbientIntensity"]);
			self.MorningAmbientIntensity  = float.Parse(LoadedUniStormColorSettings["MorningAmbientIntensity"]);
			self.DayAmbientIntensity  = float.Parse(LoadedUniStormColorSettings["DayAmbientIntensity"]);
			self.EveningAmbientIntensity = float.Parse(LoadedUniStormColorSettings["EveningAmbientIntensity"]);
			self.NightAmbientIntensity = float.Parse(LoadedUniStormColorSettings["NightAmbientIntensity"]);
			
			self.RegularCloudFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["RegularCloudFadeInMultiplier"]);
			self.RegularCloudFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["RegularCloudFadeOutMultiplier"]);
			self.CloudFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["CloudFadeInMultiplier"]);
			self.CloudFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["CloudFadeOutMultiplier"]);
			self.SoundFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["SoundFadeInMultiplier"]);
			self.SoundFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["SoundFadeOutMultiplier"]);
			self.EffectsFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["EffectsFadeInMultiplier"]);
			self.EffectsFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["EffectsFadeOutMultiplier"]);
			self.FogDistanceFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["FogDistanceFadeInMultiplier"]);
			self.FogDistanceFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["FogDistanceFadeOutMultiplier"]);
			self.FogColorFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["FogColorFadeInMultiplier"]);
			self.FogColorFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["FogColorFadeOutMultiplier"]);
			self.ParticleEffectsFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["ParticleEffectsFadeInMultiplier"]);
			self.ParticleEffectsFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["ParticleEffectsFadeOutMultiplier"]);
			self.WindFadeInMultiplier = float.Parse(LoadedUniStormColorSettings["WindFadeInMultiplier"]);
			self.WindFadeOutMultiplier = float.Parse(LoadedUniStormColorSettings["WindFadeOutMultiplier"]);
			
			self.dayShadowIntensity = float.Parse(LoadedUniStormColorSettings["dayShadowIntensity"]);
			self.nightShadowIntensity = float.Parse(LoadedUniStormColorSettings["nightShadowIntensity"]);
			self.lightningShadowIntensity = float.Parse(LoadedUniStormColorSettings["lightningShadowIntensity"]);
			
			self.moonLightIntensity = float.Parse(LoadedUniStormColorSettings["moonLightIntensity"]);
			self.SunIntensityMultiplier = float.Parse(LoadedUniStormColorSettings["SunIntensityMultiplier"]);
			
			//Ints
			self.AmbientSource = int.Parse(LoadedUniStormColorSettings["AmbientSource"]);
			self.StormyMoonLightIntensity = int.Parse(LoadedUniStormColorSettings["StormyMoonLightIntensity"]);
			self.StormySunIntensity = int.Parse(LoadedUniStormColorSettings["StormySunIntensity"]);
			self.CloudDensity = int.Parse(LoadedUniStormColorSettings["CloudDensity"]);
			self.AutoCalculateAmbientIntensity = int.Parse(LoadedUniStormColorSettings["AutoCalculateAmbientIntensity"]);
			self.fogMode = int.Parse(LoadedUniStormColorSettings["fogMode"]);
			self.cloudSpeed = int.Parse(LoadedUniStormColorSettings["cloudSpeed"]);
			self.heavyCloudSpeed = int.Parse(LoadedUniStormColorSettings["stormyCloudSpeed"]);
			self.starSpeed = int.Parse(LoadedUniStormColorSettings["starSpeed"]);
			
			/*
			self.PrecipitationType = int.Parse(LoadedUniStormColorSettings["PrecipitationType"]);
			self.precipitationOddsSpring = int.Parse(LoadedUniStormColorSettings["precipitationOddsSpring"]);
			self.precipitationOddsSummer = int.Parse(LoadedUniStormColorSettings["precipitationOddsSummer"]);
			self.precipitationOddsFall = int.Parse(LoadedUniStormColorSettings["precipitationOddsFall"]);
			self.precipitationOddsWinter = int.Parse(LoadedUniStormColorSettings["precipitationOddsWinter"]);
			*/
			
			self.stormyFogDistanceStart = int.Parse(LoadedUniStormColorSettings["stormyFogDistanceStart"]);
			self.stormyFogDistance = int.Parse(LoadedUniStormColorSettings["stormyFogDistanceEnd"]);
			self.fogStartDistance = int.Parse(LoadedUniStormColorSettings["fogStartDistance"]);
			self.fogEndDistance = int.Parse(LoadedUniStormColorSettings["fogEndDistance"]);
		}

		EditorUtility.SetDirty(self); 
		serializedObject.ApplyModifiedProperties ();
	}

	void ProgressBar (float value, string label) {
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}
	
}