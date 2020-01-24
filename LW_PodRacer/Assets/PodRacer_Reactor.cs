using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PodRacer_Reactor : MonoBehaviour
{
    [Header("MODIFIERS")]
    private bool started;
    public float scoups_opening_mult = -10f;
    public float reactorPower;
    public float podSpeed;
    public float shutter_opening_mult = 20f;
    public float sideshutter_opening_mult = -30f;
    public float light_powermin = 50f;
    public float light_powermax = 150f;
    public float boosting_value;
    //BRAKES
    public bool isBreaking = false;
    public float breakingValue = 0;
    public float ScoupsBreakingSpeed = 20f;

    [Header("LIGHTS & VFX")]
    public Light reactorLight;
    public Light lightningLight;
    public ParticleSystem reactorFlameParticles;
    public ParticleSystem reactorFlameStartingParticles;

    [Header("SUBCOMPONENTS")]
    public GameObject[] shuttersRoots;
    public GameObject[] scoupsRoots;
    public GameObject[] sideShutterRoots;
    public GameObject[] blacksmokes;

    public GameObject explosion_prefab;

    [Header("VFX COLORS")]
    public Color normalRColor_min = new Color(255, 169, 0);
    public Color normalRColor_max = new Color(255, 245, 0);
    public Color boostRColor_min = new Color(0, 255, 237);
    public Color boostRColor_max = new Color(0, 255, 148);

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
        reactorLight.color = normalRColor_min;
        reactorLight.intensity = 0;
        reactorFlameStartingParticles.Clear();
        reactorFlameStartingParticles.Stop();
        isBreaking = false;
        foreach (var item in blacksmokes) item.SetActive(false);

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
        reactorLight.color = normalRColor_min;
        //SHUTTERS
        Vector3 tmp = new Vector3((1) * shutter_opening_mult, 0, 0);
        for (int i = 0; i < shuttersRoots.Length; i++) shuttersRoots[i].transform.localEulerAngles = tmp;
    }
    public void spawnExplosion()
    {
        GameObject.Instantiate(explosion_prefab, gameObject.transform);
    }
    public void Update()
    {
        if (started)
        {
            Vector3 tmp;

            //BREAKING SCOUPS
            breakingValue = Mathf.Lerp(breakingValue, (isBreaking ? 1f : 0f), Time.deltaTime * ScoupsBreakingSpeed);
            tmp = new Vector3(breakingValue * scoups_opening_mult, 0, 0);
            for (int i = 0; i < scoupsRoots.Length; i++) scoupsRoots[i].transform.localEulerAngles = tmp;

            //SHUTTERS
            tmp = new Vector3((reactorPower) * shutter_opening_mult, 0, 0);
            for (int i = 0; i < shuttersRoots.Length; i++) shuttersRoots[i].transform.localEulerAngles = tmp;

            //SIDESHUTTERS
            tmp = new Vector3(Mathf.Clamp01(1f - Mathf.Min(reactorPower, 1f)) * sideshutter_opening_mult, 0, 0);
            for (int i = 0; i < sideShutterRoots.Length; i++) sideShutterRoots[i].transform.localEulerAngles = tmp;

            //PARTICLES
            var main = reactorFlameParticles.main;
            main.startSize = new ParticleSystem.MinMaxCurve(0.02f + Mathf.Clamp01(reactorPower) * 0.03f);
            main.startLifetime = new ParticleSystem.MinMaxCurve(0.3f + Mathf.Clamp01(reactorPower) * 0.2f);
            var gradient = new ParticleSystem.MinMaxGradient(
                Color.Lerp(normalRColor_min, boostRColor_min, boosting_value)
                , Color.Lerp(normalRColor_max, boostRColor_max, boosting_value));
            main.startColor = gradient;

            //LIGHT
            reactorLight.intensity = light_powermin + (reactorPower) * light_powermax * Random.Range(0.5f, 1f);
            reactorLight.color = Color.Lerp(normalRColor_min, boostRColor_min, boosting_value);
        }
    }
}
