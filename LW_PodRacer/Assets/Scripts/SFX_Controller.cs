using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class SFX_Controller : MonoBehaviour
{
    [Header("SFX_Ressources")]
    public AudioClip[] clips;
    public AudioSource[] sources;

    [Header("SFX_Engine_Physics")]
    public Rigidbody body;
    public float ground_repulsation = 1f;
    public float body_speed_mult = 1f;
    public float body_rotation_mult = 1f;
    public float body_stabilisator_mult = 0.1f;
    public float body_inclinaison_maxangle = 45f;
    public float body_hover_height = 4f;

    [Header("SCRIPT_Reactors")]
    public PodRacer_Reactor LeftReactor;
    public PodRacer_Reactor RightReactor;

    [Header("VFX_Lightning")]
    public GameObject[] LightningAnimation;
    public Light[] LightningLight;
    //public Light LightningLight;

    [Header("VFX_PostProcess")]
    public PostProcessingProfile postprocess;

    [Header("SFX_Engine_Power")]
    /// <summary>
    /// 0 : Arret
    /// 1 : Extinction
    /// 2 : Demarrage
    /// 3 : Idle
    /// 4 : Deceleration
    /// 5 : Acceleration
    /// </summary>
    private int engineState = 0;
    /// <summary>
    /// 0-0 : stable
    /// 0-1 : accelaration 
    /// 1-2 : overboosting
    /// </summary>
    private float enginePowerL = 0;
    private float enginePowerR = 0;

    [Header("INPUT_Direction")]
    private float rotation = 0;
    private float rotation_target = 0;
    public float rotation_variation = 5f;

    [Header("INPUT_Speed")]
    private float speed_value = 0;
    private float speed_target = 0;
    public float speed_timeVariationSec = 3f;
    public float speed_acceleration = 2f;
    public float speed_deceleration = 2f;

    private bool breaking = false;

    [Header("INPUT_Boosting")]
    private bool boosting = false;
    private float boosting_value = 0f;
    public float boosting_timeVariationSec = 3f;
    public float boosting_power = 0.5f;
    private float boosting_duration = 0f;
    public float boosting_durationLimitSec = 5f;
    private bool boosting_disabled = false;
    private float boosting_disable_duration = 0f;
    public float boosting_disable_durationLimitSec = 5f;

    [Header("SFX_Pertubations_effect")]
    public bool invulnerable = false;
    public float invulnerable_duration = 0;
    public float invulnerable_durationLimitSec = 5f;

    [Header("SFX_Durations")]
    public float duration_engine_starting = 5f;
    public float duration_engine_shutdown = 3f;
    public float duration_engine_acceleratorStrike = 2f;
    public float duration_engine_decelerationStrike = 2f;

    [Header("UI_COCKPIT")]
    public PodRacer_UI ui;

    LayerMask terrainMask;
    PostProcessingProfile ppProfile;
    private int lives;
    public int lives_start = 3;
    //public bool disableInputs;

    private void Awake()
    {
        terrainMask = LayerMask.GetMask("Terrain");
    }
    void Start()
    {
        set_ChromaticAberrationValue(0f);
        ppProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        breaking = false;
        boosting_value = 0;
        reset_UI();
        lives = lives_start;
        update_UI_lives();
    }

    private float lastvelocity = 0.0f;
    private float lastPower = 0.0f;
    private float nextSFX_slowing = 0.0f;
    private float nextSFX_Heavyslowing = 0.0f;
    private float nextSFX_accelerating = 0.0f;

    // LES CONTROLES
    private bool input_RESET { get => Input.GetKeyDown(KeyCode.KeypadMultiply) || Input.GetButtonDown("joystick button 6"); }
    private bool input_BOOSTING { get => (Input.GetButton("joystick button 4") && Input.GetButton("joystick button 5")) || Input.GetKey(KeyCode.Keypad3); }
    private bool input_BREAKING { get => (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey("joystick button 3")); }
    private bool input_BREAKING_down { get => (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown("joystick button 3")); }
    private bool input_SWITCHPOWER { get => (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("joystick button 7")); }
    private float input_POWER_LEFT { get => (Input.GetKey(KeyCode.Keypad2) ? 1f : 0) + Input.GetAxis("LT"); }
    private float input_POWER_RIGHT { get => (Input.GetKey(KeyCode.Keypad1) ? 1f : 0) + Input.GetAxis("RT"); }

    void Update()
    {
        /* --- BASIC INPUTS ----------*/
        if (input_RESET)
        {
            reset_Podracer();
        }
        if (input_SWITCHPOWER && engineState == 0)
        {
            print("Engine_Start");
            sources[0].PlayOneShot(clips[0]);
            Invoke("Engine_Start", duration_engine_starting);
        }
        else if (input_SWITCHPOWER && engineState == 1)
        {
            print("Engine_Shutdown");
            sources[0].PlayOneShot(clips[7]);
            Invoke("Engine_Shutdown", duration_engine_shutdown);
        }

        if (engineState > 0)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus)) ApplyDamage(true);
            if (input_BREAKING_down) sources[0].PlayOneShot(clips[9]);
            /* --- UPDATE BOOSTING ----------*/
            update_Boosting();
            /* --- UPDATE BREAKING INPUT ----------*/
            breaking = input_BREAKING;
            /* --- UPDATE POWER INPUT ----------*/
            enginePowerL = Mathf.Lerp(
                enginePowerL
                , input_POWER_LEFT + boosting_value * boosting_power
                , Time.deltaTime * speed_acceleration);
            enginePowerR = Mathf.Lerp(
                enginePowerR
                , input_POWER_RIGHT + boosting_value * boosting_power
                , Time.deltaTime * speed_acceleration);

            /* --- UPDATE POD_PHYSICS ----------*/
            update_PodDirection();
            update_PodSpeed();
            update_Invulnerable();
            /* --- UPDATE SFX ------------------*/
            updateSFX_Reactors();
            updateSFX_Boosting();
            /* --- UPDATE COMPONENTS -----------*/
            updateSCRIPT_Reactors();
            /* --- UPDATE UI ------------------*/
            update_UI_Cockpit();
            update_UI_distanceTempete();
            update_UI_surchauffe();
        }
    }
    void update_UI_surchauffe()
    {
        ui.img_boost.fillAmount = Mathf.Clamp01(boosting_duration / boosting_durationLimitSec);
    }
    void update_Invulnerable()
    {
        if (invulnerable)
        {
            invulnerable_duration += Time.deltaTime;
            if (invulnerable_duration > invulnerable_durationLimitSec)
            {
                invulnerable = false;
                invulnerable_duration = 0f;
                print("invulnerable is OFF");
            }
        }
    }
    bool boosting_old = false;
    void update_Boosting()
    {
        // SET BOOSTING
        boosting = input_BOOSTING && !boosting_disabled;
        boosting_value = Mathf.Lerp(boosting_value, (boosting ? 1f : 0f), Time.deltaTime / boosting_timeVariationSec);
        // SFX BOOST
        if(!boosting && boosting_old) sources[0].PlayOneShot(clips[10]);
        // GESTION DUREE BOOSTING
        if (boosting) boosting_duration += Time.deltaTime;
        else boosting_duration -= Time.deltaTime / 3f;
        // GESTION bOOSTING DESACTIVEE
        if (boosting_disabled) boosting_disable_duration += Time.deltaTime;
        //SURCHARGE
        if (boosting && boosting_duration > boosting_durationLimitSec)
        {
            print("SURCHARGE");
            ApplyDamage(false);
            boosting_disabled = true;
            boosting_duration = 0f;
        }
        else if (boosting_disabled && boosting_disable_duration > boosting_disable_durationLimitSec)
        {
            print("BOOST DISPO");
            boosting_disabled = false;
            boosting_disable_duration = 0f;
        }
        boosting_old = boosting;
    }
    void update_PodDirection()
    {
        // DIRECTION CALC
        float dirRatio = enginePowerR - enginePowerL;
        rotation_target = dirRatio;
        rotation = Mathf.Lerp(rotation, rotation_target, Time.deltaTime * rotation_variation);
    }
    void update_PodSpeed()
    {
        //SPEED CALC // MAXSPEED = 100f
        speed_target = (float)(Math.Pow((enginePowerR + enginePowerL), 2) * 25d) - (breaking ? speed_value : 0f);
        speed_value = Mathf.Lerp(speed_value, speed_target, Time.deltaTime / speed_timeVariationSec);
    }
    void updateSFX_Reactors()
    {
        sources[1].pitch = 0.8f + (speed_value / 180f) * 0.3f;
        sources[3].pitch = 0.9f + (speed_value / 180f) * 0.2f;
        sources[4].pitch = 0.8f + (speed_value / 180f) * 0.4f;
        //sources[3].pitch = 0.8f + (podSpeed / 100f) * 0.4f;
        sources[1].volume = 1f - Mathf.Max(-0.5f + speed_value / 85f, 0.0f);
        //sources[2].volume = Mathf.Max(- 0.5f + podSpeed / 90f,0.2f);
        sources[2].volume = Mathf.Max(-0.5f + (enginePowerR + enginePowerL) / 5f, 0.2f);

        // WIND PERTUBATION SFX CALC
        //TODO volume perturb = speed * angle
    }
    void updateSFX_Boosting()
    {
        if (input_BOOSTING && Time.timeSinceLevelLoad > nextSFX_accelerating)
        {
            sources[0].PlayOneShot(clips[6]);
            nextSFX_accelerating = Time.timeSinceLevelLoad + 10f;
        }
    }
    void updateSCRIPT_Reactors()
    {
        LeftReactor.podSpeed = RightReactor.podSpeed = lastvelocity;
        //SPOON OPENING
        LeftReactor.reactorPower = enginePowerL;
        RightReactor.reactorPower = enginePowerR;
        LeftReactor.isBreaking = breaking;
        RightReactor.isBreaking = breaking;
        RightReactor.boosting_value = boosting_value;
        LeftReactor.boosting_value = boosting_value;

    }
    void update_UI_Cockpit()
    {
        ui.powerimg_l.fillAmount = enginePowerL;
        ui.powerimg_r.fillAmount = enginePowerR;
        ui.text_dir.text = (rotation * 90f).ToString("F") + "°";
        ui.text_spd.text = speed_value.ToString("F") + "m/s";

    }
    void update_UI_lives()
    {
        ui.text_vie.text = lives.ToString() + " vie" + (lives < 0 ? "" : "s");
        ui.img_feu.fillAmount = 1f - Mathf.Clamp01((lives) / (float)lives_start);
    }
    void update_UI_distanceTempete()
    {
        ui.text_tempete.text = 0f.ToString() + "mètres";
        ui.img_tempete.fillAmount = 0f; ;
    }

    private void FixedUpdate()
    {
        if (engineState > 0)
        {
            //CONSTANT REDUCTION FORCE
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z) * 0.9f;
            body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y + rotation * -body_rotation_mult, body.angularVelocity.z) * 0.9f;

            //HOVERCRAFT FORCE
            RaycastHit hit;
            /**BUG ICI=============================================*/
            Vector3 hoverDirection = Quaternion.AngleAxis(5f - transform.localEulerAngles.x, transform.right) * transform.forward;
            Vector3 hoverUp = transform.up;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000f, terrainMask))
            {
                Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
                Debug.DrawRay(hit.point, hit.normal * 10f, Color.red);
                hoverDirection = Quaternion.AngleAxis(90, transform.right) * hit.normal;
                hoverUp = hit.normal;
                Debug.DrawRay(hit.point, hoverDirection * 10f, Color.cyan);
                body.AddForce(Vector3.up * (body_hover_height - hit.distance) * ground_repulsation * (UnityEngine.Random.Range(1f, body_hover_height / 2f)), ForceMode.Acceleration);
            }
            else
            {
                body.AddForce(Physics.gravity * 10f, ForceMode.Acceleration);

            }

            //SPEED BY ENGINE FORCE
            body.AddForce((transform.forward * speed_value * body_speed_mult), ForceMode.Force);// + (Vector3.up * podSpeed)

            //VIRAGE VELOCITEE
            //STABILISATEUR ASSIETTE.
            Quaternion hoverQuat = Quaternion.LookRotation(hoverDirection, hoverUp);
            //print(hoverQuat.eulerAngles);
            transform.localRotation = Quaternion
                .Lerp(
                transform.localRotation
                , hoverQuat//(hoverDirection, hoverUp) 
                    * (Quaternion.Euler(0, 0, (rotation_target * body_inclinaison_maxangle) - transform.localEulerAngles.z))
                , body_stabilisator_mult);

            //DECELERATION DETECTION
            if (body.velocity.sqrMagnitude + 2f < lastvelocity && Time.timeSinceLevelLoad > nextSFX_slowing)
            {
                sources[0].PlayOneShot(clips[4]);
                nextSFX_slowing = Time.timeSinceLevelLoad + 10f;
            }
            //HEAVY SLOW
            if (body.velocity.sqrMagnitude < 32f && Time.timeSinceLevelLoad > nextSFX_Heavyslowing)
            {
                sources[0].PlayOneShot(clips[5]);
                nextSFX_Heavyslowing = Time.timeSinceLevelLoad + 20f;
            }
            lastvelocity = body.velocity.sqrMagnitude;


            //CHROMATIC ABERRATION / SPEED
            set_ChromaticAberrationValue((speed_value - 80f) / 80f);

            //LIGHTNING LIGHT INTENSITY
            foreach (var l in LightningLight) l.intensity = UnityEngine.Random.Range(0.5f, 1f) * 36f;
        }
    }

    public void set_ChromaticAberrationValue(float intensity)
    {
        if (ppProfile == null) return;
        ChromaticAberrationModel.Settings chromaticSettings = ppProfile.chromaticAberration.settings;
        chromaticSettings.intensity = Mathf.Clamp01(intensity);
        ppProfile.chromaticAberration.settings = chromaticSettings;
    }
    public void Engine_Start()
    {
        print("Engine ON");
        speed_value = 0;
        speed_target = 0;
        rotation = 0;
        rotation_target = 0;
        //
        foreach (var l in LightningAnimation) l.SetActive(true);
        foreach (var l in LightningLight) l.gameObject.SetActive(true);
        set_ChromaticAberrationValue(0f);
        breaking = false;
        //
        RightReactor.startReactor();
        LeftReactor.startReactor();
        reset_EnginePower();
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
        //
        sources[4].clip = clips[8];
        sources[4].loop = true;
        sources[4].volume = 0.2f;
        sources[4].pitch = 0.9f;
        sources[4].Play();
        //
        Invoke("activateControls", 3f);
    }
    public void activateControls()
    {
        engineState = 1;
    }

    public void Engine_Shutdown()
    {
        print("Engine OFF");
        engineState = 0;
        speed_value = 0;
        speed_target = 0;
        rotation = 0;
        rotation_target = 0;
        //
        foreach (var l in LightningAnimation) l.SetActive(false);
        foreach (var l in LightningLight) l.gameObject.SetActive(false);
        set_ChromaticAberrationValue(0f);
        //
        RightReactor.shutdownReactor();
        LeftReactor.shutdownReactor();
        reset_EnginePower();
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
        //
        sources[4].loop = false;
        sources[4].Stop();
        sources[4].volume = 0.15f;
        breaking = false;
    }
    public void ApplyDamage(bool resetVelocityAndPower)
    {
        if (invulnerable) return;

        print($"OnCollisionEnter : {--lives} lives");
        sources[0].PlayOneShot(clips[11]);
        if (Mathf.Abs(lives) % 2 == 0)
        {
            foreach (var blacksmoke in LeftReactor.blacksmokes)
                if (!blacksmoke.activeInHierarchy)
                { blacksmoke.SetActive(true); break; }
            LeftReactor.spawnExplosion();
        }
        else
        {
            foreach (var blacksmoke in RightReactor.blacksmokes)
                if (!blacksmoke.activeInHierarchy)
                { blacksmoke.SetActive(true); break; }
            RightReactor.spawnExplosion();
        }

        if (lives == 0) game_gameover();
        if (resetVelocityAndPower)
        {
            reset_PodracerVelocity();
            reset_EnginePower();
        }
        setInvulnerable();
        update_UI_lives();
    }
    public void setInvulnerable()
    {
        //SET INVULNERABLE
        invulnerable = true;
        invulnerable_duration = 0f;
    }
    public void game_gameover()
    {
        print("game_gameover");
        Engine_Shutdown();
    }
    public void game_win()
    {
        print("game_win");
    }
    private void OnApplicationQuit()
    {
        set_ChromaticAberrationValue(0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (engineState > 0)
        {
            ApplyDamage(true);
        }
    }
    //private void OnCollisionExit(Collision collision)



    private void reset_UI()
    {
        ui.img_boost.fillAmount = 0f;
        ui.img_feu.fillAmount = 0f;
        ui.img_tempete.fillAmount = 0f;

        ui.text_dir.text = "-";
        ui.text_spd.text = "-";
        ui.text_tempete.text = "-";
        ui.text_vie.text = "-";

        ui.powerimg_l.fillAmount = 0f;
        ui.powerimg_r.fillAmount = 0f;
    }
    private void reset_EnginePower()
    {
        enginePowerL = 0f;
        enginePowerR = 0f;
    }
    private void reset_PodracerVelocity()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        speed_value = 0f;
    }
    private void reset_Podracer()
    {
        print("resetPodracer !");
        transform.position = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        reset_PodracerVelocity();
        reset_EnginePower();
        breaking = false;
        lives = lives_start;
        update_UI_lives();
        foreach (var blacksmoke in RightReactor.blacksmokes) blacksmoke.SetActive(false);
    }
}
