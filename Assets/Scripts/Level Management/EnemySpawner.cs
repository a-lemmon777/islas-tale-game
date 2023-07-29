using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Scriptable object of level events")]
    public LevelEvents LevelEvents;

    [Tooltip("List of enemy waves")]
    public List<WaveManager> WaveList;

    [Tooltip("The current wave number")]
    public int CurrentWave = 0;

    [Tooltip("When the current wave ends")]
    public UnityEvent WaveCompleted;

    private void Awake()
    {
        WaveCompleted.AddListener(() =>
        {
            if (CurrentWave == WaveList.Count - 1)
            {
                LevelEvents.Victory.Invoke();
                return;
            }

            CurrentWave++;
            WaveList[CurrentWave].Activate.Invoke();
        });
    }

    void OnDisable()
    {
        WaveCompleted.RemoveAllListeners();
    }

    private void Start()
    {
        StartCoroutine(ActivateFirstWave());
    }

    private IEnumerator ActivateFirstWave()
    {
        yield return new WaitForSeconds(2f);
        WaveList[0].Activate.Invoke();
    }
}
