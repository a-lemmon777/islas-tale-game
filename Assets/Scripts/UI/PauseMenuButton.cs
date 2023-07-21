using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    [Tooltip("Crown game object of this button")]
    public GameObject Crown;

    public void ShowCrown()
    {
        Crown.SetActive(true);
    }

    public void HideCrown()
    {
        Crown.SetActive(false);
    }
}
