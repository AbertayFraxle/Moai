using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform groundCheck;

    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance = 0.4f;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float gravity = -18f;
    [SerializeField] float jumpHeight = 4;
    [SerializeField] float coyoteTime = 0.2f;

    float coyoteTimer;
    Vector3 velocity;
    bool isGrounded;
    bool hasJumped;

    private void Update()
    {
        coyoteTimer += Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            coyoteTimer = 0;
            hasJumped = false;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown("space") && coyoteTimer < coyoteTime && !hasJumped)
        {
            hasJumped = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity + (velocity.y * 0.01f));
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}
