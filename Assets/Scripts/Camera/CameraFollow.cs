using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetObject;
    [SerializeField] private Vector3 cameraOffsetValue;
    void Update()
    {
        transform.position = cameraTargetObject.position + cameraOffsetValue;
    }
}
