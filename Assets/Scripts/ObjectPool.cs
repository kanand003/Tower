using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] int poolsize = 5;

    GameObject[] pool;

    private void Start()
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
        for(int i=0; i<pool.Length;i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(false);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectinPool();
            Instantiate(EnemyPrefab, transform);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
