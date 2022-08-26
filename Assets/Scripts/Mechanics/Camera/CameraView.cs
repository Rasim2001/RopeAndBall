using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraView : MonoBehaviour
{
    private float duration = 1.5f;
    public void Move(Vector3 target)
    {
        target.z = -10f;
        target.y += 5f;
        transform.DOMove(target, duration).SetEase(Ease.InOutBack); 
    }
}
