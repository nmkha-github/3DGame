using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool isGrounded;
    public float speed = 5.0f;
    public float runSpeed = 7.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        //running
        if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && UnityEngine.Input.GetKey(KeyCode.W))
        {
            controller.Move(transform.TransformDirection(moveDirection) * runSpeed * Time.deltaTime);

        }
        //walking
        else
        {
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * gravity * -3.0f);
        }
    }
}
