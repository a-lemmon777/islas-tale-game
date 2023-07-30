using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LevelEvents LevelEvents;

    public void Close()
    {
        LevelEvents.CloseTutorial.Invoke();
    }
}
