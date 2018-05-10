using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UniStormDemoUI : MonoBehaviour
{

	UniStormWeatherSystem_C uniStormSystem;
	GameObject uniStormObject;
	public Slider TimeSlider;
	public bool OpenUniStormMenu = false;
	public GameObject UniStormCanvas;

	void Start () 
	{
		uniStormObject = GameObject.Find("UniStormSystemEditor");
		uniStormSystem = uniStormObject.GetComponent<UniStormWeatherSystem_C>(); 

		TimeSlider.value = uniStormSystem.startTime;

		//MouseLook1 = GameObject.Find("Player").GetComponent<MouseLookDesktop>();
		//MouseLook2 = GameObject.Find("Camera").GetComponent<MouseLookDesktop>();
	}
	

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			OpenUniStormMenu = !OpenUniStormMenu;
		}

		if (OpenUniStormMenu)
		{
			UniStormCanvas.SetActive(true);
			uniStormSystem.staticWeather = true;
			//MouseLook1.enabled = false;
			//MouseLook2.enabled = false;
			uniStormSystem.startTime = TimeSlider.value;
		}

		if (!OpenUniStormMenu)
		{
			UniStormCanvas.SetActive(false);
			uniStormSystem.staticWeather = false;
			//MouseLook1.enabled = true;
			//MouseLook2.enabled = true;
			TimeSlider.value = uniStormSystem.startTime;
		}
	}

	public void ChangeWeather (int weatherForecaster)
	{
		uniStormSystem.weatherForecaster = weatherForecaster;
	}
}
