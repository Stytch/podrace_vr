using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{

    public GameObject MenuButton;       //Contains the Prefab MenuButton, used to create buttons while opening menus
    public GameObject Canvas;           //Contains the Parent used when Instantiating Menu Buttons

    void Start()
    {
        OpenMainMenu();
    }
    
    void Update()
    {
        
    }

    /// <summary>
    /// Launches the game (still needs work)
    /// Doesn't take any argument, doesn't return anything
    /// Called by MenuButtonController
    /// </summary>
    public void OnPlayButton()
    {
        //On lance le jeu
        Debug.Log("PlayButton Pressed");
    }

    /// <summary>
    /// Opens the Option Menu
    /// Doesn't take any argument, doesn't return anything
    /// Called by MenuButtonController
    /// </summary>
    public void OnOptionButton()
    {
        //On ouvre le menu des options
        Debug.Log("OptionButton Pressed");
    }

    /// <summary>
    /// Quits the game totally
    /// Doesn't take any argument, doesn't return anything
    /// Called by MenuButtonController
    /// </summary>
    public void OnQuitButton()
    {
        Debug.Log("QuitButton Pressed");
        Application.Quit();
    }

    /// <summary>
    /// Resumes the current game
    /// Doesn't take any argument, doesn't return anything
    /// Called by MenuButtonController
    /// </summary>
    public void OnResumeButton()
    {
        Debug.Log("ResumeButton Pressed");
    }

    /// <summary>
    /// Goes back to Main Menu
    /// Doesn't take any argument, doesn't return anything
    /// Called by MenuButtonController
    /// </summary>
    public void OnMenuButton()
    {
        Debug.Log("MenuButton Pressed");
    }

    /// <summary>
    /// Open the Main Menu
    /// Doesn't take any argument, doesn't return anything
    /// </summary>
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

    /// <summary>
    /// Open the Menu while pausing (doesn't pause the game yet)
    /// Doesn't take any argument, doesn't return anything
    /// </summary>
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

    /// <summary>
    /// Open the Menu after a death or winning the game
    /// Doesn't take any argument, doesn't return anything
    /// </summary>
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
