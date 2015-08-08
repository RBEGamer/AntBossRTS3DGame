using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public NavMeshAgent navMeshAgent;
	public bool reachedDestination = false;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateDestination(Vector3 destination) {
		if(navMeshAgent.destination != destination) {
			navMeshAgent.destination = destination;
			reachedDestination = false;
		}
	}
	
	public bool hasReachedDestination() {
		if (!navMeshAgent.pathPending)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
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
}
