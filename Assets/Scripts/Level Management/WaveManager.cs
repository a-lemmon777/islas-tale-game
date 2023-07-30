using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use this properly, all the enemy prefab instances must be children of
/// this game object.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [Tooltip("Reference to the enemy spawner")]
    public EnemySpawner EnemySpawner;

    [Tooltip("When the wave activates"), HideInInspector]
    public UnityEvent Activate;

    [Tooltip("When an enemy of this wave dies"), HideInInspector]
    public UnityEvent EnemyDown;

    [Tooltip("List of enemy prefab roots belonging to this wave")]
    public List<GameObject> Enemies = new List<GameObject>();

    [Tooltip("List of obstacles")]
    public List<GameObject> Obstacles = new List<GameObject>();

    [Tooltip("How many enemies are left")]
    public int EnemiesRemaining;

    void Awake()
    {
        Activate.AddListener(() =>
        {
            Enemies.ForEach((enemy) => enemy.SetActive(true));
            Obstacles.ForEach((obstacle) => obstacle.SetActive(true));
        });

        EnemyDown.AddListener(() =>
        {
            EnemiesRemaining--;

            if (EnemiesRemaining == 0) EnemySpawner.WaveCompleted.Invoke();
        });

        // find all the enemy prefab roots that are children of this game object
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemies")
                Enemies.Add(child.gameObject);

            if (child.tag == "Obstacles")
                Obstacles.Add(child.gameObject);
        }

        EnemiesRemaining = Enemies.Count;

        Enemies.ForEach((enemy) => enemy.SetActive(false));
        Obstacles.ForEach((obstacle) => obstacle.SetActive(false));
    }

    void OnDisable()
    {
        Activate.RemoveAllListeners();
        EnemyDown.RemoveAllListeners();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
