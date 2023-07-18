using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidSizing : MonoBehaviour
{
    private PlayerSizeLabel labelScript;
    private string sizeText = "Size 5: 2.5 units wide x 5 units tall";

    private void Start()
    {
        labelScript = GameObject.Find("Mermaid Size Text").GetComponent<PlayerSizeLabel>();
    }

    private void Update()
    {
        Vector3 currentScale = transform.localScale;
        if(Input.GetKey(KeyCode.Alpha1))
        {
            currentScale = new Vector3(0.5f, 0.5f, 1);
            sizeText = "Size 1: 1.25 units wide x 2.5 units tall";
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            currentScale = new Vector3(0.666f, 0.666f, 1);
            sizeText = "Size 2: 1.66 units wide x 3.33 units tall";
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            currentScale = new Vector3(0.8f, 0.8f, 1);
            sizeText = "Size 3: 2 units wide x 4 units tall";
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            currentScale = new Vector3(0.875f, 0.875f, 1);
            sizeText = "Size 4: 2.1875 units wide x 4.375 units tall";
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            currentScale = new Vector3(1.0f, 1.0f, 1);
            sizeText = "Size 5: 2.5 units wide x 5 units tall";
        }
        transform.localScale = currentScale;
        labelScript.SetText(sizeText);
    }
}
