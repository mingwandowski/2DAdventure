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
    public float hurtForce;
    public float thinkTime = 2f;

    private float thinkTimer = 0f;
    private bool isHurt;
    private bool isDead;

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
        if (isHurt || isDead) return;
        if (physicsCheck.isFacingWall || physicsCheck.isAtCliff) {
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
        physicsCheck.FaceDirChanged();
        faceDir = -faceDir;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

    public void OnTakeDamage(Transform attacker) {
        // turn around
        if (attacker.position.x < transform.position.x && faceDir == 1) {
            ChangeFaceDir();
        } else if (attacker.position.x > transform.position.x && faceDir == -1) {
            ChangeFaceDir();
        }
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = (transform.position - attacker.position).normalized * hurtForce;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir) {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDeath() {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        isDead = true;
        anim.SetBool("dead", true);
    }

    public void DestroyAfterAnimation() {
        Destroy(gameObject);
    }
}
