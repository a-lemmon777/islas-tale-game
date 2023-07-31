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

    [Tooltip("When the current wave ends"), HideInInspector]
    public UnityEvent WaveCompleted;

    private void Awake()
    {
        WaveCompleted.AddListener(() =>
        {
            // clean up all the stars
            foreach (var star in GameObject.FindObjectsOfType<StarfishController>())
            {
                Destroy(star.gameObject);
            }

            // destroy all the barrels
            WaveList[CurrentWave].Barrels.ForEach((barrel) => Destroy(barrel.gameObject));

            if (CurrentWave == WaveList.Count - 1)
            {
                LevelEvents.Victory.Invoke();
                return;
            }

            CurrentWave++;
            ReadyWave(CurrentWave);
        });

        LevelEvents.Start.AddListener(() => ReadyWave(0));
    }

    void OnDisable()
    {
        WaveCompleted.RemoveAllListeners();
    }

    private void ReadyWave(int waveIndex)
    {
        StartCoroutine(ActivateWave(waveIndex));
    }

    private IEnumerator ActivateWave(int waveIndex)
    {
        yield return new WaitForSeconds(2f);
        WaveList[waveIndex].Activate.Invoke();
    }
}
