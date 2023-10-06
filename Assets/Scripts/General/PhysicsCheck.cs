using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("Check Parameters")]
    public float checkRadius;
    public Vector2 bottomOffset;
    public Vector2 cliffOffset;
    public Vector2 wallOffset;
    public LayerMask groundLayer;

    [Header("Check Status")]
    public bool isGround;
    public bool isAtCliff;
    public bool isFacingWall;
    
    void Update()
    {
        Check();
    }

    private void Check() {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        isAtCliff = isGround && !Physics2D.OverlapCircle((Vector2)transform.position + cliffOffset, checkRadius, groundLayer);
        isFacingWall = Physics2D.OverlapCircle((Vector2)transform.position + wallOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + cliffOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + wallOffset, checkRadius);
    }
    
    public void FaceDirChanged() {
        bottomOffset = new Vector2(-bottomOffset.x, bottomOffset.y);
        cliffOffset = new Vector2(-cliffOffset.x, cliffOffset.y);
        wallOffset = new Vector2(-wallOffset.x, wallOffset.y);
    }
}
