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

    [Tooltip("List of barrels")]
    public List<ToxicBarrel> Barrels = new List<ToxicBarrel>();

    [Tooltip("How many enemies are left")]
    public int EnemiesRemaining;

    void Awake()
    {
        Activate.AddListener(Spawn);

        EnemyDown.AddListener(() =>
        {
            EnemiesRemaining--;

            if (EnemiesRemaining == 0)
            {
                Barrels.ForEach((barrel) => barrel.Deactivate.Invoke());
                EnemySpawner.WaveCompleted.Invoke();
            }
        });

        // find all the enemy prefab roots that are children of this game object
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemies")
                Enemies.Add(child.GetComponent<ShrimpSpawner>());

            if (child.tag == "Obstacles")
                Obstacles.Add(child.GetComponent<ObstacleDelay>());

            if (child.tag == "Barrels")
                Barrels.Add(child.GetComponent<ToxicBarrel>());
        }

        EnemiesRemaining = Enemies.Count;

        // disable everything until the wave actually starts
        Enemies.ForEach((enemy) => enemy.gameObject.SetActive(false));
        Obstacles.ForEach((obstacle) => obstacle.gameObject.SetActive(false));
        Barrels.ForEach((barrel) => barrel.gameObject.SetActive(false));
    }

    /// <summary>
    /// Properly spawns the enemies and obstacles after their designated delay
    /// </summary>
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

        IEnumerator DelayBarrel(ToxicBarrel toxicBarrel)
        {
            yield return new WaitForSeconds(toxicBarrel.DelayToSpawn);
            toxicBarrel.gameObject.SetActive(true);
        }

        Barrels.ForEach((barrel) => StartCoroutine(DelayBarrel(barrel)));
    }

    void OnDisable()
    {
        Activate.RemoveAllListeners();
        EnemyDown.RemoveAllListeners();
    }

}
