using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    public ParticleSystem chaosParticles1;
    public ParticleSystem chaosParticles2;
    public ParticleSystem harmonyParticles;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float distFromCenter = Mathf.Abs(ControllerInputHandler.instance.speed - 0.5f);
        if (ControllerInputHandler.instance.speed < 0.5f) {
            if (chaosParticles1.isPlaying) {
                chaosParticles1.Stop();
            }
            if (chaosParticles2.isPlaying) {
                chaosParticles2.Stop();
            }
            if (!harmonyParticles.isPlaying) {
                harmonyParticles.Play();
            } else {
                ParticleSystem.EmissionModule emissionModule = harmonyParticles.emission;
                emissionModule.rate = 300 * distFromCenter;
                var lifetimeColor = harmonyParticles.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                lifetimeColor.color = grad;
            }
        } else {
            if (!chaosParticles1.isPlaying) {
                chaosParticles1.Play();

            } else {
                ParticleSystem.EmissionModule emissionModule = chaosParticles1.emission;
                emissionModule.rate = 500 * distFromCenter;
            }
            if (!chaosParticles2.isPlaying) {
                chaosParticles2.Play();
            } else {
                ParticleSystem.EmissionModule emissionModule = chaosParticles2.emission;
                emissionModule.rate = 500 * distFromCenter;
            }
            if (harmonyParticles.isPlaying) {
                harmonyParticles.Stop();
            }
        }
    }
}
