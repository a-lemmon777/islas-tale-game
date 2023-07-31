using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [Tooltip("Reference to the level manager state machine")]
    public LevelEvents LevelEvents;

    /// <summary>
    /// Click listeners to menu options
    /// </summary>
    /// <param name="buttonName"></param>
    public void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "Resume":
                LevelEvents.Resume.Invoke();
                break;

            case "Options":
                Debug.Log("Clicked options");
                break;

            case "Exit":
                Application.Quit();
                break;

            default:

                break;
        }
    }
}
