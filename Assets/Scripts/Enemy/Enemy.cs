 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PhysicsCheck physicsCheck;
    Rigidbody2D rb;
    protected Animator anim;

    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public int faceDir = 1;

    public float thinkTime = 2f;
    private float thinkTimer = 0f;

    private void Awake() {
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate() {
        if (thinkTimer > 0) {
            Think();
        } else {
            Move();
        }
    }

    public virtual void Move() {
        if (physicsCheck.isFacingWall || physicsCheck.isAtCliff) {
            physicsCheck.FaceDirChanged();
            thinkTimer = thinkTime;
            return;
        }
        
        rb.velocity = new Vector2(faceDir * currentSpeed * Time.deltaTime, rb.velocity.y);
    }

    public virtual void Think() {
        thinkTimer -= Time.deltaTime;
        if (thinkTimer <= 0) {
            ChangeFaceDir();
        }
    }

    private void ChangeFaceDir() {
        faceDir = -faceDir;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
}
