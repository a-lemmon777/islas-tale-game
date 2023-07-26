using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ClickButton(string buttonName)
    {
        switch (buttonName)
        {
            case "Try Again":
                SceneManager.LoadScene("Player Mermaid");
                break;

            case "Main Menu":
                SceneManager.LoadScene("MainMenu");
                break;

            default:
                break;
        }
    }
}
