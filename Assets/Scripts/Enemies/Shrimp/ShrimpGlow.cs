using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpGlow : MonoBehaviour
{
    [Tooltip("Reference to the shrimp health")]
    public ShrimpHealth ShrimpHealth;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        // renderer is disabled by default
        _spriteRenderer = GetComponent<SpriteRenderer>();


        ShrimpHealth.PowerUp.AddListener(() =>
        {
            _spriteRenderer.enabled = true;
            StartCoroutine(FadeOut());
        });
    }

    IEnumerator FadeOut()
    {
        Color color = _spriteRenderer.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 2f * Time.deltaTime) // rate of loss * time
        {
            color.a = alpha;
            _spriteRenderer.color = color;
            yield return null;
        }

        color.a = 0;
        _spriteRenderer.enabled = false;
        yield return null;
    }
}
