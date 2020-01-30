using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public enum ButtonTypes { Play, Option, Quit, Resume, Menu};        //Enums type of buttons possible

    public ButtonTypes CurrentButtonType;                               //Contains the type of button created

    public MenuController MenuController;                               //Contains the MenuController, used to call OnClick

    public string ButtonText;                                           //Contains the text displayed on the button

    void Start()
    {
        GetComponentInChildren<Text>().text = ButtonText;               //We update the text on the button
    }
    
    void Update()
    {
        
    }

    /// <summary>
    /// Calls the corresponding OnClick fonction in MenuController
    /// Doesn't take any argument, doesn't return anything
    /// Called by the Menu Buttons
    /// </summary>
    public void OnClick()
    {
        switch(CurrentButtonType)
        {
            case ButtonTypes.Play:
                MenuController.OnPlayButton();
                break;

            case ButtonTypes.Option:
                MenuController.OnOptionButton();
                break;

            case ButtonTypes.Quit:
                MenuController.OnQuitButton();
                break;

            case ButtonTypes.Resume:
                break;

            case ButtonTypes.Menu:
                break;

            default:
                break;
        }
    }
}
