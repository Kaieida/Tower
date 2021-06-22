using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    EnemyManager enemyManager;
    [SerializeField] MonsterStats monsterStats;
    [SerializeField] PlayerStats playerStats;
    public int enemyFloor;

    private void Start()
    {
        enemyManager = GameObject.Find("Managers").GetComponent<EnemyManager>();
        enemyManager.SetHealth(monsterStats.health);
    }

    private void OnMouseDown()
    {
        enemyManager.TakeDamage(playerStats.damage,gameObject);
        transform.DOShakeScale(0.5f,new Vector3(0.2f, 0.2f, 0.2f),0);
        //transform.DORewind();
    }
}
