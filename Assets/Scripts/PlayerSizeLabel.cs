using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSizeLabel : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(442.4f, 50);
    }
    public void SetText(string sizeInformation)
    {
        GetComponent<Text>().text = sizeInformation;
    }
}
