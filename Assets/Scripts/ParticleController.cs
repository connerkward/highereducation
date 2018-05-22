using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    public ParticleSystem chaosParticles1;
    public ParticleSystem chaosParticles2;
    public ParticleSystem chaosParticlesMax;
    public ParticleSystem harmonyParticles1;
    public ParticleSystem harmonyParticles2;
    public ParticleSystem harmonyParticlesMax;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        float distFromCenter = Mathf.Abs(ControllerInputHandler.instance.speed - 0.5f);
        if (ControllerInputHandler.instance.speed < 0.5f) {
            if (ControllerInputHandler.instance.speed <= 0.01f) {
                if (!harmonyParticlesMax.isPlaying) {
                    harmonyParticlesMax.Play();
                }
            } else {
                harmonyParticlesMax.Stop();
            }
            if (chaosParticles1.isPlaying) {
                chaosParticles1.Stop();
            }
            if (chaosParticles2.isPlaying) {
                chaosParticles2.Stop();
            }
            if (!harmonyParticles1.isPlaying) {
                harmonyParticles1.Play();
            } else {
                ParticleSystem.EmissionModule emissionModule = harmonyParticles1.emission;
                emissionModule.rate = 1000 * distFromCenter;
                var lifetimeColor = harmonyParticles1.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0, 0.5f + distFromCenter, 1f), 0f), new GradientColorKey(new Color(0.5f + distFromCenter, 1f, 0f), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
                lifetimeColor.color = grad;
            }
            if (!harmonyParticles2.isPlaying) {
                harmonyParticles2.Play();
            } else {
                ParticleSystem.EmissionModule emissionModule = harmonyParticles2.emission;
                emissionModule.rate = 1000 * distFromCenter;
                var lifetimeColor = harmonyParticles2.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(distFromCenter * 2f, 1f, 0.5f), 0f), new GradientColorKey(new Color(distFromCenter * 2f, 0.5f + distFromCenter, 0.5f + distFromCenter), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
                lifetimeColor.color = grad;
            }
        } else {
            if (ControllerInputHandler.instance.speed >= 0.99f) {
                if (!chaosParticlesMax.isPlaying) {
                    Debug.Log("Chaos Max");
                    chaosParticlesMax.Play();
                }
            } else {
                chaosParticlesMax.Stop();
            }
            if (!chaosParticles1.isPlaying) {
                chaosParticles1.Play();

            } else {
                ParticleSystem.EmissionModule emissionModule = chaosParticles1.emission;
                emissionModule.rate = 1000 * distFromCenter;

                var lifetimeColor = chaosParticles1.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.5f, 0f, 1f - distFromCenter), 0f), new GradientColorKey(new Color(1f, 0.5f - distFromCenter, 0f), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
                lifetimeColor.color = grad;
            }
            if (!chaosParticles2.isPlaying) {
                chaosParticles2.Play();
            } else {
                ParticleSystem.EmissionModule emissionModule = chaosParticles2.emission;
                emissionModule.rate = 1000 * distFromCenter;

                var lifetimeColor = chaosParticles2.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.5f - distFromCenter, 0f, 0.5f), 0f), new GradientColorKey(new Color(1 - distFromCenter, 1 - distFromCenter , 1 - distFromCenter), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
                lifetimeColor.color = grad;
            }
            if (harmonyParticles1.isPlaying) {
                harmonyParticles1.Stop();
            }
            if (harmonyParticles2.isPlaying) {
                harmonyParticles2.Stop();
            }

        }
    }
}
