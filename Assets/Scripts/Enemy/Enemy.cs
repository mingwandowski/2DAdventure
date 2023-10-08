using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public int faceDir = 1;
    public float hurtForce;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public float thinkTime = 2f;
    public float thinkTimer = 0f;
    public float lostTargetTime = 5f;
    public bool isHurt;
    public bool isDead;

    protected virtual void Awake() {
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable() {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void FixedUpdate() {
        currentState.LogicUpdate();
    }

    private void Update() {
        Debug.Log(FoundPlayer());
        currentState.PhysicsUpdate();
    }

    private void OnDisable() {
        currentState.OnExit();
    }

    public void ChangeFaceDir() {
        physicsCheck.FaceDirChanged();
        faceDir = -faceDir;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

    public bool FoundPlayer() {
        return Physics2D.BoxCast((Vector2)transform.position + physicsCheck.wallOffset, checkSize, 0, new Vector2(faceDir, 0), checkDistance, attackLayer);
    }

    public void SwitchState(NPCState state) {
        BaseState newState = state switch {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
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

    private void OnDrawGizmosSelected() {
        // Physics2D.BoxCast
        Gizmos.DrawWireSphere((Vector2)transform.position + GetComponent<PhysicsCheck>().wallOffset + new Vector2(faceDir, 0) * checkDistance, 0.2f);
    }
}
