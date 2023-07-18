using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSizeLabel : MonoBehaviour
{
    public void SetText(string sizeInformation)
    {
        GetComponent<Text>().text = sizeInformation;
    }
}
