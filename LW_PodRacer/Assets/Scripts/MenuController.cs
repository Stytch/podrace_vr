using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{

    public GameObject MenuButton;
    public GameObject Canvas;

    void Start()
    {
        OpenMainMenu();
    }
    
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

    public void OpenMainMenu()
    {
        GameObject PlayButton = Instantiate(MenuButton, Canvas.transform);
        PlayButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Play;
        PlayButton.GetComponent<MenuButtonController>().MenuController = this;
        PlayButton.GetComponent<MenuButtonController>().ButtonText = "Play";

        GameObject OptionButton = Instantiate(MenuButton, Canvas.transform);
        OptionButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Option;
        OptionButton.GetComponent<MenuButtonController>().MenuController = this;
        OptionButton.GetComponent<MenuButtonController>().ButtonText = "Options";

        GameObject QuitButton = Instantiate(MenuButton, Canvas.transform);
        QuitButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Quit;
        QuitButton.GetComponent<MenuButtonController>().MenuController = this;
        QuitButton.GetComponent<MenuButtonController>().ButtonText = "Quit";
    }

    public void OpenPauseMenu()
    {
        GameObject PlayButton = Instantiate(MenuButton, Canvas.transform);
        PlayButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Resume;
        PlayButton.GetComponent<MenuButtonController>().MenuController = this;
        PlayButton.GetComponent<MenuButtonController>().ButtonText = "Resume";

        GameObject OptionButton = Instantiate(MenuButton, Canvas.transform);
        OptionButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Option;
        OptionButton.GetComponent<MenuButtonController>().MenuController = this;
        OptionButton.GetComponent<MenuButtonController>().ButtonText = "Options";

        GameObject QuitButton = Instantiate(MenuButton, Canvas.transform);
        QuitButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Menu;
        QuitButton.GetComponent<MenuButtonController>().MenuController = this;
        QuitButton.GetComponent<MenuButtonController>().ButtonText = "Quit to Menu";
    }

    public void OpenDeathMenu()
    {
        GameObject PlayButton = Instantiate(MenuButton, Canvas.transform);
        PlayButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Play;
        PlayButton.GetComponent<MenuButtonController>().MenuController = this;
        PlayButton.GetComponent<MenuButtonController>().ButtonText = "Replay";

        GameObject OptionButton = Instantiate(MenuButton, Canvas.transform);
        OptionButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Option;
        OptionButton.GetComponent<MenuButtonController>().MenuController = this;
        OptionButton.GetComponent<MenuButtonController>().ButtonText = "Options";

        GameObject QuitButton = Instantiate(MenuButton, Canvas.transform);
        QuitButton.GetComponent<MenuButtonController>().CurrentButtonType = MenuButtonController.ButtonTypes.Menu;
        QuitButton.GetComponent<MenuButtonController>().MenuController = this;
        QuitButton.GetComponent<MenuButtonController>().ButtonText = "Quit to Menu";
    }
}
