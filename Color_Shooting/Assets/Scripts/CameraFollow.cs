using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 playerPos;

    private void Update()
    {
        transform.position = playerPos;
    }
}
