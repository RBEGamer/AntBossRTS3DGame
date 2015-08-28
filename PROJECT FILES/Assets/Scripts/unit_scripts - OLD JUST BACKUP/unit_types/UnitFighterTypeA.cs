using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class UnitFighterTypeA : UnitBase {
	private bool patrolDirection = false;


	void Start() {
		unitFaction = UnitFaction.EnemyFaction;
		unitMovementTarget = transform.position;
		unitNavMeshAgent = GetComponent<NavMeshAgent>();
		
		unitCurrentAttackspeed = unitBaseAttackspeed;
		unitCurrentMovementspeed = unitBaseMovementspeed;
		unitNavMeshAgent.speed = unitCurrentMovementspeed;
		
		unitCurrentDamage = unitBaseDamage;
		unitCurrentHealth = unitBaseHealth;
		
		unitCurrentAttackRange = unitBaseAttackRange;
		unitCurrentVisionrange = unitBaseVisionRange;
		
		if(unitRange.myCollider != null) {
			unitRange.myCollider.radius = unitCurrentVisionrange;
		}

		if(unitGroup != null) {
			if (!unitGroup.myUnitList.Contains(GetComponent<UnitBase>()))
			{
				unitGroup.myUnitList.Add(GetComponent<UnitBase>());
			}
		}

		unitCommand = UnitCommand.AttackMove;
	}
	
	void Update () {

		cleanUp();

		// update destination if changed
		if (unitNavMeshAgent.destination != unitMovementTarget && !isUnitDisabled)
		{
			unitNavMeshAgent.SetDestination(unitMovementTarget);
		}

		if(unitCurrentCombatCooldown > 0) {
			unitCurrentCombatCooldown -= Time.deltaTime;
			return;
		}


		analyseUnitsInRange();

		if(unitCommand == UnitCommand.Attack && unitCombatTarget != null) {

			// attack target directly
			unitMovementTarget = unitCombatTarget.transform.position;

			if(Vector3.Distance(unitTransform.position, 
			                    unitCombatTarget.transform.position) <= ((unitRadius + enemyUnitRadius) + unitCurrentAttackRange)) {

				unitMovementTarget = unitTransform.position;

				if(unitCurrentCombatCooldown <= 0) {
					unitTransform.LookAt(unitCombatTarget.transform);
					unitAnimator.speed = 1/unitCurrentAttackspeed;
					unitAnimator.SetTrigger("doattack");
					unitCurrentCombatCooldown = unitCurrentAttackspeed;
				}
			}
		}

		else if(unitCommand == UnitCommand.Idle) {

			if (!unitNavMeshAgent.pathPending)
			{
				if (unitNavMeshAgent.remainingDistance <= unitNavMeshAgent.stoppingDistance)
				{
					if (!unitNavMeshAgent.hasPath || unitNavMeshAgent.velocity.sqrMagnitude == 0f)
					{
						// Done 
						
						if(currentIdleTime <= 0.0f) 
						{
							unitAnimator.SetBool("isrunning", false);
							currentIdleRareCooldown = idleRareCooldown;
							getNewIdlePosition(currentRoute.wayPointObjects[currentWayPoint].transform.position, spreadDistanceInGroup);
							currentIdleTime = Random.Range (lowerIdleTime, upperIdleTime);
						} 
						else 
						{
							currentIdleRareCooldown -= Time.deltaTime;
							if(currentIdleRareCooldown <= 0) {
								if(Random.Range (0, 100) <= idleRareChance) {
									//unitAnimator.SetTrigger("idlerare");
									currentIdleRareCooldown = idleRareCooldown;
								}
							}
							unitAnimator.SetBool("isrunning", false);
							currentIdleTime -= Time.deltaTime;
						}
					}
				}
			}
		}
		else if(unitCommand == UnitCommand.AttackMove){
			unitAnimator.speed = 1;
			unitAnimator.SetBool("isrunning", true);

			if(unitMovementTarget != null && currentRoute != null) {
				if(unitMovementTarget != currentRoute.wayPointTransforms[currentWayPoint].position) {
					unitMovementTarget = currentRoute.wayPointTransforms[currentWayPoint].position;
				}
			}
			if(currentRoute != null) {
				if(Vector3.Distance(this.transform.position, currentRoute.wayPointTransforms[currentWayPoint].position) < 1.5f) {
					if(currentWayPoint < currentRoute.wayPointObjects.Count -1) {
						currentWayPoint++;
						
					} else {
						if(unitGroup.isPatrol && currentWayPoint == currentRoute.wayPointObjects.Count -1) {
							currentWayPoint = 0;
						}
						else if(unitGroup.isPatrol == false) {
							unitCommand = UnitCommand.Idle;
						}

						unitAnimator.SetBool("isrunning", false);
					}
					
				}
			}
		}
	}
	public void analyseUnitsInRange() {
		cleanUp();
		if(unitGroup != null) {
			unitGroup.cleanUp();
		}
		
		
		if(unitCurrentCombatCooldown <= 0 && unitCombatTarget == null) {
			if(analyseSpecialObjects()) {
				return;
			}
			// FILL LISTS --- 
			List<GameObject> enemyScoutsInRange = new List<GameObject>();
			foreach(GameObject enemy in unitsInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains (vars.scout_ant_tag)) {
					enemyScoutsInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyWorkerInRange = new List<GameObject>();
			foreach(GameObject enemy in unitsInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains (vars.collector_ant_tag)) {
					enemyWorkerInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyFighterInRange = new List<GameObject>();
			foreach(GameObject enemy in unitsInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.unit_tag)) {
					enemyFighterInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyBasesInRange = new List<GameObject>();
			foreach(GameObject enemy in unitsInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.base_tag)) {
					enemyBasesInRange.Add (enemy);
				}
			}

			List<GameObject> enemyRessourcesInRange = new List<GameObject>();
			foreach(GameObject enemy in unitsInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.res_tag)) {
					enemyRessourcesInRange.Add (enemy);
				}
			}


			// CHECK SCOUTS
			if(enemyScoutsInRange.Count > 0) {
				GameObject closest = enemyScoutsInRange[0];
				foreach(GameObject enemy in enemyScoutsInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 100);
				return;
			}

			// CHECK UNITS TARGETING THIS ONE
			if(enemyFighterInRange.Count > 0) {
				foreach(GameObject enemy in enemyFighterInRange) {
					UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
					if(enemyUnitScript.unitCombatTarget == this) {
						setTarget(enemy, 90);
						return;
					}
				}
			}

			// CHECK WORKERS
			if(enemyWorkerInRange.Count > 0) {
				GameObject closest = enemyWorkerInRange[0];
				foreach(GameObject enemy in enemyWorkerInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 80);
				return;
			}

			// CHECK RESSOURCES
			if(enemyRessourcesInRange.Count > 0) {
				GameObject closest = enemyRessourcesInRange[0];
				foreach(GameObject enemy in enemyRessourcesInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 70);
				return;
			}

			// CHECK WAYPOINTS
			// ....

			// CHECK FIGHTERS
			if(enemyFighterInRange.Count > 0) {
				GameObject closest = enemyFighterInRange[0];
				foreach(GameObject enemy in enemyFighterInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 60);
				return;
			}

			// CHECK BASES
			if(enemyBasesInRange.Count > 0) {
				GameObject closest = enemyBasesInRange[0];
				foreach(GameObject enemy in enemyBasesInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 60);
				return;
			}
		}
	}


	
	public void attackTarget() {
		dealDamage();
	}
}
