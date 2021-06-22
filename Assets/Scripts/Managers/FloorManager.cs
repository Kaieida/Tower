using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject floorObject;
    [SerializeField] GameObject floorHolder;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Transform floorStart;
    [SerializeField] List<GameObject> allFloors;
    public List<GameObject> floorList;
    [SerializeField] GameObject player;
    [SerializeField] int loopAmounts;
    public int currentFloor;
    
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
        if (loopAmounts < 2)
        {
            CreateFirstFloor();
        }
        
    }

    public void Ascend()
    {
        if (floorList.Count > currentFloor)
        {
            currentFloor++;
            player.transform.position = floorList[currentFloor].GetComponent<FloorSpawn>().placeForPlayer.transform.position;
            enemyManager.EnemyDeath(GameObject.FindWithTag("Enemy"));
        }
    }
    
    public void Descend()
    {
        if (currentFloor > 0)
        {
            currentFloor--;
            player.transform.position = floorList[currentFloor].GetComponent<FloorSpawn>().placeForPlayer.transform.position;
            enemyManager.EnemyDeath(GameObject.FindWithTag("Enemy"));
        }
    }
    
    public void CreateFirstFloor()
    {
        int randomFloor = Random.Range(0, allFloors.Count);
        GameObject firstFloor = null;
        if (floorList.Count == 0)
        {
            firstFloor = Instantiate(allFloors[randomFloor], floorStart.position, Quaternion.identity, floorHolder.transform);
            floorList.Add(firstFloor);
            FloorSpawn(firstFloor.GetComponent<FloorSpawn>().floorAmount, randomFloor);
        }
        else
        {
            FloorSpawn(FloorAdjustment(firstFloor, randomFloor).GetComponent<FloorSpawn>().floorAmount, randomFloor);
        }
    }

    GameObject FloorAdjustment(GameObject floorType, int randomFloor)
    {
        FloorSpawn previousFloor = floorList[floorList.Count - 1].GetComponent<FloorSpawn>();
        floorType = Instantiate(allFloors[randomFloor], previousFloor.transform.position, Quaternion.identity, floorHolder.transform);

        floorList.Add(floorType);

        floorType.transform.position += previousFloor.floorTop.position - previousFloor.floorBottom.position;
        return floorType;
    }
}
