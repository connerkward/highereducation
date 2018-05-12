using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class UniStormSetupEditor : EditorWindow
{
	public UniStormWeatherSystem_C UniStormSystem = null;
	public GameObject UniStormGameObject = null;
	public GameObject PlayerObject = null;

	public Camera PlayerCamera = null;
	public Camera PlayerCamera2 = null;

	public float secs = 1f;
	public float startVal = 0f;
	public float progress = 0f;

	bool Canceled = false;
	bool Uses2Cameras = false;

	SunShafts[] sunShaftsComponentCheck = null;
	Camera PlayerCameraComponent = null;

	enum CameraType
	{
		StandardCamera = 1,
		UFPS = 2,
		RFPS = 3,
		//UltimateSurvivalCharacterController = 4,
		OculusRift = 5,
		OpsiveThirdPersonController = 6,
		InvectorsThirdPersonController = 7
	}

	enum CharacterType
	{
		_FirstPerson = 1,
		_3rdPerson = 2
	}

	CameraType editorCameraType = CameraType.StandardCamera;
	int CameraTypeNumber = 1;

	CharacterType editorCharacterType = CharacterType._FirstPerson;
	int CharacterTypeNumber = 1;

	[MenuItem("Window/UniStorm/Auto Player Setup #%h", false, 200)]
	public static void ShowWindow()
	{
		EditorWindow APS = EditorWindow.GetWindow(typeof(UniStormSetupEditor));
		APS.maxSize = new Vector2(550f, 550f);
		APS.minSize = new Vector2(500f, 540f);
	}


	void OnInspectorUpdate() 
	{
		Repaint();
	}

	void OnGUI()
	{
		GUILayout.Label ("UniStorm Auto Player Setup C# - v2.0", EditorStyles.boldLabel);

		EditorGUILayout.HelpBox("UniStorm's Auto Player Setup makes it easier than ever to setup custom characters with UniStorm. Simply select your Camera Type and UniStorm will spawn a UniStorm system, apply all needed components, and set all needed settings.", MessageType.None, true);

		GUILayout.Space(15);

		EditorGUILayout.HelpBox("Please select your Camera Type.", MessageType.None, true);

		if (CameraTypeNumber == 1)
		{
			EditorGUILayout.HelpBox("The Standard Camera setup should be used if you aren't using one of the officially supported preset character controllers. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None);
			Uses2Cameras = false;
		}

		if (CameraTypeNumber == 2)
		{
			EditorGUILayout.HelpBox("The UFPS setup should be used if you are using the UFPS Character system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = true;
		}

		if (CameraTypeNumber == 3)
		{
			EditorGUILayout.HelpBox("The RFPS setup should be used if you are using the RFPS Character system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = false;
		}

		if (CameraTypeNumber == 4)
		{
			EditorGUILayout.HelpBox("The Ultimate Survival Character Controller setup should be used if you are using the Ultimate Survival Character Controller system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = true;
		}

		if (CameraTypeNumber == 5)
		{
			EditorGUILayout.HelpBox("The Oculus Rift setup should be used if you are using the Oculus Rift system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = false;
		}

		if (CameraTypeNumber == 6)
		{
			EditorGUILayout.HelpBox("The Optive Third Person Controller setup should be used if you are using the Optive Third Person Controller system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = false;
		}

		if (CameraTypeNumber == 7)
		{
			EditorGUILayout.HelpBox("The Invector Third Person Controller setup should be used if you are using the Invector Third Person Controller system. Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);
			Uses2Cameras = false;
		}

		editorCameraType = (CameraType)EditorGUILayout.EnumPopup("Camera Type", editorCameraType);
		CameraTypeNumber = (int)editorCameraType;

		if (CameraTypeNumber == 1)
		{
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your Player Object here. If you are using a first person controller, it would be the parent object of your camera not the camera itself.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);
			
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your Main Camera here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);
			
			PlayerCamera = (Camera)EditorGUILayout.ObjectField("Main Camera", PlayerCamera, typeof(Camera), true);

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Choose to use First Person or 3rd Person for your character setup.", MessageType.None, true);
			
			editorCharacterType = (CharacterType)EditorGUILayout.EnumPopup("Character Type", editorCharacterType);
			CharacterTypeNumber = (int)editorCharacterType;
		}
		
		if (CameraTypeNumber == 2)
		{
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your UFPS Player Object here.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your Cameras here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);

			PlayerCamera = (Camera)EditorGUILayout.ObjectField("FPS Camera", PlayerCamera, typeof(Camera), true);
			PlayerCamera2 = (Camera)EditorGUILayout.ObjectField("Weapon Camera", PlayerCamera2, typeof(Camera), true);

			if (PlayerCameraComponent == null) 
			{
				PlayerCameraComponent = PlayerCamera2;
			}
		}

		if (CameraTypeNumber == 3)
		{
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your RFPS Player Object here.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);
			
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your Main Camera here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);
			
			PlayerCamera = (Camera)EditorGUILayout.ObjectField("Main Camera", PlayerCamera, typeof(Camera), true);
		}

		if (CameraTypeNumber == 4)
		{
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your Ultimate Survival Player Object here.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);
			
			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your Cameras here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);

			PlayerCamera2 = (Camera)EditorGUILayout.ObjectField("World Camera", PlayerCamera2, typeof(Camera), true);
			PlayerCamera = (Camera)EditorGUILayout.ObjectField("FP Camera", PlayerCamera, typeof(Camera), true);

			if (PlayerCameraComponent == null) 
			{
				PlayerCameraComponent = PlayerCamera2;
			}
		}

		if (CameraTypeNumber == 5)
		{
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your Oculus Rift Player Object here. If you do not have a Player Object, you can use your Camera Object.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);
			
			GUILayout.Space(15);
			
			EditorGUILayout.HelpBox("Assign your main Oculus Camera here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);
			
			PlayerCamera = (Camera)EditorGUILayout.ObjectField("Oculus Rift Camera", PlayerCamera, typeof(Camera), true);
		}

		if (CameraTypeNumber == 6)
		{
			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your Player Object here.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("Player Object", PlayerObject, typeof(GameObject), true);

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your Main Camera here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);

			PlayerCamera = (Camera)EditorGUILayout.ObjectField("Main Camera", PlayerCamera, typeof(Camera), true);
		}

		if (CameraTypeNumber == 7)
		{
			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your 3rd Person Controller (Player Object) here.", MessageType.None, true);
			PlayerObject = (GameObject)EditorGUILayout.ObjectField("3rd Person Controller", PlayerObject, typeof(GameObject), true);

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Assign your 3rd Person Camera here and UniStorm will assign the needed components, as well as make the needed changes to your camera's settings.", MessageType.None, true);

			PlayerCamera = (Camera)EditorGUILayout.ObjectField("3rd Person Camera", PlayerCamera, typeof(Camera), true);
		}

		/*
		if (CameraTypeNumber != 4 || CameraTypeNumber != 2)
		{
			EditorGUI.BeginDisabledGroup ( PlayerObject == null && PlayerCamera == null 
				|| PlayerObject == null && PlayerCamera != null 
				|| PlayerObject != null && PlayerCamera == null);
		}
		*/

		EditorGUI.BeginDisabledGroup ( 
			PlayerObject == null && PlayerCamera == null && PlayerCamera2 == null && Uses2Cameras || 
			PlayerObject == null && PlayerCamera != null && PlayerCamera2 == null && Uses2Cameras || 
			PlayerObject == null && PlayerCamera == null && PlayerCamera2 != null && Uses2Cameras || 
			PlayerObject != null && PlayerCamera == null && PlayerCamera2 == null && Uses2Cameras || 
			PlayerObject != null && PlayerCamera != null && PlayerCamera2 == null && Uses2Cameras || 
			PlayerObject != null && PlayerCamera == null && PlayerCamera2 != null && Uses2Cameras || 
			PlayerObject == null && PlayerCamera == null && !Uses2Cameras || 
			PlayerObject == null && PlayerCamera != null && !Uses2Cameras || 
			PlayerObject != null && PlayerCamera == null && !Uses2Cameras );

		GUILayout.Space(15);

		EditorGUILayout.HelpBox("Note: You will need to apply an object to all fields before you will be able to complete the setup process.", MessageType.None, true);

		GUILayout.Space(5);
		
		if (GUILayout.Button("Auto Setup Player"))
		{
			Canceled = false;

			if (GameObject.Find("Snow Dust (UniStorm)") != null || GameObject.Find("Rain Mist (UniStorm)") != null || GameObject.Find("Splashes (UniStorm)") != null || GameObject.Find("Lightning Bugs (UniStorm)") != null || GameObject.Find("Windy Leaves (UniStorm)") != null || GameObject.Find("Lightning Position (UniStorm)") != null)
			{
				if (EditorUtility.DisplayDialog("Oops!", "You already have UniStorm Particle Effects in your scene. Can UniStorm's Auto Player Setup remove these to avoid duplicates? If you select no, you will need to remove all UniStorm particle effects from your scene (These include the ones on the UniStorm Demo Player).", "Yes, remove them", "No, don't remove these"))
				{
					GameObject Rain = GameObject.Find("Rain (UniStorm)");
					DestroyImmediate(Rain);
					
					GameObject Snow = GameObject.Find("Snow (UniStorm)");
					DestroyImmediate(Snow);
					
					GameObject SnowDust = GameObject.Find("Snow Dust (UniStorm)");
					DestroyImmediate(SnowDust);
					
					GameObject RainMist = GameObject.Find("Rain Mist (UniStorm)");
					DestroyImmediate(RainMist);
					
					GameObject RainSplashes = GameObject.Find("Rain Splashes (UniStorm)");
					DestroyImmediate(RainSplashes);
					
					GameObject LightningBugs = GameObject.Find("Lightning Bugs (UniStorm)");
					DestroyImmediate(LightningBugs);
					
					GameObject WindyLeaves = GameObject.Find("Windy Leaves (UniStorm)");
					DestroyImmediate(WindyLeaves);
					
					GameObject LightningSystem = GameObject.Find("Lightning Generator (UniStorm)");
					DestroyImmediate(LightningSystem);

					Canceled = false;
				}
				else
				{
					Canceled = true;
				}
			}
			
			if (GameObject.Find("UniStormSystemEditor") == null && !Canceled)
			{
				GameObject UniStormPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/UniStormPrefabs/UniStorm System C.prefab", typeof(GameObject));
				UniStormGameObject = ((GameObject)Instantiate (UniStormPrefab, PlayerObject.transform.position, Quaternion.identity));
			}

			if (!Canceled)
			{
				UniStormSystem = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();
				bool ObjectIsPrefab = false;

				if (PrefabUtility.GetPrefabType(PlayerObject) == PrefabType.PrefabInstance)
				{
					ObjectIsPrefab = true;
				}
				else
				{
					ObjectIsPrefab = false;
				}

				if (GameObject.Find("UniStormSystemEditor") != null && UniStormSystem == null)
				{
					EditorUtility.DisplayDialog("Oops!", "The Auto Player Setup found the UniStorm Editor, but it looks like you are using the wrong version. Please ensure that you are using the C# Auto Player Setup with the C# version of UniStorm and vise versa.", "Okay");
				}
				
				if (GameObject.Find("UniStormSystemEditor") != null && UniStormSystem != null)
				{
					#if UNITY_5_5 || UNITY_5_6
					RenderSettings.sun = UniStormSystem.sun;
					#endif

					if (CameraTypeNumber == 2 || CameraTypeNumber == 4)
					{
						if (ObjectIsPrefab)
						{
							if (EditorUtility.DisplayDialog("Break Prefab Instance?", "In order for UniStorm's Auto Player Setup to work properly, UniStorm must break the prefab connection with the Player Object.", "Okay", "Cancel"))
							{
								#if UNITY_EDITOR
								PrefabUtility.DisconnectPrefabInstance(PlayerObject);
								ObjectIsPrefab = false;
								#endif
							}
						}

						if (!ObjectIsPrefab && !Canceled)
						{
							startVal = (float)EditorApplication.timeSinceStartup;

							if (CameraTypeNumber == 2)
							{
								PlayerCamera.farClipPlane = 18200;
							}

							if (CameraTypeNumber == 4)
							{
								PlayerCamera2.farClipPlane = 18200;
							}

							UniStormSystem.cameraObject = PlayerCamera2.gameObject;
							sunShaftsComponentCheck = PlayerCamera2.GetComponents<SunShafts>();


							if (sunShaftsComponentCheck.Length == 0)
							{
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();

								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;
							}

							if (sunShaftsComponentCheck.Length == 1)
							{
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();

								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;
							}

							if (sunShaftsComponentCheck.Length == 2)
							{
								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;
							}

							ParticleSystem RainPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Rain.prefab", typeof(ParticleSystem));
							ParticleSystem Rain = ((ParticleSystem)Instantiate (RainPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Rain.gameObject.name = "Rain (UniStorm)";
							Rain.gameObject.transform.parent = PlayerObject.transform;
							Rain.gameObject.transform.localPosition = new Vector3 (0,32,0);
							Rain.gameObject.transform.localRotation = Quaternion.Euler (270,130,0);
							UniStormSystem.rain = Rain;
							UniStormSystem.rainSplashes = GameObject.Find("Rain (UniStorm)/Splashes (UniStorm)").GetComponent<ParticleSystem>();
							
							ParticleSystem RainMistPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Rain Mist.prefab", typeof(ParticleSystem));
							ParticleSystem RainMist = ((ParticleSystem)Instantiate (RainMistPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							RainMist.gameObject.name = "Rain Mist (UniStorm)";
							RainMist.gameObject.transform.parent = PlayerObject.transform;
							RainMist.gameObject.transform.localPosition = new Vector3 (0,12,5);
							RainMist.gameObject.transform.localRotation = Quaternion.Euler (270,191,0);
							UniStormSystem.rainMist = RainMist;

							ParticleSystem SnowPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Snow.prefab", typeof(ParticleSystem));
							ParticleSystem Snow = ((ParticleSystem)Instantiate (SnowPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Snow.gameObject.name = "Snow (UniStorm)";
							Snow.gameObject.transform.parent = PlayerObject.transform;
							Snow.gameObject.transform.localPosition = new Vector3 (0,10.3f,0);
							Snow.gameObject.transform.localRotation = Quaternion.Euler (270,310,0);
							UniStormSystem.snow = Snow;
							
							ParticleSystem SnowDustPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Snow Dust.prefab", typeof(ParticleSystem));
							ParticleSystem SnowDust = ((ParticleSystem)Instantiate (SnowDustPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							SnowDust.gameObject.name = "Snow Dust (UniStorm)";
							SnowDust.gameObject.transform.parent = PlayerObject.transform;
							SnowDust.gameObject.transform.localPosition = new Vector3 (0,12,5);
							SnowDust.gameObject.transform.localRotation = Quaternion.Euler (270,191,0);
							UniStormSystem.snowMistFog = SnowDust;
							
							ParticleSystem ButterfliesPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Lightning Bugs.prefab", typeof(ParticleSystem));
							ParticleSystem Butterflies = ((ParticleSystem)Instantiate (ButterfliesPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Butterflies.gameObject.name = "Lightning Bugs (UniStorm)";
							Butterflies.gameObject.transform.parent = PlayerObject.transform;
							Butterflies.gameObject.transform.localPosition = new Vector3 (0,-1,0);
							Butterflies.gameObject.transform.localRotation = Quaternion.Euler (270,243,0);
							UniStormSystem.butterflies = Butterflies;
							
							ParticleSystem WindyLeavesPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Windy Leaves.prefab", typeof(ParticleSystem));
							ParticleSystem WindyLeaves = ((ParticleSystem)Instantiate (WindyLeavesPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							WindyLeaves.gameObject.name = "Windy Leaves (UniStorm)";
							WindyLeaves.gameObject.transform.parent = PlayerObject.transform;
							WindyLeaves.gameObject.transform.localPosition = new Vector3 (0,18,0);
							WindyLeaves.gameObject.transform.localRotation = Quaternion.Euler (270,221,0);
							UniStormSystem.windyLeaves = WindyLeaves;
							
							GameObject LightningSystemPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/Lightning/Lightning Generator.prefab", typeof(GameObject));
							GameObject LightningSystem = ((GameObject)Instantiate (LightningSystemPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							LightningSystem.gameObject.name = "Lightning Generator (UniStorm)";
							LightningSystem.gameObject.transform.parent = PlayerObject.transform;
							LightningSystem.gameObject.transform.localPosition = new Vector3 (0,0,0);
							LightningSystem.gameObject.transform.localRotation = Quaternion.Euler (0,0,0);
							UniStormSystem.PlayerObject = PlayerObject;
							//UniStormSystem.LightningSystem = LightningSystem.GetComponent<LightningBolt>();
						}
					}

					if (CameraTypeNumber == 1 || CameraTypeNumber == 3 || CameraTypeNumber == 5 || CameraTypeNumber == 6 || CameraTypeNumber == 7)
					{
						if (ObjectIsPrefab)
						{
							if (EditorUtility.DisplayDialog("Break Prefab Instance?", "In order for UniStorm's Auto Player Setup to work properly, UniStorm must break the prefab connection with the Player Object.", "Okay", "Cancel"))
							{
								#if UNITY_EDITOR
								PrefabUtility.DisconnectPrefabInstance(PlayerObject);
								ObjectIsPrefab = false;
								Canceled = false;
								#endif
							}
							else
							{
								Canceled = true;
							}
						}

						if (!ObjectIsPrefab && !Canceled)
						{
							startVal = (float)EditorApplication.timeSinceStartup;

							//The Standard First Person and 3rd Person setup process is exactly the same.
							//This is if statement is intentional used to avoid confustion with users not seeing an option for a 3rd person setup.
							if (CameraTypeNumber == 1 && CharacterTypeNumber == 1 || CameraTypeNumber == 1 && CharacterTypeNumber == 2)
							{
								//PlayerCameraComponent = PlayerObject.GetComponentInChildren<Camera>();
								PlayerCameraComponent = PlayerCamera;
							}

							if (CameraTypeNumber == 3)
							{
								PlayerCameraComponent = PlayerCamera;
							}

							if (CameraTypeNumber == 5)
							{
								PlayerCameraComponent = PlayerCamera;
							}

							if (CameraTypeNumber == 6)
							{
								PlayerCameraComponent = PlayerCamera;
							}

							if (CameraTypeNumber == 7)
							{
								PlayerCameraComponent = PlayerCamera;
							}

							PlayerCameraComponent.farClipPlane = 18200;
							
						
							UniStormSystem.cameraObject = PlayerCameraComponent.gameObject;

							sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

							if (sunShaftsComponentCheck.Length == 0)
							{
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();

								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;
							}

							if (sunShaftsComponentCheck.Length == 1)
							{
								PlayerCameraComponent.gameObject.AddComponent<SunShafts>();

								if (CameraTypeNumber == 3){
									sunShaftsComponentCheck[0].enabled = true;
								}

								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;
							}

							if (sunShaftsComponentCheck.Length == 2)
							{
								sunShaftsComponentCheck = PlayerCameraComponent.GetComponents<SunShafts>();

								//Sun
								sunShaftsComponentCheck[0].sunThreshold = new Color(62f/255f, 62f/255f ,62f/255f);
								sunShaftsComponentCheck[0].sunColor = new Color(250f/255f, 173f/255f ,7f/255f);
								sunShaftsComponentCheck[0].maxRadius = 0.337f;
								sunShaftsComponentCheck[0].sunShaftBlurRadius = 4.54f;
								sunShaftsComponentCheck[0].radialBlurIterations = 2;
								sunShaftsComponentCheck[0].sunShaftIntensity = 2f;

								//Moon
								sunShaftsComponentCheck[1].sunThreshold = new Color(0f/255f, 0f/255f ,0f/255f);
								sunShaftsComponentCheck[1].sunColor = new Color(75f/255f, 75f/255f ,75f/255f);
								sunShaftsComponentCheck[1].maxRadius = 0.635f;
								sunShaftsComponentCheck[1].sunShaftBlurRadius = 1.0f;
								sunShaftsComponentCheck[1].radialBlurIterations = 1;
								sunShaftsComponentCheck[1].sunShaftIntensity = 2f;
							}

							ParticleSystem RainPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Rain.prefab", typeof(ParticleSystem));
							ParticleSystem Rain = ((ParticleSystem)Instantiate (RainPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Rain.gameObject.name = "Rain (UniStorm)";
							Rain.gameObject.transform.parent = PlayerObject.transform;
							Rain.gameObject.transform.localPosition = new Vector3 (0,32,0);
							Rain.gameObject.transform.localRotation = Quaternion.Euler (270,130,0);
							UniStormSystem.rain = Rain;
							UniStormSystem.rainSplashes = GameObject.Find("Rain (UniStorm)/Splashes (UniStorm)").GetComponent<ParticleSystem>();
							
							ParticleSystem RainMistPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Rain Mist.prefab", typeof(ParticleSystem));
							ParticleSystem RainMist = ((ParticleSystem)Instantiate (RainMistPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							RainMist.gameObject.name = "Rain Mist (UniStorm)";
							RainMist.gameObject.transform.parent = PlayerObject.transform;
							RainMist.gameObject.transform.localPosition = new Vector3 (0,12,5);
							RainMist.gameObject.transform.localRotation = Quaternion.Euler (270,191,0);
							UniStormSystem.rainMist = RainMist;

							ParticleSystem SnowPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Snow.prefab", typeof(ParticleSystem));
							ParticleSystem Snow = ((ParticleSystem)Instantiate (SnowPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Snow.gameObject.name = "Snow (UniStorm)";
							Snow.gameObject.transform.parent = PlayerObject.transform;
							Snow.gameObject.transform.localPosition = new Vector3 (0,10.3f,0);
							Snow.gameObject.transform.localRotation = Quaternion.Euler (270,310,0);
							UniStormSystem.snow = Snow;
							
							ParticleSystem SnowDustPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Snow Dust.prefab", typeof(ParticleSystem));
							ParticleSystem SnowDust = ((ParticleSystem)Instantiate (SnowDustPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							SnowDust.gameObject.name = "Snow Dust (UniStorm)";
							SnowDust.gameObject.transform.parent = PlayerObject.transform;
							SnowDust.gameObject.transform.localPosition = new Vector3 (0,12,5);
							SnowDust.gameObject.transform.localRotation = Quaternion.Euler (270,191,0);
							UniStormSystem.snowMistFog = SnowDust;
							
							ParticleSystem ButterfliesPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Lightning Bugs.prefab", typeof(ParticleSystem));
							ParticleSystem Butterflies = ((ParticleSystem)Instantiate (ButterfliesPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							Butterflies.gameObject.name = "Lightning Bugs (UniStorm)";
							Butterflies.gameObject.transform.parent = PlayerObject.transform;
							Butterflies.gameObject.transform.localPosition = new Vector3 (0,-1f,0);
							Butterflies.gameObject.transform.localRotation = Quaternion.Euler (270,243,0);
							UniStormSystem.butterflies = Butterflies;
							
							ParticleSystem WindyLeavesPrefab = (ParticleSystem) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/ParticleEffects/C#/Windy Leaves.prefab", typeof(ParticleSystem));
							ParticleSystem WindyLeaves = ((ParticleSystem)Instantiate (WindyLeavesPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							WindyLeaves.gameObject.name = "Windy Leaves (UniStorm)";
							WindyLeaves.gameObject.transform.parent = PlayerObject.transform;
							WindyLeaves.gameObject.transform.localPosition = new Vector3 (0,18,0);
							WindyLeaves.gameObject.transform.localRotation = Quaternion.Euler (270,221,0);
							UniStormSystem.windyLeaves = WindyLeaves;
							
							GameObject LightningSystemPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/UniStorm (Desktop)/Prefabs (Desktop)/Lightning/Lightning Generator.prefab", typeof(GameObject));
							GameObject LightningSystem = ((GameObject)Instantiate (LightningSystemPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation));
							LightningSystem.gameObject.name = "Lightning Generator (UniStorm)";
							LightningSystem.gameObject.transform.parent = PlayerObject.transform;
							LightningSystem.gameObject.transform.localPosition = new Vector3 (0,0,0);
							LightningSystem.gameObject.transform.localRotation = Quaternion.Euler (0,0,0);
							UniStormSystem.PlayerObject = PlayerObject;
							//UniStormSystem.LightningSystem = LightningSystem.GetComponent<LightningBolt>();
						}
					}


				}
			}
		}

		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(false);
		EditorGUI.EndDisabledGroup();

		if (CameraTypeNumber != 5 && CameraTypeNumber != 1)
		{
			GUILayout.Space(25);
			GUILayout.Label ("Need help setting up the " + editorCameraType.ToString() + " camera type?", EditorStyles.label);
			EditorGUILayout.HelpBox("If you need help setting up the " + editorCameraType.ToString() + " camera type," +" press the Watch Tutorial Video button below. We have tutorials for each Camera Type.", MessageType.None, true);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Watch Tutorial Video", GUILayout.Width(150), GUILayout.Height(20)))
			{
				//UFPS Tutorial Link
				if (CameraTypeNumber == 2)
				{
					Application.OpenURL ("https://www.youtube.com/watch?v=fNl_6hwnfvM");
				}

				//RFPS Tutorial Link
				if (CameraTypeNumber == 3)
				{
					Application.OpenURL ("https://www.youtube.com/watch?v=67G1y8XaSp8");
				}

				//Ultimate Survival Tutorial Link
				if (CameraTypeNumber == 4)
				{
					Application.OpenURL ("https://www.youtube.com/watch?v=67G1y8XaSp8");
				}

				//Opsive Third Person Controller Tutorial Link
				if (CameraTypeNumber == 6)
				{
					Application.OpenURL ("https://www.youtube.com/watch?v=mbOkkjC-FpM");
				}

				//Invector Third Person Controller Tutorial Link
				if (CameraTypeNumber == 7)
				{
					Application.OpenURL ("https://www.youtube.com/watch?v=9Ld9mZov9j0");
				}
			}
			GUILayout.EndHorizontal();
		}
		
		if (progress < secs)
		{
			EditorUtility.DisplayProgressBar("Auto Setting Up Player for UniStorm", "Auto assigning needed components...", progress / secs);
		}
			
		else
		{
			EditorUtility.ClearProgressBar();
		}

		progress = (float)(EditorApplication.timeSinceStartup - startVal);
	
	}

}
