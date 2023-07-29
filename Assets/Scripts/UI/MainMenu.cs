using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// Callback function for button clicks
    /// </summary>
    /// <param name="buttonName">Name of the button</param>
    public void ClickButton(string buttonName)
    {
        switch (buttonName)
        {
            case "Start":
                SceneManager.LoadScene("Player Mermaid");
                break;
            case "Credits":
                SceneManager.LoadScene("Credits");
                break;
            default:
                break;
        }
    }

}
