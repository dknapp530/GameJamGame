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
    private List<GameObject> targets;
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
        
    }
    public List<GameObject> GetTargets()
    {
        return targets;
    }
}
