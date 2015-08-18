using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoutScript : MonoBehaviour {
	
	public wp_manager pathManager;

	public NavMeshAgent navMeshAgent;
	public Animator animator;

	public path_point targetWP;
	public List<int> currentPath;
	public int currentPointInPath = 1;

	public bool isActive = true;
	// Use this for initialization
	void Start () {
		if(pathManager == null) {
			pathManager = GameObject.Find (vars.path_manager_name).GetComponent<wp_manager>();
		}
		navMeshAgent = GetComponent<NavMeshAgent>();
	
	}

	public void initializeScout(path_point WP) {
		targetWP = WP;
		currentPath = WP.path_to_base;
		currentPointInPath = 1;
		animator.SetBool("isrunning", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(navMeshAgent.destination != currentPointPosition()) {
			navMeshAgent.destination = currentPointPosition();
		}


		if(Vector3.Distance(transform.position, currentPointPosition()) < 2.0f && isActive) {

			if(pathManager.getNodeObjectById(currentPointID()).GetComponent<path_point>().status == path_point.node_status.placed) {
				pathManager.getNodeObjectById(currentPointID()).GetComponent<path_point>().setStatus(path_point.node_status.being_built);
				animator.SetBool("isrunning", false);
				animator.SetTrigger("buildWayPoint");
				Invoke("buildWayPoint", 3.0f);
				isActive = false;
			}

			else if(currentPointInPath < targetWP.path_to_base.Count-1) {
				currentPointInPath++;
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
