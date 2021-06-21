using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyManager enemyManager;
    [SerializeField] MonsterStats monsterStats;
    [SerializeField] PlayerStats playerStats;

    private void Start()
    {
        enemyManager = GameObject.Find("Managers").GetComponent<EnemyManager>();
        enemyManager.SetHealth(monsterStats.health);
    }

    private void OnMouseDown()
    {
        enemyManager.TakeDamage(playerStats.damage,gameObject);
    }
}
