using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject mainCanvas;
    private FloorManager floorManager;

    private int currentHealth;

    private void Start()
    {
        floorManager = GameObject.FindWithTag("MainFloor").GetComponent<FloorManager>();
    }

    public void TakeDamage(int damageTaken, GameObject gameObject)
    {
        currentHealth -= damageTaken;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0)
        {
            EnemyDeath(gameObject);
        }
    }

    public void SetHealth(int monsterHealth)
    {
        healthSlider.maxValue = monsterHealth;
        healthSlider.value = monsterHealth;
        currentHealth = monsterHealth;
    }

    /*public void OnMouseDown()
    {
        TakeDamage(playerStats.damage);
    }*/

    private void EnemyDeath(GameObject obj)
    {
        //floorManager.Ascend();
        Destroy(obj);
        SpawnNewEnemy();
    }

    private void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, floorManager.floorList[floorManager.currentFloor].GetComponent<FloorSpawn>().placeForEnemy.position, Quaternion.identity,mainCanvas.transform);
    }
}
