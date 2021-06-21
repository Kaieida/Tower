using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    enum RoomType { Black, Red, Yellow, Blue };
    [SerializeField] RoomType roomType;
    public Transform floorBottom;
    public Transform floorTop;
    public GameObject placeForPlayer;
    public Transform placeForEnemy;
}
