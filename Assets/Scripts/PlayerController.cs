using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class PlayerController : MonoBehaviour
{
    public int currentLevel;

    public void FloorJump(Vector3 jumpLocation)
    {
       transform.DOMove(jumpLocation, 1f);
    }

    public IEnumerator FloorJumpCo(Vector3 jumpLocation)
    {
        transform.DOMove(jumpLocation, 1f);
        yield return new WaitForSeconds(1f);
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        yield return null;
    }
}
