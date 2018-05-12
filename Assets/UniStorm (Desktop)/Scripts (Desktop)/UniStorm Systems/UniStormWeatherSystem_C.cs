//UniStorm Weather System C# Version 2.4.1
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using System;
using UnityEngine.Events;

//CTS Support
#if CTS_PRESENT
using CTS;
#endif

[System.Serializable]
public class UniStormWeatherSystem_C : MonoBehaviour {

	[System.Serializable]
	public class UniStormEvents {
		public int WeatherTypeNumber = 1;
		public int TemperatureCalculationTypeNumber = 1;
		public bool UseTemperature = false;

		//These will be used for the next version of UniStorm for Weather Events
		public enum UniStormEventWeatherType{
			None = 0, Foggy = 1, LightRainOrLightSnowWinterOnly = 2, ThunderStormOrSnowStormWinterOnly = 3, PartlyCloudy = 4, MostlyClear = 7,Sunny = 8, LightningBugsSummerOnly = 10, MostlyCloudy = 11, HeavyRainNoThunder = 12, FallingLeavesFallOnly = 13
		}
		public enum TimeCalculationType{
			LessThan = 1, GreaterThan = 2, EqualTo = 3
		}
		public enum TemperatureCalculationType{
			LessThan = 1, GreaterThan = 2, EqualTo = 3
		}

		public int TimeCalculationTypeNumber = 3;
		public int EventTypeNumber = 1;
		public int RandomEventTypeNumber = 1;
		public int HourOfEvent = 12;
		public int DayOfEvent = 1;
		public int DayOfWeekEvent = 1;
		public int MonthOfEvent = 1;
		public bool CalculateOnStart = false;

		public enum UniStormRandomEventType{
			Daily = 1, Weekly = 2, Monthly = 3, Yearly = 4
		}
		public enum UniStormEventType{
			Hourly = 0, Daily = 1, Weekly = 2, Monthly = 3, Yearly = 4
		}
		public enum UniStormHourEvent{
			_0 = 0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23
		}
		public enum UniStormDayEvent{
			_1 = 1,_2,_3,_4,_5,_6,_7,_8,_9,_10,_11,_12,_13,_14,_15,_16,_17,_18,_19,_20,_21,_22,_23,_24,_25,_26,_27,_28,_29,_30,_31
		}
		public enum UniStormDayOfWeekEvent{
			Monday = 1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
		}
		public enum UniStormMonthEvent{
			January = 1,February = 2,March = 3,April = 4,May = 5,June = 6,July = 7,August = 8,September = 9,October = 10,November = 11,December = 12
		}
	}
	//Time Events
	public int EventTabNumber = 0;
	public int DocumentationNumber;
	public List<UnityEvent> TimeEvent = new List<UnityEvent>();
	public List<UniStormEvents> TotalUniStormTimeEvents = new List<UniStormEvents>();
	public List<string> TimeEventName = new List<string>();
	public List<bool> TimeEventFoldOut = new List<bool>();
	//Weather Events
	public List<UnityEvent> WeatherEvent = new List<UnityEvent>();
	public List<UniStormEvents> TotalUniStormWeatherEvents = new List<UniStormEvents>();
	public List<string> WeatherEventName = new List<string>();
	public List<int> WeatherEventTemperature = new List<int>();
	//Colors
	public Color snowColorTwilight;
	public Color snowColorMorning; 
	public Color snowColorDay; 
	public Color snowColorEvening; 
	public Color snowColorNight;
	public Color rainColorTwilight;
	public Color rainColorMorning; 
	public Color rainColorDay; 
	public Color rainColorEvening; 
	public Color rainColorNight;
	public Color particleMistColorTwilight;
	public Color particleMistColorMorning; 
	public Color particleMistColorDay; 
	public Color particleMistColorEvening; 
	public Color particleMistColorNight;
	public Color stormClouds1ColorTwilight;
	public Color stormClouds1ColorMorning; 
	public Color stormClouds1ColorDay; 
	public Color stormClouds1ColorEvening; 
	public Color stormClouds1ColorNight;
	public Color stormClouds2ColorTwilight;
	public Color stormClouds2ColorMorning; 
	public Color stormClouds2ColorDay; 
	public Color stormClouds2ColorEvening; 
	public Color stormClouds2ColorNight;
	public Color SkyTwilightColor;
	public Color SkyMorningColor;
	public Color SkyDayColor;
	public Color SkyEveningColor;
	public Color SkyNightColor;
	public Color EquatorTwilightColor;
	public Color EquatorMorningColor;
	public Color EquatorDayColor;
	public Color EquatorEveningColor;
	public Color EquatorNightColor;
	public Color GroundTwilightColor;
	public Color GroundMorningColor;
	public Color GroundDayColor;
	public Color GroundEveningColor;
	public Color GroundNightColor;
	public Color nightTintColor;
	public Color stormCloudColor1;
	public Color stormCloudColor2;
	public Color skyColorMorning; 
	public Color skyColorDay; 
	public Color skyColorEvening; 
	public Color skyColorNight;
	public Color cloudColorTwilight;
	public Color cloudColorMorning; 
	public Color cloudColorDay; 
	public Color cloudColorEvening; 
	public Color cloudColorNight;
	public Color stormyFogColorTwilight;
	public Color stormyFogColorMorning; 
	public Color stormyFogColorDay; 
	public Color stormyFogColorEvening; 
	public Color stormyFogColorNight; 
	public Color originalFogColorTwilight; 
	public Color originalFogColorMorning; 
	public Color originalFogColorDay; 
	public Color originalFogColorEvening; 
	public Color originalFogColorNight;
	public Color moonColorFade;
	public Color lightningColor;
	public Color MorningAmbientLight; //Ambient light colors
	public Color MiddayAmbientLight;
	public Color DuskAmbientLight;
	public Color TwilightAmbientLight;
	public Color NightAmbientLight;
	public Color SunMorning; //Sun colors
	public Color SunDay;
	public Color SunDusk;
	public Color SunNight;
	public Color fogTwilightColor; //Fog colors
	public Color fogMorningColor;
	public Color fogDayColor;
	public Color fogDuskColor;
	public Color fogNightColor;
	public Color moonColor;
	public Color MorningAtmosphericLight; //Atmospheric colors
	public Color MiddayAtmosphericLight;
	public Color DuskAtmosphericLight;
	public Color skyTintColor;
	public Color groundColor;
	public Color ProceduralLightningStartingLightningColor;
	public Color ProceduralLightningEndingLightningColor;
	Color rainMistColorControl;
	Color rainColorControl;
	Color snowColorControl;
	public Gradient FogColorTest;
	public Gradient StarColorGradient;
	//AnimationCurves
	public AnimationCurve moonIntensityCurve = AnimationCurve.Linear(0,0,24,5);
	public AnimationCurve sunIntensityCurve = AnimationCurve.Linear(0,0,24,5);
	public AnimationCurve TemperatureCurve = AnimationCurve.Linear(1,-100,13,125);
	public AnimationCurve PrecipitationGraph = AnimationCurve.Linear(1,0,13,100);
	public AnimationCurve TemperatureFluctuationn = AnimationCurve.Linear(0,-25,24,25);
	//Ints
	public int[] PrecipitationWeatherTypes = {1, 2, 3, 4, 12};
	public int[] ClearWeatherTypes = {4, 7, 8, 10, 11, 13};
	public int CurrentPrecipitationAmountInt;
	public int precipitationOdds = 70;
	public int precipitationOddsSpring = 30;
	public int precipitationOddsSummer = 80;
	public int precipitationOddsFall = 50;
	public int precipitationOddsWinter = 30;
	public int generatedOdds;
	public int HourToChangeWeather;
	public int NextForecast;
	public int AutoCalculateAmbientIntensity = 2;
	public int AmbientSource = 3;
	public int CloudDensity = 100;
	public int StormyMoonLightIntensity;
	public int startTimeMinute;
	public int environmentType;
	public int PrecipitationType = 1;
	public int generateDateAndTime = 1;
	public int springTemp = 0;
	public int summerTemp = 0;
	public int fallTemp = 0;
	public int winterTemp = 0;
	public int temperatureType = 1;
	public int temperatureControlType = 1;
	public int temperature_Celsius = 1;
	public int forceWeatherChange = 0;
	public int randomizedRainIntensity;
	public int currentGeneratedIntensity;
	public int lastWeatherType;
	public int moonShadowQuality = 2;
	public int timeToWaitMin;
	public int timeToWaitMax;
	public int nightLengthHour;
	public int dayLengthHour;
	public int nightLengthMinute;
	public int dayLengthMinute;
	public int StormySunIntensity;
	public int calendarType = 0;
	public int numberOfDaysInMonth = 31;
	public int numberOfMonthsInYear = 12;
	public int dayShadowType = 1;
	public int nightShadowType = 1;
	public int lightningShadowType = 1;
	public int maxHeavyRainMistIntensity = 20;
	public int cloudType = 1;
	public int minuteCounter = 1; //Time keeping variables
	public int hourCounter = 0; 
	public int dayCounter = 0; 
	public int monthCounter = 0; 
	public int yearCounter = 0; 
	public int temperature = 0; 
	public int cloudSpeed; //Cloud Speeds
	public int heavyCloudSpeed;
	public int starSpeed;
	public int weatherForecaster = 0; //Weather Forecaster
	public int maxLightRainIntensity = 400; //Max rain particles
	public int maxLightRainMistCloudsIntensity = 4;
	public int maxStormRainIntensity = 1000;
	public int maxLightSnowIntensity = 400;
	public int maxLightSnowDustIntensity = 4;
	public int maxSnowStormIntensity = 1000;
	public int maxHeavySnowDustIntensity = 15;
	public int minSpringTemp; //Temperature (Simple) variables
	public int maxSpringTemp;
	public int minSummerTemp;
	public int maxSummerTemp;
	public int minFallTemp;
	public int maxFallTemp;
	public int minWinterTemp;
	public int maxWinterTemp;
	public int startingSpringTemp;
	public int startingSummerTemp;
	public int startingFallTemp;
	public int startingWinterTemp;
	public int lightningMinChance = 5; //Lightning
	public int lightningMaxChance = 10;
	public int TabNumber = 1;
	public int weatherChanceSpring = 0; //Weather odds
	public int weatherChanceSummer = 0;
	public int weatherChanceFall = 0;
	public int weatherChanceWinter = 0;
	public int stormyFogDistance;
	public int stormyFogDistanceStart;
	public int fogStartDistance;
	public int fogEndDistance;
	public int lightningNumber;
	public int fogMode = 1;
	public int cloudDensity;
	public int moonPhaseCalculator = 3;
	public int rainNeededForLightning = 150;
	public int lightningFadeSpeed = 3;
	public int ProceduralLightningMinIntensity = 20; //Procedural Lighning
	public int ProceduralLightningMaxIntensity = 80;
	public int ProceduralLightningMinSpaceBetweenPoints = 20;
	public int ProceduralLightningMaxSpaceBetweenPoints = 40;
	public int ProceduralLightningMinWidth = 10;
	public int ProceduralLightningMaxWidth = 15;
	public int ProceduralLightningWidthMultiplier = 25;
	public int ProceduralLightningtotalPoints = 15;
	public int ProceduralLightningMillisecondsBetweenLines = 4;
	public int ProceduralLightningStrikeRadius = 2000;
	public int UseProceduralLightning = 1;
	//Floats
	public float rainSplashSize = 0.25f;
	public float RegularCloudFadeInMultiplier = 1;
	public float RegularCloudFadeOutMultiplier = 1;
	public float CloudFadeInMultiplier = 1;
	public float CloudFadeOutMultiplier = 1;
	public float SoundFadeInMultiplier = 1;
	public float SoundFadeOutMultiplier = 1;
	public float EffectsFadeInMultiplier = 1;
	public float EffectsFadeOutMultiplier = 1;
	public float FogDistanceFadeInMultiplier = 1;
	public float FogDistanceFadeOutMultiplier = 1;
	public float FogColorFadeInMultiplier = 1;
	public float FogColorFadeOutMultiplier = 1;
	public float ParticleEffectsFadeInMultiplier = 1;
	public float ParticleEffectsFadeOutMultiplier = 1;
	public float WindFadeInMultiplier = 1;
	public float WindFadeOutMultiplier = 1;
	public float TwilightAmbientIntensity = 1.0f;
	public float MorningAmbientIntensity = 0.75f;
	public float DayAmbientIntensity = 0.5f;
	public float EveningAmbientIntensity = 0.75f;
	public float NightAmbientIntensity = 0.5f;
	public float sunCalculator;
	public float startTimeHour;
	public float sunHeight = 0.95f;
	public float mostlyCloudyFloat;
	public float mostlyClearFloat;
	public float colorFogFadeInFloat;
	public float sunPosition = 90;
	public float stormGrassWavingSpeed;
	public float stormGrassWavingAmount;
	public float stormGrassWavingStrength;
	public float normalGrassWavingSpeed;
	public float normalGrassWavingAmount;
	public float normalGrassWavingStrength;
	public float ReflectionIntensityTwilight = 0.5f;
	public float ReflectionIntensityMorning = 0.5f;
	public float ReflectionIntensityDay = 0.5f;
	public float ReflectionIntensityEvening = 0.5f;
	public float ReflectionIntensityNight = 0.5f;
	public float ParticleUpdate;
	public float WindUpdate;
	public float TimeOfDayUpdate;
	public float windAmount;
	public float tempSunIntensity;
	public float tempMoonIntensity;
	public float sunIntensityCalculator;
	public float CurrentTemperatureFloat;
	public float ambientLightMultiplier = 0.75f; 
	public float PreciseCurveTime;
	public float roundingCorrection;
	public float CurrentPrecipitationAmountFloat = 1;
	public float CloudHeight = 1700.0f;
	public float CloudFade = 1699.0f;
	public float SunIntensityMultiplier = 1;
	public float minHeavyRainMistIntensity = 0;
	public float moonSize = 4;
	public float moonRotationY = 4;
	public float FogFadeInFloat = 0;
	public float FogFadeOutFloat = 0;
	public float TODSoundsTimer;
	private float LightningTimer; //Weather Timers
	public float LightningOnTime;
	public float lightningOdds;
	public float lightningFlashLength;
	public float minIntensity;
	public float maxIntensity;
	public float lightningIntensity;
	public float sunSize = 1;
	public float atmosphereThickness;
	public float exposure;
	public float moonLightIntensity;
	public float starRotationSpeed;
	public float dayLength; //Day and Night lengths
	public float nightLength;
	public float startTime;
	public float moonPhaseChangeTime;
	public float SnowAmount;
	public float sunIntensity; //Sun intensity control
	public float maxSunIntensity; 
	public float sunRevolution = -90; //Sun revolution control
	public float fogDensity;
	public float partlyCloudyFloat; //Cloud fading control
	public float dynamicPartlyCloudyFloat1;
	public float dynamicPartlyCloudyFloat2;
	public float dayShadowIntensity = 1;
	public float nightShadowIntensity;
	public float lightningShadowIntensity;
	private float StormCloudFadeFloat1 = 0; //Our fade number values
	private float StormCloudFadeFloat2 = 0;
	private float butterfliesFade = 0;
	private float windyLeavesIntensity = 0;
	private float sunShaftFade = 0;
	private float dynamicSnowFade = 0;
	private float forceStorm = 0;  //Storm Control
	private float changeWeather = 0; 
	public float currentRainIntensity; //Current precipitation intensities
	private float currentRainFogIntensity;
	public float currentSnowIntensity;
	public float currentSnowFogIntensity;
	public float Hour;
	public float minuteCounterCalculator = 0; 
	public float cloudHeight = 750; 
	//Bools
	public bool menuEnabled = false;
	public bool useInstantStartingWeather;
	public bool timeOptions = true;
	public bool caledarOptions = true;
	public bool skyOptions = true;
	public bool atmosphereOptions = true;
	public bool fogOptions = true;
	public bool lightningOptions = true;
	public bool temperatureOptions = true;
	public bool sunOptions = true;
	public bool moonOptions = true;
	public bool precipitationOptions = true;
	public bool GUIOptions = true;
	public bool soundManagerOptions = true;
	public bool colorOptions = true;
	public bool objectOptions = true;
	public bool helpOptions = true;
	public bool WindOptions = true;
	public bool terrainDetection = false;
	public bool UseRainSplashes = true;
	public bool UseRainMist = true;
	public bool useSunShafts = true;
	public bool usingMultipleTerrains = false;
	public bool ExportColors = true;
	public bool ExportSettings = true;
	public bool ImportColors = true;
	public bool ImportSettings = true;
	public bool MorningSoundsFoldOut = true;
	public bool DaySoundsFoldOut = true;
	public bool EveningSoundsFoldOut = true;
	public bool NightSoundsFoldOut = true;
	public bool disableTemperatureGeneration = false;
	public bool useMoonLightShafts = true;
	public bool hourlyUpdate = true;
	public bool precipitationWeatherTypeGenerated = false;
	public bool WeatherGenerated = false;
	public bool customMoonSize = false;
	public bool customMoonRotation = false;
	public bool weatherHappened = false;
	public bool playedSound = false;
	public bool useMorningSounds = false;
	public bool useDaySounds = false;
	public bool useEveningSounds = false;
	public bool useNightSounds = false;
	public bool stormControl = true;
	public bool timeStopped = false;
	public bool staticWeather = false;
	public bool timeScrollBar = false;
	public bool horizonToggle  = true;
	public bool dynamicSnowEnabled = true;
	public bool weatherCommandPromptUseable = false;
	public bool timeScrollBarUseable = false;
	public bool shadowsDuringDay = true;
	public bool shadowsDuringNight;
	public bool randomizedPrecipitation = false;
	public bool shadowsLightning;
	public bool useCustomPrecipitationSounds = false;
	public bool useUnityFog = true;
	private bool commandPromptActive = false;
	public bool isSpring;
	public bool isFall;
	public bool isSummer;
	public bool isWinter;
	public bool fadeLightning;
	public bool lightingGenerated = false;
	bool ParticlesDisabled = false;
	//Strings
	private string stringToEdit = "0";
	public string filePath; //Export Settings path
	public string fileName;
	public string weatherString;
	public string WeatherTypeGenerated;
	//GameObjects
	public GameObject sunObject;
	public GameObject mostlyClearClouds1; //Clouds game objects
	public GameObject partlyCloudyClouds1;
	public GameObject mostlyCloudyClouds1;
	public GameObject heavyClouds;
	public GameObject heavyCloudsLayerLight;
	public GameObject starSphere; //Star System game objects
	public GameObject moonObject;
	public GameObject rainSound; //Storm sound effects
	public GameObject windSound;
	public GameObject windSnowSound;
	public GameObject cameraObject;
	public GameObject windZone;
	public GameObject sunShaftsPositionObject;
	public GameObject sunTrans;
	public GameObject PlayerObject;
	//Renderers
	Renderer starSphereComponent;
	public Renderer moonObjectComponent;
	Renderer heavyCloudsComponent; //Dyanmic Clouds
	Renderer mostlyClearClouds1Component;
	Renderer heavyCloudsLayerLightComponent;
	Renderer partlyCloudyClouds1Component;
	Renderer mostlyCloudyClouds1Component;
	//Vector2s
	Vector2 uvAnimationRateA = new Vector2( 1.0f, 0.0f );
	Vector2 uvAnimationRateB = new Vector2( 1.0f, 0.0f );
	Vector2 uvAnimationRateStars = new Vector2( 1.0f, 0.0f );
	Vector2 uvAnimationRateHeavyA = new Vector2( 1.0f, 0.0f );
	Vector2 uvAnimationRateHeavyB = new Vector2( 1.0f, 0.0f );
	Vector2 uvOffsetA = Vector2.zero;
	Vector2 uvOffsetB = Vector2.zero;
	Vector2 uvOffsetC = Vector2.zero;
	Vector2 uvOffsetHeavyA = Vector2.zero;
	Vector2 uvOffsetHeavyB = Vector2.zero;
	Vector2 uvOffsetHeavyC = Vector2.zero;
	Vector2 uvOffsetStars = Vector2.zero;
	//AudioClips
	public AudioClip customRainSound;
	public AudioClip customRainWindSound;
	public AudioClip customSnowWindSound;
	//AudioSource Components
	AudioSource soundComponent;
	public AudioSource rainSoundComponent;
	public AudioSource windSoundComponent;
	public AudioSource windSnowSoundComponent;
	//Lists
	public List<AudioClip> ambientSoundsMorning = new List<AudioClip>();
	public List<AudioClip> ambientSoundsDay = new List<AudioClip>();
	public List<AudioClip> ambientSoundsEvening = new List<AudioClip>();
	public List<AudioClip> ambientSoundsNight = new List<AudioClip>();
	public List<AudioClip> ThunderSounds = new List<AudioClip>();
	public List<bool> foldOutList = new List<bool>();
	public List<string> WeatherCodes = new List<string>();
	public List<Material> MoonPhases = new List<Material>();
	//Materials
	public Material SkyBoxMaterial;
	Material starSphereMaterial;
	Material heavyCloudsMaterial;
	Material mostlyClearClouds1Material;
	Material heavyCloudsLayerLightMaterial;
	Material partlyCloudyClouds1Material;
	Material mostlyCloudyClouds1Material;
	//Weather game objects
	public ParticleSystem rain;
	public ParticleSystem rainMist;
	public ParticleSystem butterflies;
	public ParticleSystem rainSplashes;
	public ParticleSystem snow; 
	public ParticleSystem snowMistFog; 
	public ParticleSystem windyLeaves;
	//Lights
	public Light sun;
	public Light moon;
	public Light lightningLight;
	//Image Effects
	public SunShafts sunShaftScript;
	public SunShafts moonShaftScript;
	//Other Components
	public Camera cameraObjectComponent;
	public DateTime UniStormDate;
	public TextAsset CustomUniStormTxtFile;
	public Terrain currentTerrain;
	public TerrainData currentTerrainData;
	public LightningBolt LightningSystem;
	GradientColorKey[] gck;

	//CTS Support
	#if CTS_PRESENT
	CTSWeatherManager CTSWM;
	bool CTSActive = false;
	public bool UseCTS = true;
	public int SnowMeltingTemperature = 33;
	public float MaxWetness = 0.32f;
	public float MaxSnowAmount = 1f;
	public int SnowBuildUpSpeed = 5;
	public int SnowMeltSpeed = 5;
	public int RainBuildUpSpeed = 5;
	public int RainDrySpeed = 5;
	public Color SpringTint = new Color((147)/255.0f, (186)/255.0f, (120)/255.0f, 1);
	public Color SummerTint = new Color((193)/255.0f, (168)/255.0f, (129)/255.0f, 1);
	public Color FallTint = new Color((176)/255.0f, (166)/255.0f, (135)/255.0f, 1);
	public Color WinterTint = new Color((188)/255.0f, (188)/255.0f, (188)/255.0f, 1);
	public float CurrentSeasonalProgress;
	#endif
	
	void Awake (){
		if (useCustomPrecipitationSounds){
			rainSound.GetComponent<AudioSource>().clip = customRainSound;
			rainSound.GetComponent<AudioSource>().enabled = false;
			
			windSound.GetComponent<AudioSource>().clip = customRainWindSound;
			windSound.GetComponent<AudioSource>().enabled = false;
			
			windSnowSound.GetComponent<AudioSource>().clip = customSnowWindSound;
			windSnowSound.GetComponent<AudioSource>().enabled = false;
		}

		//CTS Support
		#if CTS_PRESENT
		if (UseCTS){
			CTSWM = GameObject.FindObjectOfType<CTSWeatherManager>();
			if (CTSWM == null){
				GameObject CreatedCTSWeatherManager = new GameObject();
				CreatedCTSWeatherManager.name = "CTS Weather Manager";
				CreatedCTSWeatherManager.AddComponent<CTSWeatherManager>();
				CTSWM = GameObject.FindObjectOfType<CTSWeatherManager>();
			}
		}
		#endif
	}
	
	void Start (){
		//Initialize and setup UniStorm
		InitializeUniStorm();
	}

	//CTS Support
	//Initializes CTS and sets the settings from UniStorm, if enabled
	public void InitializeCTS (){
		#if CTS_PRESENT
		if (CTSWM != null){
			if (currentRainIntensity > 50){
				CTSWM.RainPower = MaxWetness;
				CTSActive = true;
			}
			else if (currentSnowIntensity > 50){
				CTSWM.SnowPower = MaxSnowAmount;
				CTSActive = true;
			}
			else{
				CTSWM.RainPower = 0.01f;
				CTSWM.SnowPower = 0.01f;
			}
			
			CurrentSeasonalProgress = ((float)UniStormDate.DayOfYear / 365) * 4;
			CTSWM.Season = CurrentSeasonalProgress;
			
			CTSWM.SpringTint = SpringTint;
			CTSWM.SummerTint = SummerTint;
			CTSWM.AutumnTint = FallTint;
			CTSWM.WinterTint = WinterTint;
		}
		#endif
	}

	//CTS Support
	//Controls CTS weather transitions, when needed.
	public void CTSControl (){
		#if CTS_PRESENT
		if (CTSActive){
			if (currentRainIntensity > 50 && currentSnowIntensity < 50){
				CTSWM.RainPower += Time.deltaTime * (RainBuildUpSpeed * 0.001f);
				CTSWM.SnowPower -= Time.deltaTime * (SnowMeltSpeed * 0.001f);
				
				if (CTSWM.RainPower >= MaxWetness){
					CTSWM.RainPower = MaxWetness;
				}
				if (CTSWM.SnowPower <= 0.01f){
					CTSWM.SnowPower = 0.01f;
				}
			}
			
			else if (currentSnowIntensity > 50 && currentRainIntensity < 50){
				CTSWM.SnowPower += Time.deltaTime * (SnowBuildUpSpeed * 0.001f);
				CTSWM.RainPower -= Time.deltaTime * (RainDrySpeed * 0.001f);
				
				if (CTSWM.SnowPower >= MaxSnowAmount){
					CTSWM.SnowPower = MaxSnowAmount;
				}
				if (CTSWM.RainPower <= 0.01f){
					CTSWM.RainPower = 0.01f;
				}
			}
			
			else {
				if (temperature >= SnowMeltingTemperature){
					CTSWM.SnowPower -= Time.deltaTime * (SnowMeltSpeed * 0.001f);
					
					if (CTSWM.SnowPower <= 0.01f){
						CTSWM.SnowPower = 0.01f;
					}
				}
				
				CTSWM.RainPower -= Time.deltaTime * (RainDrySpeed * 0.001f);
				
				if (CTSWM.RainPower <= 0.01f){
					CTSWM.RainPower = 0.01f;
				}
			}
			
			if (currentRainIntensity == 0 && CTSWM.RainPower == 0.01f && currentSnowIntensity == 0 && CTSWM.SnowPower == 0.01f){
				CTSActive = false;
			}
		}
		#endif
	}

	public void InitializeUniStorm (){
		cloudType = 1;

		if (yearCounter <= 0){
			yearCounter = 1;
		}

		if (useCustomPrecipitationSounds){
			rainSound.GetComponent<AudioSource>().enabled = true;
			windSound.GetComponent<AudioSource>().enabled = true;
			windSnowSound.GetComponent<AudioSource>().enabled = true;
		}

		if (AmbientSource == 1){
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
		}
		else if (AmbientSource == 2){
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
		}
		else if (AmbientSource == 3){
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
		}

		//Call the GetAllComponents to assign all of our needed components
		GetAllComponents();

		//Apply the settings from the UniStorm Editor to the Skybox shader
		RenderSettings.skybox = SkyBoxMaterial;
		SkyBoxMaterial.SetColor("_SkyTint", skyTintColor);
		SkyBoxMaterial.SetColor("_GroundColor", groundColor);
		SkyBoxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
		SkyBoxMaterial.SetFloat("_Exposure", exposure);
		SkyBoxMaterial.SetColor("_NightSkyTint", nightTintColor);
		SkyBoxMaterial.SetFloat("_SunSize", (sunSize*0.01f));

		//Calculates our start time based off the user's input
		float startTimeMinuteFloat = (int)startTimeMinute;
		startTime = startTimeHour / 24 + startTimeMinuteFloat / 1440;
		HourToChangeWeather = UnityEngine.Random.Range(0,25);

		//If no terrain is found, set terrainDetection to false to disable certain terrain only features
		if (Terrain.activeTerrain == null){	
			terrainDetection = false;
		}

		if (Terrain.activeTerrain != null){	
			terrainDetection = true;
		}

		//If a terrain is found, set our wind values from the UniStorm Editor
		if (terrainDetection){
			currentTerrainData = Terrain.activeTerrain.terrainData;

			if (Terrain.activeTerrain.terrainData.wavingGrassSpeed <= normalGrassWavingSpeed || Terrain.activeTerrain.terrainData.wavingGrassSpeed >= normalGrassWavingSpeed){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
			}
			if (Terrain.activeTerrain.terrainData.wavingGrassAmount <= normalGrassWavingStrength || Terrain.activeTerrain.terrainData.wavingGrassAmount >= normalGrassWavingStrength){
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
			}
			if (Terrain.activeTerrain.terrainData.wavingGrassStrength <= normalGrassWavingAmount || Terrain.activeTerrain.terrainData.wavingGrassStrength >= normalGrassWavingAmount){
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		//Instantly fade in the clouds according to the current weather type
		if (cloudType == 1 && weatherForecaster == 4 || cloudType == 1 && weatherForecaster == 5 || cloudType == 1 && weatherForecaster == 6){
			partlyCloudyFloat = 1;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 0;
		}

		else if (cloudType == 1 && weatherForecaster == 7){
			partlyCloudyFloat = 0;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 0;
		}

		else if (cloudType == 1 && weatherForecaster == 8){
			partlyCloudyFloat = 0;
			colorFogFadeInFloat = 0;
			mostlyCloudyFloat = 0;
		}

		else if (cloudType == 1 && weatherForecaster == 11){
			partlyCloudyFloat = 1;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 1;
		}
			
		timeScrollBar = false;
		FogFadeOutFloat = 1;

		if (UseRainSplashes){
			rainSplashes.gameObject.SetActive(true);
		}

		if (!UseRainSplashes){
			rainSplashes.gameObject.SetActive(false);
		}

		if (!UseRainMist){
			rainMist.gameObject.SetActive(false);
			snowMistFog.gameObject.SetActive(false);
		}

		//Grab our originally set fog values for they're altered during stormy weather types
		originalFogColorTwilight = fogTwilightColor;
		originalFogColorMorning = fogMorningColor;
		originalFogColorDay = fogDayColor;
		originalFogColorEvening = fogDuskColor;
		originalFogColorNight = fogNightColor;

		//Setup our Unity fog settings
		if (useUnityFog){
			RenderSettings.fog = true;
		}

		if (fogMode == 1){
			RenderSettings.fogMode = FogMode.Linear;
		}

		else if (fogMode == 2){
			RenderSettings.fogMode = FogMode.Exponential;
		}

		else if (fogMode == 3){
			RenderSettings.fogMode = FogMode.ExponentialSquared;
		}

		//Set our light intensities
		moon.color = moonColor;
		lightningLight.color = lightningColor;

		if (UseRainSplashes){
			rainSplashes.startSize = rainSplashSize;
		}

		//Set dynamic cloud values
		uvAnimationRateA = new Vector2(0.0f, 0.001f);
		uvAnimationRateB = new Vector2(0.001f, 0.001f);
		uvAnimationRateHeavyA = new Vector2(0.005f, 0.001f);
		uvAnimationRateHeavyB = new Vector2(0.004f, 0.0035f);
		uvAnimationRateStars = new Vector2(0.1f, 0.0f);

		GenerateMostlyClear();
		GeneratePartlyCloudy();
		GenerateMostlyCloudy();

		CreateSun();
		CreateMoon();
		UpdateShadowSettings();

		if (RenderSettings.fogMode == FogMode.Linear){
			RenderSettings.fogStartDistance = fogStartDistance;
			RenderSettings.fogEndDistance = fogEndDistance;
		}

		CalculateMonths();
		CalculateSeaon();

		//A custom date time is created for UniStorm here and updated every day.
		if (calendarType == 1){
			UniStormDate = new DateTime(yearCounter, monthCounter, dayCounter, hourCounter, minuteCounter, 0);
		}

		//This algorithm uses an Animation curve to calculate the precipitation odds given the date from the Animation Curve
		roundingCorrection = UniStormDate.DayOfYear * 0.00273972602f;
		PreciseCurveTime = ((UniStormDate.DayOfYear / 28.07692307692308f)) + 1 - roundingCorrection;
		PreciseCurveTime = Mathf.Round(PreciseCurveTime * 10f) / 10f;

		CurrentPrecipitationAmountFloat = PrecipitationGraph.Evaluate(PreciseCurveTime);

		sun.intensity = sunIntensityCurve.Evaluate((float)Hour) * SunIntensityMultiplier;

		if (temperatureControlType == 1 && !disableTemperatureGeneration){
			TemperatureGeneration();
		}

		else if (temperatureControlType == 2 & !disableTemperatureGeneration){
			CurrentTemperatureFloat = TemperatureCurve.Evaluate(PreciseCurveTime);
			temperature = (int)TemperatureCurve.Evaluate(PreciseCurveTime) + (int)TemperatureFluctuationn.Evaluate((float)startTimeHour);
		}

		if (PrecipitationType == 2){
			CurrentPrecipitationAmountInt = (int)Mathf.Round(CurrentPrecipitationAmountFloat);
			precipitationOdds = CurrentPrecipitationAmountInt;
		}

		if (useInstantStartingWeather){
			InstantWeather();
		}

		if (timeStopped){
			if (Hour >= dayLengthHour && Hour < nightLengthHour) {
				startTime = startTime + Time.deltaTime/dayLength/60 * 0.5f;
			}

			if (Hour >= nightLengthHour || Hour < dayLengthHour){
				startTime = startTime + Time.deltaTime/nightLength/60 * 0.5f;
			}
		}

		//Intialize weather codes
		int WeatherCode = 1;
		for (int i = 0; i < 13; i++){
			WeatherCode = i + 1;
			WeatherCodes.Add(WeatherCode.ToString());
		}

		//Applies the cloud settings from the UniStorm Editor
		mostlyClearClouds1Material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));
		partlyCloudyClouds1Material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));
		mostlyCloudyClouds1Material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));
		
		mostlyClearClouds1Material.SetFloat("_LoY", CloudHeight);
		mostlyClearClouds1Material.SetFloat("_HiY", CloudFade);
		
		partlyCloudyClouds1Material.SetFloat("_LoY", CloudHeight);
		partlyCloudyClouds1Material.SetFloat("_HiY", CloudFade);
		
		mostlyCloudyClouds1Material.SetFloat("_LoY", CloudHeight);
		mostlyCloudyClouds1Material.SetFloat("_HiY", CloudFade);

		//Calculates our time on Start	
		Hour = startTime * 24;
		hourCounter = (int)Hour;
		minuteCounterCalculator = Hour*60f;	
		minuteCounter = (int)minuteCounterCalculator % 60;

		//Calls the events function to check for events that are calculated on Start 
		TimeOfDayCalculator();
		CalculateEvents ();
		lightningOdds = UnityEngine.Random.Range(lightningMinChance, lightningMaxChance);

		GameObject LightningObject = GameObject.Find("Lightning Generator (UniStorm)");

		//Check for the procedural lightning system. If it isn't
		//found, disable UniStorm's procedural lightning.
		if (LightningObject != null){
			LightningSystem = LightningObject.GetComponent<LightningBolt>();
			if (PlayerObject != null){
				SetLightningOptions();
			}
		}
		else{
			UseProceduralLightning = 2;
		}

		if (PlayerObject == null){
			UseProceduralLightning = 2;
		}

		//CTS Support
		#if CTS_PRESENT
		if (UseCTS){
			InitializeCTS();
		}
		#endif
	}

	//Sets our procedural lightning options from UniStorm
	//to the procedural lightning system
	void SetLightningOptions ()
	{
		if (UseProceduralLightning == 1){
			LightningSystem.InitializeLightning();
			LightningSystem.PlayerPos = PlayerObject.transform;
			LightningSystem.MinIntensity = ProceduralLightningMinIntensity;
			LightningSystem.MaxIntensity = ProceduralLightningMaxIntensity;
			LightningSystem.MinSpaceBetweenPoints = ProceduralLightningMinSpaceBetweenPoints;
			LightningSystem.MaxSpaceBetweenPoints = ProceduralLightningMaxSpaceBetweenPoints;
			LightningSystem.MinWidth = ProceduralLightningMinWidth;
			LightningSystem.MaxWidth = ProceduralLightningMaxWidth;
			LightningSystem.MillisecondsBetweenLines = ProceduralLightningMillisecondsBetweenLines;
			LightningSystem.StrikeRadius = ProceduralLightningStrikeRadius;
			LightningSystem.StartingLightningColor = ProceduralLightningStartingLightningColor;
			Color EndingLighningColor = ProceduralLightningEndingLightningColor;
			EndingLighningColor.a = 0;
			LightningSystem.EndingLightningColor = EndingLighningColor;

			#if UNITY_5_5_OR_NEWER
			LightningSystem.lr.widthMultiplier = ProceduralLightningWidthMultiplier;
			LightningSystem.lr.startColor = ProceduralLightningStartingLightningColor;
			LightningSystem.lr.endColor = ProceduralLightningStartingLightningColor;
			gck = new GradientColorKey[1];
			gck[0].color = ProceduralLightningStartingLightningColor;
			LightningSystem.LightningGradient.SetKeys(gck, LightningSystem.LightningGradient.alphaKeys);
			LightningSystem.lr.colorGradient = LightningSystem.LightningGradient;
			#else
			LightningSystem.lr.SetColors(ProceduralLightningStartingLightningColor,ProceduralLightningStartingLightningColor);
			#endif
		}
		else
		{
			LightningSystem.enabled = false;
		}
	}

	/// <summary>
	/// Updates UniStorm's shadow settings when called.
	/// </summary>
	public void UpdateShadowSettings (){

		//Controls shadows for both day and night light sources
		if (shadowsDuringDay){
			if (dayShadowType == 1){
				sun.shadows = LightShadows.Hard;
			}
			
			if (dayShadowType == 2){
				sun.shadows = LightShadows.Soft;
			}
			
			sun.shadowStrength = dayShadowIntensity;
		}
		
		if (!shadowsDuringDay){
			sun.shadows = LightShadows.None;
		}
		
		if (shadowsDuringNight){
			if (nightShadowType == 1){
				moon.shadows = LightShadows.Hard;
			}
			
			if (nightShadowType == 2){
				moon.shadows = LightShadows.Soft;
			}
			
			moon.shadowStrength = nightShadowIntensity;
		}
		
		if (!shadowsDuringNight){
			moon.shadows = LightShadows.None;
		}
		
		if (shadowsLightning){
			if (lightningShadowType == 1){
				lightningLight.shadows = LightShadows.Hard;
			}
			
			if (lightningShadowType == 2){
				lightningLight.shadows = LightShadows.Soft;
			}
			
			lightningLight.shadowStrength = lightningShadowIntensity;
		}
		
		if (!shadowsLightning){
			lightningLight.shadows = LightShadows.None;
		}
	}

	void CreateSun (){
		//UniStorm now uses our procedural Skybox sun.
		//Below, we create a gameobject as a reference point for our sun's position
		//Once it's created, we assign it to the Sun Shafts Sun Transform
		sunTrans = GameObject.Find("Light Parent");

		//Only apply the Sun Shafts setting if Sun Shafts are enabled
		if (useSunShafts){
			sunShaftsPositionObject = new GameObject();
			sunShaftsPositionObject.name = "Sun Transform";
			sunShaftsPositionObject.transform.parent = sun.transform; 
			sunShaftsPositionObject.transform.localPosition = new Vector3 (0,0,-999999);
			sunShaftsPositionObject.transform.localRotation = Quaternion.Euler (0,0,0);
			sunShaftsPositionObject.transform.localScale = new Vector3 (1,1,1);

			//Don't try to assign sun if no Sun Shafts component is found
			if (cameraObjectComponent.GetComponent<SunShafts>() != null){
				cameraObjectComponent.GetComponent<SunShafts>().sunTransform = sunShaftsPositionObject.transform;
			}
		}
	}

	void CreateMoon (){
		//UniStorm's moon is now assigned and positioned at runtime.
		moonObject.transform.parent = moon.transform; 
		moonObject.transform.localPosition = new Vector3 (0,0,-11000);

		moonObject.transform.localEulerAngles = new Vector3(270, 0, 0);

		if (customMoonSize){
			moonObject.transform.localScale = new Vector3(moonSize*10, moonSize*10, moonSize*10);
		}

		//If useMoonLightShafts is enabled, assign light reference.
		//Don't try to assign light reference if a second Sun Shafts component isn't found
		if (useMoonLightShafts){
			if (cameraObjectComponent.GetComponent<SunShafts>() != null){
				SunShafts[] sunShaftsComponent = cameraObjectComponent.GetComponents<SunShafts>();

				if (sunShaftsComponent.Length >= 2){
					sunShaftsComponent[1].sunTransform = moonObject.transform;
					moonShaftScript = sunShaftsComponent[1];
				}
				//If a second sun shafts script isn't found, disable Moonlight Shafts
				else{
					useMoonLightShafts = false;
				}
			}
		}

		//If a second Sun Shafts component is found, but useMoonLightShafts is disabled, disable the second Sun Shafts
		if (!useMoonLightShafts){
			if (cameraObjectComponent.GetComponent<SunShafts>() != null){
				SunShafts[] sunShaftsComponent = cameraObjectComponent.GetComponents<SunShafts>();

				if (sunShaftsComponent.Length >= 2){
					sunShaftsComponent[1].enabled = false;
				}
			}
		}

		//Run the MoonPhaseCalculator function to assign the moon phase
		MoonPhaseCalculator();
	}

	//Gets all our needed components on Start
	void GetAllComponents (){
		if (lightningLight == null){
			//Lightning Light
			lightningLight = GameObject.Find("Lightning Light").GetComponent<Light>();
		}

		//Renderer Components
		heavyCloudsComponent = heavyClouds.GetComponent<Renderer>();; 
		starSphereComponent = starSphere.GetComponent<Renderer>();
		mostlyClearClouds1Component = mostlyClearClouds1.GetComponent<Renderer>();
		heavyCloudsLayerLightComponent = heavyCloudsLayerLight.GetComponent<Renderer>();
		partlyCloudyClouds1Component = partlyCloudyClouds1.GetComponent<Renderer>();
		mostlyCloudyClouds1Component = mostlyCloudyClouds1.GetComponent<Renderer>();

		//Moob Object
		moonObjectComponent = moonObject.GetComponent<Renderer>();

		mostlyClearClouds1Material = mostlyClearClouds1Component.material;
		partlyCloudyClouds1Material = partlyCloudyClouds1Component.material;
		mostlyCloudyClouds1Material = mostlyCloudyClouds1Component.material;
	
		starSphereMaterial = starSphereComponent.material;
		heavyCloudsLayerLightMaterial = heavyCloudsLayerLightComponent.material;
		heavyCloudsMaterial = heavyCloudsComponent.material;

		//Sound Components
		this.gameObject.AddComponent<AudioSource>();
		soundComponent = GetComponent<AudioSource>();
		rainSoundComponent = rainSound.GetComponent<AudioSource>();
		windSoundComponent = windSound.GetComponent<AudioSource>();
		windSnowSoundComponent = windSnowSound.GetComponent<AudioSource>();
		
		//Camera Component
		cameraObjectComponent = cameraObject.GetComponent<Camera>();

		//SunShafts Component
		sunShaftScript = cameraObjectComponent.GetComponent<SunShafts>();
		
		if (sunShaftScript == null){
			Debug.LogError("Please apply a C# Sun Shaft Script to your camera GameObject.");
		}
	}
		
	void Update () {
		//UniStorm 2.2 uses an Animation Curve for calculating the intenisty of the sun.
		//This allows user to control the precise intentensity throughout the day all the way up till the time the sun sets.
		//This allows the sun to be as bright as needed up until it sets where the intensity can be faded to 0 based on the exact time it's needed.
		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13){
			if (tempSunIntensity >= sunIntensityCurve.Evaluate((float)Hour)){
				sun.intensity = sunIntensityCurve.Evaluate((float)Hour) * SunIntensityMultiplier;
				tempSunIntensity = 1;
			}

			if (tempMoonIntensity >= moonIntensityCurve.Evaluate((float)Hour)){
				tempMoonIntensity = 1;
				moon.intensity = moonIntensityCurve.Evaluate((float)Hour);
			}
				
			if (tempSunIntensity < sunIntensityCurve.Evaluate((float)Hour)){
				tempSunIntensity += Time.deltaTime * 0.03f * EffectsFadeInMultiplier;

				if (tempSunIntensity >= 1){
					tempSunIntensity = 1;
				}

				sun.intensity = (sunIntensityCurve.Evaluate((float)Hour) * tempSunIntensity) * SunIntensityMultiplier;
			}
				
			if (tempMoonIntensity < moonIntensityCurve.Evaluate((float)Hour)){
				tempMoonIntensity += Time.deltaTime * 0.03f * EffectsFadeInMultiplier;

				if (tempMoonIntensity >= 1){
					tempMoonIntensity = 1;
				}

				moon.intensity = moonIntensityCurve.Evaluate((float)Hour) * tempMoonIntensity;
			}

		}

		//This handles fading the sun and moon according to the percentage allowed vai the editor.
		//If the sun's setting is %50, then the sun intensity will be faded to %50 intensity during precipitation weather types.
		//The same applies to the moon with the moon settings
		else if (weatherForecaster == 1 || weatherForecaster == 2 || weatherForecaster == 3 || weatherForecaster == 12 || weatherForecaster == 9){
			tempSunIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;

			if (tempSunIntensity <= StormySunIntensity * 0.01f){
				tempSunIntensity = StormySunIntensity * 0.01f;
			}

			sun.intensity = (sunIntensityCurve.Evaluate((float)Hour) * tempSunIntensity) * SunIntensityMultiplier;

			tempMoonIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;

			if (tempMoonIntensity <= StormyMoonLightIntensity * 0.01f){
				tempMoonIntensity = StormyMoonLightIntensity * 0.01f;
			}

			moon.intensity = moonIntensityCurve.Evaluate((float)Hour) * tempMoonIntensity;


			if (moon.intensity > 0.01f){
				moon.color = moonColor;
			}
		}
		
		if (moon.intensity > 0.0f){
			moon.color = moonColor;
		}

		if (weatherForecaster == 2 && UseRainMist){
			if (minHeavyRainMistIntensity <= 0){
				rainMist.gameObject.SetActive(false);
			}
		}

		if (weatherForecaster == 3 && UseRainMist || weatherForecaster == 12 && UseRainMist){
			if (minHeavyRainMistIntensity >= 1 && !ParticlesDisabled){
				rainMist.gameObject.SetActive(true);
			}
		}

		//Calculates our time
		hourCounter = (int)Hour;
		minuteCounterCalculator = Hour*60f;	
		minuteCounter = (int)minuteCounterCalculator % 60;
		
		//Controls wether the weather command prompt is enabled or disabled	
		if (weatherCommandPromptUseable == true){
			if(Input.GetKeyDown(KeyCode.U)){
				commandPromptActive = !commandPromptActive;
			}
		} 

		//Toggles the Time Scroll Bar to control the time of day while in-game
		if (timeScrollBarUseable == true){
			if(Input.GetKeyDown(KeyCode.U)){
				timeScrollBar = !timeScrollBar;
			}
		} 
		
		if (weatherCommandPromptUseable == false){ 
			commandPromptActive = false;
		}
		
		if (monthCounter == -1){
			monthCounter = 11;
		}

		if (moonShaftScript != null){
			moonShaftScript.sunShaftIntensity = moon.intensity * 2;
		}

		if (moonShaftScript == null){
			useMoonLightShafts = false;
		}
		
		//Calculates our minutes into hours
		//Any hourly event can be created by watching yhr hourlyUpdate variable
		if(minuteCounter <= 0 && !hourlyUpdate){	
			if (temperatureControlType == 2 && !disableTemperatureGeneration){
				temperature = (int)TemperatureCurve.Evaluate(PreciseCurveTime) + (int)TemperatureFluctuationn.Evaluate((float)hourCounter);
			}

			if (temperatureControlType == 1 && !disableTemperatureGeneration){
				TemperatureGeneration();
			}

			if (hourCounter == 12){
				moonPhaseCalculator += 1;
				MoonPhaseCalculator();
			}
		
			CalculateEvents();

			hourlyUpdate = true;
		}

		if (minuteCounter >= 1 && minuteCounter <= 59){
			hourlyUpdate = false;
		}

		CalculateMonths();
		CalculateSeaon();

		//Calculates our day length so it stays consistent	
		Hour = startTime * 24;

		//This adds support for night length
		//If timeStopped is checked, time doesn't flow
		if (timeStopped == false){	
			if (Hour >= dayLengthHour && Hour < nightLengthHour) {
				startTime = startTime + Time.deltaTime/dayLength/60 * 0.5f;
			}
			
			if (Hour >= nightLengthHour || Hour < dayLengthHour){
				startTime = startTime + Time.deltaTime/nightLength/60 * 0.5f;
			}
		}

		sunCalculator = Hour / 24;
		
		if (startTime >= 1.0f){
			startTime = 0;
			CalculateDays();
		}
		
		//Forces precipitation if none has happened in an in-game week, prevents drouts
		if (forceStorm >= 7){
			if (staticWeather == false){	
				weatherForecaster = UnityEngine.Random.Range(2,3);
				forceStorm = 0;
			}
		}
		
		//Changes our weather type if there has been precipitation for more than 3 in-game days
		if (changeWeather >= forceWeatherChange && stormControl == true){
			if (staticWeather == false){	
				weatherForecaster = UnityEngine.Random.Range(4,11);
				changeWeather = 0;
			}
		}

		//Controls fading in and out our fog during weather transitions
		if (weatherForecaster == 1 || weatherForecaster == 2 || weatherForecaster == 3 || weatherForecaster == 12 || weatherForecaster == 9){
			//New fade multiplier
			FogFadeInFloat += Time.deltaTime * 0.05f * FogColorFadeInMultiplier; 
			FogFadeOutFloat -= Time.deltaTime * 0.05f * FogColorFadeOutMultiplier; 
			
			if (FogFadeOutFloat <= 0){
				FogFadeOutFloat = 0;
			}
			
			if (FogFadeInFloat >= 1){
				FogFadeInFloat = 1;
				weatherHappened = true;
			}

			fogTwilightColor = Color.Lerp (originalFogColorTwilight, stormyFogColorTwilight, FogFadeInFloat);
			fogMorningColor = Color.Lerp (originalFogColorMorning, stormyFogColorMorning, FogFadeInFloat);
			fogDayColor = Color.Lerp (originalFogColorDay, stormyFogColorDay, FogFadeInFloat);
			fogDuskColor = Color.Lerp (originalFogColorEvening, stormyFogColorEvening, FogFadeInFloat);
			fogNightColor = Color.Lerp (originalFogColorNight, stormyFogColorNight, FogFadeInFloat);
		}
	
		else if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13){
			FogFadeOutFloat += Time.deltaTime * 0.05f * FogColorFadeInMultiplier; 
			FogFadeInFloat -= Time.deltaTime * 0.05f * FogColorFadeOutMultiplier; 

			fogTwilightColor = Color.Lerp (stormyFogColorTwilight, originalFogColorTwilight, FogFadeOutFloat);
			fogMorningColor = Color.Lerp (stormyFogColorMorning, originalFogColorMorning, FogFadeOutFloat);
			fogDayColor = Color.Lerp (stormyFogColorDay, originalFogColorDay, FogFadeOutFloat);
			fogDuskColor = Color.Lerp (stormyFogColorEvening, originalFogColorEvening, FogFadeOutFloat);
			fogNightColor = Color.Lerp (stormyFogColorNight, originalFogColorNight, FogFadeOutFloat);
			
			if (FogFadeOutFloat >= 1){
				weatherHappened = false;
				FogFadeOutFloat = 1;
			}
			
			if (FogFadeInFloat <= 0){
				FogFadeInFloat = 0;
			}
		}

		//Assigns our generated weather when the hour is reached
		if (WeatherGenerated){
			if (hourCounter == HourToChangeWeather){
				if (WeatherTypeGenerated == "Non-Precipitation"){
					weatherForecaster = NextForecast;
					WeatherGenerated = false;
				}

				if (WeatherTypeGenerated == "Precipitation"){
					weatherForecaster = NextForecast;
					WeatherGenerated = false;
				}
			}
		}

		TimeOfDayUpdate += Time.deltaTime;
		ParticleUpdate += Time.deltaTime;

		if (TimeOfDayUpdate > 0.05f){
			TimeOfDayCalculator();
			TimeOfDayUpdate = 0;
		}
	
		DynamicTimeOfDaySounds ();

		//CTS Support
		#if CTS_PRESENT
		if (UseCTS){
			if (currentRainIntensity > 50 || currentSnowIntensity > 50){
				CTSActive = true;
			}
			
			if (CTSActive){
				CTSControl ();
			}
		}
		#endif
	}

	void CalculateEvents (){
		//Handles our hourly events
		if (TotalUniStormTimeEvents.Count > 0){
			for (int i = 0; i < TotalUniStormTimeEvents.Count; i++){
				//Calculates our Yearly Events
				if (TotalUniStormTimeEvents[i].HourOfEvent == hourCounter && TotalUniStormTimeEvents[i].DayOfEvent == dayCounter && TotalUniStormTimeEvents[i].MonthOfEvent == monthCounter && TotalUniStormTimeEvents[i].EventTypeNumber != 0 && TotalUniStormTimeEvents[i].EventTypeNumber != 2){
					TimeEvent[i].Invoke();
				}

				//Calculates our Monthly Events
				else if (TotalUniStormTimeEvents[i].HourOfEvent == hourCounter && TotalUniStormTimeEvents[i].DayOfEvent == dayCounter && TotalUniStormTimeEvents[i].EventTypeNumber == 3){
					TimeEvent[i].Invoke();
				}

				//Calculates our Weekly Events
				else if (TotalUniStormTimeEvents[i].HourOfEvent == hourCounter && TotalUniStormTimeEvents[i].EventTypeNumber == 2 && TotalUniStormTimeEvents[i].DayOfWeekEvent == (int)UniStormDate.DayOfWeek){
					TimeEvent[i].Invoke();
				}

				//Calculates our Daily events (Less Than)
				else if (TotalUniStormTimeEvents[i].TimeCalculationTypeNumber == 1 && hourCounter <= TotalUniStormTimeEvents[i].HourOfEvent && TotalUniStormTimeEvents[i].EventTypeNumber == 1){
					TimeEvent[i].Invoke();
					TotalUniStormTimeEvents[i].TimeCalculationTypeNumber = 3;
				}

				//Calculates our Daily events on Start (Greater Than)
				else if (TotalUniStormTimeEvents[i].TimeCalculationTypeNumber == 2 && hourCounter >= TotalUniStormTimeEvents[i].HourOfEvent && TotalUniStormTimeEvents[i].EventTypeNumber == 1){
					TimeEvent[i].Invoke();
					TotalUniStormTimeEvents[i].TimeCalculationTypeNumber = 3;
				}

				//Calculates our Daily events on Start (Equal To)
				else if (TotalUniStormTimeEvents[i].TimeCalculationTypeNumber == 3 && TotalUniStormTimeEvents[i].HourOfEvent == hourCounter && TotalUniStormTimeEvents[i].EventTypeNumber == 1){
					TimeEvent[i].Invoke();
				}

				//Calculates our Hourly Events
				else if (TotalUniStormTimeEvents[i].EventTypeNumber == 0 && minuteCounter == 0){
					TimeEvent[i].Invoke();
				}
			}
		}
	}


	void FixedUpdate (){
		CloudController();
		//Rotates our sun using quaternion rotations so the angles don't coincide (sunAngle angles the sun based off the user's input)
		sunTrans.transform.eulerAngles = new Vector3(startTime * 360 - 90, sunRevolution, 180 + sunRevolution);
	}

	//This function is called once per UniStorm day.
	//When called, it uses the Precipitation Odds from the UniStorm Editor to generate the odds for weather. The odds are based on percentage of precipitation.
	//If the generated odds are greater than the precipitation odds, UniStorm will generate weather based on the precipitation weather types.
	//If the generated odds are less than or equal to the precipitation odds, UniStorm will generate weather based on the non-precipitation weather types.
	//Finally, UniStorm genertates an hour that the weather change will happen. When that hour is reached, the weather will change according to what was generated here.
	/// <summary>
	/// Generates a weather type using UniStorm's weather generating algorithm that takes in account the weather odds and time. Note: The generated weather will not happen right away. UniStorm will generate a weather change that will happen in within the next 1 to 24 hours.
	/// </summary>
	public void GenerateWeather (){
		if (!staticWeather){
			generatedOdds = UnityEngine.Random.Range(1, 101);
			
			if (generatedOdds > precipitationOdds){
				HourToChangeWeather = UnityEngine.Random.Range(0,23);
				WeatherTypeGenerated = "Non-Precipitation";
				NextForecast = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
				WeatherGenerated = true;
			}
			
			if (generatedOdds <= precipitationOdds){
				HourToChangeWeather = UnityEngine.Random.Range(0,23);
				WeatherTypeGenerated = "Precipitation";
				NextForecast = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
				WeatherGenerated = true;
			}
		}
	}

	//Handles all of the lerp and time related changes
	void TimeOfDayCalculator (){
		if (useMoonLightShafts){
			if (moonShaftScript.sunShaftIntensity <= 0.01f){
				moonShaftScript.enabled = false;
			}
			else if (moonShaftScript.sunShaftIntensity > 0.01f){
				moonShaftScript.enabled = true;
			}
		}

		starSphereMaterial.color = StarColorGradient.Evaluate((float)Hour / 24);

		if (StormCloudFadeFloat1 > 0.01f){
		ParticleColorController();
		}
		
		if (hourCounter == 5){
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorNight, cloudColorTwilight, Mathf.Lerp(0,1, Hour - 5));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorNight, stormClouds1ColorTwilight, Mathf.Lerp(0,1, Hour - 5));
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorNight, stormClouds2ColorTwilight, Mathf.Lerp(0,1, Hour - 5));

				rainMistColorControl = Color.Lerp (particleMistColorNight, particleMistColorTwilight, Mathf.Lerp(0,1, Hour - 5));
				rainColorControl = Color.Lerp (rainColorNight, rainColorTwilight, Mathf.Lerp(0,1, Hour - 5));
				snowColorControl = Color.Lerp (snowColorNight, snowColorTwilight, Mathf.Lerp(0,1, Hour - 5));
			}

			RenderSettings.fogColor = Color.Lerp (fogNightColor, fogTwilightColor, Mathf.Lerp(0,1, Hour - 5));
		}

		else if (hourCounter == 6){
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorTwilight, cloudColorMorning, Mathf.Lerp(0,1, Hour - 6));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorTwilight, stormClouds1ColorMorning, Mathf.Lerp(0,1, Hour - 6));
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorTwilight, stormClouds2ColorMorning, Mathf.Lerp(0,1, Hour - 6));

				rainMistColorControl = Color.Lerp (particleMistColorTwilight, particleMistColorMorning, Mathf.Lerp(0,1, Hour - 6));
				snowColorControl = Color.Lerp (snowColorTwilight, snowColorMorning, Mathf.Lerp(0,1, Hour - 6));
				rainColorControl = Color.Lerp (rainColorTwilight, rainColorMorning, Mathf.Lerp(0,1, Hour - 6));
			}

			RenderSettings.fogColor = Color.Lerp (fogTwilightColor, fogMorningColor, Mathf.Lerp(0,1, Hour - 6));
		}

		else if (hourCounter >= 7 && hourCounter < 8){
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorMorning, cloudColorDay, Mathf.Lerp(0,1, Hour - 7));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorMorning, stormClouds1ColorDay, Mathf.Lerp(0,1, Hour - 7));
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorMorning, stormClouds2ColorDay, Mathf.Lerp(0,1, Hour - 7));

				rainMistColorControl = Color.Lerp (particleMistColorMorning, particleMistColorDay, Mathf.Lerp(0,1, Hour - 7));
				snowColorControl = Color.Lerp (snowColorMorning, snowColorDay, Mathf.Lerp(0,1, Hour - 7));
				rainColorControl = Color.Lerp (rainColorMorning, rainColorDay, Mathf.Lerp(0,1, Hour - 7));
			}

			RenderSettings.fogColor = Color.Lerp (fogMorningColor, fogDayColor, Mathf.Lerp(0,1, Hour - 7));
		}

		else if (hourCounter == 16){
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorDay, cloudColorEvening, Mathf.Lerp(0,1, Hour - 16));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorDay, stormClouds1ColorEvening, Mathf.Lerp(0,1, Hour - 16));
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorDay, stormClouds2ColorEvening, Mathf.Lerp(0,1, Hour - 16));

				rainMistColorControl = Color.Lerp (particleMistColorDay, particleMistColorEvening, Mathf.Lerp(0,1, Hour - 16));
				snowColorControl = Color.Lerp (snowColorDay, snowColorEvening, Mathf.Lerp(0,1, Hour - 16));
				rainColorControl = Color.Lerp (rainColorDay, rainColorEvening, Mathf.Lerp(0,1, Hour - 16));
			}
			
			RenderSettings.fogColor = Color.Lerp (fogDayColor, fogDuskColor, Mathf.Lerp(0,1, Hour - 16));
		}
	
		else if (hourCounter >= 17 && hourCounter < 18){
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorEvening, cloudColorTwilight, Mathf.Lerp(0,1, Hour - 17));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorEvening, stormClouds1ColorTwilight, Mathf.Lerp(0,1, Hour - 17));
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorEvening, stormClouds2ColorTwilight, Mathf.Lerp(0,1, Hour - 17));

				rainMistColorControl = Color.Lerp (particleMistColorEvening, particleMistColorTwilight, Mathf.Lerp(0,1, Hour - 17));
				snowColorControl = Color.Lerp (snowColorEvening, snowColorTwilight, Mathf.Lerp(0,1, Hour - 17));
				rainColorControl = Color.Lerp (rainColorEvening, rainColorTwilight, Mathf.Lerp(0,1, Hour - 17));
			}
			
			RenderSettings.fogColor = Color.Lerp (fogDuskColor, fogTwilightColor, Mathf.Lerp(0,1, Hour - 17));
		}

		//Calculates Twilight
		if(Hour > 2 && Hour < 4){
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityNight, ReflectionIntensityTwilight, (Hour/2)-1); 
			
			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyNightColor, SkyTwilightColor, (Hour/2)-1);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorNightColor, EquatorTwilightColor, (Hour/2)-1);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundNightColor, GroundTwilightColor, (Hour/2)-1);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (NightAmbientLight, TwilightAmbientLight, (Hour/2)-1);
			}
			
			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, TwilightAmbientIntensity,(Hour/2)-1);
			}
		}

		//Calculates morning fading in from night
		else if(Hour > 4 && Hour < 6){
			sun.color = Color.Lerp (SunNight, SunMorning, (Hour/2)-2);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorMorning, (Hour/2)-2));
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityTwilight, ReflectionIntensityMorning, (Hour/2)-2); 

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyTwilightColor, SkyMorningColor, (Hour/2)-2);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorTwilightColor, EquatorMorningColor, (Hour/2)-2);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundTwilightColor, GroundMorningColor, (Hour/2)-2);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (TwilightAmbientLight, MorningAmbientLight, (Hour/2)-2);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, MorningAmbientIntensity,(Hour/2)-2);
			}

			if (sunShaftScript != null){
				sunShaftScript.sunColor = Color.Lerp (DuskAtmosphericLight, MorningAtmosphericLight, (Hour/2)-2);
			}
		}

		//Calculates Morning
		else if(Hour > 6 && Hour < 8){
			sun.color = Color.Lerp (SunMorning, SunDay, (Hour/2)-3);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorMorning, skyColorDay, (Hour/2)-3));
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityMorning, ReflectionIntensityDay, (Hour/2)-3); 

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyMorningColor, SkyDayColor, (Hour/2)-3);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorMorningColor, EquatorDayColor, (Hour/2)-3);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundMorningColor, GroundDayColor, (Hour/2)-3);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (MorningAmbientLight, MiddayAmbientLight, (Hour/2)-3);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(MorningAmbientIntensity, DayAmbientIntensity,(Hour/2)-3);
			}

			if (sunShaftScript != null){
				sunShaftScript.sunColor = Color.Lerp (MorningAtmosphericLight, MiddayAtmosphericLight, (Hour/2)-3);
			}
		}

		//Calculates Day
		else if(Hour > 8 && Hour < 16){
			sun.color = Color.Lerp (SunDay, SunDay, (Hour/2)-4);
			RenderSettings.fogColor = Color.Lerp (fogDayColor, fogDayColor, (Hour/2)-4);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorDay, skyColorDay, (Hour/2)-4));
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorDay, cloudColorDay, (Hour/2)-4);
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityDay, ReflectionIntensityDay, (Hour/2)-4); 

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorDay, stormClouds1ColorDay, (Hour/2)-4);
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorDay, stormClouds2ColorDay, (Hour/2)-4);

				rainMistColorControl = Color.Lerp (particleMistColorDay, particleMistColorDay, (Hour/2)-4);
				snowColorControl = Color.Lerp (snowColorDay, snowColorDay, (Hour/2)-4);
				rainColorControl = Color.Lerp (rainColorDay, rainColorDay, (Hour/2)-4);
			}

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyDayColor, SkyDayColor, (Hour/2)-4);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorDayColor, EquatorDayColor, (Hour/2)-4);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundDayColor, GroundDayColor, (Hour/2)-4);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (MiddayAmbientLight, MiddayAmbientLight, (Hour/2)-4);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(DayAmbientIntensity, DayAmbientIntensity,(Hour/2)-4);
			}

			if (sunShaftScript != null){
				sunShaftScript.sunColor = Color.Lerp (MiddayAtmosphericLight, MiddayAtmosphericLight, (Hour/2)-4);
			}
		}

		//Calculates dusk fading in from day
		else if(Hour > 16 && Hour < 18){
			sun.color = Color.Lerp (SunDay, SunDusk, (Hour/2)-8);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorDay, skyColorEvening, (Hour/2)-8));
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityDay, ReflectionIntensityEvening, (Hour/2)-8); 

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyDayColor, SkyEveningColor, (Hour/2)-8);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorDayColor, EquatorEveningColor, (Hour/2)-8);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundDayColor, GroundEveningColor, (Hour/2)-8);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (MiddayAmbientLight, DuskAmbientLight, (Hour/2)-8);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(DayAmbientIntensity, EveningAmbientIntensity,(Hour/2)-8);
			}

			if (sunShaftScript != null){
				sunShaftScript.sunColor = Color.Lerp (MiddayAtmosphericLight, DuskAtmosphericLight, (Hour/2)-8);
			}
		}

		//Calculates night fading in from dusk
		else if(Hour > 18 && Hour < 20){
			sun.color = Color.Lerp (SunDusk, SunNight, (Hour/2)-9);
			RenderSettings.fogColor = Color.Lerp (fogTwilightColor, fogNightColor, (Hour/2)-9);
			RenderSettings.fogColor = Color.Lerp (fogTwilightColor, fogNightColor, (Hour/2)-9);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorEvening, skyColorNight, (Hour/2)-9));
			mostlyClearClouds1Material.color = Color.Lerp (cloudColorTwilight, cloudColorNight, (Hour/2)-9);
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityEvening, ReflectionIntensityTwilight, (Hour/2)-9);

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = Color.Lerp (stormClouds1ColorTwilight, stormClouds1ColorNight, (Hour/2)-9);
				heavyCloudsLayerLightMaterial.color = Color.Lerp (stormClouds2ColorTwilight, stormClouds2ColorNight, (Hour/2)-9);

				rainMistColorControl = Color.Lerp (particleMistColorTwilight, particleMistColorNight, (Hour/2)-9);
				snowColorControl = Color.Lerp (snowColorTwilight, snowColorNight, (Hour/2)-9);
				rainColorControl = Color.Lerp (rainColorTwilight, rainColorNight, (Hour/2)-9);
			}

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyEveningColor, SkyTwilightColor, (Hour/2)-9);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorEveningColor, EquatorTwilightColor, (Hour/2)-9);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundEveningColor, GroundTwilightColor, (Hour/2)-9);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (DuskAmbientLight, TwilightAmbientLight, (Hour/2)-9);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(EveningAmbientIntensity, NightAmbientIntensity,(Hour/2)-9);
			}

			if (sunShaftScript != null){
				sunShaftScript.sunColor = Color.Lerp (DuskAtmosphericLight, DuskAtmosphericLight, (Hour/2)-9);
			}
		}

		else if(Hour > 20 && Hour < 22){
			RenderSettings.fogColor = Color.Lerp (fogNightColor, fogNightColor, (Hour/2)-10f);
			RenderSettings.reflectionIntensity = Mathf.Lerp (ReflectionIntensityTwilight, ReflectionIntensityNight, (Hour/2)-10);

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = Color.Lerp (SkyTwilightColor, SkyNightColor, (Hour/2)-10);
				RenderSettings.ambientEquatorColor = Color.Lerp (EquatorTwilightColor, EquatorNightColor, (Hour/2)-10);
				RenderSettings.ambientGroundColor = Color.Lerp (GroundTwilightColor, GroundNightColor, (Hour/2)-10);
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = Color.Lerp (TwilightAmbientLight, NightAmbientLight, (Hour/2)-10f);
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(TwilightAmbientIntensity, NightAmbientIntensity,(Hour/2)-10);
			}
		}

		else if (Hour > 22){
			RenderSettings.fogColor = fogNightColor;
			RenderSettings.reflectionIntensity = ReflectionIntensityNight;

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = SkyNightColor;
				RenderSettings.ambientEquatorColor = EquatorNightColor;
				RenderSettings.ambientGroundColor = GroundNightColor;
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = NightAmbientLight;
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = NightAmbientIntensity;
			}
		}

		//Calculates Night
		if(Hour > 20){
			sun.color = Color.Lerp (SunNight, SunNight, (Hour/2)-10);	
			mostlyClearClouds1Material.color = cloudColorNight;
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorNight, (Hour/2)-10));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = stormClouds1ColorNight;
				heavyCloudsLayerLightMaterial.color = stormClouds2ColorNight;

				rainMistColorControl = particleMistColorNight;
				snowColorControl = snowColorNight;
				rainColorControl = rainColorNight;
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, NightAmbientIntensity,(Hour/2)-10);
			}
		}

		if (Hour >= 0 && Hour <= 4){
			mostlyClearClouds1Material.color = cloudColorNight;
			RenderSettings.fogColor = fogNightColor;

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = stormClouds1ColorNight;
				heavyCloudsLayerLightMaterial.color = stormClouds2ColorNight;

				rainMistColorControl = particleMistColorNight;
				snowColorControl = snowColorNight;
				rainColorControl = rainColorNight;
			}
		}

		else if (Hour <= 5){
			RenderSettings.fogColor = fogNightColor;
			mostlyClearClouds1Material.color = cloudColorNight;
		}

		if (Hour >= 0 && Hour <= 2){
			RenderSettings.fogColor = fogNightColor;
			RenderSettings.reflectionIntensity = ReflectionIntensityNight;

			if (AmbientSource == 2){
				RenderSettings.ambientSkyColor = SkyNightColor;
				RenderSettings.ambientEquatorColor = EquatorNightColor;
				RenderSettings.ambientGroundColor = GroundNightColor;
			}
			else if (AmbientSource == 3){
				RenderSettings.ambientLight = NightAmbientLight;
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = NightAmbientIntensity;
			}
		}

		if(Hour < 4){
			sun.color = Color.Lerp (SunNight, SunNight, (Hour/2)-2);	
			mostlyClearClouds1Material.color = cloudColorNight;
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorNight, (Hour/2)-10));

			if (StormCloudFadeFloat1 > 0.01f){
				heavyCloudsMaterial.color = stormClouds1ColorNight;
				heavyCloudsLayerLightMaterial.color = stormClouds2ColorNight;

				rainMistColorControl = particleMistColorNight;
				snowColorControl = snowColorNight;
				rainColorControl = rainColorNight;
			}

			if (AutoCalculateAmbientIntensity == 2){
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, NightAmbientIntensity,(Hour/2)-2);
			}	
		}

		if (AutoCalculateAmbientIntensity == 1){
			RenderSettings.ambientIntensity = RenderSettings.ambientLight.grayscale * ambientLightMultiplier;
		}
	}
	
	//Puts all fading in and out weather types into 2 function then picks by weather type to control individual weather factors
	void WeatherForecaster () {	
		//Foggy 
		if (weatherForecaster == 1){
			FadeInPrecipitation();
			weatherString = "Foggy";
		}
		//Light Snow / Light Rain
		else if (weatherForecaster == 2){
			FadeInPrecipitation();

			if (temperature >= 33){
				weatherString = "Light Rain";
			}

			else if (temperature <= 32){
				weatherString = "Light Snow";
			}
		}
		//Heavy Snow
		else if (weatherForecaster == 3){
			FadeInPrecipitation();

			if (temperature >= 33){
				weatherString = "Heavy Rain & Thunder Storm";
			}
			
			else if (temperature <= 32){
				weatherString = "Heavy Snow";
			}
		}
		//Partly Cloudy
		else if (weatherForecaster == 4){
			FadeOutPrecipitation ();
			weatherString = "Partly Cloudy";
		}
		//Partly Cloudy
		else if (weatherForecaster == 5){
			FadeOutPrecipitation ();
			weatherForecaster = 3;
		}
		//Partly Cloudy
		else if (weatherForecaster == 6){
			FadeOutPrecipitation ();
			weatherForecaster = 7;
		}
		//Mostly Clear
		else if (weatherForecaster == 7){
			FadeOutPrecipitation();
			weatherString = "Mostly Clear";
		}
		//Clear
		else if (weatherForecaster == 8){
			FadeOutPrecipitation ();
			weatherString = "Clear";
		}
		//Mostly Cloudy
		else if (weatherForecaster == 11){
			FadeOutPrecipitation ();
			weatherString = "Mostly Cloudy";
		}
		//Cloudy/Foggy
		else if (weatherForecaster == 9){
			weatherForecaster = 1;
		}
		//Lighning Bugs (Summer Only)
		else if (weatherForecaster == 10 && isSummer){
			FadeOutPrecipitation ();
			weatherString = "Lighning Bugs";
		}	
		else if (weatherForecaster == 10 && !isSummer){
			weatherForecaster = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}
		//Heavy Rain (No Thunder)
		else if (weatherForecaster == 12){
			FadeInPrecipitation ();
			weatherString = "Heavy Rain (No Thunder)";
		}
		//Falling Fall Leaves
		else if (weatherForecaster == 13 && isFall){
			FadeOutPrecipitation ();
			weatherString = "Falling Fall Leaves";
		}
		else if (weatherForecaster == 13 && !isFall){
			weatherForecaster = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}
	}

	//Gives our users an in-game GUI to change the weather and time
	void OnGUI (){
		//Allows a scrolling GUI bar that controls the time of day by the user
		if (timeScrollBar == true){
			startTime = GUI.HorizontalSlider(new Rect(25, 25, 200, 30), startTime, 0.0F, 0.99F);	
		}

		//Allows a text GUI box that controls the weather based
		if (commandPromptActive == true){
			stringToEdit = GUI.TextField (new Rect (10, 430, 40, 20), stringToEdit, 10);
			
			if(GUI.Button(new Rect(10, 450, 60, 40), "Change")){
				weatherCommandPrompt ();
			}
		}
	}
	
	//Calculates our weather command prompts
	void weatherCommandPrompt (){
		weatherForecaster = 1+(int)WeatherCodes.IndexOf(stringToEdit);
		WeatherForecaster();
		print(weatherString);
	}	
	
	//Calculates our lightning generation
	void Lightning () {
		LightningTimer += Time.deltaTime;

		if (LightningTimer >= lightningOdds && !lightingGenerated){
			lightingGenerated = true;
			lightningLight.enabled = true;

			//Only use procedural lightning if it's enabled
			if (UseProceduralLightning == 1){
			LightningSystem.GenerateLightningPoints(); //Call the procedural lightning system to generate a lightning bolt.
			}

			soundComponent.PlayOneShot(ThunderSounds[UnityEngine.Random.Range(0,ThunderSounds.Count)]);
		}
		
		if (lightingGenerated){
			if (!fadeLightning){
				lightningLight.intensity += Time.deltaTime * lightningFadeSpeed;
			}
			
			if (lightningLight.intensity >= lightningIntensity && !fadeLightning){
				lightningLight.intensity = lightningIntensity;
				fadeLightning = true;
			}
		}
		
		if (fadeLightning){
			LightningOnTime += Time.deltaTime;
			
			if (LightningOnTime >= lightningFlashLength){
				lightningLight.intensity -= Time.deltaTime * lightningFadeSpeed;
			}
			
			if (lightningLight.intensity <= 0){
				lightningLight.intensity = 0;
				fadeLightning = false;
				lightingGenerated = false;
				LightningTimer = 0;
				LightningOnTime = 0;
				lightningLight.enabled = false;
				lightningLight.transform.rotation = Quaternion.Euler (UnityEngine.Random.Range(10,40), UnityEngine.Random.Range(0,360), 0);
				lightningOdds = UnityEngine.Random.Range(lightningMinChance, lightningMaxChance);
				lightningIntensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
			}
		}
	}

	//This function handles the Time Manager's sound generation. 
	//The Sound Manager randomly generates sounds based on the time of day and the min and max seconds
	void DynamicTimeOfDaySounds (){
		if(TODSoundsTimer > Time.time){
			return;
		}

		AudioClip PlayedClip = null;

		if (Hour > 4 && Hour <= 8 && useMorningSounds){
			PlayedClip = ambientSoundsMorning[UnityEngine.Random.Range(0, ambientSoundsMorning.Count)];
			playedSound = true;
		}
		else if (Hour > 8 && Hour < 16 && useDaySounds){
			PlayedClip = ambientSoundsDay[UnityEngine.Random.Range(0,ambientSoundsDay.Count)];
			playedSound = true;
		}
		else if ( Hour >= 16 && Hour < 20 && useEveningSounds){
			PlayedClip = ambientSoundsEvening[UnityEngine.Random.Range(0,ambientSoundsEvening.Count)];
			playedSound = true;
		}
		else if (Hour >= 20 && Hour < 25 && useNightSounds){
			PlayedClip = ambientSoundsNight[UnityEngine.Random.Range(0,ambientSoundsNight.Count)];
			playedSound = true;
		}
		else if (Hour >= 0 && Hour <= 4 && useNightSounds){
			PlayedClip = ambientSoundsNight[UnityEngine.Random.Range(0,ambientSoundsNight.Count)];
			playedSound = true;
		}

		TODSoundsTimer = Time.time + UnityEngine.Random.Range(timeToWaitMin, timeToWaitMax);

		if (playedSound && PlayedClip != null){
			soundComponent.PlayOneShot(PlayedClip);
			TODSoundsTimer += PlayedClip.length;
			playedSound = false;
		}
	}

	//Puts all fading out weather types into one function then picks by weather type to control individual weather factors
	void FadeOutPrecipitation (){
		StormCloudFadeFloat1 -= Time.deltaTime * .04f * CloudFadeOutMultiplier; 
		StormCloudFadeFloat2 -= Time.deltaTime * .06f * CloudFadeOutMultiplier; 
		sunShaftFade += Time.deltaTime * 10 * EffectsFadeInMultiplier;
		currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
		currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
		minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
		currentSnowFogIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;	
		currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
		sunIntensity += Time.deltaTime * .14f * EffectsFadeInMultiplier;
		dynamicSnowFade -= Time.deltaTime * .0095f; 

		ParticleUpdater ();

		if (StormCloudFadeFloat1 > 0.01f){
		StormCloudColorControl();
		}

		lightningLight.enabled = false;
			
		//Fade in and out leaves for Fall weather type
		if (weatherForecaster == 13 && isFall){
			windyLeavesIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;

			if (windyLeavesIntensity >= 6){
					windyLeavesIntensity = 6;
			}
		}
		else{
			windyLeavesIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
			
			if (windyLeavesIntensity <= 0){
				windyLeavesIntensity = 0;
			}
		}

		//Fade in and out butterflies (lightning bugs) for Summer weather type
		if (weatherForecaster == 10 && isSummer){
			butterfliesFade += Time.deltaTime * ParticleEffectsFadeInMultiplier;

			if (butterfliesFade >= 8){
				butterfliesFade = 8;
			}
		}
		else{
			butterfliesFade -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;

			if (butterfliesFade <= 0){
				butterfliesFade = 0;
			}
		}

		if (terrainDetection){
			FadeOutWind();
		}
	
		if (RenderSettings.fogMode == FogMode.Linear){
			RenderSettings.fogStartDistance += Time.deltaTime * 0.75f  * FogDistanceFadeOutMultiplier; //Was 0.75f
			RenderSettings.fogEndDistance += Time.deltaTime * 10f * FogDistanceFadeOutMultiplier;
				
			if (RenderSettings.fogStartDistance >= fogStartDistance){
				RenderSettings.fogStartDistance = fogStartDistance;
			}
				
			if (RenderSettings.fogEndDistance >= fogEndDistance){
					RenderSettings.fogEndDistance = fogEndDistance;
			}
		}
			
		rainSoundComponent.volume -= Time.deltaTime * .07f * SoundFadeOutMultiplier;
		windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
		windSnowSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			
		if (currentRainIntensity <= 100){
			windZone.gameObject.SetActive(false);
		}
		
		if (sunShaftScript != null){
			sunShaftScript.sunShaftIntensity += Time.deltaTime * 0.15f * EffectsFadeInMultiplier;
		}
			
		if (sunShaftScript != null){
			if (sunShaftScript.sunShaftIntensity >= 2){
				sunShaftScript.sunShaftIntensity = 2;
				RenderSettings.fogDensity += .00012f * Time.deltaTime;

				if (RenderSettings.fogDensity >= fogDensity){
					RenderSettings.fogDensity = fogDensity;
				}
			}
		}

		//Keeps our fade numbers from going below 0
		if (currentRainFogIntensity <= 0){currentRainFogIntensity = 0;}
		if (minHeavyRainMistIntensity <= 0){minHeavyRainMistIntensity = 0;}
		if (currentSnowIntensity <= 0){currentSnowIntensity = 0;}
		if (currentSnowFogIntensity <= 0){currentSnowFogIntensity = 0;}
		if (currentRainIntensity <= 0){currentRainIntensity = 0;}
		if (StormCloudFadeFloat1 <= 0){StormCloudFadeFloat1 = 0;}
		if (StormCloudFadeFloat2 <= 0){StormCloudFadeFloat2 = 0;}
		if (dynamicSnowFade <= .25){dynamicSnowFade = .25f;}
	}

	//Puts all fading in weather types into one function then picks by weather type to control individual weather factors
	void FadeInPrecipitation(){
		StormCloudFadeFloat1 += Time.deltaTime * .015f * CloudFadeInMultiplier;
		StormCloudFadeFloat2 += Time.deltaTime * .015f * CloudFadeInMultiplier;
		butterfliesFade -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
		windyLeavesIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
		sunShaftFade -= Time.deltaTime * 0.14f * EffectsFadeOutMultiplier;
		sunIntensity -= Time.deltaTime * .14f * EffectsFadeOutMultiplier;
		dynamicSnowFade -= Time.deltaTime * .0095f; 

		//Fades our fog in	
		RenderSettings.fogDensity -= .00012f * Time.deltaTime;
		
		if (RenderSettings.fogDensity <= .0006){
			RenderSettings.fogDensity = .0006f;
		}
		
		if (StormCloudFadeFloat2 >= .75){StormCloudFadeFloat2 = .75f;}
		if (StormCloudFadeFloat1 >= 1){StormCloudFadeFloat1 = 1;}
		if (butterfliesFade <= 0){butterfliesFade = 0;}
		
		if (sunShaftScript != null){
			sunShaftScript.sunShaftIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;
			
			if (sunShaftScript.sunShaftIntensity <= 0.025f){
				sunShaftScript.sunShaftIntensity = 0;
			}
		}
		
		if (weatherForecaster != 3){
			lightningLight.enabled = false;
		}
	
		if (weatherForecaster == 1){
			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			currentSnowFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;	
			currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			
			rainSoundComponent.volume -= Time.deltaTime * .07f  * SoundFadeOutMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			windSnowSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			
			//Keeps our fade numbers from going below 0
			if (currentRainFogIntensity <= 0){currentRainFogIntensity = 0;}
			if (minHeavyRainMistIntensity <= 0){minHeavyRainMistIntensity = 0;}
			if (currentSnowIntensity <= 0){currentSnowIntensity = 0;}
			if (currentSnowFogIntensity <= 0){currentSnowFogIntensity = 0;}
			if (currentRainIntensity <= 0){currentRainIntensity = 0;}
		}
	
		//Light Rain
		else if (temperature >= 33 && temperatureType == 1 && weatherForecaster == 2 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 2){
			rainSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
		
			if (StormCloudFadeFloat2 >= 0.3f){
				currentRainIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
			}

			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			
			if (currentRainIntensity >= maxLightRainIntensity){
				currentRainIntensity = maxLightRainIntensity;
			}
			
			if (currentRainFogIntensity <= 0){
				currentRainFogIntensity = 0;
			}
			
			if (minHeavyRainMistIntensity <= 0){
				minHeavyRainMistIntensity = 0;
			}
			
			//This keeps the sound levels low because this is just a little rain	
			if (windSoundComponent.volume >= .0){
				windSoundComponent.volume = .0f;
			}
			
			if (rainSoundComponent.volume >= .3){
				rainSoundComponent.volume = .3f;
			}

			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Light Rain
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation){
				randomizedRainIntensity = UnityEngine.Random.Range(100,maxLightRainIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}

			if (!randomizedPrecipitation){
				if (currentRainIntensity >= maxLightRainIntensity)
				{
					currentRainIntensity = maxLightRainIntensity;
				}
			}
			
			if (randomizedPrecipitation){
				if (currentRainIntensity >= currentGeneratedIntensity){
					currentRainIntensity = currentGeneratedIntensity;
				}
			}
		}

		//Thunder Storm or Heavy Rain (No Thunder)
		else if (temperature >= 33 && temperatureType == 1  && weatherForecaster == 3 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 3 || temperature >= 33 && temperatureType == 1  && weatherForecaster == 12 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 12){
			if (StormCloudFadeFloat2 >= 0.3f){
				currentRainIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				currentRainFogIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;

				if (currentRainIntensity > 400){
					minHeavyRainMistIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;
				}
			}

			currentSnowFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;	
			currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;

			rainSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			windSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;

			if (weatherForecaster == 3 && currentRainIntensity >= rainNeededForLightning){
				Lightning ();
			}

			if (!randomizedPrecipitation){
				//Gradually fades our rain effects in
				if (currentRainIntensity >= maxStormRainIntensity){
					currentRainIntensity = maxStormRainIntensity;
				}
			}
			
			else if (randomizedPrecipitation){
				if (currentRainIntensity >= currentGeneratedIntensity){
					currentRainIntensity = currentGeneratedIntensity;
				}
			}

			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Heavy Rain
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation){
				randomizedRainIntensity = UnityEngine.Random.Range(1000,maxStormRainIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}
			
			//Gradually fades our rain effects in
			if (currentRainIntensity >= maxStormRainIntensity){
				currentRainIntensity = maxStormRainIntensity;
			}
			
			if (minHeavyRainMistIntensity >= maxHeavyRainMistIntensity){
				minHeavyRainMistIntensity = maxHeavyRainMistIntensity;
			}

			if (currentSnowIntensity <= 0){currentSnowIntensity = 0;}
			if (currentSnowFogIntensity <= 0){currentSnowFogIntensity = 0;}
		}

		//Snow Storm
		else if (temperature <= 32 && temperatureType == 1  && weatherForecaster == 3 || temperatureType == 2 && temperature <= 0  && weatherForecaster == 3 || temperature <= 32 && temperatureType == 1  && weatherForecaster == 12){
			if (StormCloudFadeFloat2 >= 0.3f){
				currentSnowIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				currentSnowFogIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;
			}

			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;

			rainSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			windSnowSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;

			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Light Snow
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation){
				randomizedRainIntensity = UnityEngine.Random.Range(500,maxSnowStormIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}

			if (!randomizedPrecipitation){
				//Keeps our snow level maxed at users input
				if (currentSnowIntensity >= maxSnowStormIntensity){
					currentSnowIntensity = maxSnowStormIntensity;
				}
			}

			if (randomizedPrecipitation){
				if (currentSnowIntensity >= currentGeneratedIntensity){
					currentSnowIntensity = currentGeneratedIntensity;
				}
			}
			
			//Keeps our snow level maxed at users input
			if (currentSnowIntensity >= maxSnowStormIntensity){
				currentSnowIntensity = maxSnowStormIntensity;
			}
			
			//Keeps our snow fog level maxed at users input
			if (currentSnowFogIntensity >= maxHeavySnowDustIntensity){
				currentSnowFogIntensity = maxHeavySnowDustIntensity;
			}

			//Keeps our fade values from going below 0
			if (currentRainFogIntensity <= 0){currentRainFogIntensity = 0;}
			if (minHeavyRainMistIntensity <= 0){minHeavyRainMistIntensity = 0;}
			if (currentRainIntensity <= 0){currentRainIntensity = 0;}
		}

		//Light Snow
		else if (temperature <= 32 && temperatureType == 1  && weatherForecaster == 2 || temperatureType == 2 && temperature <= 0  && weatherForecaster == 2){
			if (StormCloudFadeFloat2 >= 0.3f){
				currentSnowIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				currentSnowFogIntensity += .008f * ParticleEffectsFadeInMultiplier;	
			}

			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;

			windSnowSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			rainSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;

			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Light Snow
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation){
				randomizedRainIntensity = UnityEngine.Random.Range(100,maxLightSnowIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}

			if (!randomizedPrecipitation){
				//Keeps our snow level maxed at users input
				if (currentSnowIntensity >= maxLightSnowIntensity){
					currentSnowIntensity = maxLightSnowIntensity;
				}
			}

			else if (randomizedPrecipitation){
				if (currentSnowIntensity >= currentGeneratedIntensity){
					currentSnowIntensity = currentGeneratedIntensity;
				}
			}

			//Keeps our snow level maxed at users input
			if (currentSnowIntensity >= maxLightSnowIntensity){
				currentSnowIntensity = maxLightSnowIntensity;
			}
			
			//Keeps our snow fog level maxed at users input
			if (currentSnowFogIntensity >= maxLightSnowDustIntensity){
				currentSnowFogIntensity = maxLightSnowDustIntensity;
			}

			//Keeps our fade values from going below 0
			if (currentRainFogIntensity <= 0){currentRainFogIntensity = 0;}
			if (minHeavyRainMistIntensity <= 0){minHeavyRainMistIntensity = 0;}
			if (currentRainIntensity <= 0){currentRainIntensity = 0;}
			if (dynamicSnowFade <= .25){dynamicSnowFade = .25f;}

			if (windSnowSoundComponent.volume >= .3){
				windSnowSoundComponent.volume = .3f;
			}
		}

		if (terrainDetection){
			//Fades in our Dynamic Wind, but only if a terrain is present
			if (weatherForecaster == 3 || weatherForecaster == 12){
				FadeInWind();
			}

			//Fades out our Dynamic Wind
			if (weatherForecaster == 1 || weatherForecaster == 2){
				FadeOutWind();
			}
		}

		if (sunIntensity <= StormySunIntensity){
			sunIntensity = StormySunIntensity;
		}
		
		if (RenderSettings.fogMode == FogMode.Linear){
			RenderSettings.fogStartDistance -= Time.deltaTime * 0.75f * FogDistanceFadeInMultiplier;
			RenderSettings.fogEndDistance -= Time.deltaTime * 10f * FogDistanceFadeInMultiplier;
			
			if (RenderSettings.fogStartDistance <= stormyFogDistanceStart){
				RenderSettings.fogStartDistance = stormyFogDistanceStart;
			}
			
			if (RenderSettings.fogEndDistance <= stormyFogDistance){
				RenderSettings.fogEndDistance = stormyFogDistance;
			}
		}

		//Activates our stormy wind
		if (currentRainIntensity >= 1){
			//Makes our wind stronger for the storm
			windZone.gameObject.SetActive(true);
		}

		if (StormCloudFadeFloat1 > 0.01f){
		StormCloudColorControl();
		}

		ParticleUpdater ();
	}

	void CalculateSeaon (){
		//Calculates our seasons
		if (monthCounter >= 3 && monthCounter <= 5){
			isSpring = true;
			isSummer = false;
			isFall = false;
			isWinter = false;
			WeatherForecaster ();
		}
		//Calculates our seasons
		else if (monthCounter >= 6 && monthCounter <= 8){
			isSummer = true;
			isSpring = false;
			isFall = false;
			isWinter = false;
			WeatherForecaster ();
		}
		//Calculates our seasons
		else if (monthCounter >= 9 && monthCounter <= 11){
			isSummer = false;
			isSpring = false;
			isFall = true;
			isWinter = false; 
			WeatherForecaster ();
		}
		//Calculates our seasons
		else if (monthCounter == 12 || monthCounter == 1 || monthCounter == 2){
			isSummer = false;
			isSpring = false;
			isFall = false;
			isWinter = true;
			WeatherForecaster ();
		}
	}

	//Calculates our months for an accurate calendar that also calculates leap year
	void CalculateMonths (){
		if (calendarType == 1){				
			//Calculates our days into months
			if(dayCounter >= 32 && monthCounter == 1 || dayCounter >= 32 && monthCounter == 3 || dayCounter >= 32 && monthCounter == 5 || dayCounter >= 32 && monthCounter == 7 || dayCounter >= 32 && monthCounter == 8 || dayCounter >= 32 && monthCounter == 10 || dayCounter >= 32 && monthCounter == 12){
				dayCounter = dayCounter % 32;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			if(dayCounter >= 31 && monthCounter == 4 || dayCounter >= 31 && monthCounter == 6 || dayCounter >= 31 && monthCounter == 9 || dayCounter >= 31 && monthCounter == 11){
				dayCounter = dayCounter % 31;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates Leap Year
			if(dayCounter >= 30 && monthCounter == 2 && (yearCounter % 4 == 0 && yearCounter % 100 != 0) || (yearCounter % 400 == 0)){
				dayCounter = dayCounter % 30;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			else if (dayCounter >= 29 && monthCounter == 2 && yearCounter % 4 != 0){
				dayCounter = dayCounter % 29;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates our months into years
			if (monthCounter > 12){
				monthCounter = monthCounter % 13;
				yearCounter += 1;
				monthCounter += 1;

				//Reset our roundingCorrection variable to 0
				roundingCorrection = 0;
			}
		}

		//A custom calendar option for creating a custom calendar based off of the user's values
		else if (calendarType == 2){
			//Calculates our custom days into months
			if(dayCounter > numberOfDaysInMonth){
				dayCounter = dayCounter % numberOfDaysInMonth - 1;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates our custom months into years
			if (monthCounter > numberOfMonthsInYear){
				monthCounter = monthCounter % numberOfMonthsInYear - 1;
				yearCounter += 1;
				monthCounter += 1;
			}
		}
	}

	//Calculates our days and updates our Animation curves.
	void CalculateDays(){	
		roundingCorrection += 0.00273972602f;
		PreciseCurveTime = ((UniStormDate.DayOfYear / 28.07692307692308f) + 1) - roundingCorrection;
		PreciseCurveTime = Mathf.Round(PreciseCurveTime * 10f) / 10f;
		
		CurrentPrecipitationAmountFloat = PrecipitationGraph.Evaluate(PreciseCurveTime);
		TemperatureCurve.Evaluate(PreciseCurveTime);
		
		CurrentPrecipitationAmountInt = (int)Mathf.Round(CurrentPrecipitationAmountFloat);

		sunCalculator = 0;
		Hour = 0;
		dayCounter += 1;

		if (weatherForecaster == 3 || weatherForecaster == 2 || weatherForecaster == 12) {
			changeWeather += 1; 
		}

		CalculateMonths();
		
		if (calendarType == 1){
			UniStormDate = new DateTime(yearCounter, monthCounter, dayCounter, hourCounter, minuteCounter, 0);
		}

		if (PrecipitationType == 1){
			//Roll for weather based on the odds for the season
			if (isSpring == true){	
				precipitationOdds = precipitationOddsSpring;
				GenerateWeather();
			}
			//Summer Weather Odds
			else if (isSummer == true)
			{	
				precipitationOdds = precipitationOddsSummer;
				GenerateWeather();
			}
			//Fall Weather Odds
			else if (isFall == true){	
				precipitationOdds = precipitationOddsFall;
				GenerateWeather();
			}
			//Winter Weather Odds
			else if (isWinter == true){	
				precipitationOdds = precipitationOddsWinter;
				GenerateWeather();
			}
		}
		else if (PrecipitationType == 2){
			precipitationOdds = CurrentPrecipitationAmountInt;
			GenerateWeather();
		}
	}
	
	//This function is now obsolete
	/// <summary>
	/// (LoadTime () is Deprecated) Use the functions SetDateAndTime(), SetTime(), or SetDate() instead. 
	/// If you are loading a player's time and date data, it is recommended that you use SetDateAndTime.
	/// </summary>
	public void LoadTime (){
		float startTimeMinuteFloat = (int)startTimeMinute;
		startTime = startTimeHour / 24 + startTimeMinuteFloat / 1440;
	}

	/// <summary>
	/// Instant Weather can be called if you want the weather to change instantly. This can be done for quests, loading a player's game, events, etc. 
	/// It will set all needed values to be fully faded in. This also goes for the starting weather, if desired.
	/// </summary>
	public void InstantWeather (){
		if(weatherForecaster == 1){
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 0;
			}
			
			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 2 && temperatureType == 1 && temperature > 32 || 
		        weatherForecaster == 2 && temperatureType == 2 && temperature >= 1){
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = maxLightRainIntensity;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0.3f;
			windSoundComponent.volume = 0.3f;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 2 && temperatureType == 1 && temperature <= 32 || 
		        weatherForecaster == 2 && temperatureType == 2 && temperature <= 0){
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = maxLightSnowIntensity;
			currentSnowFogIntensity = maxLightSnowDustIntensity;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = .3f;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 3 && temperatureType == 1 && temperature > 32 || weatherForecaster == 12 && temperatureType == 1 && temperature > 32 || 
		        weatherForecaster == 3 && temperatureType == 2 && temperature >= 1 || weatherForecaster == 12 && temperatureType == 2 && temperature >= 1){
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			minHeavyRainMistIntensity = maxHeavyRainMistIntensity;
			rainSoundComponent.volume = 1.0f;
			windSoundComponent.volume = 1.0f;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (randomizedPrecipitation){
				randomizedRainIntensity = UnityEngine.Random.Range(1000,maxStormRainIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				currentRainIntensity = randomizedRainIntensity;
			}
			else{
				currentRainIntensity = maxStormRainIntensity;
			}

			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = stormGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = stormGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = stormGrassWavingAmount;
			}
		}

		//Instant Snow
		else if(weatherForecaster == 3 && temperatureType == 1 && temperature <= 32 || weatherForecaster == 12 && temperatureType == 1 && temperature <= 32 ||
		        weatherForecaster == 3 && temperatureType == 2 && temperature <= 0 || weatherForecaster == 12 && temperatureType == 2 && temperature <= 0){
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentRainFogIntensity = 0;
			currentGeneratedIntensity = 1000;
			currentSnowIntensity = maxSnowStormIntensity;
			currentSnowFogIntensity = maxHeavySnowDustIntensity;
			rainSoundComponent.volume = 0.0f;
			windSoundComponent.volume = 0.0f;
			windSnowSoundComponent.volume = 1.0f;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 0;
			}
			
			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = stormGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = stormGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = stormGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 4){
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 7 || weatherForecaster == 8){
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;

			if (weatherForecaster == 7){
				partlyCloudyFloat = 0;
				mostlyClearFloat = 1;
				mostlyCloudyFloat = 0;
			}

			if (weatherForecaster == 9){
				partlyCloudyFloat = 0;
				mostlyClearFloat = 0;
				mostlyCloudyFloat = 0;
			}

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 11){
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			mostlyClearFloat = 1;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 1;

			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 10 && isSummer){
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 8;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 2;
			}
			
			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;
			
			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		else if(weatherForecaster == 13 && isFall){
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 6;
			
			if (sunShaftScript != null){
				sunShaftScript.sunShaftIntensity = 2;
			}
			
			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;
			
			if (terrainDetection){
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13){
			sun.intensity = sunIntensityCurve.Evaluate((float)Hour);
			tempSunIntensity = 1;

			moon.intensity = moonIntensityCurve.Evaluate((float)Hour);
			tempMoonIntensity = 1;
		}
		else if (weatherForecaster == 1 || weatherForecaster == 2 || weatherForecaster == 3 || weatherForecaster == 12 || weatherForecaster == 9){
			tempSunIntensity = StormySunIntensity * 0.01f;
			sun.intensity = (sunIntensityCurve.Evaluate((float)Hour) * tempSunIntensity) * SunIntensityMultiplier;

			tempMoonIntensity = StormyMoonLightIntensity * 0.01f;
			moon.intensity = moonIntensityCurve.Evaluate((float)Hour) * tempMoonIntensity;
		}

		ParticleUpdate = 1;
		ParticleUpdater();
}

	//This function generates tempertures for the Simple option within the UniStorm Editor Temperature Options
	void TemperatureGeneration (){
		if (hourCounter >= 17){
			temperature -= UnityEngine.Random.Range (1,3);
		}
		else if (hourCounter >= 0 && hourCounter <= 5){
			temperature -= UnityEngine.Random.Range (1,3);
		}
		else if (hourCounter > 5 && hourCounter <= 16){
			temperature += UnityEngine.Random.Range (1,3);
		}

		if (isSpring){
			summerTemp = 0;
			winterTemp = 0;
			fallTemp = 0;
			
			if (springTemp == 0){
				springTemp = 1;
			}
	
			if (springTemp == 1){
				temperature = startingSpringTemp;
				springTemp = 2;	
			}

			if (temperature <= minSpringTemp && springTemp == 2){
				temperature = minSpringTemp;
			}

			if (temperature >= maxSpringTemp && springTemp == 2){
				temperature = maxSpringTemp;
			}
		}
		else if (isSummer){
			winterTemp = 0;
			fallTemp = 0;
			springTemp = 0;
			
			if (summerTemp == 0){
				summerTemp = 1;
			}

			if (summerTemp == 1){
				temperature = startingSummerTemp;
				summerTemp = 2;	
			}

			if (temperature <= minSummerTemp && summerTemp == 2){
				temperature = minSummerTemp;
			}

			if (temperature >= maxSummerTemp && summerTemp == 2){
				temperature = maxSummerTemp;
			}
		}
		
		else if (isFall){
			summerTemp = 0;
			winterTemp = 0;
			springTemp = 0;
			
			if (fallTemp == 0){
				fallTemp = 1;
			}

			if (fallTemp == 1){
				temperature = startingFallTemp;
				fallTemp = 2;
			}

			if (temperature <= minFallTemp && fallTemp == 2){
				temperature = minFallTemp;
			}

			if (temperature >= maxFallTemp && fallTemp == 2){
				temperature = maxFallTemp;
			}
		}
		else if (isWinter){
			summerTemp = 0;
			fallTemp = 0;
			springTemp = 0;
			
			if (winterTemp == 0){
				winterTemp = 1;
			}

			if (winterTemp == 1){
				temperature = startingWinterTemp;
				winterTemp = 2;
			}

			if (temperature <= minWinterTemp && winterTemp == 2){
				temperature = minWinterTemp;
			}

			if (temperature >= maxWinterTemp && winterTemp == 2){
				temperature = maxWinterTemp;
			}
		}
	}

	//Calculates our moon phases. This is updated daily at exactly 12:00.
	void MoonPhaseCalculator (){
		moonObjectComponent.material = MoonPhases[moonPhaseCalculator-1];
		if (moonPhaseCalculator == 8){
			moonPhaseCalculator = 0;
		}
	}

	//Fades in our terrain wind during stormy weather types.
	void FadeInWind (){
		if (usingMultipleTerrains){
			currentTerrainData = Terrain.activeTerrain.terrainData;
		}

		currentTerrainData.wavingGrassSpeed += Time.deltaTime * 0.001f * WindFadeInMultiplier;
		currentTerrainData.wavingGrassAmount += Time.deltaTime * 0.001f * WindFadeInMultiplier;
		currentTerrainData.wavingGrassStrength += Time.deltaTime * 0.001f * WindFadeInMultiplier;
		
		if (currentTerrainData.wavingGrassSpeed >= stormGrassWavingSpeed){
			currentTerrainData.wavingGrassSpeed = stormGrassWavingSpeed;
		}
		
		if (currentTerrainData.wavingGrassAmount >= stormGrassWavingStrength){
			currentTerrainData.wavingGrassAmount = stormGrassWavingStrength;
		}
		
		if (currentTerrainData.wavingGrassStrength >= stormGrassWavingAmount){
			currentTerrainData.wavingGrassStrength = stormGrassWavingAmount;
		}
	}

	//Fades out our terrain wind to the originally set values during non-stormy weather types.
	void FadeOutWind (){
		if (usingMultipleTerrains){
			currentTerrainData = Terrain.activeTerrain.terrainData;
		}

		currentTerrainData.wavingGrassSpeed -= Time.deltaTime * 0.001f * WindFadeOutMultiplier;
		currentTerrainData.wavingGrassAmount -= Time.deltaTime *  0.001f * WindFadeOutMultiplier;
		currentTerrainData.wavingGrassStrength -= Time.deltaTime *  0.001f * WindFadeOutMultiplier;
		
		if (currentTerrainData.wavingGrassSpeed <= normalGrassWavingSpeed){
			currentTerrainData.wavingGrassSpeed = normalGrassWavingSpeed;
		}
		
		if (currentTerrainData.wavingGrassAmount <= normalGrassWavingStrength){
			currentTerrainData.wavingGrassAmount = normalGrassWavingStrength;
		}
		
		if (currentTerrainData.wavingGrassStrength <= normalGrassWavingAmount){
			currentTerrainData.wavingGrassStrength = normalGrassWavingAmount;
		}
	}

	//The 3 functions below generate new dynamic cloud paterns each time a new weather type is generated with increased cloud cover
	void GenerateMostlyClear (){
		float MostlyClearGenerator = UnityEngine.Random.Range(3.0f,7.0f);
		MostlyClearGenerator = Mathf.Round(MostlyClearGenerator * 10f) / 10f;

		mostlyClearClouds1Material.SetTextureScale("_MainTex1", new Vector2(MostlyClearGenerator - 1, MostlyClearGenerator));
		mostlyClearClouds1Material.SetTextureScale("_MainTex2", new Vector2(MostlyClearGenerator, MostlyClearGenerator));
		mostlyClearClouds1Material.SetTextureScale("_MainTex3", new Vector2(MostlyClearGenerator -1, MostlyClearGenerator));
	}
	
	void GeneratePartlyCloudy (){
		float PartlyCloudyGenerator = UnityEngine.Random.Range(3.0f,7.0f);
		PartlyCloudyGenerator = Mathf.Round(PartlyCloudyGenerator * 10f) / 10f;

		partlyCloudyClouds1Material.SetTextureScale("_MainTex1", new Vector2(PartlyCloudyGenerator - 1, PartlyCloudyGenerator));
		partlyCloudyClouds1Material.SetTextureScale("_MainTex2", new Vector2(PartlyCloudyGenerator, PartlyCloudyGenerator));
		partlyCloudyClouds1Material.SetTextureScale("_MainTex3", new Vector2(PartlyCloudyGenerator - 1, PartlyCloudyGenerator));
	}

	void GenerateMostlyCloudy (){
		float MostlyCloudyGenerator = UnityEngine.Random.Range(5.0f,7.0f);
		MostlyCloudyGenerator = Mathf.Round(MostlyCloudyGenerator * 10f) / 10f;

		mostlyCloudyClouds1Material.SetTextureScale("_MainTex1", new Vector2(MostlyCloudyGenerator - 1, MostlyCloudyGenerator));
		mostlyCloudyClouds1Material.SetTextureScale("_MainTex2", new Vector2(MostlyCloudyGenerator, MostlyCloudyGenerator));
		mostlyCloudyClouds1Material.SetTextureScale("_MainTex3", new Vector2(MostlyCloudyGenerator - 1, MostlyCloudyGenerator));
	}

	//This function controls all regular cloud, stormy cloud, and star movement
	void CloudController (){
		float heavCloudScrollSpeedCalculator = Time.deltaTime *  heavyCloudSpeed * 0.1f;
		float heavyCloudScrollSpeed = Time.deltaTime * heavCloudScrollSpeedCalculator;
		float starScrollSpeed = Time.deltaTime * starSpeed * 0.015f; 
		float starRotationSpeedCalc = Time.time * starRotationSpeed * 0.1f;

		uvOffsetA = (uvAnimationRateA * Time.time * cloudSpeed * 0.1f);
		uvOffsetB = (uvAnimationRateB * Time.time * cloudSpeed * 0.1f);
		uvOffsetC = (uvAnimationRateA * Time.time * cloudSpeed * 0.02f); //Was 0.01

		if (StormCloudFadeFloat1 < 0.9){
			mostlyClearClouds1Material.SetTextureOffset("_MainTex1", uvOffsetA );
			mostlyClearClouds1Material.SetTextureOffset("_MainTex2", uvOffsetC );
			mostlyClearClouds1Material.SetTextureOffset("_MainTex3", uvOffsetB );
			partlyCloudyClouds1Material.SetTextureOffset("_MainTex1", uvOffsetA );
			partlyCloudyClouds1Material.SetTextureOffset("_MainTex2", uvOffsetC );
			partlyCloudyClouds1Material.SetTextureOffset("_MainTex3", uvOffsetB );
			mostlyCloudyClouds1Material.SetTextureOffset("_MainTex1", uvOffsetA );
			mostlyCloudyClouds1Material.SetTextureOffset("_MainTex2", uvOffsetC );
			mostlyCloudyClouds1Material.SetTextureOffset("_MainTex3", uvOffsetB );
		}

		if (StormCloudFadeFloat1 > 0.01){
			uvOffsetHeavyA += (uvAnimationRateHeavyA * Time.deltaTime * heavyCloudSpeed * 0.1f);
			uvOffsetHeavyB += (uvAnimationRateHeavyB * Time.deltaTime * heavyCloudSpeed * 0.1f);
			uvOffsetHeavyC += (uvAnimationRateHeavyB * Time.deltaTime * heavyCloudSpeed * 0.1f);

			heavyCloudsLayerLightMaterial.SetTextureOffset("_MainTex1", uvOffsetHeavyA );
			heavyCloudsLayerLightMaterial.SetTextureOffset("_MainTex2", uvOffsetHeavyB );
			heavyCloudsLayerLightMaterial.SetTextureOffset("_MainTex3", uvOffsetHeavyC );
			heavyCloudsMaterial.mainTextureOffset = new Vector2 (heavyCloudScrollSpeed,heavyCloudScrollSpeed); 
		}

		if (starSphereMaterial.color.a >= 0.01f){
			uvOffsetStars += uvAnimationRateStars * starScrollSpeed;
			starSphereMaterial.SetTextureOffset("_StarTex1", uvOffsetStars);
			starSphereMaterial.SetTextureOffset("_StarTex2", uvOffsetStars);

			starSphere.transform.eulerAngles = new Vector3(270, starRotationSpeedCalc, 0);
		}

		Color PartlyCloudyColor = mostlyClearClouds1Material.color;
		PartlyCloudyColor.a = partlyCloudyFloat;
		partlyCloudyClouds1Material.color = PartlyCloudyColor;

		Color MostlyCloudyColor = mostlyClearClouds1Material.color;
		MostlyCloudyColor.a = mostlyCloudyFloat;
		mostlyCloudyClouds1Material.color = MostlyCloudyColor;

		//Mostly Cloudy
		if (weatherForecaster == 11){
			partlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			mostlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;
			cloudColorTwilight.a = mostlyClearFloat;

			if (cloudType == 1){
				if (mostlyCloudyFloat >= 1){
					mostlyCloudyFloat = 1;
				}

				if (partlyCloudyFloat >= 1){
					partlyCloudyFloat = 1;
				}

				if (mostlyClearFloat >= 1){
					mostlyClearFloat = 1;
				}
			}

			if (mostlyCloudyFloat <= 0){
				GenerateMostlyCloudy ();
			}
		}
		//Partly Cloudy
		else if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6){
			partlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;

			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;
			cloudColorTwilight.a = mostlyClearFloat;

			if (cloudType == 1){
				if (mostlyCloudyFloat <= 0){
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat >= 1){
					partlyCloudyFloat = 1;
				}

				if (mostlyClearFloat >= 1){
					mostlyClearFloat = 1;
				}
			}

			if (partlyCloudyFloat <= 0){
				GeneratePartlyCloudy ();
			}
		}
		else if(weatherForecaster == 7){
			partlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;
			cloudColorTwilight.a = mostlyClearFloat;

			if (cloudType == 1){
				if (mostlyCloudyFloat <= 0){
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat <= 0){
					partlyCloudyFloat = 0;
				}

				if (mostlyClearFloat >= 1){
					mostlyClearFloat = 1;
				}

				if (mostlyClearFloat <= 0){
					GenerateMostlyClear ();
				}
			}
		}
		else if(weatherForecaster == 8){
			partlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;
			cloudColorTwilight.a = mostlyClearFloat;

			if (cloudType == 1){
				if (mostlyCloudyFloat <= 0){
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat <= 0){
					partlyCloudyFloat = 0;
				}

				if (mostlyClearFloat <= 0){
					mostlyClearFloat = 0;
				}
			}
		}
	}

	void StormCloudColorControl (){
		Color stormClouds1ColorTwilightTemp = stormClouds1ColorTwilight;
		stormClouds1ColorTwilightTemp.a = StormCloudFadeFloat1;
		stormClouds1ColorTwilight = stormClouds1ColorTwilightTemp;

		Color stormClouds1ColorMorningTemp = stormClouds1ColorMorning;
		stormClouds1ColorMorningTemp.a = StormCloudFadeFloat1;
		stormClouds1ColorMorning = stormClouds1ColorMorningTemp;

		Color stormClouds1ColorDayTemp = stormClouds1ColorDay;
		stormClouds1ColorDayTemp.a = StormCloudFadeFloat1;
		stormClouds1ColorDay = stormClouds1ColorDayTemp;

		Color stormClouds1ColorEveningTemp = stormClouds1ColorEvening;
		stormClouds1ColorEveningTemp.a = StormCloudFadeFloat1;
		stormClouds1ColorEvening = stormClouds1ColorEveningTemp;

		Color stormClouds1ColorNightTemp = stormClouds1ColorNight;
		stormClouds1ColorNightTemp.a = StormCloudFadeFloat1;
		stormClouds1ColorNight = stormClouds1ColorNightTemp;

		Color stormClouds2ColorTwilightTemp = stormClouds2ColorTwilight;
		stormClouds2ColorTwilightTemp.a = StormCloudFadeFloat2;
		stormClouds2ColorTwilight = stormClouds2ColorTwilightTemp;

		Color stormClouds2ColorMorningTemp = stormClouds2ColorMorning;
		stormClouds2ColorMorningTemp.a = StormCloudFadeFloat2;
		stormClouds2ColorMorning = stormClouds2ColorMorningTemp;

		Color stormClouds2ColorDayTemp = stormClouds2ColorDay;
		stormClouds2ColorDayTemp.a = StormCloudFadeFloat2;
		stormClouds2ColorDay = stormClouds2ColorDayTemp;

		Color stormClouds2ColorEveningTemp = stormClouds2ColorEvening;
		stormClouds2ColorEveningTemp.a = StormCloudFadeFloat2;
		stormClouds2ColorEvening = stormClouds2ColorEveningTemp;

		Color stormClouds2ColorNightTemp = stormClouds2ColorNight;
		stormClouds2ColorNightTemp.a = StormCloudFadeFloat2;
		stormClouds2ColorNight = stormClouds2ColorNightTemp;
	}

	void ParticleColorController (){
		#if UNITY_5_2 || UNITY_5_3 || UNITY_5_4
		rainMist.startColor = rainMistColorControl;
		snowMistFog.startColor = rainMistColorControl;
		rain.startColor = rainColorControl;
		rainSplashes.startColor = rainColorControl;
		snow.startColor = snowColorControl;
		#elif UNITY_5_5_OR_NEWER
		var rainMistModule = rainMist.main;
		rainMistModule.startColor = rainMistColorControl;
		var snowMistFogModule = snowMistFog.main;
		snowMistFogModule.startColor = rainMistColorControl;
		var rainModule = rain.main;
		rainModule.startColor = rainColorControl;
		var rainSplashesModule = rainSplashes.main;
		rainSplashesModule.startColor = rainColorControl;
		var snowModule = snow.main;
		snowModule.startColor = snowColorControl;
		#endif
	}
	
	void ParticleUpdater (){
		//This is the new method for particles to be manipulated because the old method has been deprecated
		//In the event of an older version of Unity being used, revert to the old method
		if (ParticleUpdate >= 1.0f){
			//Lightning Bugs
			#if UNITY_5_3 || UNITY_5_4 
			ParticleSystem.EmissionModule butterfliesEmission = butterflies.emission;
			butterfliesEmission.rate = new ParticleSystem.MinMaxCurve(butterfliesFade);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule butterfliesEmission = butterflies.emission;
			butterfliesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(butterfliesFade);
			#elif UNITY_5_2
			butterflies.emissionRate = butterfliesFade;
			#endif
			
			//Snow	
			#if UNITY_5_3 || UNITY_5_4
			ParticleSystem.EmissionModule snowEmission = snow.emission;
			snowEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowIntensity);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule snowEmission = snow.emission;
			snowEmission.rateOverTime = new ParticleSystem.MinMaxCurve(currentSnowIntensity);
			#elif UNITY_5_2
			snow.emissionRate = currentSnowIntensity;
			#endif
			
			//Snow Fog	
			#if UNITY_5_3 || UNITY_5_4
			ParticleSystem.EmissionModule snowFogEmission = snowMistFog.emission;
			snowFogEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowFogIntensity);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule snowFogEmission = snowMistFog.emission;
			snowFogEmission.rateOverTime = new ParticleSystem.MinMaxCurve(currentSnowFogIntensity);
			#elif UNITY_5_2
			snowMistFog.emissionRate = currentSnowFogIntensity;
			#endif
			
			//Rain	
			#if UNITY_5_3 || UNITY_5_4
			ParticleSystem.EmissionModule rainEmission = rain.emission;
			rainEmission.rate = new ParticleSystem.MinMaxCurve(currentRainIntensity);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule rainEmission = rain.emission;
			rainEmission.rateOverTime = new ParticleSystem.MinMaxCurve(currentRainIntensity);
			#elif UNITY_5_2
			rain.emissionRate = currentRainIntensity;
			#endif
			
			//Rain mist
			#if UNITY_5_3 || UNITY_5_4
			ParticleSystem.EmissionModule rainMistEmission = rainMist.emission;
			rainMistEmission.rate = new ParticleSystem.MinMaxCurve(minHeavyRainMistIntensity);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule rainMistEmission = rainMist.emission;
			rainMistEmission.rateOverTime = new ParticleSystem.MinMaxCurve(minHeavyRainMistIntensity);
			#elif UNITY_5_2
			rainMist.emissionRate = minHeavyRainMistIntensity;
			#endif
			
			//Windy Leaves
			#if UNITY_5_3 || UNITY_5_4
			ParticleSystem.EmissionModule windyLeavesEmission = windyLeaves.emission;
			windyLeavesEmission.rate = new ParticleSystem.MinMaxCurve(windyLeavesIntensity);
			#elif UNITY_5_5_OR_NEWER
			ParticleSystem.EmissionModule windyLeavesEmission = windyLeaves.emission;
			windyLeavesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(windyLeavesIntensity);
			#elif UNITY_5_2
			windyLeaves.emissionRate = windyLeavesIntensity;
			#endif
			
			ParticleUpdate = 0;
		}
	}

	// <summary>
	/// Changes the weather by number. Note: Some weather types are dependent on the temperature and season.
	/// 
	/// -Weather Types List-
	/// (Foggy = 1, Light Rain or Light Snow = 2, Heavy Snow or Lightning Storm = 3, Partly Cloudy = 4, Mostly Clear = 7, Clear = 8, Lightning Bugs = 10, Mostly Cloudy = 11, Heavy Rain = 12, and Falling Leaves = 13)
	/// </summary>
	public void ChangeWeather (int WeatherNumber){
		weatherForecaster = WeatherNumber;
	}

	// <summary>
	/// Changes the weather by number, instantly. This is helpful for loading a player's weather or for events. Note: Some weather types are dependent on the temperature and season.
	/// 
	/// -Weather Types List-
	/// (Foggy = 1, Light Rain or Light Snow = 2, Heavy Snow or Lightning Storm = 3, Partly Cloudy = 4, Mostly Clear = 7, Clear = 8, Lightning Bugs = 10, Mostly Cloudy = 11, Heavy Rain = 12, and Falling Leaves = 13)
	/// </summary>
	public void ChangeWeatherInstant (int WeatherNumber){
		weatherForecaster = WeatherNumber;
		InstantWeather();
	}

	// <summary>
	/// Changes the weather by generating a random weather type.
	/// </summary>
	public void RandomWeather (){
		weatherForecaster = UnityEngine.Random.Range(1, 14);
	}

	// <summary>
	/// Changes the weather by generating a random non-precipitation weather type.
	/// 
	/// -Possible Weather Types-
	/// (Clear, Mostly Clear, Partly Cloudy, and Mostly Cloudy)
	/// </summary>
	public void RandomNonPrecipitationWeather (){
		weatherForecaster = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
	}

	// <summary>
	/// Changes the weather by generating a random precipitation or cloudy weather type. Note: Some weather types are dependent on the temperature and season.
	/// 
	/// -Possible Weather Types-
	/// (Foggy, Light Rain, Light Snow, Heavy Rain, Heavy Snow, and Lightning Storm)
	/// </summary>
	public void RandomPrecipitationWeather (){
		weatherForecaster = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
	}

	/// <summary>
	/// Call this function to get UniStorm's current date and/or time using DateTime.
	/// </summary>
	public DateTime GetDate (){
		UniStormDate = new DateTime(yearCounter, monthCounter, dayCounter, hourCounter, minuteCounter, 0);
		return UniStormDate;
	}

	/// <summary>
	/// Changes UniStorm's date to the desired date by setting the Day, Month, and Year parameters.
	/// </summary>
	public void SetDate (int Month, int Day, int Year){
		dayCounter = Day;
		monthCounter = Month;
		yearCounter = Year;
	}

	/// <summary>
	/// Changes UniStorm's time to the desired time by setting the hour and minute parameters.
	/// </summary>
	public void SetTime (int Hour, int Minute){
		startTime = (float)Hour / 24 + (float)Minute / 1440;
	}

	/// <summary>
	/// Changes UniStorm's current time and date. This is useful for applying a player's saved time and date.
	/// </summary>
	public void SetDateAndTime (int Hour, int Minute, int Month, int Day, int Year){
		dayCounter = Day;
		monthCounter = Month;
		yearCounter = Year;
		startTime = (float)Hour / 24 + (float)Minute / 1440;
	}

	/// <summary>
	/// Changes the Day and Night lengths. This is useful for something like a resting system or 
	/// an event where the time needs to be fast forwarded or slowed.
	/// </summary>
	public void SetDayAndNightLenth (int DayLength, int NightLength){
		dayLength = DayLength;
		nightLength = NightLength;
	}

	/// <summary>
	/// Disables UniStorm's particle effects. This can be useful for events like entering
	/// areas where you don't want particle effects visible such as a building or cave.
	/// AlsoDisableSounds controls whether or not weather sounds are also disabled. 
	/// This can be set to false if not desired.
	/// </summary>
	public void DisableParticleEffects (bool AlsoDisableSounds){
		ParticlesDisabled = true;
		rain.gameObject.SetActive(false);
		rainMist.gameObject.SetActive(false);
		butterflies.gameObject.SetActive(false);
		rainSplashes.gameObject.SetActive(false);
		snow.gameObject.SetActive(false);
		snowMistFog.gameObject.SetActive(false);
		windyLeaves.gameObject.SetActive(false);

		if (AlsoDisableSounds){
			rainSound.gameObject.SetActive(false);
			windSound.gameObject.SetActive(false);
			windSnowSound.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Enables UniStorm's particle effects. This can be useful for events like exiting
	/// areas where you previously had particle effects disabled such as in a building or cave.
	/// AlsoDisableSounds controls whether or not weather sounds are also disabled. 
	/// This can be set to false if not desired.
	/// </summary>
	public void EnableParticleEffects (bool AlsoDisableSounds){
		ParticlesDisabled = false;
		rain.gameObject.SetActive(true);
		rainMist.gameObject.SetActive(true);
		butterflies.gameObject.SetActive(true);
		rainSplashes.gameObject.SetActive(true);
		snow.gameObject.SetActive(true);
		snowMistFog.gameObject.SetActive(true);
		windyLeaves.gameObject.SetActive(true);

		if (AlsoDisableSounds){
			rainSound.gameObject.SetActive(true);
			windSound.gameObject.SetActive(true);
			windSnowSound.gameObject.SetActive(true);
		}
	}
}