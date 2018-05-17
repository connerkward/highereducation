using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightningPosition : MonoBehaviour {

	float timer = 0;

	void Update () {
		timer += Time.deltaTime;

		if (timer >= 2)
		{
			transform.localPosition = new Vector3(Random.Range(-200,200), 45, Random.Range(-200,200));
			timer = 0;

		}
	}
}
