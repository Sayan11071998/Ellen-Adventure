using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     [SerializeField] private Transform cameraTargetObject;
//     [SerializeField] private Vector3 cameraOffsetValue;
//     [SerializeField] private Camera mainCamera;

//     void Update()
//     {
//         if (mainCamera != null)
//         {
//             transform.position = cameraTargetObject.position + cameraOffsetValue;
//         }
//     }
// }

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetObject;
    [SerializeField] private Vector3 cameraOffsetValue;
    [SerializeField] private float topClamp = 2.5f;
    [SerializeField] private float botClamp = 3.5f;

    private float y;

    void Update()
    {
        y = Mathf.Clamp(transform.position.y, cameraTargetObject.position.y - topClamp, cameraTargetObject.position.y + botClamp);
        transform.position = new Vector3(cameraTargetObject.position.x, y, -10f) + cameraOffsetValue;
    }
}