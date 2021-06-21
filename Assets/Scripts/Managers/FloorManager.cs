using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] int floorAmount;
    [SerializeField] GameObject floorObject;
    [SerializeField] GameObject floorHolder;
    [SerializeField] EnemyManager enemyManager;
    public List<GameObject> floorList;
    [SerializeField] GameObject player;
    public int currentFloor;

    void Start()
    {
        FloorAdjustments();
    }

    void FloorAdjustments()
    {
        for (int i = 1; i <= floorAmount; i++)
        {
            FloorSpawn previousFloor = floorList[i - 1].GetComponent<FloorSpawn>();

            GameObject nextFloor = Instantiate(floorObject, previousFloor.transform.position, Quaternion.identity, floorHolder.transform);
            floorList.Add(nextFloor);

            nextFloor.transform.position += previousFloor.floorTop.position - previousFloor.floorBottom.position;
        }
    }

    public void Ascend()
    {
        if (floorAmount > currentFloor)
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
}
