using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] Transform floorStart;
    [SerializeField] Transform floorPanel;
    [SerializeField] Transform healthPanel;
    [SerializeField] Transform shopButton;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] List<FloorSpawn> floorPool;
    
    public int previousFloor;
    public int currentFloor;
    public int maxCompletedFloor;
    private Transform placeForEnemy;

    void Start()
    {
        SortFirstFloors();
    }

    private void SortFirstFloors()
    {
        for (int i = 0; i < floorPool.Count; i++)
        {
            if (i == 0)
            {
                floorPool[i].gameObject.transform.position = floorStart.position;
                floorPool[i].SetFloorLevel(currentFloor);
            }
            else
            {
                floorPool[i].gameObject.transform.position += floorPool[i - 1].transform.position + floorPool[i - 1].floorTop.position - floorPool[i - 1].floorBottom.position;
                floorPool[i].SetFloorLevel(currentFloor + 5 * i);
            }
        }
        ChangeFloor(currentFloor);
    }

    public Transform FindNextFloor(int floor)
    {
        foreach (FloorSpawn obj in floorPool)
        {
            foreach (FloorInfo infObj in obj.floorInfo)
            {
                if (infObj.thisFloor == floor)
                {
                    placeForEnemy = infObj.placeForEnemy;
                    return placeForEnemy;
                }
            }
        }
        return transform;
    }

    private bool CheckIfFloorExist(int floor)
    {
        foreach (FloorSpawn obj in floorPool)
        {
            foreach (FloorInfo infObj in obj.floorInfo)
            {
                if (infObj.thisFloor == floor)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ChangeFloor(int floor)
    {
        if (floor != playerController.currentLevel)
        {
            playerController.currentLevel = floor;
            currentFloor = floor;
            MovePool(floor);
            
            foreach (FloorSpawn obj in floorPool)
            {
                foreach (FloorInfo infObj in obj.floorInfo)
                {
                    if (infObj.thisFloor == floor)
                    {
                        //playerController.FloorJump(infObj.placeForPlayer.transform.position);
                        StartCoroutine(playerController.FloorJumpCo(infObj.placeForPlayer.transform.position));
                        SetInfoPanels(infObj);
                        enemyManager.SpawnNewEnemyTesting(infObj);
                        break;
                    }
                }
            }
            previousFloor = floor;
            enemyManager.RestartLevel(GameObject.FindWithTag("Enemy"));
        }
    }

    public void SetReachedFloor()
    {
        if (playerController.currentLevel > maxCompletedFloor)
        {
            maxCompletedFloor = playerController.currentLevel;
        }
    }

    private void MovePool(int floor)
    {
        if (floor % 5 == 0 && !CheckIfFloorExist(floor + 1))
        {
            MoveUp(floor);
        }
        else if (floor % 5 == 1 && !CheckIfFloorExist(floor - 1))
        {
            MoveDown(floor);
        }
        else if (!CheckIfFloorExist(floor))
        {
            if(previousFloor > currentFloor)
            {
                for (int i = 0; i < floorPool.Count; i++)
                {
                    floorPool[floorPool.Count - 1].transform.position = floorPool[0].transform.position;
                    floorPool[floorPool.Count - 1].transform.position -= floorPool[0].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                    floorPool[floorPool.Count - 1].SetFloorLevel(floor - i * 5);
                    floorPool.Insert(0, floorPool[floorPool.Count - 1]);
                    floorPool.RemoveAt(floorPool.Count - 1);
                }
            }
            else if (previousFloor < currentFloor)
            {
                for (int i = 0; i < floorPool.Count; i++)
                {
                    floorPool[0].transform.position = floorPool[floorPool.Count - 1].transform.position;
                    floorPool[0].transform.position += floorPool[floorPool.Count - 1].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                    floorPool[0].SetFloorLevel(floor + i * 5);
                    floorPool.Add(floorPool[0]);
                    floorPool.Remove(floorPool[0]);
                }
            }
        }
    }

    private void MoveUp(int floor)
    {
        if (!CheckIfFloorExist(floor))
        {
            for (int i = 0; i < floorPool.Count; i++)
            {
                floorPool[0].transform.position = floorPool[floorPool.Count - 1].transform.position;
                floorPool[0].transform.position += floorPool[floorPool.Count - 1].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                floorPool[0].SetFloorLevel(floor + i * 5);
                floorPool.Add(floorPool[0]);
                floorPool.Remove(floorPool[0]);
            }
        }
        else if (CheckIfFloorExist(floor))
        {
            for (int i = 0; i < floorPool.Count-1; i++)
            {
                floorPool[0].transform.position = floorPool[floorPool.Count - 1].transform.position;
                floorPool[0].transform.position += floorPool[floorPool.Count - 1].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                floorPool[0].SetFloorLevel(floorPool[floorPool.Count - 1].floorInfo[4].thisFloor + 1);
                floorPool.Add(floorPool[0]);
                floorPool.Remove(floorPool[0]);
            }
        }
    }

    private void MoveDown(int floor)
    {
        if (!CheckIfFloorExist(floor))
        {
            for (int i = 0; i < floorPool.Count; i++)
            {
                floorPool[floorPool.Count - 1].transform.position = floorPool[0].transform.position;
                floorPool[floorPool.Count - 1].transform.position -= floorPool[0].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                floorPool[floorPool.Count-1].SetFloorLevel(floor - i * 5);
                floorPool.Insert(0, floorPool[floorPool.Count - 1]);
                floorPool.RemoveAt(floorPool.Count - 1);
            }
        }
        else if (CheckIfFloorExist(floor))
        {
            for (int i = 0; i < floorPool.Count - 1; i++)
            {
                floorPool[floorPool.Count - 1].transform.position = floorPool[0].transform.position;
                floorPool[floorPool.Count - 1].transform.position -= floorPool[0].floorTop.position - floorPool[floorPool.Count - 1].floorBottom.position;
                floorPool[floorPool.Count - 1].SetFloorLevel(floorPool[0].floorInfo[0].thisFloor - 1);
                floorPool.Insert(0, floorPool[floorPool.Count - 1]);
                floorPool.RemoveAt(floorPool.Count - 1);
            }
        }
    }

    public void GoFloorLower()
    {
        ChangeFloor(currentFloor - 1);
    }

    public void SetInfoPanels(FloorInfo floor)
    {
        floorPanel.position = floor.placeForButtons.position;
        healthPanel.position = floor.placeForHealth.position;
        shopButton.position = floor.placeForShop.position;
    }
}
