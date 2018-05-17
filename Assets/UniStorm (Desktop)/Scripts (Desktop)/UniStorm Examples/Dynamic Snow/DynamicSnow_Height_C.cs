//This script demonstrates UniStorm's dynamic snow shader (currently in beta)
//If it's snowing, the dynamic snow will start to form.
//If it's above 32 degrees, it will start to melt.
//This is globally altered so all materials using this shader will be affected
//Requires the reference of at least 1 material using the snow shader

//Black Horizon Studios

using UnityEngine;
using System.Collections;

public class DynamicSnow_Height_C : MonoBehaviour {

	private GameObject uniStormSystem;
	public UniStormWeatherSystem_C uniStormSystemScript;
	public float snowAmount;

	void Start () 
	{
		if (transform.position.y > 375)
		{
			Shader.SetGlobalFloat("_LayerStrength", 0.6f);
		}
	}
	

	void Update () 
	{

	}
}
