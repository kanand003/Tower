using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] [Range(0.1f,20f)] float spawnTimer = 1f;
    [SerializeField] [Range(0, 40)] int poolsize = 5;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolsize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(EnemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectinPool() // TODO Gives a NullRef
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectinPool();
            //Instantiate(EnemyPrefab, transform);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
