using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParticleController : MonoBehaviour {
    public ParticleSystem weather;
    public ParticleSystem rain;
    public ParticleSystem leaves;
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (weather.isPlaying) {
            var lifetimeColor = weather.colorOverLifetime;
            float speed = ControllerInputHandler.instance.speed;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1 - speed / 1.1f, 1 - speed / 1.1f, 1 - speed / 1.1f), 1f), new GradientColorKey(new Color(1 - speed / 1.25f, 1 - speed / 1.25f, 1 - speed / 1.25f), 0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
            lifetimeColor.color = grad;
        }
        if(rain.gameObject.activeSelf && rain.isPlaying) {
            source.volume = (0.5f + ControllerInputHandler.instance.speed / 2) - 0.4f;
            if (!source.isPlaying) {
                source.Play();
            }
            ParticleSystem.EmissionModule emissionModule = rain.emission;
            emissionModule.rateOverTime = 500 + 500 * ControllerInputHandler.instance.speed;
            var main = rain.main;
            main.startSize = 0.75f + ControllerInputHandler.instance.speed / 4;
        }

        if (leaves.isPlaying)
        {
            ParticleSystem.EmissionModule emissionModule = leaves.emission;
            emissionModule.rateOverTime = 25 + 25 * ControllerInputHandler.instance.speed;
        }
	}
}
