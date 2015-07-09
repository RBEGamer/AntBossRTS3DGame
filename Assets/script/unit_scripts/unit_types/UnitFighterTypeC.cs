using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class UnitFighterTypeC : UnitBase {
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
	}
	
	void Update () {
		/*
		if(uiManager.is_in_menu) {
			unitNavMeshAgent.velocity = Vector3.zero;
			unitNavMeshAgent.ResetPath();
			unitAnimator.speed = 0;
			return;
		}*/

		if(!unitCombatTarget) {
			unitTargetPriority = 0;
		}

		cleanUp();
		if(unitCurrentCombatCooldown > 0) {
			unitCurrentCombatCooldown -= Time.deltaTime;
			return;
		}
		analyseUnitsInRange();
		if(unitCombatTarget != null) {
			// attack target directly
			unitMovementTarget = unitCombatTarget.transform.position;
			if(Vector3.Distance(this.transform.position, unitCombatTarget.transform.position) <= unitCurrentAttackRange) {
				unitMovementTarget = this.transform.position;
				if(unitCurrentCombatCooldown <= 0) {
					unitAnimator.speed = 1/unitCurrentAttackspeed;
					unitAnimator.SetTrigger("doattack");
					unitCurrentCombatCooldown = unitCurrentAttackspeed;
				}
			}
		}
		else if(unitMovementTarget != null && currentRoute != null){
			unitAnimator.speed = 1;
			unitAnimator.SetBool("isrunning", true);
			if(unitMovementTarget != currentRoute.wayPointObjects[currentWayPoint].transform.position) {
				unitMovementTarget = currentRoute.wayPointObjects[currentWayPoint].transform.position;
			}
			if(Vector3.Distance(this.transform.position, currentRoute.wayPointObjects[currentWayPoint].transform.position) < 1.5f) {
				if(currentWayPoint < currentRoute.wayPointObjects.Count -1) {
					currentWayPoint++;
					
				} else {
					if(unitGroup.isPatrol && currentWayPoint == currentRoute.wayPointObjects.Count -1) {
						currentWayPoint = 0;
					}
					unitAnimator.SetBool("isrunning", false);
				}
				
			}
		}
		// update destination if changed
		if (unitNavMeshAgent.destination != unitMovementTarget && !isUnitDisabled)
		{
			unitNavMeshAgent.SetDestination(unitMovementTarget);
		}
	}
	
	public void analyseUnitsInRange() {
		cleanUp();
		unitGroup.cleanUp();
		
		
		if(unitCurrentCombatCooldown <= 0) {
			if(analyseSpecialObjects()) {
				return;
			}
			
			// FILL LISTS --- 
			List<GameObject> enemyScoutsInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains (vars.scout_ant_tag)) {
					enemyScoutsInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyWorkerInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains (vars.collector_ant_tag)) {
					enemyWorkerInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyFighterInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.unit_tag)) {
					enemyFighterInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyBasesInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.base_tag)) {
					enemyBasesInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyRessourcesInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag.Contains (vars.friendly_tag) && enemy.tag.Contains(vars.res_tag)) {
					enemyRessourcesInRange.Add (enemy);
				}
			}
			
			List<GameObject> enemyUnitsInGroupRange = new List<GameObject>();
			foreach(GameObject enemy in unitGroup.enemiesInGroupRange) {
				if(enemy.tag.Contains (vars.friendly_tag)) {
					enemyUnitsInGroupRange.Add (enemy);
				}
			}
			
			// CHECK UNITS TARGETING THIS ONE
			if(enemyFighterInRange.Count > 0) {
				foreach(GameObject enemy in enemyFighterInRange) {
					UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
					if(enemyUnitScript.unitCombatTarget == this) {
						setTarget(enemy, 100);
						return;
					}
				}
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
				setTarget(closest, 90);
			}


			// CHECK FIGHTERS
			if(enemyFighterInRange.Count > 0) {
				GameObject closest = enemyFighterInRange[0];
				foreach(GameObject enemy in enemyFighterInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 80);
				return;
			}

			// CHECK WAYPOINTS
			// ....

			
			// CHECK SCOUTS
			if(enemyScoutsInRange.Count > 0) {
				GameObject closest = enemyScoutsInRange[0];
				foreach(GameObject enemy in enemyScoutsInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				setTarget(closest, 70);
				return;
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
				setTarget(closest, 60);
				return;
			}
			

			
			// CHECK UNITS IN GROUP RANGE
			if(enemyUnitsInGroupRange.Count > 0) {
				GameObject closestInGroup = enemyUnitsInGroupRange[0];
				foreach(GameObject enemy in enemyUnitsInGroupRange) {
					if(enemy != null) {
						if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
						   Vector3.Distance(this.transform.position, closestInGroup.transform.position)) {
							closestInGroup = enemy;
						}
					}
				}

				setTarget(closestInGroup, 50);
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
				setTarget(closest, 40);
				return;
			}
			
		}
	}
	
	
	public void attackTarget() {
		dealDamage();
	}
}
