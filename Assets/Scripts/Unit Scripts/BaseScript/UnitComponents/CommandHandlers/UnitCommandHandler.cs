using UnityEngine;
using System.Collections;

public class UnitCommandHandler : MonoBehaviour {
	// General scripts
	public UnitScript unitScript;

	// Attack target
	public GameObject attackTarget;

	// Command before attack
	public UnitCommand previousCommand;
	
	public float currentCooldown = 0.0f;

	public bool died = false;
	public void Start() {
		unitScript = GetComponent<UnitScript>();
	}

	public virtual void HandleCommands() {

		currentCooldown -= Time.deltaTime;
		Profiler.BeginSample ("OuterCommand");
		if(unitScript.healthScript.hasHealth) {
			Profiler.BeginSample ("InnerCommand");
			switch(unitScript.currentCommand) {

				case UnitCommand.AttackMove: {
				Profiler.BeginSample ("Command1");
					AttackMove();
				Profiler.EndSample ();
					break;
				}
				case UnitCommand.Move: {
				Profiler.BeginSample ("Command2");
					Move();
				Profiler.EndSample ();
					break;
				}
				case UnitCommand.Attack: {
				Profiler.BeginSample ("Command3");
					Attack();
				Profiler.EndSample ();
					break;
				}
				case UnitCommand.Idle: {
				Profiler.BeginSample ("Command4");
					Idle();
				Profiler.EndSample ();
					break;
				}
				case UnitCommand.RetreatToBase: {
				Profiler.BeginSample ("Command5");
					RetreatToBase();
				Profiler.EndSample ();
					break;
				}
			}
			Profiler.EndSample ();
		} else {
			Profiler.BeginSample ("InnerCommand2");
			if(!died) {
				unitScript.animator.SetTrigger("death");
				died = true;
			}
			Profiler.EndSample ();
		}
		Profiler.EndSample ();
	}

	public virtual void AttackMove() {
		unitScript.animator.SetBool("isrunning", true);
		// See if a target is in range and attack
		if(attackTarget = unitScript.unitTargetScript.attackTarget) {
			previousCommand = unitScript.currentCommand;
			unitScript.currentCommand = UnitCommand.Attack;
			return;
		}

		if(unitScript.movementScript.followTarget == null) {
			if(unitScript.movementScript.hasReachedDestination()) {
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		} else {
			if(unitScript.movementScript.currentDestination != unitScript.unitGroupScript.transform.position) {
				unitScript.movementScript.UpdateDestination(unitScript.unitGroupScript.transform.position);
			}

			if(unitScript.movementScript.isWithinGroupRange()) {
				unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
				unitScript.currentCommand = UnitCommand.Idle;
			}
		}
	}

	public virtual void Move() {
		unitScript.unitTargetScript.resetTarget();
		unitScript.animator.SetBool("isrunning", true);
		if(unitScript.movementScript.currentDestination != unitScript.unitGroupScript.transform.position) {
			unitScript.movementScript.UpdateDestination(unitScript.unitGroupScript.transform.position);
		}
		
		if(unitScript.movementScript.isWithinGroupRange()) {
			unitScript.movementScript.UpdateDestination(unitScript.newIdlePosition(unitScript.unitGroupScript.transform.position, unitScript.spreadDistance));
			unitScript.currentCommand = UnitCommand.Idle;
		}
	}

	public virtual void Attack() {
		unitScript.animator.SetBool("isrunning", true);

		if(attackTarget != null) {
			unitScript.weaponScript.attackTarget = attackTarget;
			unitScript.movementScript.UpdateDestination(attackTarget.transform.position);

			if(Vector3.Distance(transform.position, 
			                    attackTarget.transform.position) <= ((unitScript.attributeScript.UnitRadius) + unitScript.attributeScript.CurrentAttackRange)) {
				unitScript.movementScript.UpdateDestination(transform.position);
				if(currentCooldown <= 0) {
					transform.LookAt(attackTarget.transform);
					unitScript.animator.speed = 1/unitScript.attributeScript.CurrentAttackSpeed;
					unitScript.animator.SetBool("isrunning", false);
					unitScript.animator.SetTrigger("doattack");
					currentCooldown = unitScript.attributeScript.CurrentAttackSpeed;
				}
			}
			
		}
		else {	
			if(previousCommand != null) {
				unitScript.currentCommand = previousCommand;
			}
			else {
				unitScript.currentCommand = UnitCommand.Idle;
			}
		}
	}
	
	public virtual void Idle() {
		unitScript.movementScript.followTarget = null;

		// See if a target is in range and attack
		if(attackTarget = unitScript.unitTargetScript.attackTarget) {
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
				unitScript.animator.SetBool("isrunning", false);
				unitScript.currentIdleTime -= Time.deltaTime;
			}
		} else {
			unitScript.animator.speed = 1;
			unitScript.animator.SetBool("isrunning", true);
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
