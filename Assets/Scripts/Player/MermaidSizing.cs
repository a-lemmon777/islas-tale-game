using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidSizing : MonoBehaviour
{

    private void FixedUpdate()
    {
        Vector3 currentScale = transform.localScale;
        if(Input.GetKey(KeyCode.Alpha1))
        {
            currentScale = new Vector3(0.5f, 0.5f, 1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            currentScale = new Vector3(0.666f, 0.666f, 1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            currentScale = new Vector3(0.8f, 0.8f, 1);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            currentScale = new Vector3(0.875f, 0.875f, 1);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            currentScale = new Vector3(1.0f, 1.0f, 1);
        }
        transform.localScale = currentScale;
    }
}
