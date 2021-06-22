using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    public enum RoomType { Black, Red, Yellow, Blue };
    public RoomType roomType;
    public Transform floorBottom;
    public Transform floorTop;
    public GameObject placeForPlayer;
    public Transform placeForEnemy;
    public int floorAmount;
}
