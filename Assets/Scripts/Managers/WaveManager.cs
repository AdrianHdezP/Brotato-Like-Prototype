using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager instance;

    public static WaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("No instance");
            }

            return instance;
        }
    }

    [Header("Wave Manager")]
    public int enemiesInRound = 0;
    [SerializeField] private float spawnRate = 2.5f;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private int[] enemyCost;

    //public int round { get; private set; } = 1;
    public int round = 1;
    public int maxRound = 25;
    public bool isInRound { get; private set; } = true;

    private int defaultRoundCost = 5;
    private int roundCost = 5;
    private float enemySpawnTimer = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        EnemiesWarning();

        enemySpawnTimer -= Time.deltaTime;

        RoundSystem();
    }

    private void EnemiesWarning()
    {
        if (enemiesInRound < 0)
            Debug.LogWarning("Enemies < 0");
    }

    private void RoundSystem()
    {
        if (roundCost > 0)
        {
            isInRound = true;

            SpawnEnemies();
        }
        else if (roundCost <= 0 && enemiesInRound <= 0)
        {
            isInRound = false;
        }
    }

    private void SpawnEnemies()
    {
        int randomSpawn = Random.Range(0, spawns.Length);
        int randomIndex = Random.Range(0, enemyCost.Length);

        if (enemyCost[randomIndex] <= roundCost && enemySpawnTimer <= 0)
        {
            Instantiate(enemyPrefab[randomIndex], spawns[randomSpawn].position, Quaternion.identity);
            enemiesInRound++;
            roundCost -= enemyCost[randomIndex];
            enemySpawnTimer = spawnRate;
        }
        else if (enemyCost[randomIndex] > roundCost) //Cuidao
        {
            randomIndex = Random.Range(0, enemyCost.Length);
        }
    }

    public void NextRound()
    {      
        round++;
        roundCost = defaultRoundCost + 5;
        defaultRoundCost = roundCost;
        ShopManager.Instance.LoadItems();
    }

}
