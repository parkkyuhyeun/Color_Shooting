using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float gravitationalAcceleration;
    private CharacterController controller;
    private Vector3 velocity;
    float mouseX = 0;
    float moveSpeed = 10f;
    float rotateSpeed = 10f;
    float _verticalVelocity;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        PlayerRotate();
        if(controller.isGrounded == false)
        {
            _verticalVelocity = -9.8f * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = -9.8f * 0.3f * Time.fixedDeltaTime;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(moveX, 0, moveZ);
        controller.Move((transform.TransformDirection(dir) * Time.deltaTime * moveSpeed)+_verticalVelocity*Vector3.up);
        //PlayerMoving();
    }

    private void PlayerRotate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    /*private void PlayerMoving()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(moveX, 0, moveZ);
        controller.Move(transform.TransformDirection(dir) * Time.deltaTime * moveSpeed);
    }*/
}
