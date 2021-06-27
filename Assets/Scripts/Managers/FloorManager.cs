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
    private Vector3 floorForEnemy;

    void Start()
    {
        CreateFirstFloors();
    }

    public void CreateFirstFloors()
    {
        GameObject floorObject = Instantiate(allFloors[0], floorStart.position, Quaternion.identity, floorHolder.transform);
        FloorSpawn floorObjectInfo = floorObject.GetComponent<FloorSpawn>();
        floorObjectInfo.SetFloorLevel(currentFloor);
        floorList.Add(floorObjectInfo);
        ChangeFloor(currentFloor);
    }

    public Vector3 FindNextFloor(int floor)
    {
        foreach(FloorSpawn obj in floorList)
        {
            foreach(FloorInfo infObj in obj.floorInfo)
            {
                if(infObj.thisFloor == floor)
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
        GameObject floorObject = Instantiate(allFloors[0], FloorAdjustment(), Quaternion.identity, floorHolder.transform);
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
        if(floorList.Count >= 4)
        {
            Destroy(floorList[0]);
            floorList.Remove(floorList[0]);
        }
    }

    private void CreateAdditionalFloor(int floor)
    {
        if (floor%5==0 && !CheckIfFloorExist(floor+1))
        {
            GameObject floorObject = Instantiate(allFloors[0], FloorAdjustment(), Quaternion.identity, floorHolder.transform);
            FloorSpawn floorObjectInfo = floorObject.GetComponent<FloorSpawn>();
            floorObjectInfo.SetFloorLevel(currentFloor+1);
            floorList.Add(floorObjectInfo);
        }
    }

    /*
    void FloorAdjustment()
    {
        /*FloorSpawn previousFloor = floorList[floorList.Count - 1];
        floorType = Instantiate(allFloors[randomFloor], previousFloor.transform.position, Quaternion.identity, floorHolder.transform);

        floorList.Add(floorType.GetComponent<FloorSpawn>());
        floorType.GetComponent<FloorSpawn>().thisFloor = previousFloor.thisFloor + 1;

        floorType.transform.position += previousFloor.floorTop.position - previousFloor.floorBottom.position;
        return floorType;


    }

    private void InfiniteFloorGenerator()
    {
        if(playerController.currentLevel % 5 == 0)
        {
            CreateFirstFloor();
        }
        else if(playerController.currentLevel % 5 == 1 && playerController.currentLevel != 1)
        {
            DeletePreviousFloors();
        }
    }

    public void DeletePreviousFloors()
    {
        for(int i = 0; i < 5; i++)
        {
            Destroy(floorList[i].gameObject);
        }
        floorList.RemoveRange(0, 5);
    }*/
}
