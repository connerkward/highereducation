//UniStorm Procedural Lightning
//Note: This feature is new and will be improved over time

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningBolt : MonoBehaviour {

	[HideInInspector]
	public Gradient LightningGradient;
	[HideInInspector]
	public Transform PlayerPos;
	[HideInInspector]
	public int MinIntensity = 20;
	[HideInInspector]
	public int MaxIntensity = 80;
	[HideInInspector]
	public int MinSpaceBetweenPoints = 20;
	[HideInInspector]
	public int MaxSpaceBetweenPoints = 40;
	[HideInInspector]
	public int MinWidth = 10;
	[HideInInspector]
	public int MaxWidth = 15;
	[HideInInspector]
	public int StrikeRadius = 2000;
	[HideInInspector]
	public int totalPoints = 15;
	[HideInInspector]
	public int MillisecondsBetweenLines = 4;
	[HideInInspector]
	public Color StartingLightningColor;
	[HideInInspector]
	public Color EndingLightningColor;
	[HideInInspector]
	public LineRenderer lr;

	GameObject EndPoint;
	int generatedIntensity;
	int RandomLinePos;
	int PositionInLine;
	List<Vector3> positions = new List<Vector3>();
	float ColorFade;
	bool FadeLightning = false;
	float MillisecondsBetweenLinesFloat;

	//Initialize our procedural lightning
	public void InitializeLightning (){
		EndPoint = new GameObject();
		EndPoint.name = "Lightning Position";
		lr = GetComponent<LineRenderer>();
		positions = new List<Vector3>(new Vector3[totalPoints]);
		#if UNITY_5_5_OR_NEWER
		lr.textureMode = LineTextureMode.Stretch;
		lr.endWidth = 0.2f;
		#else
		//lr.SetWidth(Random.Range(30, 40), 1.5f);
		lr.SetWidth(Random.Range(8, 16), 2.5f);
		#endif
		MillisecondsBetweenLinesFloat = (float)MillisecondsBetweenLines * 0.001f;
	}

	//When called, generate our lightning points based on the total points, which by default is 15.
	//Loop through each point and generate a randomized intensity for each position on the line renderer
	//based on the minimum and maximum intenisty. Have each point reference the end point transform
	//to match the generated lightning position.
	public void GenerateLightningPoints (){
		GenerateLighningPosition ();
		
		for (int i = totalPoints-1; i >= 0; i--){
			generatedIntensity = Random.Range(MinIntensity,MaxIntensity+1);
			RandomLinePos += Random.Range(-generatedIntensity,generatedIntensity+1);
			PositionInLine += Random.Range(MinSpaceBetweenPoints, MaxSpaceBetweenPoints+1);
			positions[i] = new Vector3(RandomLinePos + EndPoint.transform.position.x, PositionInLine + EndPoint.transform.position.y, RandomLinePos + EndPoint.transform.position.z);

			//Have the last point in the array match the EndPoint transform 
			//to complete the lightning generation process.
			if (i == totalPoints-1){
				positions[totalPoints-1] = EndPoint.transform.position;
			}
		}

		//Once our lighning points are generated, start the RenderLightningPoints Coroutine to render them.
		StartCoroutine("RenderLightningPoints");
	}

	//Render our lightning points by looping through each point based on the customizeable 
	//millisecond delay so our rendering process isn't instant, but slightly noticeable.
	IEnumerator RenderLightningPoints (){
		lr.enabled = true;
		
		for (int i = 0; i < positions.Count; i++){
			#if UNITY_5_5_OR_NEWER
			lr.positionCount = i + 1;
			#else
			lr.SetVertexCount(i+1);
			#endif
			lr.SetPosition(i, positions[i]);
			yield return new WaitForSeconds(MillisecondsBetweenLinesFloat);
		}
		
		StartCoroutine("WaitForLightningToFinish");
	}

	//Wait for the lightning to finish fading. Once finished, 
	//reset all values so they can be regenerated when called.
	IEnumerator WaitForLightningToFinish (){
		FadeLightning = true;
		yield return new WaitForSeconds(1.0f);
		PositionInLine = 0;
		RandomLinePos = 0;
		ColorFade = 0;
		lr.enabled = false;
		#if UNITY_5_5_OR_NEWER
		lr.startWidth = Random.Range(MinWidth, MaxWidth+1);
		#else
		//lr.SetWidth(Random.Range(30, 40), 1.5f);
		lr.SetWidth(Random.Range(8, 16), 2.5f);
		#endif
		lr.sharedMaterial.SetColor("_TintColor", StartingLightningColor);
		FadeLightning = false;
	}

	//Fades the lightning bolt from its starting color to its ending color
	void Update (){
		if (FadeLightning){
			ColorFade += Time.deltaTime * 2f;
			lr.sharedMaterial.SetColor("_TintColor", Color.Lerp(StartingLightningColor, EndingLightningColor, ColorFade));
		}
	}

	//Generate a randomized lightning position using the customized StrikeRadius variable and the player's Y position.
	//Add an additional 125 units of height and then do a RayCast to get the height for the end point to ensure it doesn't go through anything.
	//Once finished, randomize the height between 0 and 100 units for added randomization. In a future update, a near 0 generated height will be used for a successful lightning strike event. 
	void GenerateLighningPosition (){
		EndPoint.transform.position =  new Vector3(Random.Range(-StrikeRadius,StrikeRadius) + PlayerPos.position.x, PlayerPos.position.y + 125, Random.Range(-StrikeRadius,StrikeRadius) + PlayerPos.position.z);
		RaycastHit hit;
		if (Physics.Raycast(EndPoint.transform.position, -Vector3.up, out hit)) {
			EndPoint.transform.position = new Vector3(EndPoint.transform.position.x, hit.point.y+Random.Range(0,101), EndPoint.transform.position.z);
		}
	}
}
