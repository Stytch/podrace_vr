using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Controller : MonoBehaviour
{
    [Header("SFX_Ressources")]
    public AudioClip[] clips;
    public AudioSource[] sources;
    public static Dictionary<string, SliderSetting> dict_Sliders = new Dictionary<string, SliderSetting>();

    [Header("SFX_Engine_Physics")]
    public Rigidbody body;
    public float ground_repulsation = 1f;
    public float body_speed_mult = 1f;
    public float body_rotation_mult = 1f;
    public float body_stabilisator_mult = 0.1f;
    public float body_inclinaison_maxangle = 45f;
    public float body_hover_height = 4f;


    [Header("SFX_Engine_Power")]
    /// <summary>
    /// 0 : Arret
    /// 1 : Extinction
    /// 2 : Demarrage
    /// 3 : Idle
    /// 4 : Deceleration
    /// 5 : Acceleration
    /// </summary>
    public int engineState = 0;

    //public int engineFlags;//-------------------------------//todo

    /// <summary>
    /// 0-0 : stable
    /// 0-1 : accelaration 
    /// 1-2 : overboosting
    /// </summary>
    public float enginePowerL = 0;
    public float enginePowerR = 0;
    public float enginePowerBoostAddition = 0.5f;
    public float enginePowerAcceleration = 5f;
    public float enginePowerDeceleration = 5f;

    [Header("INPUT_Direction")]
    public float podRotation = 0;
    public float podRotation_target = 0;
    public float podRotation_variation = 5f;

    [Header("INPUT_Speed")]
    public float podSpeed = 0;
    public float podSpeed_target = 0;
    public float podSpeed_variation = 0.333f;

    [Header("SFX_Pertubations_effect")]
    public float perturbator_wind = 0;
    public float perturbator_canyonreverb = 0;

    [Header("SFX_Durations")]
    public float duration_engine_starting = 5f;
    public float duration_engine_shutdown = 3f;
    public float duration_engine_acceleratorStrike = 2f;
    public float duration_engine_decelerationStrike = 2f;

    [Header("SFX_HUD_DEBUGGER")]
    public bool useKeyboard = true;
    public Image dir;
    public Text angle;
    public Text speed;


    [Header("VFX_SPOONS")]
    public GameObject spoon_left_top;
    public GameObject spoon_left_downright;
    public GameObject spoon_left_downleft;
    public GameObject spoon_right_top;
    public GameObject spoon_right_downright;
    public GameObject spoon_right_downleft;
    public float spoon_opening_mult;

    private void Awake()
    {
        terrainMask = LayerMask.GetMask("Terrain");

    }

    void Start()
    {
        dict_Sliders["Power_L"].initSettingView(0, 2, 0);
        //dict_Sliders["Power_L"].AddListener_OnValueChanged(() => { print("Power_L is changed in UI"); });
        dict_Sliders["Power_R"].initSettingView(0, 2, 0);
        //dict_Sliders["Power_R"].AddListener_OnValueChanged(() => { print("Power_R is changed in UI"); });

    }
    private float lastvelocity = 0.0f;
    private float lastPower = 0.0f;
    private float nextSFX_slowing = 0.0f;
    private float nextSFX_Heavyslowing = 0.0f;
    private float nextSFX_accelerating = 0.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMultiply) || Input.GetButtonDown("joystick button 6"))
        {
            print("reset !");
            transform.position = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }

        if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("joystick button 7")) && engineState == 0)
        {
            print("Engine_Start");
            sources[0].PlayOneShot(clips[0]);
            Invoke("Engine_Start", duration_engine_starting);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("joystick button 7")) && engineState == 1)
        {
            print("Engine_Shutdown");
            sources[0].PlayOneShot(clips[7]);
            Invoke("Engine_Shutdown", duration_engine_shutdown);
        }

        if (engineState > 0)
        {
            //INPUTS [KEYMODE]
            if (useKeyboard)
            {

                if (Input.GetKey(KeyCode.Keypad1))
                {
                    enginePowerL = Mathf.Lerp(enginePowerL, 1f + (Input.GetKey(KeyCode.Keypad3) ? enginePowerBoostAddition : 0f), Time.deltaTime * enginePowerAcceleration);
                }
                else
                {
                    enginePowerL = Mathf.Lerp(enginePowerL, 0, Time.deltaTime * enginePowerDeceleration);
                }

                if (Input.GetKey(KeyCode.Keypad2))
                {
                    enginePowerR = Mathf.Lerp(enginePowerR, 1f + (Input.GetKey(KeyCode.Keypad3) ? enginePowerBoostAddition : 0f), Time.deltaTime * enginePowerAcceleration);
                }
                else
                {
                    enginePowerR = Mathf.Lerp(enginePowerR, 0, Time.deltaTime * enginePowerDeceleration);
                }


                if (Input.GetKey(KeyCode.Keypad3) && Time.timeSinceLevelLoad > nextSFX_accelerating)
                {
                    sources[0].PlayOneShot(clips[6]);
                    nextSFX_accelerating = Time.timeSinceLevelLoad + 10f;
                }
            }
            else // BY XBOX CONTROLLER
            {
                enginePowerL = Mathf.Lerp(enginePowerL, Input.GetAxis("LT") + (Input.GetButton("joystick button 4") && Input.GetButton("joystick button 5") ? enginePowerBoostAddition : 0f), Time.deltaTime * enginePowerAcceleration*2);
                enginePowerR = Mathf.Lerp(enginePowerR, Input.GetAxis("RT") + (Input.GetButton("joystick button 4") && Input.GetButton("joystick button 5") ? enginePowerBoostAddition : 0f), Time.deltaTime * enginePowerAcceleration*2);
                if (Input.GetButton("joystick button 4") && Input.GetButton("joystick button 5") && Time.timeSinceLevelLoad > nextSFX_accelerating)
                {
                    sources[0].PlayOneShot(clips[6]);
                    nextSFX_accelerating = Time.timeSinceLevelLoad + 10f;
                }
            }

            // DIRECTION CALC
            float dirRatio = enginePowerR - enginePowerL;
            if (Math.Abs(dirRatio) < 0.1f)
            {
                //podRotation_target = 0f;
                podRotation_target = dirRatio;
            }
            else
            {
                podRotation_target = dirRatio;
            }
            podRotation = Mathf.Lerp(podRotation, podRotation_target, Time.deltaTime * podRotation_variation);

            //SPEED CALC // MAXSPEED = 100f
            podSpeed_target = (float)(Math.Pow((enginePowerR + enginePowerL), 2) * 25d);
            podSpeed = Mathf.Lerp(podSpeed, podSpeed_target, Time.deltaTime * podSpeed_variation);
            sources[1].pitch = 0.9f + (podSpeed / 100f) * 0.2f;
            sources[3].pitch = 0.8f + (podSpeed / 100f) * 0.4f;
            sources[1].volume = 1f - Mathf.Max(-0.5f + podSpeed / 85f, 0.0f);
            //sources[2].volume = Mathf.Max(- 0.5f + podSpeed / 90f,0.2f);
            sources[2].volume = Mathf.Max(-0.5f + (enginePowerR + enginePowerL) / 50f, 0.2f);

            // WIND PERTUBATION SFX CALC
            //TODO volume perturb = speed * angle

            // ENGINE SFX CALC
            //TODO 

            // UPDATE HUD
            dir.transform.localRotation = Quaternion.Euler(0, 0, (podRotation * 90f));
            angle.text = (podRotation * 90f).ToString("F") + "°";
            speed.text = podSpeed.ToString("F") + "m/s";
            dict_Sliders["Power_L"].setValue(enginePowerL);
            dict_Sliders["Power_R"].setValue(enginePowerR);


        }
    }
    //private void FixedUpdate()

    private LayerMask terrainMask;
    private void FixedUpdate()
    {
        if (engineState > 0)
        {
            //CONSTANT REDUCTION FORCE
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z) * 0.9f;
            body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y + podRotation * -body_rotation_mult, body.angularVelocity.z) * 0.9f;

            //HOVERCRAFT FORCE
            RaycastHit hit;
            /**BUG ICI=============================================*/
            Vector3 hoverDirection = Quaternion.AngleAxis(1, transform.right) * transform.forward;
            Vector3 hoverUp = transform.up;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, body_hover_height * 100f, terrainMask))
            {
                Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
                Debug.DrawRay(hit.point, hit.normal * 10f, Color.red);
                hoverDirection = Quaternion.AngleAxis(90, transform.right) * hit.normal;
                hoverUp = hit.normal;
                Debug.DrawRay(hit.point, hoverDirection * 10f, Color.cyan);
                body.AddForce(Vector3.up * (body_hover_height - hit.distance) * ground_repulsation * (UnityEngine.Random.Range(1f, body_hover_height / 2f)), ForceMode.Acceleration);
            }

            //SPEED BY ENGINE FORCE
            body.AddForce((transform.forward * podSpeed * body_speed_mult), ForceMode.Force);// + (Vector3.up * podSpeed)

            //VIRAGE VELOCITEE
            //STABILISATEUR ASSIETTE.
            Quaternion hoverQuat = Quaternion.LookRotation(hoverDirection, hoverUp);
            print(hoverQuat.eulerAngles);
            transform.localRotation = Quaternion
                .Lerp(transform.localRotation
                , hoverQuat//(hoverDirection, hoverUp) 
                    * (Quaternion.Euler(0, 0, (podRotation_target * body_inclinaison_maxangle) - transform.localEulerAngles.z))
                , body_stabilisator_mult);

            //DECELERATION DETECTION
            if (body.velocity.sqrMagnitude + 2f < lastvelocity && Time.timeSinceLevelLoad > nextSFX_slowing)
            {
                sources[0].PlayOneShot(clips[4]);
                nextSFX_slowing = Time.timeSinceLevelLoad + 10f;
            }
            //HEAVY SLOW
            if (body.velocity.sqrMagnitude < 16f && Time.timeSinceLevelLoad > nextSFX_Heavyslowing)
            {
                sources[0].PlayOneShot(clips[5]);
                nextSFX_Heavyslowing = Time.timeSinceLevelLoad + 20f;
            }
            lastvelocity = body.velocity.sqrMagnitude;

            //SPOON OPENING
            spoon_left_top.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerL, 1f)) * spoon_opening_mult, 0, 0);   //+ UnityEngine.Random.Range(-0.1f, 0.1f)
            spoon_left_downleft.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerL, 1f)) * spoon_opening_mult, 0, 0);  //+ UnityEngine.Random.Range(-0.1f, 0.1f)
            spoon_left_downright.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerL, 1f)) * spoon_opening_mult, 0, 0); //+ UnityEngine.Random.Range(-0.1f, 0.1f)
            spoon_right_top.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerR, 1f)) * spoon_opening_mult, 0, 0);   //+ UnityEngine.Random.Range(-0.1f, 0.1f)
            spoon_right_downleft.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerR, 1f)) * spoon_opening_mult, 0, 0);  //+ UnityEngine.Random.Range(-0.1f, 0.1f)
            spoon_right_downright.transform.localEulerAngles = new Vector3((1f - Mathf.Min(enginePowerR, 1f)) * spoon_opening_mult, 0, 0); //+ UnityEngine.Random.Range(-0.1f, 0.1f)
        }
    }


    public void Engine_Start()
    {
        print("Engine ON");
        engineState = 1;
        podSpeed = 0;
        podSpeed_target = 0;
        podRotation = 0;
        podRotation_target = 0;

        //
        sources[1].clip = clips[1];
        sources[1].loop = true;
        sources[1].pitch = 0.9f;
        sources[1].Play();
        //
        sources[2].clip = clips[2];
        sources[2].loop = true;
        sources[2].volume = 0f;
        sources[2].Play();
        //
        sources[3].clip = clips[3];
        sources[3].loop = true;
        sources[3].volume = 0.2f;
        sources[3].pitch = 0.9f;
        sources[3].Play();
    }
    public void Engine_Shutdown()
    {
        print("Engine OFF");
        engineState = 0;
        podSpeed = 0;
        podSpeed_target = 0;
        podRotation = 0;
        podRotation_target = 0;

        //
        sources[1].loop = false;
        sources[1].Stop();
        //
        sources[2].loop = false;
        sources[2].Stop();
        sources[2].volume = 0f;
        //
        sources[3].loop = false;
        sources[3].Stop();
        sources[3].volume = 0.15f;
    }

    public static bool keyStartButtonDown()
    {
        return Input.GetKeyDown(KeyCode.Keypad0);
        //return Input.GetKeyDown(KeyCode.Keypad0) || Input.get;
    }
}
