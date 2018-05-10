using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode()] 
[DrawGizmo (GizmoType.Selected)]

[CustomEditor(typeof(Climate_Zone_C))] 
public class Climate_Zone_Editor_C : Editor {

	public string weatherType = "";
	public string ClimateName = "";
	bool confirmationToGenerate = false;
	public Vector3 size = new Vector3(10,10,10);

	enum DetectionType
	{
		OnTrigger = 0,
		ByHeight = 1
	}

	enum IfGreaterOrLessThan
	{
		IfGreaterThan = 0,
		IfLessThan = 1
	}

	enum TemperatureType
	{
		Fahrenheit = 0,
		Celsius = 1
	}

	DetectionType editorDetectionType = DetectionType.ByHeight;
	TemperatureType editorTemperatureType = TemperatureType.Fahrenheit;
	IfGreaterOrLessThan editorIfGreaterOrLessThan = IfGreaterOrLessThan.IfGreaterThan;
	
 		public override void OnInspectorGUI () 
		{

		Climate_Zone_C self = (Climate_Zone_C)target;
    
    	//Time Number Variables
    	EditorGUILayout.LabelField("UniStorm Climate Generator", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("By: Black Horizon Studios", EditorStyles.label);
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Climate Options", EditorStyles.boldLabel);

		EditorGUILayout.Space();

		self.ClimateName = EditorGUILayout.TextField ("Climate Name", self.ClimateName);

		EditorGUILayout.HelpBox("The name of your climate.", MessageType.None, true);

		EditorGUILayout.Space();

		if (self.DetectionType == 0)
		{
			EditorGUILayout.Space();

			self.transform.localScale = EditorGUILayout.Vector3Field("Climate Zone Size", self.transform.localScale);

			EditorGUILayout.HelpBox("Climate Zone Size controls the size of your Climate Zone.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		editorDetectionType = (DetectionType)self.DetectionType;
		editorDetectionType = (DetectionType)EditorGUILayout.EnumPopup("Detection Type", editorDetectionType);
		self.DetectionType = (int)editorDetectionType;

		EditorGUILayout.HelpBox("The Detection Type determins how your weather zone is triggered. This can be based on the Height (where your player must reach a certain height in order for the Climate Zone to change; this can be perfect for Mountains) or based on an OnTrigger collision (where you player must hit the trigger in order for the Climate Zone to change; perfect for Desert areas, Rainforests, Swamps, etc).", MessageType.None, true);

		EditorGUILayout.Space();

		if (self.DetectionType == 1)
		{
			bool PlayerObject = !EditorUtility.IsPersistent (self);
			self.PlayerObject = (GameObject)EditorGUILayout.ObjectField ("Player Object", self.PlayerObject, typeof(GameObject), PlayerObject);

			EditorGUILayout.HelpBox("Assign your player object here. The Climate System will use your player's height (Y position) to detect when to change Climate Zones.", MessageType.None, true);

			EditorGUILayout.Space();

			self.climateHeight = EditorGUILayout.IntField ("Climate Height", self.climateHeight);

			EditorGUILayout.HelpBox("The height needed to change the Climate Zone.", MessageType.None, true);

			EditorGUILayout.Space();

			editorIfGreaterOrLessThan = (IfGreaterOrLessThan)self.ifGreaterOrLessThan;
			editorIfGreaterOrLessThan = (IfGreaterOrLessThan)EditorGUILayout.EnumPopup("If Greater Or Less Than", editorIfGreaterOrLessThan);
			self.ifGreaterOrLessThan = (int)editorIfGreaterOrLessThan;

			EditorGUILayout.HelpBox("Based on the height above, does your player need to be above or below " + self.climateHeight.ToString() + " to make the climate change? Example: Moutains are above 300 units and Grasslands are below 300 units." , MessageType.None, true);

			EditorGUILayout.Space();

			self.updateInterval = EditorGUILayout.FloatField ("Update Interval", self.updateInterval);

			EditorGUILayout.HelpBox("How often (in seconds) the Climate Zone is updated to check the player's height" , MessageType.None, true);
		}

		if (self.DetectionType == 0)
		{
			self.playerTag = EditorGUILayout.TextField ("Tag Name", self.playerTag);
			
			EditorGUILayout.HelpBox("The Tag name of your player.", MessageType.None, true);
			
			EditorGUILayout.Space();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();

			
		EditorGUILayout.LabelField("Current Climate", EditorStyles.boldLabel);

		EditorGUILayout.HelpBox("This is the current climate that is generated.", MessageType.None, true);

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


		EditorGUILayout.HelpBox("You can adjust the Precipitation Odds for each season using the sliders below. Weather will be generated based on the values for each season. The sliders determine the odds (in percent) for both precipitation and clear weather. The higher the Precipitation Odds, the more chance of precipitation. The lower the Precipitation Odds, the lower chance of precipitation. The bars below visually represent the odds for each season. UniStorm will generate its weather based off the percentages below.", MessageType.None, true);
		
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

		EditorGUILayout.Space();
		EditorGUILayout.Space();


		if(GUILayout.Button("Generate Climate"))
		{
			confirmationToGenerate = !confirmationToGenerate;
		}

			
			EditorGUILayout.HelpBox("Generate climate cannot be undone. Once this button has been pressed, your old settings will be gone.", MessageType.Warning, true);
			
			EditorGUILayout.HelpBox("Generate Climate will randomize several UniStorm settings to generate a climate for you. This includeds Weather Odds, Min and Max Temperautes (as well as calculating your seasonal averages), Starting Time, Date, Starting Weather, Moon Phases, and more. This can be useful for testing randomized settings or even generating a climate for your games. The Presets all use real world data (excluding the Random Preset) to give you a well rounded generated climate of that type.", MessageType.None, true);
			
			
		if (confirmationToGenerate)
		{
			EditorGUILayout.Space();

			editorTemperatureType = (TemperatureType)self.TemperatureType;
			editorTemperatureType = (TemperatureType)EditorGUILayout.EnumPopup("Temperautre Type", editorTemperatureType);
			self.TemperatureType = (int)editorTemperatureType;

			EditorGUILayout.HelpBox("The Temperautre Type determins whether Fahrenheit or Celsius temperatures will be generated. Both types will still use real-world data.", MessageType.None, true);

			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Generate climate cannot be undone. Once this button has been pressed, your old settings will be gone.", MessageType.Warning, true);

				EditorGUILayout.HelpBox("Generating a new climate will change your current settings. This process cannot be undone. However, you can always reset to the default settings we used with our demos.", MessageType.Warning, true);
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				
				if(GUILayout.Button("Random"))
				{
					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
				if(GUILayout.Button("Rainforest"))
				{
					
					self.precipitationOddsSpring = 75;
					self.precipitationOddsSummer = 80;
					self.precipitationOddsWinter = 70;
					self.precipitationOddsFall = 75;


				/*
				self.PrecipitationGraph = EditorGUILayout.CurveField("Percentage Curve", self.PrecipitationGraph);
				self.PrecipitationGraph = AnimationCurve.Linear(1,0,13,100);
				self.PrecipitationGraph.AddKey(1,Random.Range(60,70));
				self.PrecipitationGraph.AddKey(4,Random.Range(60,70));
				self.PrecipitationGraph.AddKey(7,Random.Range(60,70));
				self.PrecipitationGraph.AddKey(10,Random.Range(60,70));
				self.PrecipitationGraph.AddKey(12,Random.Range(60,70));
				*/

					
					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
				if(GUILayout.Button("Desert"))
				{
					
					self.precipitationOddsSpring = 10;
					self.precipitationOddsSummer = 5;
					self.precipitationOddsWinter = 10;
					self.precipitationOddsFall = 10;
					
					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
				if(GUILayout.Button("Mountainous"))
				{
					
					self.precipitationOddsSpring = 60;
					self.precipitationOddsSummer = 50;
					self.precipitationOddsWinter = 65;
					self.precipitationOddsFall = 60;
					
					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
				if(GUILayout.Button("Grassland"))
				{
					
					self.precipitationOddsSpring = 55;
					self.precipitationOddsSummer = 60;
					self.precipitationOddsWinter = 20;
					self.precipitationOddsFall = 20;
					
					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
				if(GUILayout.Button("Reset to Default settings"))
				{
					
					self.precipitationOddsSpring = 60;
					self.precipitationOddsSummer = 20;
					self.precipitationOddsFall = 40;
					self.precipitationOddsWinter = 80;

					if (self.TemperatureType == 0)
					{
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

					if (self.TemperatureType == 1)
					{
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
				
			
		
		}

    	
		if (GUI.changed) 
		{ 
			EditorUtility.SetDirty(self); 
		}
		

    
    }
    
	void ProgressBar (float value, string label) 
	{
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}
}
