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
    [SerializeField] FloorManager floorManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Countdown countdown;
    public int maxCompletedLevel;
    private int currentHealth;

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

    public void EnemyDeath(GameObject obj)
    {
        MaxCompletedLevel(obj.GetComponent<EnemyController>().enemyFloor+ 2);
        buttonManager.UpdateButtons();
        Destroy(obj);
        countdown.ResetCountdown();
        SpawnNewEnemy();
    }

    private void SpawnNewEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, floorManager.floorList[floorManager.currentFloor].GetComponent<FloorSpawn>().placeForEnemy.position, Quaternion.identity,mainCanvas.transform);
        enemy.GetComponent<EnemyController>().enemyFloor = floorManager.currentFloor;
    }

    public void MaxCompletedLevel(int floorLevel)
    {
        if(floorLevel > maxCompletedLevel)
        {
            maxCompletedLevel = floorLevel;
        }
    }

    public void RestartLevel(GameObject obj)
    {
        countdown.ResetCountdown();
        Destroy(obj);
        SpawnNewEnemy();
    }
}
