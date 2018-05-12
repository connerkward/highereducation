
using UnityEditor;
using UnityEngine;
public class UniStormMobileMenu : MonoBehaviour {

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
	[MenuItem ("Window/UniStorm/Create Weather System/Mobile", false, 100)]
	static void InstantiateCVersion_Mobile () {	
		Selection.activeObject = SceneView.currentDrawingSceneView;
		
		GameObject codeInstantiatedPrefab = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Mobile)/Prefabs (Mobile)/UniStormMobilePrefabs/UniStormMobilePrefab_C_Basic.prefab", typeof(GameObject))) as GameObject;
		
		codeInstantiatedPrefab.name = "UniStormMobilePrefab_C_Basic";
		codeInstantiatedPrefab.transform.position = new Vector3 (0, 0, 0);
		Selection.activeGameObject = codeInstantiatedPrefab;
	}

}