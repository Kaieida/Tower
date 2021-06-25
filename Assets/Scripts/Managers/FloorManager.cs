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
    int loopAmounts = 0;

    void Start()
    {
        CreateFirstFloor();
    }

    void FloorSpawn(int floorsAmount, int floorType)
    {
        GameObject nextFloor = null;
        for (int i = 1; i <= floorsAmount; i++)
        {
            FloorAdjustment(nextFloor, floorType);
        }
        loopAmounts++;
        if (loopAmounts < 3)
        {
            CreateFirstFloor();
        }
    }

    public void ChangeFloor(int floor)
    {
        if (floor != playerController.currentLevel)
        {
            playerController.currentLevel = floor;
            for (int i = 0; i < floorList.Count; i++)
            {
                if (floorList[i].thisFloor == floor)
                {
                    player.transform.position = floorList[i].placeForPlayer.transform.position;
                    
                    currentFloor = floor;

                    positionInFloorList = i;

                    break;
                }
            }
            InfiniteFloorGenerator();
            enemyManager.RestartLevel(GameObject.FindWithTag("Enemy"));
        }
    }
    
    public void CreateFirstFloor()
    {
        int randomFloor = Random.Range(0, allFloors.Count);
        GameObject firstFloor = null;
        if (floorList.Count == 0)
        {
            firstFloor = Instantiate(allFloors[randomFloor], floorStart.position, Quaternion.identity, floorHolder.transform);
            floorList.Add(firstFloor.GetComponent<FloorSpawn>());
            firstFloor.GetComponent<FloorSpawn>().thisFloor = 1;
            FloorSpawn(firstFloor.GetComponent<FloorSpawn>().floorAmount, randomFloor);
        }
        else
        {
            FloorSpawn(FloorAdjustment(firstFloor, randomFloor).GetComponent<FloorSpawn>().floorAmount, randomFloor);
        }
    }

    GameObject FloorAdjustment(GameObject floorType, int randomFloor)
    {
        FloorSpawn previousFloor = floorList[floorList.Count - 1];
        floorType = Instantiate(allFloors[randomFloor], previousFloor.transform.position, Quaternion.identity, floorHolder.transform);

        floorList.Add(floorType.GetComponent<FloorSpawn>());
        floorType.GetComponent<FloorSpawn>().thisFloor = previousFloor.thisFloor + 1;

        floorType.transform.position += previousFloor.floorTop.position - previousFloor.floorBottom.position;
        return floorType;
    }

    private void InfiniteFloorGenerator()
    {
        if (floorList[10].thisFloor == playerController.currentLevel)
        {
            Debug.Log("executed");
            MoveFloorsUP();
        }
    }

    private void MoveFloorsUP()
    {
        FloorSpawn previousFloor = floorList[floorList.Count - 1];
        floorList[0].thisFloor = previousFloor.thisFloor + 1;
        floorList[0].transform.position = previousFloor.transform.position + (previousFloor.floorTop.position - previousFloor.floorBottom.position);
        floorList.Insert(floorList.Count - 1, floorList[0]);
        floorList.Remove(floorList[0]);
        for (int i = 1; i < 5; i++)
        {
            previousFloor = floorList[i - 1];
            floorList[i].thisFloor = previousFloor.thisFloor + 1;
            floorList[i].transform.position = previousFloor.transform.position + (previousFloor.floorTop.position - previousFloor.floorBottom.position);
            floorList.Insert(floorList.Count - 1, floorList[i]);
            floorList.Remove(floorList[i]);
        }
    }

    public void SetReachedFloor()
    {
        if(playerController.currentLevel > maxCompletedFloor)
        {
            maxCompletedFloor = playerController.currentLevel;
        }
    }

    public Vector3 FindNextFloor(int levelToSpawn)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            if (floorList[i].thisFloor == levelToSpawn)
            {
                Debug.Log("Found");
                floorForEnemy = floorList[i].placeForEnemy.position;
            }
        }
        return floorForEnemy;
    }
}
