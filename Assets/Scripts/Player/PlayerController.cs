using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    public bool walk;
    public float hurtForce;
    public bool isHurt;
    public bool isDead;

    [Header("Movement")]
    public float speed = 200f;
    public float jumpForce = 5f;

    private int faceDir = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
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
        walk = inputControl.Gameplay.Walk.IsPressed();
    }

    private void FixedUpdate() {
        Move(); 
    }

    private void Move() {
        if (isHurt) return;
        float moveSpeed = inputDirection.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(walk ? moveSpeed / 2 : moveSpeed, rb.velocity.y);
        faceDir = inputDirection.x == 0 ? faceDir : inputDirection.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void GetHurt(Transform attacker) {
        isHurt = true;
        rb.velocity = Vector2.zero;
        rb.AddForce((transform.position - attacker.position).normalized * hurtForce, ForceMode2D.Impulse);
        Debug.Log(attacker.position);
    }

    public void PlayerDead() {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}
