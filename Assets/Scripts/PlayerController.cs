using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public int currentLevel;

    public void FloorJump(Vector3 jumpLocation)
    {
       transform.DOMove(jumpLocation, 1f);
    }
}
