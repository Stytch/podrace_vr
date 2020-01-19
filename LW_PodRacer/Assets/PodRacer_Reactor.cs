using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PodRacer_Reactor : MonoBehaviour
{
    public bool started;
    public float scoups_opening_mult = -10f;
    public float reactorPower;
    public float podSpeed;
    public float shutter_opening_mult = 20f;
    public float sideshutter_opening_mult = -30f;
    public float light_powermin = 50f;
    public float light_powermax = 150f;

    public Light reactorLight;
    public Light lightningLight;
    public ParticleSystem reactorFlameParticles;
    public ParticleSystem reactorFlameStartingParticles;

    public GameObject[] shuttersRoots;
    public GameObject[] scoupsRoots;
    public GameObject[] sideShutterRoots;

    public Color lowSpeedColor_min;
    public Color lowSpeedColor_max;
    public Color highSpeedColor_min;
    public Color highSpeedColor_max;

    void Start()
    {
        shutdownReactor();
    }

    public void shutdownReactor()
    {
        for (int i = 0; i < scoupsRoots.Length; i++) scoupsRoots[i].transform.localEulerAngles = Vector3.zero;
        for (int i = 0; i < scoupsRoots.Length; i++) scoupsRoots[i].transform.localEulerAngles = Vector3.zero;
        reactorFlameParticles.gameObject.SetActive(false);
        reactorFlameStartingParticles.gameObject.SetActive(false);
        started = false;
        reactorLight.color = lowSpeedColor_min;
        reactorLight.intensity = 0;
        reactorFlameStartingParticles.Clear();
        reactorFlameStartingParticles.Stop();

    }

    public void startReactor()
    {
        reactorFlameStartingParticles.gameObject.SetActive(true);
        reactorFlameStartingParticles.Clear();
        reactorFlameStartingParticles.Play();
        Invoke("finalStartReactor", 3f);
    }

    public void finalStartReactor()
    {

        started = true;
        reactorFlameParticles.gameObject.SetActive(true);
        reactorLight.intensity = light_powermin;
        reactorLight.color = lowSpeedColor_min;
    }

    public void Update()
    {
        if (started)
        {
            Vector3 tmp;

            //SCOUPS
            tmp = new Vector3(Mathf.Clamp01(1f - Mathf.Min(reactorPower, 1f)) * scoups_opening_mult, 0, 0);
            for (int i = 0; i < scoupsRoots.Length; i++) scoupsRoots[i].transform.localEulerAngles = tmp;
            //spoon_left_top.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerL, 1f)) * spoon_opening_mult, 0, 0);   //+ UnityEngine.Random.Range(-0.1f, 0.1f)

            //SHUTTERS
            tmp = new Vector3((reactorPower) * shutter_opening_mult, 0, 0);
            for (int i = 0; i < shuttersRoots.Length; i++) shuttersRoots[i].transform.localEulerAngles = tmp;

            //SIDESHUTTERS
            tmp = new Vector3(Mathf.Clamp01(1f - Mathf.Min(reactorPower, 1f)) * sideshutter_opening_mult, 0, 0);
            for (int i = 0; i < sideShutterRoots.Length; i++) sideShutterRoots[i].transform.localEulerAngles = tmp;

            //PARTICLES
            var main = reactorFlameParticles.main;
            main.startSize = new ParticleSystem.MinMaxCurve(0.02f + Mathf.Clamp01(reactorPower) * 0.03f);
            main.startLifetime = new ParticleSystem.MinMaxCurve(0.2f + Mathf.Clamp01(reactorPower) * 0.4f);
            var gradient = new ParticleSystem.MinMaxGradient(
                Color.Lerp(lowSpeedColor_min, highSpeedColor_min, reactorPower)
                , Color.Lerp(lowSpeedColor_max, highSpeedColor_max, reactorPower));
            main.startColor = gradient;

            //LIGHT
            reactorLight.intensity = light_powermin +(reactorPower) * light_powermax * Random.Range(0.5f,1f);
            reactorLight.color = Color.Lerp(lowSpeedColor_min, highSpeedColor_min, reactorPower);
        }
    }
}
