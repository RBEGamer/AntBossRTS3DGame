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
	public NavMeshObstacle navMeshObstacle;
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

	public bool retreat = false;

	public float currentMovementOffset = 2.0f;
	public float movementUpperOffset = 3.0f;
	public float movementLowerOffset = 2.0f;

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
		navMeshObstacle = GetComponent<NavMeshObstacle>();
	}
	
	public void initializeWorker(path_point WP) {
		retreat = false;
		targetWP = WP;
		targetRessource = WP.GetComponent<ressource>();
		currentPath = WP.path_to_base;
		currentPointInPath = 1;
		animator.SetBool("isrunning", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(navMeshAgent.enabled == true) {
			if(navMeshAgent.destination != currentPointPosition()) {
				navMeshAgent.destination = currentPointPosition();
			}
		}

		if(!hasRessource) {
			if(Vector3.Distance(transform.position, currentPointPosition()) < currentMovementOffset && isActive) {
				
				if(currentPointInPath < targetWP.path_to_base.Count-1) {
					currentPointInPath++;
					currentMovementOffset = Random.Range(movementLowerOffset, movementUpperOffset);

				} else if(!bitten){

					if(targetRessource.res.current_harvest_amount >= 5) {
						animator.SetBool("harvesting", true);
						animator.SetBool("isrunning", false);
						navMeshAgent.enabled = false;
						navMeshObstacle.enabled = true;
						transform.LookAt(pathManager.getNodeObjectById(currentPointID()).transform);
						Invoke ("harvest", 5.0f);
						bitten = true;
					} else {
						bitten = false;
						hasRessource = true;
						retreat = true;
					}
				}
				
			}
		}
		else {
			if(Vector3.Distance(transform.position, currentPointPosition()) < currentMovementOffset && isActive) {
				if(pathManager.getNodeObjectById(currentPath[currentPointInPath]).GetComponent<path_point>().type == path_point.node_type.base_node) {
					baseManager.add_to_storage(ressourceType, currentResourceAmount);
					hasRessource = false;
					currentResourceAmount = 0;
					meshRenderer.material.mainTexture = normalTexture;
					ressourceType = vars.ressource_type.default_type;
					if(retreat) {
						retreatToBase();
					}
				}
				if(currentPointInPath > 0) {
					currentPointInPath--;
					if(pathManager.getNodeObjectById(currentPath[currentPointInPath]).GetComponent<path_point>().type == path_point.node_type.base_node) {
						currentMovementOffset = 3.0f;
					} else {
						currentMovementOffset = Random.Range(movementLowerOffset, movementUpperOffset);
					}
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
		GameObject.Find ("ui_manager").GetComponent<ui_manager>().refresh_ressource_ui();
		GameObject.Find ("ui_manager").GetComponent<ui_manager>().refresh_ressource_ui_slots();
		navMeshAgent.enabled = true;
		navMeshObstacle.enabled = false;
		//navMeshAgent.Resume();
		meshRenderer.material.mainTexture = fullTexture;
		currentResourceAmount = targetRessource.ant_bite();
		ressourceType = targetRessource.res_type;
		hasRessource = true;
		bitten = false;
		animator.SetBool("harvesting", false);
		animator.SetBool("isrunning", true);
	}

	public void retreatToBase() {
		baseManager.bought_collector_ants += 1;
		Destroy(gameObject);
	}
}
