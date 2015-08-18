using UnityEngine;
using System.Collections;

public class WorkerDestroyed : MonoBehaviour {

	public WorkerScript workerScript;

	void destroyObject() {
		workerScript.targetRessource.res.current_collection_ants--;
		workerScript.targetRessource.res.target_collection_ants--;
		workerScript.targetRessource.current_ants_working_on_this_res--;
		gameObject.SetActive(false);
		Destroy(gameObject, 0.1f);
	}
}
