using UnityEngine;
using System.Collections;

public class EnemyCommandHandler : UnitCommandHandler {
	public EnemyGroupAttributes enemyGroupAttributes;

	public RouteScript routeScript;
	public int currentWayPoint = 1;

	public void Start() {
		base.Start();

		enemyGroupAttributes = transform.parent.GetComponent<EnemyGroupAttributes>();
		routeScript = enemyGroupAttributes.routeScript;
		if(routeScript != null) {
			currentWayPoint = 0;
		}
	}

	public override void HandleCommands() {

		currentCooldown -= Time.deltaTime;

		if(unitScript.healthScript.hasHealth) {
			switch(unitScript.currentCommand) {
				case UnitCommand.AttackMove: {
					AttackMove();
					break;
				}
				case UnitCommand.Attack: {
					Attack();
					break;
				}
				case UnitCommand.Idle: {
					Idle();
					break;
				}
			}
		} else {
			if(!died) {
				unitScript.animator.SetTrigger("death");
				died = true;
			}
		}
	}

	public override void AttackMove() {
		unitScript.animator.SetBool("isrunning", true);
		unitScript.animator.speed = 1;
		// See if a target is in range and attack
		if(unitScript.unitTargetScript.attackTarget != null) {
			attackTarget = unitScript.unitTargetScript.attackTarget;
			previousCommand = unitScript.currentCommand;
			unitScript.currentCommand = UnitCommand.Attack;
			return;
		}


		if(routeScript != null) {

			if(unitScript.movementScript.currentDestination != routeScript.wayPointTransforms[currentWayPoint].position) {
				unitScript.movementScript.UpdateDestination(routeScript.wayPointTransforms[currentWayPoint].position);
			}
			if(Vector3.Distance(this.transform.position, routeScript.wayPointTransforms[currentWayPoint].position) < 1.5f) {
				if(currentWayPoint < routeScript.wayPointObjects.Count -1) {
					currentWayPoint++;
					
				} else {
					if(enemyGroupAttributes.isPatrol && currentWayPoint == routeScript.wayPointObjects.Count -1) {
						currentWayPoint = 0;
					}
					else if(enemyGroupAttributes.isPatrol == false) {
						unitScript.currentCommand = UnitCommand.Idle;
					}
					
					unitScript.animator.SetBool("isrunning", false);
				}
				
			}
		} else {
			unitScript.currentCommand = UnitCommand.Idle;
		}

	}

	public override void Idle() {
		unitScript.movementScript.followTarget = null;
		
		// See if a target is in range and attack
		if(unitScript.unitTargetScript.attackTarget != null) {
			attackTarget = unitScript.unitTargetScript.attackTarget;
			previousCommand = unitScript.currentCommand;
			unitScript.currentCommand = UnitCommand.Attack;
			return;
		}
		unitScript.unitTargetScript.resetTarget();
		if(unitScript.movementScript.reachedDestination) {
			if(unitScript.currentIdleTime <= 0.0f) 
			{
				unitScript.animator.SetBool("isrunning", false);
				unitScript.currentIdleRareCooldown = unitScript.idleRareCooldown;

				if(routeScript != null){
					unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(routeScript.wayPointTransforms[routeScript.wayPointTransforms.Count-1].position, unitScript.spreadDistance));
				} else {
					unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				}
				unitScript.currentIdleTime = Random.Range (unitScript.lowerIdleTime, unitScript.upperIdleTime);
			} 
			else 
			{
				unitScript.currentIdleRareCooldown -= Time.deltaTime;
				if(unitScript.currentIdleRareCooldown <= 0) {
					if(Random.Range (0, 100) < unitScript.idleRareChance) {
						unitScript.animator.SetTrigger("idlerare");
						unitScript.currentIdleRareCooldown = unitScript.idleRareCooldown;
					}
				}
				unitScript.animator.SetBool("isrunning", false);
				unitScript.currentIdleTime -= Time.deltaTime;
			}
		} else {
			unitScript.animator.speed = 1;
			unitScript.animator.SetBool("isrunning", true);
		}
	}
}

