//By: Black Horizon Studios
//To be used with UniStorm

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class HarvestCheck : MonoBehaviour {

	public string harvestTagName = "Plant";
	public float harvestDistance = 6.0f;
	public AudioClip harvestSound;
	private Ray ray;
	private RaycastHit hit;
	private AudioSource _AS;

	void Start () 
	{
		_AS = GetComponent<AudioSource>();

		if (harvestSound == null)
		{
			Debug.LogWarning("You have not added a sound to Harvest Sound of this script. Sounds will not be used.");
		}
	}

	void FixedUpdate () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			
			if (Physics.Raycast(ray, out hit, harvestDistance))
			{
				CheckHarvest();
			}
		}
	}
	
	void CheckHarvest ()
	{
		if (hit.collider.tag == harvestTagName)
		{
			if (harvestSound != null)
			{
				_AS.PlayOneShot(harvestSound);
			}

			if (hit.collider.gameObject.GetComponent<PlantGrowthSystem>().isFullyGrown)
			{
				Debug.Log("This plant is fully grown!");
			}

			if (!hit.collider.gameObject.GetComponent<PlantGrowthSystem>().isFullyGrown)
			{
				Debug.Log("This plant is not fully grown yet, come back later.");
			}
		}
	}
}

