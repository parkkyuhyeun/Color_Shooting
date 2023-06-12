using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float mouseSpeed = 10f;
    float mouseY = 0;

    private void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -55f, 55f);
        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
