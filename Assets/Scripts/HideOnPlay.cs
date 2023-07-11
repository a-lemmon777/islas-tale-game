using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideOnPlay : MonoBehaviour
{
    /// <summary>
    /// The image is a red X
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }
}
