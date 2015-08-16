using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitTargetScript : MonoBehaviour {
	public UnitScript unitScript;
	public int currentTargetPriority = 0;
	public GameObject attackTarget;

	public void Start() {
		unitScript = GetComponent<UnitScript>();
	}
	public void Update() {
		UpdateTarget();
	}

	public void changeTarget(GameObject target, int priority) {
		if(currentTargetPriority < priority) {
			attackTarget = target;
			currentTargetPriority = priority;
		}
	}

	public void resetTarget() {
		attackTarget = null;
		currentTargetPriority = 0;
	}

	public virtual void UpdateTarget() {

		if(attackTarget != null) {
			if(attackTarget.GetComponent<HealthScript>().hasHealth == false) {
				resetTarget();
			}
			if(!attackTarget.activeSelf) {
				resetTarget();
			}
		}

		unitScript.unitVision.cleanUp();

		List<GameObject> eligibleTargets = new List<GameObject>();
		for(int i = 0; i < unitScript.unitVision.objectsInRange.Count; i++) {

			if(unitScript.unitVision.objectsInRange[i].GetComponent<FlagScript>().Faction != unitScript.flagScript.Faction) {
				eligibleTargets.Add(unitScript.unitVision.objectsInRange[i]);

			}
		}

		if(eligibleTargets.Count > 0) {
			GameObject closest = eligibleTargets[0];
			for(int i = 0; i < eligibleTargets.Count; i++) {
				UnitScript enemyUnitScript = eligibleTargets[i].GetComponent<UnitScript>();
				if(enemyUnitScript) {
					if(enemyUnitScript.currentTarget == this.gameObject) {
						changeTarget(eligibleTargets[i], 100);
					}
				}

				if(Vector3.Distance(this.transform.position, eligibleTargets[i].transform.position) <= 
				   Vector3.Distance(this.transform.position, closest.transform.position)) {
					closest = eligibleTargets[i];
				}
			}

			changeTarget(closest, 90);
		}


		List<GameObject> eligibleTargetsInGroupRange = new List<GameObject>();
		unitScript.unitGroupScript.cleanUp();
		for(int i = 0; i < unitScript.unitGroupScript.unitsInGroupRange.Count; i++) {

			if(unitScript.unitGroupScript.unitsInGroupRange[i].GetComponent<FlagScript>().Faction != unitScript.flagScript.Faction) {
				eligibleTargetsInGroupRange.Add(unitScript.unitGroupScript.unitsInGroupRange[i]);
			}
		}

		if(eligibleTargetsInGroupRange.Count > 0){
			GameObject closestInGroup = eligibleTargetsInGroupRange[0];
			foreach(GameObject enemy in eligibleTargetsInGroupRange) {
				if(enemy != null) {
					UnitScript enemyUnitScript = enemy.GetComponent<UnitScript>();
					if(enemyUnitScript) {
						if(enemyUnitScript.currentTarget == this) {
							changeTarget(enemy, 80);
							return;
						}
					}
					
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closestInGroup.transform.position)) {
						closestInGroup = enemy;
					}
				}
			}
			changeTarget(closestInGroup, 70);
			return;
		}
	}
}
