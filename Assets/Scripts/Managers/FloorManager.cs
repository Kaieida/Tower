using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject floorHolder;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Transform floorStart;
    [SerializeField] List<GameObject> allFloors;
    [SerializeField] GameObject player;
    [SerializeField] PlayerController playerController;

    public List<FloorSpawn> floorList;
    public int positionInFloorList;
    public int currentFloor;
    public int maxCompletedFloor;
    private int floorQueue;
    private int floorQueue2 = 2;
    private Vector3 floorForEnemy;

    void Start()
    {
        CreateFirstFloors();
    }

    public void CreateFirstFloors()
    {
        GameObject floorObject = Instantiate(allFloors[3], floorStart.position, Quaternion.identity, floorHolder.transform);
        FloorSpawn floorObjectInfo = floorObject.GetComponent<FloorSpawn>();
        floorObjectInfo.SetFloorLevel(currentFloor);
        floorList.Add(floorObjectInfo);
        ChangeFloor(currentFloor);
    }

    public Vector3 FindNextFloor(int floor)
    {
        foreach (FloorSpawn obj in floorList)
        {
            foreach (FloorInfo infObj in obj.floorInfo)
            {
                if (infObj.thisFloor == floor)
                {
                    floorForEnemy = infObj.placeForEnemy.position;
                    return floorForEnemy;
                }
            }
        }
        return Vector3.zero;
    }

    private bool CheckIfFloorExist(int floor)
    {
        foreach (FloorSpawn obj in floorList)
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
        if (floor != playerController.currentLevel && CheckIfFloorExist(floor))
        {
            playerController.currentLevel = floor;
            foreach (FloorSpawn obj in floorList)
            {
                foreach (FloorInfo infObj in obj.floorInfo)
                {
                    if (infObj.thisFloor == floor)
                    {
                        player.transform.position = infObj.placeForPlayer.transform.position;
                        currentFloor = floor;
                        break;
                    }
                }
            }
            enemyManager.RestartLevel(GameObject.FindWithTag("Enemy"));
        }
        else if (floor != playerController.currentLevel && !CheckIfFloorExist(floor))
        {
            playerController.currentLevel = floor;
            currentFloor = floor;
            NewFloorCreation();
            foreach (FloorSpawn obj in floorList)
            {
                foreach (FloorInfo infObj in obj.floorInfo)
                {
                    if (infObj.thisFloor == floor)
                    {
                        player.transform.position = infObj.placeForPlayer.transform.position;
                        currentFloor = floor;
                        break;
                    }
                }
            }

            enemyManager.RestartLevel(GameObject.FindWithTag("Enemy"));
        }
        CreateAdditionalFloor(floor);
        DestroyPreviousLevels();
    }

    public void SetReachedFloor()
    {
        if (playerController.currentLevel > maxCompletedFloor)
        {
            maxCompletedFloor = playerController.currentLevel;
        }
    }

    private void NewFloorCreation()
    {
        if (floorQueue >= 3)
        {
            floorQueue = 0;
        }
        else
        {
            floorQueue++;
        }
        GameObject floorObject = Instantiate(allFloors[floorQueue], FloorAdjustment(), Quaternion.identity, floorHolder.transform);
        FloorSpawn floorObjectInfo = floorObject.GetComponent<FloorSpawn>();
        floorObjectInfo.SetFloorLevel(currentFloor);
        floorList.Add(floorObjectInfo);
        ChangeFloor(currentFloor);
    }

    private Vector3 FloorAdjustment()
    {
        Vector3 pos = floorList[floorList.Count - 1].transform.position;
        pos += floorList[floorList.Count - 1].floorTop.position - floorList[floorList.Count - 1].floorBottom.position;
        return pos;
    }

    private void DestroyPreviousLevels()
    {
        while (floorList.Count > 3)
        {
            Destroy(floorList[0].gameObject);
            floorList.Remove(floorList[0]);
        }
    }

    private void CreateAdditionalFloor(int floor)
    {
        if (floorQueue2 >= 3)
        {
            floorQueue2 = 0;
        }
        else
        {
            floorQueue2++;
        }

        if (floor % 5 == 0 && !CheckIfFloorExist(floor + 1))
        {
            GameObject floorObject = Instantiate(allFloors[floorQueue2], FloorAdjustment(), Quaternion.identity, floorHolder.transform);
            FloorSpawn floorObjectInfo = floorObject.GetComponent<FloorSpawn>();
            floorObjectInfo.SetFloorLevel(currentFloor + 1);
            floorList.Add(floorObjectInfo);
        }
    }
}
