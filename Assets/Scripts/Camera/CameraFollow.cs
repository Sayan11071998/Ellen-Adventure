using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetObject;
    [SerializeField] private Vector3 cameraOffsetValue;
    [SerializeField] private Camera mainCamera;

    // private void LateUpdate() {
    //     if(mainCamera != null){
    //         Vector3 desiredPosition = transform.position + cameraOffsetValue;
    //         mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, Time.deltaTime * 5f);
    //         mainCamera.transform.LookAt(transform);
    //     }
    // }


    void Update()
    {
        if (mainCamera != null)
        {
            transform.position = cameraTargetObject.position + cameraOffsetValue;
        }

    }
}
