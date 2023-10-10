using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{

    private float lostTargetTimer;

    public override void OnEnter(Enemy enemy) {
        base.enemy = enemy;
        enemy.currentSpeed = enemy.chaseSpeed;
        enemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate() {
        Move();
    }

    public override void PhysicsUpdate() {
        if (enemy.FoundPlayer()) {
            lostTargetTimer = enemy.lostTargetTime;
        } else if(lostTargetTimer > 0) {
            lostTargetTimer -= Time.deltaTime;
        }

        if (lostTargetTimer <= 0) {
            enemy.SwitchState(NPCState.Patrol);
        }
    }
    

    public override void OnExit() {
        enemy.anim.SetBool("run", false);
    }

    private void Move() {
        if (enemy.isHurt || enemy.isDead) return;
        if (enemy.physicsCheck.isFacingWall || enemy.physicsCheck.isAtCliff) {
            enemy.ChangeFaceDir();
        }
        
        float velocityX = enemy.faceDir * enemy.currentSpeed * Time.deltaTime;
        enemy.rb.velocity = new Vector2(velocityX, enemy.rb.velocity.y);

        enemy.anim.SetBool("run", true);
    }
}
