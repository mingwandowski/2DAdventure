using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    public float speed = 200f;

    private int faceDir = 1;

    private void Awake() {
        inputControl = new PlayerInputControl(); 
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log(123);
    }

    private void Move() {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        faceDir = inputDirection.x == 0 ? faceDir : inputDirection.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
}
