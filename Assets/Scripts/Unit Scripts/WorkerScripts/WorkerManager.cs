using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkerManager : MonoBehaviour {
	[SerializeField]
	List<WorkerScript> workerScripts;
	[SerializeField]
	List<ressource> ressourceScripts; 


	public GameObject workerPrefab;
	void Awake() {
		workerScripts = new List<WorkerScript>();
		ressourceScripts = new List<ressource>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for(int i = 0; i < ressourceScripts.Count -1; i++) {
			if(ressourceScripts[i].res.current_collection_ants < ressourceScripts[i].res.target_collection_ants) {
				/*GameObject newWorker = (GameObject)Instantiate(workerPrefab, new Vector3(transform.position.x+2.0f, transform.position.y, transform.position.z), Quaternion.identity);
				WorkerScript workerScript = newWorker.GetComponent<WorkerScript>();
				if(workerScript) {
					workerScript.initializeWorker(ressourceScripts[i].gameObject.GetComponent<path_point>());
				}
				ressourceScripts[i].res.current_collection_ants++;
				ressourceScripts[i].current_ants_working_on_this_res++;*/
				StartCoroutine(SpawnWorker(ressourceScripts[i]));
				ressourceScripts[i].res.current_collection_ants++;
				ressourceScripts[i].current_ants_working_on_this_res++;
			}
		}
	}

	public void clearOneAnt(ressource clearRessource) {
		for(int i = 0; i < workerScripts.Count; i++) {
			if(workerScripts[i].targetRessource == clearRessource) {
				workerScripts[i].retreat = true;
				break;
			}
		}
	}
	public void clearAllAnts(ressource clearRessource) {
		for(int i = 0; i < workerScripts.Count; i++) {
			if(workerScripts[i].targetRessource == clearRessource) {
				workerScripts[i].retreat = true;
			}
		}
	}

	public void addRessource(ressource newRessource) {
		if(!ressourceScripts.Contains(newRessource)) {
			ressourceScripts.Add(newRessource);
		}
	}

	public void removeRessource(ressource oldRessource) {
		if(ressourceScripts.Contains(oldRessource)) {
			ressourceScripts.Remove(oldRessource);
		}
	}


	public void addWorker(WorkerScript newWorker) {
		if(!workerScripts.Contains(newWorker)) {
			workerScripts.Add(newWorker);
		}
	}

	public void removeWorker(WorkerScript oldWorker) {
		if(workerScripts.Contains(oldWorker)) {
			workerScripts.Remove(oldWorker);
		}
	}

	public IEnumerator SpawnWorker(ressource ressourceScripts) {
		yield return new WaitForSeconds(0.5f);
		GameObject newWorker = (GameObject)Instantiate(workerPrefab, new Vector3(transform.position.x+2.0f, transform.position.y, transform.position.z), Quaternion.identity);
		WorkerScript workerScript = newWorker.GetComponent<WorkerScript>();
		if(workerScript) {
			workerScript.initializeWorker(ressourceScripts.gameObject.GetComponent<path_point>());
		}

	}
}
