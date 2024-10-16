using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Debug.Log("Level Completed!!");
        }
    }
}
