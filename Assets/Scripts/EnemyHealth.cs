using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 5;
    [Tooltip("Adds amount to maxhitpoints when enemy dies. ")]
    [SerializeField] int difficulty = 1;

    int currentHitPoints=0;
    Enemy enemy;

    void OnEnable()
    {
       currentHitPoints=MaxHitPoints;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit(); 
    }
    void ProcessHit()
    {
        currentHitPoints--;
        if(currentHitPoints<=0)
        {
            gameObject.SetActive(false);
            MaxHitPoints += difficulty;
            enemy.RewardGold();
        }
    }
}
