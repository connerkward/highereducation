using UnityEngine;
using System.Collections;

public class DrawSolidCube : MonoBehaviour {

	public Color zoneColor = new Color(1, 0, 0, 0.5F);

	void OnDrawGizmos() 
	{
		Gizmos.color = zoneColor;
		Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
	}

}
