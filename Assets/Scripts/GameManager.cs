using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> enemyPool;
    [SerializeField]
    private int enemyPoolSize;
    [SerializeField]
    private GameObject enemyPrefab;
    
    [SerializeField]
    private List<GameObject> spawners;
    [SerializeField]
    private List<Wave> waves;

    [SerializeField]
    private float randomMagniutde;

    [SerializeField]
    private List<GameObject> targets;
    [SerializeField]
    private int totalEnemyCount = 0;

    [SerializeField]
    private float timerSetting = 5f;
    [SerializeField]
    private float timer = 5f;
    [SerializeField]
    private bool timerBegan = false;
    void Start()
    {
        Target[] targetArray = FindObjectsOfType<Target>() as Target[];
        
        for(int i = 0; i < targetArray.Length; i++)
        {
            targets.Add(targetArray[i].gameObject);
        }

        enemyPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < enemyPoolSize; i++)
        {
            tmp = Instantiate(enemyPrefab);
            tmp.SetActive(false);
            enemyPool.Add(tmp);
        }
    }
    void Update()
    {
        if(totalEnemyCount <= 0 && timerBegan == false)
        {
            timerBegan = true;
        }
        if(timerBegan)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            timerBegan = false;
            timer = timerSetting;
            StartWave();
        }
    }
    public List<GameObject> GetTargets()
    {
        return targets;
    }
    public List<GameObject> GetEnemies()
    {
        return enemyPool;
    }
    public void StartWave()
    {
        if (waves.Count > 0)
        {
            Wave currentWave = waves[0];
            totalEnemyCount = 0;
            for (int i = 0; i < currentWave.EnemyDistributions.Count; i++)
            {
                if (spawners[i] != null)
                {
                    for (int j = 0; j < currentWave.EnemyDistributions[i]; j++)
                    {
                        SpawnEnemy(spawners[i].transform);
                    }
                }
            }
            waves.RemoveAt(0);
        }
    }
    private void SpawnEnemy(Transform transform)
    {
        GameObject enemy = null;
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemy = enemyPool[i];
                totalEnemyCount++;
                break;
            }
        }
        if(enemy != null)
        {
            enemy.transform.position = new Vector3(transform.position.x + Random.insideUnitSphere.x * randomMagniutde, transform.position.y, transform.position.z + Random.insideUnitSphere.z * randomMagniutde);
            enemy.SetActive(true);
        }
    }
    public void EnemyRemoved()
    {
        totalEnemyCount--;
    }
}
