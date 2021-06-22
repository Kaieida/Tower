using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        transform.DOShakeScale(0.1f,new Vector3(2.7f, 2.7f, 2.7f),0);
        //transform.DORewind();
    }
}
