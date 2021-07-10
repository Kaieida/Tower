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
    [SerializeField] PlayerController playerController;
    [SerializeField] FloorManager floorManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Countdown countdown;
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
        floorManager.SetReachedFloor();
        buttonManager.UpdateButtons();
        Destroy(obj);
        countdown.ResetCountdown();
        SpawnNewEnemy();
    }

    public void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, floorManager.FindNextFloor(floorManager.currentFloor).position, Quaternion.identity, mainCanvas.transform);
    }

    public void SpawnNewEnemyTesting(FloorInfo spawnPlace)
    {
        Instantiate(enemyPrefab, spawnPlace.placeForEnemy.position, Quaternion.identity, mainCanvas.transform);
    }

    public void RestartLevel(GameObject obj)
    {
        countdown.ResetCountdown();
        Destroy(obj);
        SpawnNewEnemy();
    }
}
