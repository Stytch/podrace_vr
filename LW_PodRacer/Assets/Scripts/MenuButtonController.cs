using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public enum ButtonTypes { Play, Option, Quit, Resume, Menu};

    public ButtonTypes CurrentButtonType;

    public MenuController MenuController;

    public string ButtonText;

    void Start()
    {
        GetComponentInChildren<Text>().text = ButtonText;
    }
    
    void Update()
    {
        
    }

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
