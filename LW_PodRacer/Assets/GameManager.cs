using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource as_Music;
    public AudioSource as_InterfaceSound;
    public AudioClip[] ac_Clips;
    public GameObject gop_Podracer;
    public Transform t_spawnPodracer;
    public Transform t_spawnTempete;
    public GameObject go_tempete;
    // PODRACER - PLAYER
    private GameObject go_Podracer;
    private SFX_Controller m_Podracer;
    // GAME - STATUS
    private Coroutine cor_gameCoroutine;
    bool gameAlive = false;
    // GAME - STATUS
    public GameObject panel_win;
    public GameObject panel_loose;
    public GameObject panel_mainmenu; //attente de jules...
    //
    public GameObject camera;

    void Start()
    {
        print("GAMEMANAGER_START");
        panel_mainmenu.SetActive(true);
    }

    public void startupbtn()
    {
        print("startupbtn");
        resetGame();
        cor_gameCoroutine = StartCoroutine(StartNewGame());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !gameAlive)
        {
            resetGame();
            cor_gameCoroutine = StartCoroutine(StartNewGame());
        }
        if (Input.GetKeyDown(KeyCode.P) && gameAlive)
        {
            endGame(GameEndStatus.abandon);
        }
    }

    public void generatePlayerPodracer()
    {
        go_Podracer = Instantiate(gop_Podracer, t_spawnPodracer.position, t_spawnPodracer.rotation);
        m_Podracer = go_Podracer.GetComponent<SFX_Controller>();
        m_Podracer.m_gamemanager = this;
        // TODO : ATTACH CAMERA ?
    }
    public void destroyPlayerPodRacer()
    {
        // TODO : DETACH CAMERA ?
        Destroy(go_Podracer);
    }
    public void endGame(GameEndStatus status)
    {
        if (gameAlive)
        {
            camera.SetActive(true);
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
            panel_mainmenu.SetActive(true);
            gameAlive = false;
        }
    }

    IEnumerator StartNewGame()
    {
        gameAlive = true;
        print("START NEW GAME");

        //SETUP GAME
        closeEndGamePanel();
        panel_loose.SetActive(false);
        generatePlayerPodracer();
        camera.SetActive(false);
        //
        while (gameAlive)
        {
            yield return null;
        }
        destroyPlayerPodRacer();
        cor_gameCoroutine = null;
        gameAlive = false;
    }

    // pour plus tard...
    public void resetGame()
    {
        go_tempete.transform.position = t_spawnTempete.position;
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
