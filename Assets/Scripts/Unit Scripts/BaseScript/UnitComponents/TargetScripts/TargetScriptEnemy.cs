using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetScriptEnemy : UnitTargetScript {
	public int scoutPriority = 0;
	public int fighterThisTargetPriority = 0;
	public int fighterPriority = 0;
	public int workerPriority = 0;
	public int resourcePriority = 0;
	public int waypointPriority = 0;
	public int basePriority = 0;
	public int unitsInGroupRangePriority = 0;

	public override void UpdateTarget() {
		List<GameObject> eligibleTargets = new List<GameObject>();
		for(int i = 0; i < unitScript.unitVision.objectsInRange.Count; i++) {
			unitScript.unitVision.cleanUp();
			if(unitScript.unitVision.objectsInRange[i].GetComponent<FlagScript>().Faction != unitScript.flagScript.Faction) {
				eligibleTargets.Add(unitScript.unitVision.objectsInRange[i]);
				
			}
		}

		if(scoutPriority > 0 ){
		// CHECK SCOUTS
			List<GameObject> scoutsInRange = new List<GameObject>();
			for(int i = 0; i < eligibleTargets.Count; i++) {
				if(eligibleTargets[i].tag.Contains(vars.scout_ant_tag)) {
					scoutsInRange.Add (eligibleTargets[i]);
				}
			}

			if(scoutsInRange.Count > 0) {
				GameObject closest = scoutsInRange[0];
				foreach(GameObject enemy in scoutsInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				changeTarget(closest, scoutPriority);
			}
		}
		List<GameObject> fightersInRange = new List<GameObject>();
		for(int i = 0; i < eligibleTargets.Count; i++) {
			if(eligibleTargets[i].tag.Contains(vars.unit_tag)) {
				fightersInRange.Add (eligibleTargets[i]);
			}
		}
		if(fighterThisTargetPriority > 0) {
			// CHECK UNITS TARGETING THIS ONE

			if(fightersInRange.Count > 0) {
				foreach(GameObject enemy in fightersInRange) {
					UnitScript enemyUnitScript = enemy.GetComponent<UnitScript>();
					if(enemyUnitScript.currentTarget == this) {
						changeTarget(enemy, fighterThisTargetPriority);
					}
				}
			}
		}

		if(workerPriority > 0) {
			// CHECK WORKERS
			List<GameObject> workersInRange = new List<GameObject>();
			for(int i = 0; i < eligibleTargets.Count; i++) {
				if(eligibleTargets[i].tag.Contains(vars.collector_ant_tag)) {
					workersInRange.Add (eligibleTargets[i]);
				}
			}
			if(workersInRange.Count > 0) {
				GameObject closest = workersInRange[0];
				foreach(GameObject enemy in workersInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				changeTarget(closest, workerPriority);
			}
		}

		if(resourcePriority > 0) {
			// CHECK RESSOURCES
			List<GameObject> resourcesInRange = new List<GameObject>();
			for(int i = 0; i < eligibleTargets.Count; i++) {
				if(eligibleTargets[i].tag.Contains(vars.res_tag)) {
					resourcesInRange.Add (eligibleTargets[i]);
				}
			}
			if(resourcesInRange.Count > 0) {
				GameObject closest = resourcesInRange[0];
				foreach(GameObject enemy in resourcesInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				changeTarget(closest, resourcePriority);
			}
		}

		if(waypointPriority > 0) {
			// CHECK WAYPOINTS
			// ....

		}
		
		// CHECK FIGHTERS
		if(fighterPriority > 0) {
			if(fightersInRange.Count > 0) {
				GameObject closest = fightersInRange[0];
				foreach(GameObject enemy in fightersInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				changeTarget(closest, fighterPriority);
			}
		}
		if(basePriority > 0) {
			// CHECK BASES
			List<GameObject> basesInRange = new List<GameObject>();
			for(int i = 0; i < eligibleTargets.Count; i++) {
				if(eligibleTargets[i].tag.Contains(vars.unit_tag)) {
					basesInRange.Add (eligibleTargets[i]);
				}
			}
			if(basesInRange.Count > 0) {
				GameObject closest = basesInRange[0];
				foreach(GameObject enemy in basesInRange) {
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				changeTarget(closest, basePriority);
			}
		}

		if(unitsInGroupRangePriority > 0) {
			if(unitScript.unitGroupScript.unitsInGroupRange.Count > 0) {
				GameObject closestInGroup = unitScript.unitGroupScript.unitsInGroupRange[0];
				for(int i = 0; i < unitScript.unitGroupScript.unitsInGroupRange.Count; i++) {
					if(unitScript.unitGroupScript.unitsInGroupRange[i] != null) {
						if(Vector3.Distance(this.transform.position, unitScript.unitGroupScript.unitsInGroupRange[i].transform.position) <= 
						   Vector3.Distance(this.transform.position, closestInGroup.transform.position)) {
							closestInGroup = unitScript.unitGroupScript.unitsInGroupRange[i];
						}
					}
				}
				changeTarget(closestInGroup, unitsInGroupRangePriority);
				return;
			}
		}
	}
}
