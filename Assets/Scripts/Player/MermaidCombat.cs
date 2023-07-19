using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidCombat : MonoBehaviour
{
    [Tooltip("Time between starfish throws in seconds")]
    public float StarfishCooldown = 0.2f;

    [Tooltip("The distance from center from which to throw projectiles")]
    public float ThrowReleaseOffset = 2f;

    [Tooltip("Starfish Prefab")]
    public GameObject StarfishPrefab;

    private float _nextStarfishThrowTime = 0f;

    /// <summary>
    /// Attack by throwing a starfish.
    /// </summary>
    /// <param name="direction">The direction to throw the starfish.</param>
    public void ThrowStarfish(Vector2 direction)
    {
        float currentTime = Time.time;
        if (currentTime >= _nextStarfishThrowTime)
        {
            _nextStarfishThrowTime = currentTime + StarfishCooldown;
            Vector3 throwReleaseOffset = (Vector3)(direction.normalized * ThrowReleaseOffset * transform.localScale.x);
            Vector3 spawnLocation = transform.position + throwReleaseOffset;
            GameObject starfish = Instantiate(StarfishPrefab, spawnLocation, Quaternion.identity);
            starfish.GetComponent<StarfishController>().SetDirection(direction);
        }
    }
}
