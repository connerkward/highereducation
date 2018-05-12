
using UnityEditor;
using UnityEngine;
public class UniStormMenu : MonoBehaviour {
	
	[MenuItem ("Window/UniStorm/Documentation/Home", false, 500)]
	static void Home ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Home");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Documentation", false, 500)]
	static void Introduction ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Documentation");
	}
	
	[MenuItem ("Window/UniStorm/Help/Video Tutorials", false, 500)]
	static void VideoTutorials ()
	{
		Application.OpenURL("https://www.youtube.com/playlist?list=PLlyiPBj7FznYmPW9NR6U0WKudaeFuAgKL");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Video Tutorials", false, 500)]
	static void VideoTutorialsOther ()
	{
		Application.OpenURL("https://www.youtube.com/playlist?list=PLlyiPBj7FznYmPW9NR6U0WKudaeFuAgKL");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Tutorials", false, 500)]
	static void Tutorials ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Tutorials");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Code References", false, 500)]
	static void CodeReferences ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Code_References");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Example Scripts", false, 500)]
	static void ExampleScripts ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Example_Scripts");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Solutions to Possible Issues", false, 500)]
	static void Solutions ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/Solutions_to_Possible_Issues");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Realease Notes", false, 500)]
	static void PatchNotes ()
	{
		Application.OpenURL("http://unistorm-weather-system.wikia.com/wiki/UniStorm_Patch_Notes");
	}
	
	[MenuItem ("Window/UniStorm/Documentation/Forums", false, 500)]
	static void Forums ()
	{
		Application.OpenURL("http://forum.unity3d.com/threads/unistorm-v2-0-dynamic-day-night-weather-system-released-now-with-playable-demo.121021/");
	}
	
	[MenuItem ("Window/UniStorm/Help/Customer Support", false, 500)]
	static void CustomerService ()
	{
		Application.OpenURL("http://www.blackhorizonstudios.com/contact/");
	}

    // Add UniStorm to the menu bar.
	[MenuItem ("Window/UniStorm/Create Weather System/Desktop", false, 100)]
    static void InstantiateCVersion () {	
	   Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStormPrefabs/UniStormPrefab_C_Basic.prefab", typeof(GameObject))) as GameObject;
		
       codeInstantiatedPrefab.name = "UniStormDesktopPrefab_C_Basic";
	   codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
       Selection.activeGameObject = codeInstantiatedPrefab;
    }

	[MenuItem ("Window/UniStorm/Create Climate Zone/Desert Climate Zone", false, 300)]
	static void InstantiateDesertClimateZoneC () {
		
		Selection.activeObject = SceneView.currentDrawingSceneView;

		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Climate Zones/Desert Climate.prefab", typeof(GameObject))) as GameObject;
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		
		codeInstantiatedPrefab.name = "Desert Climate Zone";
	}

	[MenuItem ("Window/UniStorm/Create Climate Zone/Grassland Climate Zone", false, 300)]
	static void IInstantiateGrasslandClimateZoneC () {
		
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Climate Zones/Grassland Climate.prefab", typeof(GameObject))) as GameObject;
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		
		codeInstantiatedPrefab.name = "Grassland Climate Zone";
	}

	[MenuItem ("Window/UniStorm/Create Climate Zone/Mountain Climate Zone", false, 300)]
	static void IInstantiateMountainClimateZoneC () {
		
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Climate Zones/Mountain Climate.prefab", typeof(GameObject))) as GameObject;
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		
		codeInstantiatedPrefab.name = "Mountain Climate Zone";
	}

	[MenuItem ("Window/UniStorm/Create Climate Zone/Rainforest Climate Zone", false, 300)]
	static void IInstantiateRainforestClimateZoneC () {
		
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Climate Zones/Rainforest Climate.prefab", typeof(GameObject))) as GameObject;
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		
		codeInstantiatedPrefab.name = "Rainforest Climate Zone";
	}

	[MenuItem ("Window/UniStorm/Create Weather Forecaster/Desktop", false, 300)]
	static void WeatherForewcaster () {	
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Forecaster/Weather Forecaster.prefab", typeof(GameObject))) as GameObject;
		
		codeInstantiatedPrefab.name = "Weather Forecaster";
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		Selection.activeGameObject = codeInstantiatedPrefab;
	}

	[MenuItem ("Window/UniStorm/Create Weather Zone", false, 300)]
	static void InstantiateWeatherZoneC () {
		
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStorm Weather Zone/C#/Weather Zone C#.prefab", typeof(GameObject))) as GameObject;
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		codeInstantiatedPrefab.name = "Weather Zone C#";
	}
	
   

	
}