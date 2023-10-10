using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy) {
        base.enemy = enemy;
        enemy.currentSpeed = enemy.normalSpeed;
    }

    public override void LogicUpdate() {
        if (enemy.FoundPlayer()) {
            enemy.SwitchState(NPCState.Chase);
            return;
        }

        if (enemy.thinkTimer > 0) {
            Think();
            enemy.anim.SetBool("walk", false);
        } else {
            Move();
        }
    }

    public override void PhysicsUpdate() {}

    public override void OnExit() {
        enemy.anim.SetBool("walk", false);
    }

    private void Think() {
        enemy.thinkTimer -= Time.deltaTime;
        if (enemy.thinkTimer <= 0) {
            enemy.ChangeFaceDir();
        }
    }

    private void Move() {
        if (enemy.isHurt || enemy.isDead) return;
        if (enemy.physicsCheck.isFacingWall || enemy.physicsCheck.isAtCliff) {
            enemy.thinkTimer = enemy.thinkTime;
            return;
        }
        
        float velocityX = enemy.faceDir * enemy.currentSpeed * Time.deltaTime;
        enemy.rb.velocity = new Vector2(velocityX, enemy.rb.velocity.y);

        enemy.anim.SetBool("walk", true);
    }
}
