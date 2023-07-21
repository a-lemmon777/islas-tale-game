using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [Tooltip("Reference to the level manager state machine")]
    public LevelStateMachine LevelStateMachine;

    public void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "Resume":
                LevelStateMachine.TransitionToState(LevelStateMachine.LevelState.IN_BATTLE);
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
