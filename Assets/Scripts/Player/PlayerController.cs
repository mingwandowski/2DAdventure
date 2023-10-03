using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    [Header("Movement")]
    public float speed = 200f;
    public float jumpForce = 5f;

    private int faceDir = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
    }

    private void OnEnable() {
        inputControl.Enable();
    }

    private void OnDisable() {
        inputControl.Disable();
    }

    private void Update() {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        Move(); 
    }

    private void Move() {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        faceDir = inputDirection.x == 0 ? faceDir : inputDirection.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
