using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SFX/MANAGEMENT")]
    public AudioSource as_Music;
    public AudioSource as_InterfaceSound;
    public AudioClip[] ac_Clips;
    [Header("GAM/GAME OBJECTS")]
    public GameObject gop_Podracer;
    public Transform t_spawnPodracer;
    public Transform t_spawnTempete;
    public Tempete m_tempete;
    // PODRACER - PLAYER
    private GameObject go_Podracer;
    private SFX_Controller m_Podracer;
    // GAME - STATUS
    private Coroutine cor_gameCoroutine;
    bool gameAlive = false;
    [Header("UI/PANELS")]
    // GAME - STATUS
    public GameObject panel_win;
    public GameObject panel_loose;
    public GameObject panel_mainmenu; //attente de jules...
    //
    [Header("UI/CAMERA")]
    public GameObject cam_mainmenu;
    public GameObject cam_podracer;
    [Header("GAM/TRACK GENERATOR")]
    public TrackManager m_trackManager; // attente full integration procedural finoux
    public GameObject go_ChunkEnd;
    [Header("GAM/CAMERAMAINMENU")]
    private GameObject go_playerVR;
    public GameObject gop_playerVR;
    public Transform t_spawnMainCamera;

    void Start()
    {
        print("GAMEMANAGER_START");
        //panel_mainmenu.SetActive(true);
        //spawnPlayerMainMenu();
    }

    public void startupbtn()
    {
        print("startupbtn");
        if (!gameAlive)
        {
            resetGame();
            cor_gameCoroutine = StartCoroutine(StartNewGame());
        }
        else
        {
            endGame(GameEndStatus.abandon);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) startupbtn();
    }

    public void generatePlayerPodracer()
    {
        go_Podracer = Instantiate(gop_Podracer, t_spawnPodracer.position, t_spawnPodracer.rotation);
        m_Podracer = go_Podracer.GetComponent<SFX_Controller>();
        m_Podracer.m_gamemanager = this;
        m_Podracer.btnpower();
        cam_podracer = go_Podracer.GetComponentInChildren<Camera>().gameObject;
        m_Podracer.m_tempete = m_tempete;
        // TODO : ATTACH CAMERA ?
    }
    public void destroyPlayerPodRacer()
    {
        // TODO : DETACH CAMERA ?
        Destroy(go_Podracer);
        m_Podracer = null;
        cam_podracer = null;
    }
    public void endGame(GameEndStatus status)
    {
        if (gameAlive)
        {
            switch (status)
            {
                case GameEndStatus.win:
                    panel_win.SetActive(true);
                    break;
                case GameEndStatus.loose:
                    panel_loose.SetActive(true);
                    break;
                default:
                    break;
            }

            //PRIMODIAL
            cam_mainmenu.SetActive(true);
            //panel_mainmenu.SetActive(true);
            gameAlive = false;
            m_tempete.moveEnabled = false;
        }
    }

    IEnumerator StartNewGame()
    {
        //GENERATION DU TRACK
        try { m_trackManager.CleanAll(); }
        catch { }
        m_trackManager.GenerateTrack();
        var end = m_trackManager.GetEndPosition();
        end.y = go_ChunkEnd.transform.position.y;
        print(end);
        go_ChunkEnd.transform.position = end;

        gameAlive = true;
        print("START NEW GAME");
        m_tempete.moveEnabled = true;
        //SETUP GAME
        closeEndGamePanel();
        panel_loose.SetActive(false);
        cam_mainmenu.SetActive(false);
        //panel_mainmenu.SetActive(false);
        generatePlayerPodracer();
        cam_podracer.SetActive(true);
        //
        while (gameAlive)
        {
            yield return null;
        }
        destroyPlayerPodRacer();
        cor_gameCoroutine = null;
        gameAlive = false;
        m_tempete.moveEnabled = false;
        spawnPlayerMainMenu();



    }

    public void spawnPlayerMainMenu()
    {
        if (go_playerVR != null) Destroy(go_playerVR);
        go_playerVR = Instantiate(gop_playerVR, t_spawnMainCamera.position, t_spawnMainCamera.rotation);
        cam_mainmenu = go_playerVR.GetComponentInChildren<Camera>().gameObject;
    }

    // pour plus tard...
    public void resetGame()
    {
        endGame(GameEndStatus.abandon);
        m_tempete.transform.position = t_spawnTempete.position;
        cor_gameCoroutine = null;
        gameAlive = false;
    }
    public void closeEndGamePanel()
    {
        panel_win.SetActive(false);
        panel_loose.SetActive(false);
    }
}
public enum GameEndStatus
{
    abandon,
    win,
    loose,
}
