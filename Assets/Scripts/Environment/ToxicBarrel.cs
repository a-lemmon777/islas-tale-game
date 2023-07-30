using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToxicBarrel : MonoBehaviour
{
    [Tooltip("Prefab of the toxic obstacle")]
    public GameObject ToxicObstaclePrefab;

    [Tooltip("Direction of the emissions")]
    public Transform EmissionDirection;

    [Tooltip("Source of the emission")]
    public Transform EmissionSource;

    [Tooltip("Time in seconds between emissions")]
    public float TimeInterval;

    [Tooltip("Time to wait before spawning in the wave, in seconds")]
    public float DelayToSpawn;

    [Tooltip("How fast should the barrel wind up, relative to one second")]
    public float WindUpSpeedMultiplier;

    public UnityEvent Deactivate;

    private Animator _animator;

    void Awake()
    {
        Deactivate.AddListener(() =>
        {
            _animator.SetTrigger("Deactivate");
        });
    }

    void Start()
    {
        _animator = GetComponent<Animator>();

        PassiveLinearEmission();
    }

    private void PassiveLinearEmission()
    {
        _animator.SetFloat("Speed Multiplier", 0.5f);
        _animator.SetFloat("Wind Up Speed Multiplier", WindUpSpeedMultiplier);
    }

    public void EmitToxicObstacle()
    {
        var obstacle = Instantiate(ToxicObstaclePrefab, EmissionSource.transform.position, Quaternion.identity);
        var destination = obstacle.transform.Find("Destination");
        destination.position = EmissionDirection.position;
    }
}
