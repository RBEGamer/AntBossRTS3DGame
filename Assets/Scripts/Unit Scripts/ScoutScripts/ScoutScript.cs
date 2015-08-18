using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoutScript : MonoBehaviour {
	
	public static WorkerManager workerManager;
	public static wp_manager pathManager;
	public static base_manager baseManager;

	public NavMeshAgent navMeshAgent;
	public Animator animator;

	public path_point targetWP;
	public List<int> currentPath;
	public int currentPointInPath = 1;

	public bool isActive = true;

	public float currentMovementOffset = 2.0f;
	public float movementUpperOffset = 4.0f;
	public float movementLowerOffset = 1.0f;
	// Use this for initialization
	void Awake () {
		if(pathManager == null) {
			pathManager = GameObject.Find (vars.path_manager_name).GetComponent<wp_manager>();
		}

		if(baseManager == null) {
			baseManager = GameObject.Find (vars.base_name).GetComponent<base_manager>();
		}
		
		
		if(workerManager == null) {
			workerManager = GameObject.Find (vars.worker_manager_name).GetComponent<WorkerManager>();
		}
		navMeshAgent = GetComponent<NavMeshAgent>();
	
	}

	public void initializeScout(path_point WP) {
		targetWP = WP;
		currentPath = WP.path_to_base;
		currentPointInPath = 1;
		animator.SetBool("isrunning", true);
		baseManager.bought_scout_ants--;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(navMeshAgent.enabled == true) {
			if(navMeshAgent.destination != currentPointPosition()) {
				navMeshAgent.destination = currentPointPosition();
			}
		}


		if(Vector3.Distance(transform.position, currentPointPosition()) < currentMovementOffset && isActive) {

			if(pathManager.getNodeObjectById(currentPointID()).GetComponent<path_point>().status == path_point.node_status.placed) {
				pathManager.getNodeObjectById(currentPointID()).GetComponent<path_point>().setStatus(path_point.node_status.being_built);
				animator.SetBool("isrunning", false);
				animator.SetTrigger("buildWayPoint");
				Invoke("buildWayPoint", 3.0f);
				isActive = false;
			}

			else if(currentPointInPath < targetWP.path_to_base.Count-1) {
				currentPointInPath++;
				currentMovementOffset = Random.Range(movementLowerOffset, movementUpperOffset);
			}

		}
	}

	Vector3 currentPointPosition() {
		return pathManager.getNodeObjectById(currentPath[currentPointInPath]).transform.position;
	}

	int currentPointID() {
		return currentPath[currentPointInPath];
	}

	public void buildWayPoint() {
		pathManager.getNodeObjectById(currentPointID()).GetComponent<path_point>().setStatus(path_point.node_status.built);
		pathManager.refresh_edge_visuals();
		Destroy (gameObject);
	}
}
