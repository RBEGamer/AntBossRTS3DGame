using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkerScript : MonoBehaviour {

	public static WorkerManager workerManager;
	public static wp_manager pathManager;
	public static base_manager baseManager;

	public SkinnedMeshRenderer meshRenderer;
	public Texture normalTexture;
	public Texture fullTexture;
	
	public NavMeshAgent navMeshAgent;
	public Animator animator;
	
	public path_point targetWP;
	public ressource targetRessource;
	public List<int> currentPath;
	public int currentPointInPath = 1;
	
	public bool isActive = true;
	
	public float currentResourceAmount;
	public vars.ressource_type ressourceType = vars.ressource_type.default_type;
	public bool hasRessource = false;
	public bool bitten = false;

	// Use this for initialization
	void Start () {
		if(pathManager == null) {
			pathManager = GameObject.Find (vars.path_manager_name).GetComponent<wp_manager>();
		}

		if(baseManager == null) {
			baseManager = GameObject.Find (vars.base_name).GetComponent<base_manager>();
		}


		if(workerManager == null) {
			workerManager = GameObject.Find (vars.worker_manager_name).GetComponent<WorkerManager>();
		}

		workerManager.addWorker(this);
		navMeshAgent = GetComponent<NavMeshAgent>();
		
	}
	
	public void initializeWorker(path_point WP) {
		targetWP = WP;
		targetRessource = WP.GetComponent<ressource>();
		currentPath = WP.path_to_base;
		currentPointInPath = 1;
		animator.SetBool("isrunning", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(navMeshAgent.destination != currentPointPosition()) {
			navMeshAgent.destination = currentPointPosition();
			Debug.Log ("NEW DESTINATION:" + navMeshAgent.destination+ " - "+ pathManager.getNodeObjectById(currentPath[currentPointInPath]).gameObject.name);
		}

		if(!hasRessource) {
			if(Vector3.Distance(transform.position, currentPointPosition()) < 3.0f && isActive) {
				
				if(currentPointInPath < targetWP.path_to_base.Count-1) {
					currentPointInPath++;

				} else if(!bitten){
					animator.SetBool("harvesting", true);
					animator.SetBool("isrunning", false);
					navMeshAgent.Stop();
					transform.LookAt(pathManager.getNodeObjectById(currentPointID()).transform);
					Invoke ("harvest", 5.0f);
					bitten = true;
				}
				
			}
		}
		else {
			if(Vector3.Distance(transform.position, currentPointPosition()) < 3.0f && isActive) {
				if(pathManager.getNodeObjectById(currentPath[currentPointInPath]).GetComponent<path_point>().type == path_point.node_type.base_node) {
					baseManager.add_to_storage(ressourceType, currentResourceAmount);
					hasRessource = false;
					currentResourceAmount = 0;
					meshRenderer.material.mainTexture = normalTexture;
					ressourceType = vars.ressource_type.default_type;
				}
				if(currentPointInPath > 0) {
					currentPointInPath--;
				}
			}
		}
	}
	
	Vector3 currentPointPosition() {
		return pathManager.getNodeObjectById(currentPath[currentPointInPath]).transform.position;
	}
	
	int currentPointID() {
		return currentPath[currentPointInPath];
	}
	
	public void harvest() {
		navMeshAgent.Resume();
		meshRenderer.material.mainTexture = fullTexture;
		currentResourceAmount = targetRessource.ant_bite();
		ressourceType = targetRessource.res_type;
		hasRessource = true;
		bitten = false;
		animator.SetBool("harvesting", false);
		animator.SetBool("isrunning", true);


	}
}
