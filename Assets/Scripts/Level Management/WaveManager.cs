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
    public List<ShrimpSpawner> Enemies = new List<ShrimpSpawner>();

    [Tooltip("List of obstacles")]
    public List<ObstacleDelay> Obstacles = new List<ObstacleDelay>();

    [Tooltip("How many enemies are left")]
    public int EnemiesRemaining;

    void Awake()
    {
        Activate.AddListener(Spawn);

        EnemyDown.AddListener(() =>
        {
            EnemiesRemaining--;

            if (EnemiesRemaining == 0) EnemySpawner.WaveCompleted.Invoke();
        });

        // find all the enemy prefab roots that are children of this game object
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemies")
                Enemies.Add(child.GetComponent<ShrimpSpawner>());

            if (child.tag == "Obstacles")
                Obstacles.Add(child.GetComponent<ObstacleDelay>());
        }

        EnemiesRemaining = Enemies.Count;

        // disable everything until the wave actually starts
        Enemies.ForEach((enemy) => enemy.gameObject.SetActive(false));
        Obstacles.ForEach((obstacle) => obstacle.gameObject.SetActive(false));
    }

    private void Spawn()
    {
        IEnumerator DelayShrimp(ShrimpSpawner shrimpSpawner)
        {
            yield return new WaitForSeconds(shrimpSpawner.DelayToSpawn);
            shrimpSpawner.gameObject.SetActive(true);
        }

        Enemies.ForEach((enemy) => StartCoroutine(DelayShrimp(enemy)));

        IEnumerator DelayObstacle(ObstacleDelay obstacleDelay)
        {
            yield return new WaitForSeconds(obstacleDelay.DelayToSpawn);
            obstacleDelay.gameObject.SetActive(true);
        }

        Obstacles.ForEach((obstacle) => StartCoroutine(DelayObstacle(obstacle)));
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
