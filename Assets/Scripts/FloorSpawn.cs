using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    public Transform floorBottom;
    public Transform floorTop;
    public FloorInfo[] floorInfo;

    public void SetFloorLevel(int floor)
    {
        int testing = floor % 5;
        if (testing == 0)
        {
            floor -= + 4;
            foreach (FloorInfo obj in floorInfo)
            {
                obj.thisFloor += floor;
            }
        }
        else
        {
            floor -= + testing - 1;
            foreach (FloorInfo obj in floorInfo)
            {
                obj.thisFloor += floor;
            }
        }
        
    }

    /*public void SetCorrectFloor(int floor)
    {

    }*/
}
