using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void ClickButton(string buttonName)
    {
        switch (buttonName)
        {
            case "Main Menu":
                SceneManager.LoadScene("MainMenu");
                break;
            case "Credits":
                break;
            default:
                break;
        }
    }
}
