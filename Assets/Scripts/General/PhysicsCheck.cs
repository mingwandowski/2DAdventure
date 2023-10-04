using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("Check Parameters")]
    public float checkRadius;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    [Header("Check Status")]
    public bool isGround;
    
    void Update()
    {
        Check();
    }

    private void Check() {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
    
}
