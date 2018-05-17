using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UniStormWeatherForecast))] 
public class UniStormWeatherForecastEditor : Editor 
{
	enum Monday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Tuesday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Wednesday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Thursday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Friday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Saturday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	enum Sunday
	{
		Foggy = 1, 
		LightRainOrLightSnowWinterOnly = 2, 
		ThunderStormOrSnowStormWinterOnly = 3, 
		PartlyCloudy = 4, 
		MostlyClear = 7,
		Sunny = 8, 
		//LightningBugsSummerOnly = 10,
		MostlyCloudy = 11, 
		HeavyRainNoThunder = 12,  
		//FallingLeavesFallOnly = 13
	}

	Monday menuMonday = Monday.Foggy;
	Tuesday menuTuesday = Tuesday.Foggy;
	Wednesday menuWednesday = Wednesday.Foggy;
	Thursday menuThursday= Thursday.Foggy;
	Friday menuFriday = Friday.Foggy;
	Saturday menuSaturday = Saturday.Foggy;
	Sunday menuSunday = Sunday.Foggy;

	public int[] WeatherTypes = {1, 2, 3, 4, 7, 8, 10, 11, 12, 13};

	public override void OnInspectorGUI () 
	{
		UniStormWeatherForecast self = (UniStormWeatherForecast)target;

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("UniStorm Weather Forecaster", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Below you can set the weather for each day of the week. UniStorm will update the weather at the start of each day. Using this system requires that dynamic weather is disabled (this is done automatically). After a week has been completed, UniStorm will generate another forecast for the week.", MessageType.None, true);

		EditorGUILayout.Space();

		menuMonday = (Monday)self.Monday;
		menuMonday = (Monday)EditorGUILayout.EnumPopup("Monday", menuMonday);
		self.Monday = (int)menuMonday;

		EditorGUILayout.Space();

		menuTuesday = (Tuesday)self.Tuesday;
		menuTuesday = (Tuesday)EditorGUILayout.EnumPopup("Tuesday", menuTuesday);
		self.Tuesday = (int)menuTuesday;

		EditorGUILayout.Space();

		menuWednesday = (Wednesday)self.Wednesday;
		menuWednesday = (Wednesday)EditorGUILayout.EnumPopup("Wednesday", menuWednesday);
		self.Wednesday = (int)menuWednesday;

		EditorGUILayout.Space();

		menuThursday = (Thursday)self.Thursday;
		menuThursday = (Thursday)EditorGUILayout.EnumPopup("Thursday", menuThursday);
		self.Thursday = (int)menuThursday;

		EditorGUILayout.Space();

		menuFriday = (Friday)self.Friday;
		menuFriday = (Friday)EditorGUILayout.EnumPopup("Friday", menuFriday);
		self.Friday = (int)menuFriday;

		EditorGUILayout.Space();

		menuSaturday = (Saturday)self.Saturday;
		menuSaturday = (Saturday)EditorGUILayout.EnumPopup("Saturday", menuSaturday);
		self.Saturday = (int)menuSaturday;

		EditorGUILayout.Space();

		menuSunday = (Sunday)self.Sunday;
		menuSunday = (Sunday)EditorGUILayout.EnumPopup("Sunday", menuSunday);
		self.Sunday = (int)menuSunday;

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Below you can adjust the weather odds using slider below. Weather will be generated based on the odds below when pressing the Generate Forecast button. The single slider determines the odds for both weather types. The higher the Clear Odds, the less chance of precpipitation. The lower the Clear Odds, the higher chance of precpitation.", MessageType.None, true);

		self.clearOdds = EditorGUILayout.IntSlider ("Clear Odds", self.clearOdds, 1, 100);

		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Odds of Clear Weather: " + (self.clearOdds) + "%", MessageType.None, true);
		EditorGUILayout.HelpBox("Odds of Precipitation: " + (100 - self.clearOdds) + "%", MessageType.None, true);

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Generate Forecast will generate a forecast based on the odds set above. Dynamic weather will be disabled and the generated weather above will be used for each UniStorm day.", MessageType.None, true);

		EditorGUILayout.Space();

		if(GUILayout.Button("Generate Forecast"))
		{
			self.GenerateForecast();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}
}
