using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D coll;

    public PlayerInputControl inputControl;
    public Vector2 inputDirection;

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    private int faceDir = 1;
    public float speed = 200f;
    public float jumpForce = 5f;
    public float hurtForce;

    public bool isWalk;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += PlayerAttack;
    }

    private void OnEnable() {
        inputControl.Enable();
    }

    private void OnDisable() {
        inputControl.Disable();
    }

    private void Update() {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        isWalk = inputControl.Gameplay.Walk.IsPressed();
        CheckState();
    }

    private void FixedUpdate() {
        Move(); 
    }

    private void Move() {
        if (isHurt || isAttack) return;
        float moveSpeed = inputDirection.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(isWalk ? moveSpeed / 2 : moveSpeed, rb.velocity.y);
        faceDir = inputDirection.x == 0 ? faceDir : inputDirection.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
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

    private void CheckState() {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}
