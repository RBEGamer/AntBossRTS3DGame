using UnityEngine;
using System.Collections;

public class UnitCommandHandler : MonoBehaviour {
	// General scripts
	public UnitScript unitScript;

	// Command before attack
	public UnitCommand previousCommand;
	
	public float currentCooldown = 0.0f;

	public bool isDead = false;

	public void Start() {
		unitScript = GetComponent<UnitScript>();
	}


	public virtual void HandleCommands() {
		currentCooldown -= Time.deltaTime;
		if(unitScript.healthScript.hasHealth) {
			if(currentCooldown <= 0.0f) {
				//unitScript.navMeshAgent.Resume();
				unitScript.navMeshObstacle.enabled = false;
				unitScript.navMeshAgent.enabled = true;

				switch(unitScript.currentCommand) {

					case UnitCommand.AttackMove: {
						AttackMove();
						break;
					}
					case UnitCommand.Move: {
						Move();
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
					case UnitCommand.RetreatToBase: {
						RetreatToBase();
						break;
					}
				}
			}
		} else {
			if(!isDead) {
				unitScript.animator.SetTrigger("death");
				isDead = true;
			}
		}
	}

	public virtual void AttackMove() {
		setRunning(true);
		unitScript.unitTargetScript.UpdateTarget();

		if(unitScript.unitTargetScript.attackTarget) {
			previousCommand = unitScript.currentCommand;
			unitScript.currentCommand = UnitCommand.Attack;
			return;
		}

		if(unitScript.movementScript.followTarget == null) {
			if(unitScript.movementScript.hasReachedDestination(unitScript.spreadDistance / 2.0f)) {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		} else {
			if(unitScript.movementScript.isWithinGroupRange()) {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		}
	}

	public virtual void Move() {
		unitScript.unitTargetScript.resetTarget();
		setRunning(true);

		if(unitScript.movementScript.followTarget == null) {
			if(unitScript.movementScript.hasReachedDestination(unitScript.spreadDistance / 2.0f)) {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		} else {
			if(unitScript.movementScript.isWithinGroupRange()) {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		}
	}

	public virtual void Attack() {
		setRunning(true);
		if(unitScript.unitTargetScript.attackTarget) {
			unitScript.movementScript.UpdateDestination(unitScript.unitTargetScript.attackTarget.transform.position);
			
			if(Vector3.Distance(transform.position, unitScript.unitTargetScript.attackTarget.transform.position) <= 
			   ((unitScript.attributeScript.UnitRadius + unitScript.unitTargetScript.attackTarget.GetComponent<AttributeScript>().UnitRadius) + unitScript.attributeScript.CurrentAttackRange)) {
					transform.LookAt(unitScript.unitTargetScript.attackTarget.transform);
					setRunning(false);

					unitScript.animator.speed = 1/unitScript.attributeScript.CurrentAttackSpeed;
					//unitScript.navMeshAgent.Stop();	
					unitScript.navMeshAgent.enabled = false;
					unitScript.navMeshObstacle.enabled = true;
					unitScript.animator.SetTrigger("doattack");
					currentCooldown = unitScript.attributeScript.CurrentAttackSpeed;
			}
			
		}
		else {	
			if(previousCommand != null) {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.currentCommand = previousCommand;
			}
			else {
				unitScript.unitTargetScript.resetTarget();
				unitScript.currentIdleTime = 0.0f;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		}
	}
	

	public virtual void Idle() {
		unitScript.movementScript.reset();

		unitScript.currentIdleTime -= Time.deltaTime;
		if(unitScript.navMeshAgent.velocity.magnitude <= 1.0f && unitScript.currentIdleTime <= 0.0f) {
			unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
		}
		unitScript.unitTargetScript.UpdateTarget();
		// See if a target is in range and attack
		if(unitScript.unitTargetScript.attackTarget) {
			previousCommand = unitScript.currentCommand;
			unitScript.currentCommand = UnitCommand.Attack;
			return;
		}
		unitScript.unitTargetScript.resetTarget();
		if(unitScript.movementScript.hasReachedDestination(0.1f)) {
			if(unitScript.currentIdleTime <= 0.0f) 
			{
				setRunning(false);
				unitScript.currentIdleRareCooldown = unitScript.idleRareCooldown;
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentIdleTime = Random.Range (unitScript.lowerIdleTime, unitScript.upperIdleTime);
			} 
			else 
			{
				unitScript.currentIdleRareCooldown -= Time.deltaTime;
				if(unitScript.currentIdleRareCooldown <= 0) {
					if(Random.Range (0, 100) <= unitScript.idleRareChance) {
						unitScript.animator.SetTrigger("idlerare");
						unitScript.currentIdleRareCooldown = unitScript.idleRareCooldown;
					}
				}
				setRunning(false);

			}
		} else {
			setRunning(true);
		}
	}

	public void setRunning(bool isrunning) {
		unitScript.animator.SetBool("isrunning", isrunning);
		if(isrunning) {
			unitScript.animator.speed = 1;
		}
	}
	public virtual void RetreatToBase() {
		if(unitScript.movementScript.followTarget == null) {
			unitScript.movementScript.UpdateDestination(unitScript.unitGroupScript.unitGroupAttackTarget.transform.position);

		}
		if(Vector3.Distance(this.transform.position, unitScript.unitGroupScript.unitGroupAttackTarget.transform.position) < 5.0f) {
			foreach (Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			unitScript.unitGroupScript.addUnitToBaseList(this.unitScript);
			Destroy(gameObject);
		}
	}
}
