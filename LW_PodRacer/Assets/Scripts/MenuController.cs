using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button OptionButton;
    public Button QuitButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButton()
    {
        //On lance le jeu
        Debug.Log("PlayButton Pressed");
    }

    public void OnOptionButton()
    {
        //On ouvre le menu des options
        Debug.Log("OptionButton Pressed");
    }

    public void OnQuitButton()
    {
        Debug.Log("QuitButton Pressed");
        Application.Quit();
    }
}
