using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
	public UnitScript unitScript;

	public bool reachedDestination = false;

	public Vector3 currentDestination;

	public GameObject followTarget;
	// Use this for initialization
	void Awake () {
		unitScript = GetComponent<UnitScript>();

		if(unitScript.navMeshAgent.speed != unitScript.attributeScript.CurrentMovementSpeed) {
			unitScript.navMeshAgent.speed = unitScript.attributeScript.CurrentMovementSpeed;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		hasReachedDestination();
		if(followTarget != null) {
			follow();
		}
	}

	public void UpdateDestination(Vector3 destination) {
		if(unitScript.navMeshAgent.speed != unitScript.attributeScript.CurrentMovementSpeed) {
			unitScript.navMeshAgent.speed = unitScript.attributeScript.CurrentMovementSpeed;
		}
		if(unitScript.navMeshAgent.destination != destination) {
			unitScript.navMeshAgent.destination = destination;
			reachedDestination = false;
			currentDestination = destination;
		}
	}

	public void follow() {
		UpdateDestination(followTarget.transform.position);
	}
	
	public bool hasReachedDestination() {
		if (!unitScript.navMeshAgent.pathPending)
		{
			if (unitScript.navMeshAgent.remainingDistance <= unitScript.navMeshAgent.stoppingDistance)
			{
				if (!unitScript.navMeshAgent.hasPath || unitScript.navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					return reachedDestination = true;
				}
				else {
					return reachedDestination = false;
				}
			}
			else {
				return reachedDestination = false;
			}
		}
		return reachedDestination = false;
	}

	public bool isWithinGroupRange() {
		if(Vector3.Distance(this.transform.position, unitScript.unitGroupScript.transform.position) < unitScript.spreadDistance) {
			return true;
		} else {
			return false;
		}
	}
}
